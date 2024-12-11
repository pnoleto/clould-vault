namespace cloudVault.Classes
{
    public sealed class CloudVaultSettings
    {
        public int SaltSyze { get; set; } = 64;
        public int IteractionsLimit { get; set; } = 5000;
        public char[] AllowedChars { get; set; } = [];
        public string[] IgnoredPaths { get; set; } = [];
        public string[] AllowedExtension { get; set; } = [];
        public string DefaultExtension { get; set; } = string.Empty;
    }
}
