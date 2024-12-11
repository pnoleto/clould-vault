using System.Security.Cryptography;
using System.Text.Json;
using cloudVault.Classes;

namespace cloudVault
{
    public partial class FrmMain : Form
    {
        private CloudVaultSettings _cloudVaultSettings;
        private const string settingsPath = @$".\settings.json";
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

            if (!File.Exists(settingsPath)) File.WriteAllText(settingsPath, JsonSerializer.Serialize(new CloudVaultSettings()));

            _cloudVaultSettings = JsonSerializer.Deserialize<CloudVaultSettings>(File.ReadAllText(settingsPath)) ?? new CloudVaultSettings();

            _semaphore = new SemaphoreSlim(2, 10);
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
                randomChars[index] = _cloudVaultSettings.AllowedChars[random.Next(_cloudVaultSettings.AllowedChars.Length)];
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

                                newFilePath = FileManager.AddExtension(filePath, _cloudVaultSettings.DefaultExtension);

                                await _cryptoManager.EncodeFileAsync(filePath, newFilePath);

                                _notification.Report($@"Encrypted {newFilePath}");

                                RemoveFile(filePath);

                                _notification.Report($"Removed file {filePath}");

                                break;

                            case CypherMode.Decode:

                                if (!FileManager.HasExtension(filePath, _cloudVaultSettings.DefaultExtension)) break;

                                newFilePath = FileManager.RemoveExtension(filePath, _cloudVaultSettings.DefaultExtension);

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
            return _cloudVaultSettings.IgnoredPaths.Contains(new DirectoryInfo(rootPath).Name);
        }

        private bool IsValidExtension(string extension)
        {
            return _cloudVaultSettings.AllowedExtension.Contains(extension.ToLower());
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

                _cryptoManager.DefineSettings(new(GetHashedPassword(txtKey.Text), GetSaltBytes(_cloudVaultSettings.SaltSyze), _cloudVaultSettings.IteractionsLimit));

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
                tasks.Add(TaskChangeAllFilesAsync(item.Text, mode));
            }

            await Task.WhenAll(tasks);
        }

        private Task TaskChangeAllFilesAsync(string path, CypherMode mode)
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

            }, _cancellationTokenSource.Token);
        }

        private void BtnExplorer_Click(object sender, EventArgs e)
        {
            using FolderBrowserDialog openDialog = new();

            openDialog.ShowDialog(this);

            if (openDialog.SelectedPath is null) return;

            listFolders.Items.Add(new ListViewItem([openDialog.SelectedPath, new DirectoryInfo(openDialog.SelectedPath).Name]));
        }

        private void BtnSettings_Click(object sender, EventArgs e)
        {
            FrmConfig frmConfig = new(settingsPath, _cloudVaultSettings);
            frmConfig.ShowDialog(this);
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {

        }
    }
}