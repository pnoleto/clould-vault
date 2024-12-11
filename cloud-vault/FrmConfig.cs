using cloudVault.Classes;
using System.Text.Json;

namespace cloudVault
{
    public partial class FrmConfig(string settingsPath, CloudVaultSettings cloudVaultSettings) : Form
    {
        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                File.WriteAllText(settingsPath, JsonSerializer.Serialize(cloudVaultSettings));
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error while saving", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FrmConfig_Load(object sender, EventArgs e)
        {
            txtAllowedChars.Text = cloudVaultSettings.AllowedChars.ToString();
            txtAllowedExtensions.Text = cloudVaultSettings.AllowedExtension.ToString();
            txtDefaultExtension.Text = cloudVaultSettings.DefaultExtension.ToString();
            txtIteraction.Text = cloudVaultSettings.IteractionsLimit.ToString();
            txtSaltSize.Text = cloudVaultSettings.SaltSyze.ToString();
        }
    }
}
