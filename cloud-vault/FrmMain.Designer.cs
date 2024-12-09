namespace cloudVault
{
    partial class FrmMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            txtLogs = new RichTextBox();
            btnDecode = new Button();
            txtKey = new TextBox();
            listFolders = new ListView();
            btnExplorer = new Button();
            SuspendLayout();
            // 
            // txtLogs
            // 
            txtLogs.BackColor = SystemColors.ScrollBar;
            txtLogs.Location = new Point(12, 307);
            txtLogs.Name = "txtLogs";
            txtLogs.Size = new Size(776, 93);
            txtLogs.TabIndex = 0;
            txtLogs.Text = "";
            // 
            // btnDecode
            // 
            btnDecode.BackColor = Color.LightCoral;
            btnDecode.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnDecode.Location = new Point(652, 406);
            btnDecode.Name = "btnDecode";
            btnDecode.Size = new Size(136, 32);
            btnDecode.TabIndex = 1;
            btnDecode.Text = "ENCODE";
            btnDecode.UseVisualStyleBackColor = false;
            btnDecode.Click += BtnDecode_Click;
            // 
            // txtKey
            // 
            txtKey.Location = new Point(12, 411);
            txtKey.Name = "txtKey";
            txtKey.Size = new Size(634, 23);
            txtKey.TabIndex = 2;
            // 
            // listFolders
            // 
            listFolders.Location = new Point(12, 57);
            listFolders.Name = "listFolders";
            listFolders.Size = new Size(776, 244);
            listFolders.TabIndex = 3;
            listFolders.UseCompatibleStateImageBehavior = false;
            listFolders.View = View.List;
            // 
            // btnExplorer
            // 
            btnExplorer.BackColor = Color.LightCoral;
            btnExplorer.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnExplorer.Location = new Point(652, 19);
            btnExplorer.Name = "btnExplorer";
            btnExplorer.Size = new Size(136, 32);
            btnExplorer.TabIndex = 5;
            btnExplorer.Text = "EXPLORER";
            btnExplorer.UseVisualStyleBackColor = false;
            btnExplorer.Click += BtnExplorer_Click;
            // 
            // FrmMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Brown;
            ClientSize = new Size(800, 450);
            Controls.Add(btnExplorer);
            Controls.Add(listFolders);
            Controls.Add(txtKey);
            Controls.Add(btnDecode);
            Controls.Add(txtLogs);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MinimumSize = new Size(800, 450);
            Name = "FrmMain";
            SizeGripStyle = SizeGripStyle.Hide;
            StartPosition = FormStartPosition.CenterScreen;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private RichTextBox txtLogs;
        private Button btnDecode;
        private TextBox txtKey;
        private ListView listFolders;
        private Button btnExplorer;
    }
}