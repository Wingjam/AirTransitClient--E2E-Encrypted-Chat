using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AirTransit_WindowsForms
{
    public partial class FormLogin : Form
    {
        public string PhoneNumber;
        public FormLogin()
        {
            ControlBox = false;
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void TxtPhoneNumber_Validating(object sender, CancelEventArgs e)
        {
            if (!PhoneNumberValid())
            {
                e.Cancel = true;
                MessageBox.Show("The phone number must be 10 digits.");
            }
        }

        private void FormLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!PhoneNumberValid())
            {
                e.Cancel = true;
            }
            PhoneNumber = TxtPhoneNumber.Text;
        }

        private bool PhoneNumberValid()
        {
            string phoneRegex = @"\d{10}";
            Match match = Regex.Match(TxtPhoneNumber.Text, phoneRegex, RegexOptions.IgnoreCase);
            return match.Success;
        }
    }
}
