using System.Security.Cryptography;
using System.Text.Json;
using cloudVault.Classes;

namespace cloudVault
{
    public partial class FrmMain : Form
    {
        private readonly int _saltSyze;
        private readonly int _iteractionsLimit;
        private readonly char[] _allowedChars;
        private readonly string[] _ignoredPaths;
        private readonly string[] _allowedExtensions;
        private readonly string _defaultExtension;
        private readonly int ZERO = 0;

        private readonly CancellationTokenSource _cancellationTokenSource;
        private readonly IProgress<string> _notification;
        private readonly CryptoManager _cryptoManager;
        private readonly SemaphoreSlim _semaphore;

        private enum CypherMode
        {
            Encode,
            Decode
        }

        public FrmMain()
        {
            InitializeComponent();
            
            _ignoredPaths = JsonSerializer.Deserialize<string[]>(Settings.Default.IGNORED_PATHS);
            _allowedChars = JsonSerializer.Deserialize<char[]>(Settings.Default.CHARS);
            _allowedExtensions = JsonSerializer.Deserialize<string[]>(Settings.Default.ALLOWED_EXTENSIONS);
            _defaultExtension = JsonSerializer.Deserialize<string>(Settings.Default.DEFAULT_EXTENSION);
            _iteractionsLimit = JsonSerializer.Deserialize<int>(Settings.Default.ITERACTIONS_LIMIT);
            _saltSyze = JsonSerializer.Deserialize<int>(Settings.Default.SALT_SIZE);

            _semaphore = new SemaphoreSlim(1, 10);
            _notification = new Progress<string>(WriteLine);
            _cancellationTokenSource = new CancellationTokenSource();
            _cryptoManager = new CryptoManager(_cancellationTokenSource.Token);
        }

        private static byte[] GetSaltBytes(int KeySize)
        {
            byte[] saltBytes = new byte[KeySize];
            RandomNumberGenerator.Create().GetBytes(saltBytes);
            return saltBytes;
        }

        private static byte[] GetHashedPassword(string Password)
        {
            return GetHashBytes(GetKeyBytes(Password));
        }

        private static byte[] GetHashBytes(byte[] passwordBytes)
        {
            return SHA256.HashData(passwordBytes);
        }

        private static byte[] GetKeyBytes(string password)
        {
            return System.Text.Encoding.Default.GetBytes(password);
        }

        public string GetRandomString(int length)
        {
            char[] randomChars = new char[length];

            Random random = new();

            for (int index = ZERO; index < length; index++)
            {
                randomChars[index] = _allowedChars[random.Next(_allowedChars.Length)];
            }

            return new string(randomChars);
        }

        public void WriteLine(string text)
        {
            txtLogs.Text += $"[{DateTime.Now:yyyyy/MM/dd HH:mm:ss}]: {text}{Environment.NewLine}";
        }

        private async Task ChangeAllFilesAsync(string rootPath, CypherMode mode)
        {
            try
            {
                if ((File.GetAttributes(rootPath) & FileAttributes.ReparsePoint) is not FileAttributes.ReparsePoint)
                {
                    string newFilePath = string.Empty;

                    if (IgnoredPath(rootPath)) return;

                    foreach (string filePath in Directory.GetFiles(Path.GetFullPath(rootPath)))
                    {
                        string extension = Path.GetExtension(filePath);

                        switch (mode)
                        {
                            case CypherMode.Encode:

                                if (!IsValidExtension(extension)) break;

                                newFilePath = FileManager.AddExtension(filePath, _defaultExtension);

                                await _cryptoManager.EncodeFileAsync(filePath, newFilePath);

                                _notification.Report($@"Encrypted {newFilePath}");

                                RemoveFile(filePath);

                                _notification.Report($"Removed file {filePath}");

                                break;

                            case CypherMode.Decode:

                                if (!FileManager.HasExtension(filePath, _defaultExtension)) break;

                                newFilePath = FileManager.RemoveExtension(filePath, _defaultExtension);

                                await _cryptoManager.DecodeFileAsync(filePath, newFilePath);

                                _notification.Report($"Decrypted {newFilePath}");

                                RemoveFile(filePath);

                                _notification.Report($"Removed file {filePath}");

                                break;

                            default: throw new NotSupportedException();

                        }
                    }

                    foreach (string directory in GetDirectories(rootPath))
                    {
                        await ChangeAllFilesAsync(directory, mode);
                    }
                }
            }
            catch (Exception ex)
            {
                _notification.Report(ex.Message);
            }
        }

        private bool IgnoredPath(string rootPath)
        {
            return _ignoredPaths.Contains(new DirectoryInfo(rootPath).Name);
        }

        private bool IsValidExtension(string extension)
        {
            return _allowedExtensions.Contains(extension.ToLower());
        }

        private static string[] GetDirectories(string path)
        {
            return Directory.GetDirectories(path);
        }

        private void RemoveFile(string filePath)
        {
            try
            {
                File.Delete(filePath);
            }
            catch (Exception ex)
            {
                WriteLine($"Cant delete file {ex.Message}");
            }
        }

        private async void BtnDecode_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtKey.Text.Trim())) return;

                _cryptoManager.DefineSettings(new(GetHashedPassword(txtKey.Text), GetSaltBytes(_saltSyze), _iteractionsLimit));

                switch (btnDecode.Text)
                {
                    case "DECODE":

                        ChangeButton();

                        await ManageFolders(CypherMode.Decode);

                        btnDecode.Text = "ENCODE";

                        ChangeButton();

                        break;

                    case "ENCODE":

                        ChangeButton();

                        await ManageFolders(CypherMode.Encode);

                        btnDecode.Text = "DECODE";

                        ChangeButton();

                        break;

                    default: throw new InvalidOperationException();
                }
            }
            catch (Exception exeption)
            {
                _notification.Report(exeption.Message);
            }
        }

        private void ChangeButton()
        {
            btnDecode.Enabled = !btnDecode.Enabled;
        }

        private async Task ManageFolders(CypherMode mode)
        {
            IList<Task> tasks = [];

            foreach (ListViewItem item in listFolders.Items)
            {
                tasks.Add(TaskChangeAllFiles(item.Text, mode));
            }

            await Task.WhenAll(tasks);
        }

        private Task TaskChangeAllFiles(string path, CypherMode mode)
        {
            return Task.Run(async () =>
            {
                try
                {
                    await _semaphore.WaitAsync();

                    await ChangeAllFilesAsync(path, mode);
                }
                finally
                {
                    _semaphore.Release();
                }
            });
        }

        private void BtnExplorer_Click(object sender, EventArgs e)
        {
            using FolderBrowserDialog openDialog = new();

            openDialog.ShowDialog(this);

            if (openDialog.SelectedPath is null) return;

            listFolders.Items.Add(new ListViewItem([openDialog.SelectedPath, new DirectoryInfo(openDialog.SelectedPath).Name]));
        }
    }
}