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
        private IAuthenticationService Auth;
        private IContactRepository Contact;
        private IMessageRepository Message;
        private IMessageService MessageService;
        private Color UserColor = Color.DarkRed;
        private Color ContactColor = Color.DarkBlue;

        public AirTransit()
        {
            InitializeComponent();
        }

        private void AirTransit_Load(object sender, EventArgs e)
        {
            using (FormLogin form2 = new FormLogin())
            {
                form2.ShowDialog();
                PhoneNumber = form2.PhoneNumber;
            }
            //Auth = new //TODO
            //Contact = new //TODO
            //Message = new //TODO
            //MessageService = new //TODO
            Auth.SignUp(phoneNumber);
            Contacts = Contact.GetContacts().ToList();
            if (ListContacts.SelectedItem == null && ListContacts.Items.Count > 0)
            {
                ListContacts.SelectedIndex = 0;
            }
        }

        private void BtnSend_Click(object sender, EventArgs e)
        {
            if (ListContacts.SelectedItem != null)
                MessageService.SendMessage(((Contact)ListContacts.SelectedItem).PhoneNumber, TxtInput.Text);
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
            //TODO
            //aller chercher les messages avec ce contact
            //pour chaque message, indiquer le nom de la personne qui l'a enovoyer
            //ensuite ecrire le message
            List<Message> messages = Message.GetMessages(contact).ToList();
            foreach (Message message in messages)
            {

                //Txtconversation.ForeColor = message.
                //Txtconversation.AppendText(message.);
            }
        }

        //        private void PrintMessage(AirTransit_Core.Message message)
        //        {
        //
        //        }

        public string PhoneNumber
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