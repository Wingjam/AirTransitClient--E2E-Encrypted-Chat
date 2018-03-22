using System;
using System.Windows.Forms;

namespace AirTransit_WindowsForms
{
    public partial class NewContact : Form
    {
        public string PhoneNumber;
        public string ContactName;
        private string oldContactName;

        public NewContact() : this(null, null)
        {
        }

        public NewContact(string PhoneNumber, string oldContactName)
        {
            InitializeComponent();
            this.oldContactName = oldContactName;
            this.PhoneNumber = PhoneNumber;
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

        private void NewContact_Load(object sender, EventArgs e)
        {
            if (PhoneNumber != null)
            {
                TxtPhoneNumber.ReadOnly = true;
                TxtPhoneNumber.Text = PhoneNumber;
                TxtName.Text = oldContactName;
            }
        }
    }
}