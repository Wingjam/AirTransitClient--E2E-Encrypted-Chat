using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using AirTransit_Core;
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
        private CoreServices Core;
        private IContactRepository Contact;
        private IMessageRepository Message;
        private IMessageService MessageService;
        private Color UserColor = Color.DarkRed;
        private Color ContactColor = Color.DarkBlue;
        private bool WasUser;

        public AirTransit()
        {
            Core = new CoreServices();
            InitializeComponent();
        }

        private void AirTransit_Load(object sender, EventArgs e)
        {
            DialogResult LoginCompleted;
            using (FormLogin login = new FormLogin())
            {
                LoginCompleted = login.ShowDialog();
                PhoneNumber = login.PhoneNumber;
            }
            if (LoginCompleted == DialogResult.Abort)
            {
                MessageBox.Show("Login aborted. Closing...");
                Close();
            }
            else
            {
                if (Core.Init(phoneNumber))
                {

                    Contact = Core.ContactRepository;
                    Message = Core.MessageRepository;
                    MessageService = Core.MessageService;
                    Contacts = Contact.GetContacts().ToList();
                    if (ListContacts.SelectedItem == null && ListContacts.Items.Count > 0)
                    {
                        ListContacts.SelectedIndex = 0;
                    }
                }
                else
                {
                    MessageBox.Show("An error as occur during initialization. Closing.");
                    Close();
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
            if (WasUser != currentlyUser || Txtconversation.TextLength == 0)
            {
                Txtconversation.AppendText(message.Sender.Name);
                WasUser = currentlyUser;
            }

            Txtconversation.AppendText(message.Content);
        }

        private void BtnContact_Click(object sender, EventArgs e)
        {
            NewContact newContact = new NewContact();
            if (newContact.ShowDialog() == DialogResult.OK)
            {
                Contact.AddContact(new Contact(newContact.PhoneNumber, newContact.ContactName));
            }
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