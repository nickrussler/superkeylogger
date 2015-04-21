using n7aKeylogger;
namespace WindowsFormsApplication1
{
    partial class klgForm
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.lbltextboxVkCodesAsString = new System.Windows.Forms.Label();
            this.lblVKCodes = new System.Windows.Forms.Label();
            this.cbKeyloggerEnabled = new System.Windows.Forms.CheckBox();
            this.rbHooks = new System.Windows.Forms.RadioButton();
            this.rbPolling = new System.Windows.Forms.RadioButton();
            this.lblKeyloggerMode = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.keylogger1 = new n7aKeylogger.Keylogger();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.textBox1.Location = new System.Drawing.Point(12, 61);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(361, 189);
            this.textBox1.TabIndex = 1;
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.textBox2.Location = new System.Drawing.Point(379, 61);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(361, 189);
            this.textBox2.TabIndex = 6;
            // 
            // lbltextboxVkCodesAsString
            // 
            this.lbltextboxVkCodesAsString.AutoSize = true;
            this.lbltextboxVkCodesAsString.Location = new System.Drawing.Point(12, 45);
            this.lbltextboxVkCodesAsString.Name = "lbltextboxVkCodesAsString";
            this.lbltextboxVkCodesAsString.Size = new System.Drawing.Size(102, 13);
            this.lbltextboxVkCodesAsString.TabIndex = 7;
            this.lbltextboxVkCodesAsString.Text = "VK Codes As String:";
            // 
            // lblVKCodes
            // 
            this.lblVKCodes.AutoSize = true;
            this.lblVKCodes.Location = new System.Drawing.Point(376, 45);
            this.lblVKCodes.Name = "lblVKCodes";
            this.lblVKCodes.Size = new System.Drawing.Size(57, 13);
            this.lblVKCodes.TabIndex = 8;
            this.lblVKCodes.Text = "VK Codes:";
            // 
            // cbKeyloggerEnabled
            // 
            this.cbKeyloggerEnabled.AutoSize = true;
            this.cbKeyloggerEnabled.Location = new System.Drawing.Point(15, 12);
            this.cbKeyloggerEnabled.Name = "cbKeyloggerEnabled";
            this.cbKeyloggerEnabled.Size = new System.Drawing.Size(115, 17);
            this.cbKeyloggerEnabled.TabIndex = 9;
            this.cbKeyloggerEnabled.Text = "Keylogger Enabled";
            this.cbKeyloggerEnabled.UseVisualStyleBackColor = true;
            this.cbKeyloggerEnabled.CheckedChanged += new System.EventHandler(this.cbKeyloggerEnabled_CheckedChanged);
            // 
            // rbHooks
            // 
            this.rbHooks.AutoSize = true;
            this.rbHooks.Checked = true;
            this.rbHooks.Location = new System.Drawing.Point(599, 11);
            this.rbHooks.Name = "rbHooks";
            this.rbHooks.Size = new System.Drawing.Size(56, 17);
            this.rbHooks.TabIndex = 10;
            this.rbHooks.TabStop = true;
            this.rbHooks.Text = "Hooks";
            this.rbHooks.UseVisualStyleBackColor = true;
            this.rbHooks.CheckedChanged += new System.EventHandler(this.rbHooks_CheckedChanged);
            // 
            // rbPolling
            // 
            this.rbPolling.AutoSize = true;
            this.rbPolling.Location = new System.Drawing.Point(684, 11);
            this.rbPolling.Name = "rbPolling";
            this.rbPolling.Size = new System.Drawing.Size(56, 17);
            this.rbPolling.TabIndex = 11;
            this.rbPolling.Text = "Polling";
            this.rbPolling.UseVisualStyleBackColor = true;
            this.rbPolling.CheckedChanged += new System.EventHandler(this.rbHooks_CheckedChanged);
            // 
            // lblKeyloggerMode
            // 
            this.lblKeyloggerMode.AutoSize = true;
            this.lblKeyloggerMode.Location = new System.Drawing.Point(524, 13);
            this.lblKeyloggerMode.Name = "lblKeyloggerMode";
            this.lblKeyloggerMode.Size = new System.Drawing.Size(37, 13);
            this.lblKeyloggerMode.TabIndex = 12;
            this.lblKeyloggerMode.Text = "Mode:";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel1.Location = new System.Drawing.Point(2, 257);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(750, 20);
            this.linkLabel1.TabIndex = 13;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Project Site: http://www.whitebyte.info/?page_id=158 (Here you can comment and ge" +
                "t the newest version)";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // keylogger1
            // 
            this.keylogger1.Enabled = false;
            this.keylogger1.Keylogger_Mode = n7aKeylogger.Keylogger.Keylogger_Modes.Hooks;
            this.keylogger1.VKCodeAsStringDown += new n7aKeylogger.Keylogger.ValueChangedEventHandler(this.keylogger1_VKCodeAsStringDown);
            this.keylogger1.VKCodeAsStringUp += new n7aKeylogger.Keylogger.ValueChangedEventHandler(this.keylogger1_VKCodeAsStringUp);
            this.keylogger1.VKCodeDown += new n7aKeylogger.Keylogger.ValueChangedEventHandler2(this.keylogger1_VKCodeDown);
            this.keylogger1.VKCodeUp += new n7aKeylogger.Keylogger.ValueChangedEventHandler2(this.keylogger1_VKCodeUp);
            // 
            // klgForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(752, 283);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.lblKeyloggerMode);
            this.Controls.Add(this.rbPolling);
            this.Controls.Add(this.rbHooks);
            this.Controls.Add(this.cbKeyloggerEnabled);
            this.Controls.Add(this.lblVKCodes);
            this.Controls.Add(this.lbltextboxVkCodesAsString);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "klgForm";
            this.Text = "Keylogger Example";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private Keylogger keylogger1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label lbltextboxVkCodesAsString;
        private System.Windows.Forms.Label lblVKCodes;
        private System.Windows.Forms.CheckBox cbKeyloggerEnabled;
        private System.Windows.Forms.RadioButton rbHooks;
        private System.Windows.Forms.RadioButton rbPolling;
        private System.Windows.Forms.Label lblKeyloggerMode;
        private System.Windows.Forms.LinkLabel linkLabel1;
    }
}

