using System.Security.Cryptography;

namespace cloudVault.Classes
{
    internal sealed partial class CryptoManager
    {
        private const int ZERO = 0;
        private const int BYTE_SIZE = 8;
        private const int KEY_SIZE = 256;
        private const int BLOCK_SIZE = 128;
        private const int BUFFER_STREAM_LENGTH = 8192;

        private readonly CancellationToken _cancellationToken;

        private CryptoSettings? _settings;

        public CryptoManager(CancellationToken cancellationToken)
        {
            _cancellationToken = cancellationToken;
        }
        public CryptoManager(CryptoSettings settings, CancellationToken cancellationToken)
        {
            _settings = settings;
            _cancellationToken = cancellationToken;
        }

        public void DefineSettings(CryptoSettings settings)
        {
            ArgumentNullException.ThrowIfNull(settings);

            _settings = settings;
        }

        private Aes ConfiguredAES()
        {
            ArgumentNullException.ThrowIfNull(_settings);

            Aes aes = Aes.Create();

            aes.KeySize = KEY_SIZE;
            aes.BlockSize = BLOCK_SIZE;
            aes.Padding = PaddingMode.PKCS7;
            aes.Mode = CipherMode.CFB;

            using (Rfc2898DeriveBytes key = new(_settings.PasswordHash, _settings.SaltBytes, _settings.IteractionsLimit, HashAlgorithmName.SHA256))
            {
                aes.Key = key.GetBytes(aes.KeySize / BYTE_SIZE);
                aes.IV = key.GetBytes(aes.BlockSize / BYTE_SIZE);
            }

            return aes;
        }

        public async Task EncodeFileAsync(string actualFilePath, string newFilePath)
        {
            ArgumentNullException.ThrowIfNull(_settings);

            using (FileStream actualFileStream = new(actualFilePath, FileMode.Open))
            {
                using (FileStream newFileStream = new(newFilePath, FileMode.Create))
                {
                    newFileStream.Write(_settings.SaltBytes, ZERO, _settings.SaltBytes.Length);

                    using (Aes AES = ConfiguredAES())
                    {
                        using (CryptoStream cryptoStream = new(newFileStream, AES.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            int read = ZERO;
                            byte[] buffer = new byte[BUFFER_STREAM_LENGTH];

                            while ((read = await actualFileStream.ReadAsync(buffer.AsMemory(ZERO, buffer.Length), _cancellationToken)) > ZERO)
                            {
                                await cryptoStream.WriteAsync(buffer.AsMemory(ZERO, read), _cancellationToken);
                            }

                            cryptoStream.Close();
                        }

                        AES.Clear();
                    }

                    newFileStream.Close();
                }

                actualFileStream.Close();
            }
        }

        public async Task DecodeFileAsync(string actualFilePath, string newFilePath)
        {
            ArgumentNullException.ThrowIfNull(_settings);

            using (FileStream newFileStream = new(newFilePath, FileMode.Create))
            {
                using (FileStream actualFileStream = new(actualFilePath, FileMode.Open))
                {
                    actualFileStream.Read(_settings.SaltBytes, ZERO, _settings.SaltBytes.Length);

                    using (Aes AES = ConfiguredAES())
                    {
                        using (CryptoStream cryptoStream = new(actualFileStream, AES.CreateDecryptor(), CryptoStreamMode.Read))
                        {
                            int read = ZERO;
                            byte[] buffer = new byte[BUFFER_STREAM_LENGTH];

                            while ((read = await cryptoStream.ReadAsync(buffer.AsMemory(ZERO, buffer.Length), _cancellationToken)) > ZERO)
                            {
                                await newFileStream.WriteAsync(buffer.AsMemory(ZERO, read), _cancellationToken);
                            }

                            cryptoStream.Close();
                        }

                        AES.Clear();
                    }

                    actualFileStream.Close();
                }

                newFileStream.Close();
            }
        }

    }
}
