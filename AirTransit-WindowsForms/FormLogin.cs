using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AirTransit_WindowsForms
{
    public partial class FormLogin : Form
    {
        public const string phoneRegex = @"\d{10}";
        public string PhoneNumber;
        public FormLogin()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            if (PhoneNumberValid())
            {
                Close();
            }
            else
            {
                MessageBox.Show("The phone number must be 10 digits.");
            }
        }

        private void FormLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            PhoneNumber = !PhoneNumberValid() ? "" : TxtPhoneNumber.Text;
        }

        private bool PhoneNumberValid()
        {
            return PhoneNumberValid(TxtPhoneNumber.Text);
        }

        public static bool PhoneNumberValid(string phoneNumber)
        {
            Match match = Regex.Match(phoneNumber, phoneRegex, RegexOptions.IgnoreCase);
            return match.Success;
        }
    }
}
