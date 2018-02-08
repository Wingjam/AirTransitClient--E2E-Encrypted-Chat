using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using AirTransit_Core.Models;
using AirTransit_Core.Repositories;
using AirTransit_Core.Services;
using Message = AirTransit_Core.Models.Message;

namespace AirTransit_WindowsForms
{
    public partial class AirTransit : Form
    {
        string phoneNumber;
        private List<Contact> contacts;
        //private IAuthenticationService Auth;
        private IContactRepository Contact;
        private IMessageRepository Message;
        private IMessageService MessageService;
        private Color UserColor = Color.DarkRed;
        private Color ContactColor = Color.DarkBlue;
        private bool wasUser = false;

        public AirTransit()
        {
            InitializeComponent();
        }

        private void AirTransit_Load(object sender, EventArgs e)
        {
            using (FormLogin login = new FormLogin())
            {
                login.Show();
                PhoneNumber = login.PhoneNumber;
            }

            if (string.IsNullOrWhiteSpace(PhoneNumber))
            {
                MessageBox.Show("No phone number entered. Closing...");
                Close();
            }
            else
            {
                //Auth = new //TODO
                //Contact = new //TODO
                //Message = new //TODO
                //MessageService = new //TODO
                //Auth.SignUp(phoneNumber);
                Contacts = Contact.GetContacts().ToList();
                if (ListContacts.SelectedItem == null && ListContacts.Items.Count > 0)
                {
                    ListContacts.SelectedIndex = 0;
                }
            }
        }

        private void BtnSend_Click(object sender, EventArgs e)
        {
            Contact currentContact = ListContacts.SelectedItem as Contact;
            if (ListContacts.SelectedItem != null)
            {
                MessageService.SendMessage(currentContact, TxtInput.Text);
                PrintMessage(Message.GetLastMessage(currentContact));
            }
            else
                MessageBox.Show("Plz select a contact before sending a message.");
        }

        private void TxtContactList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ShowCurrentContactConvo();
        }

        private void ShowCurrentContactConvo()
        {
            if (ListContacts.SelectedItem != null)
                ShowConvo((Contact)ListContacts.SelectedItem);
        }

        private void ShowConvo(Contact contact)
        {
            Txtconversation.Text = "";
            Message.GetMessages(contact).ToList().ForEach(PrintMessage);
        }

        private void PrintMessage(Message message)
        {
            bool currentlyUser = message.Sender.PhoneNumber == PhoneNumber;
            Txtconversation.ForeColor = currentlyUser ? UserColor : ContactColor;
            if (wasUser != currentlyUser || Txtconversation.TextLength == 0)
            {
                Txtconversation.AppendText(message.Sender.Name);
                wasUser = currentlyUser;
            }

            Txtconversation.AppendText(message.Content);
        }

        private void BtnContact_Click(object sender, EventArgs e)
        {
            NewContact newContact = new NewContact();
            if (newContact.ShowDialog() == DialogResult.OK)
            {

            }
            //Contact newContact = new Contact(){Name = };
            //Contact.AddContact();
        }

        private string PhoneNumber
        {
            get => phoneNumber;
            set
            {
                phoneNumber = value;
                TxtConnectedPhone.Text = phoneNumber;
            }
        }

        private List<Contact> Contacts
        {
            set
            {
                contacts = value;
                ListContacts.DataSource = contacts;
            }
        }
    }
}