using System;
using System.Windows.Forms;
using n7aKeylogger;

namespace WindowsFormsApplication1
{
    public partial class klgForm : Form
    {
        public klgForm()
        {
            InitializeComponent();
        }

        private void cbKeyloggerEnabled_CheckedChanged(object sender, EventArgs e)
        {
            if (cbKeyloggerEnabled.Checked)
            {
                keylogger1.Enabled = true;
            }
            else
            {
                keylogger1.Enabled = false;
            }
        }

        private void rbHooks_CheckedChanged(object sender, EventArgs e)
        {
            if (rbHooks.Checked)
            {
                keylogger1.Keylogger_Mode = Keylogger.Keylogger_Modes.Hooks;
            }
            else
            {
                keylogger1.Keylogger_Mode = Keylogger.Keylogger_Modes.Polling;
            }
        }

        private void keylogger1_VKCodeDown(int vkcode)
        {
            textBox2.Text += " [+" + vkcode.ToString() + "]";
        }

        private void keylogger1_VKCodeUp(int vkcode)
        {
            textBox2.Text += " [-" + vkcode.ToString() + "]";
        }

        private void keylogger1_VKCodeAsStringDown(string value, bool isChar)
        {
            String result = value;

            if (!isChar)
                result = "[+" + result + "]";

            textBox1.Text += result;
        }

        private void keylogger1_VKCodeAsStringUp(string value, bool isChar)
        {
            if (isChar)
                return;

            String result = value;

            if (!isChar)
                result = "[-" + result + "]";

            textBox1.Text += result;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.whitebyte.info/?page_id=158");
        }
    }
}
