namespace cloudVault
{
    partial class FrmConfig
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            txtAllowedChars = new TextBox();
            label1 = new Label();
            label2 = new Label();
            txtAllowedExtensions = new TextBox();
            label3 = new Label();
            txtIteraction = new TextBox();
            label4 = new Label();
            txtDefaultExtension = new TextBox();
            label5 = new Label();
            txtSaltSize = new TextBox();
            btnSave = new Button();
            SuspendLayout();
            // 
            // txtAllowedChars
            // 
            txtAllowedChars.Location = new Point(27, 43);
            txtAllowedChars.Name = "txtAllowedChars";
            txtAllowedChars.Size = new Size(305, 23);
            txtAllowedChars.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(27, 25);
            label1.Name = "label1";
            label1.Size = new Size(81, 15);
            label1.TabIndex = 1;
            label1.Text = "Allowed chars";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(27, 77);
            label2.Name = "label2";
            label2.Size = new Size(108, 15);
            label2.TabIndex = 3;
            label2.Text = "Allowed extensions";
            // 
            // txtAllowedExtensions
            // 
            txtAllowedExtensions.Location = new Point(27, 95);
            txtAllowedExtensions.Name = "txtAllowedExtensions";
            txtAllowedExtensions.Size = new Size(305, 23);
            txtAllowedExtensions.TabIndex = 2;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(27, 184);
            label3.Name = "label3";
            label3.Size = new Size(62, 15);
            label3.TabIndex = 7;
            label3.Text = "Iteractions";
            // 
            // txtIteraction
            // 
            txtIteraction.Location = new Point(27, 202);
            txtIteraction.Name = "txtIteraction";
            txtIteraction.Size = new Size(305, 23);
            txtIteraction.TabIndex = 6;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(27, 132);
            label4.Name = "label4";
            label4.Size = new Size(98, 15);
            label4.TabIndex = 5;
            label4.Text = "Default extension";
            // 
            // txtDefaultExtension
            // 
            txtDefaultExtension.Location = new Point(27, 150);
            txtDefaultExtension.Name = "txtDefaultExtension";
            txtDefaultExtension.Size = new Size(305, 23);
            txtDefaultExtension.TabIndex = 4;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(27, 239);
            label5.Name = "label5";
            label5.Size = new Size(48, 15);
            label5.TabIndex = 9;
            label5.Text = "Salt size";
            // 
            // txtSaltSize
            // 
            txtSaltSize.Location = new Point(27, 257);
            txtSaltSize.Name = "txtSaltSize";
            txtSaltSize.Size = new Size(305, 23);
            txtSaltSize.TabIndex = 8;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(142, 286);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(75, 23);
            btnSave.TabIndex = 10;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += BtnSave_Click;
            // 
            // FrmConfig
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(357, 320);
            Controls.Add(btnSave);
            Controls.Add(label5);
            Controls.Add(txtSaltSize);
            Controls.Add(label3);
            Controls.Add(txtIteraction);
            Controls.Add(label4);
            Controls.Add(txtDefaultExtension);
            Controls.Add(label2);
            Controls.Add(txtAllowedExtensions);
            Controls.Add(label1);
            Controls.Add(txtAllowedChars);
            MaximizeBox = false;
            Name = "FrmConfig";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Settings";
            Load += FrmConfig_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label3;
        private TextBox txtIteraction;
        private Label label4;
        private TextBox txtDefaultExtension;
        private Label label2;
        private TextBox txtAllowedExtensions;
        private Label label1;
        private TextBox txtAllowedChars;
        private Label label5;
        private TextBox txtSaltSize;
        private Button btnSave;
    }
}