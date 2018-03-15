using System;
using System.Windows.Forms;

namespace AirTransit_WindowsForms
{
    public partial class NewContact : Form
    {
        public string PhoneNumber;
        public string ContactName;

        public NewContact() : this(null, null)
        {
        }

        public NewContact(string PhoneNumber, string oldContactName)
        {
            if (PhoneNumber != null)
            {
                TxtPhoneNumber.ReadOnly = true;
                TxtPhoneNumber.Text = PhoneNumber;
                TxtName.Text = oldContactName;
            }
            InitializeComponent();
        }

        private void BtnAddContact_Click(object sender, EventArgs e)
        {
            if (PhoneNumberValid())
            {
                if (string.IsNullOrWhiteSpace(TxtName.Text))
                {
                    MessageBox.Show("The contact must not be empty.");
                }
                else
                {
                    PhoneNumber = TxtPhoneNumber.Text;
                    ContactName = TxtName.Text;
                    DialogResult = DialogResult.OK;
                    Close();
                }
            }
            else
            {
                MessageBox.Show("The phone number must be 10 digits.");
            }
        }

        private bool PhoneNumberValid()
        {
            return FormLogin.PhoneNumberValid(TxtPhoneNumber.Text);
        }
    }
}