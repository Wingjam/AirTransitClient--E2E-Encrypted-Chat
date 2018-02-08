using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AirTransit_WindowsForms
{
    public partial class NewContact : Form
    {
        public string PhoneNumber;
        public string ContactName;

        public NewContact()
        {
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