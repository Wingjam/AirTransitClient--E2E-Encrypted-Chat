using System;
using System.Windows.Forms;
using AirTransit_Core.Repositories;
using AirTransit_Core.Services;

//using Message = AirTransit_Core.Message;

namespace AirTransit_WindowsForms
{
    public partial class AirTransit : Form
    {
        string phoneNumber;
        //Core airCore;
        private IAuthenticationService Auth;
        private IContactRepository Contact;
        private IMessageRepository Message;

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
        }

        private void BtnSend_Click(object sender, EventArgs e)
        {
            //TODO envoyer le message qui est dans TxtInput
        }

        private void TxtContactList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //TODO changer pour le contact selectionner
            //string selectedContact = ListContacts.SelectedItem;
        }

        private void ShowCurrentContactConvo()
        {
            //            ShowConvo((Contact)ListContacts.SelectedItem);
        }

        //        private void ShowConvo(Contact contact)
        //        {
        //TODO
        //aller chercher les messages avec ce contact
        //pour chaque message, indiquer le nom de la personne qui l'a enovoyer
        //ensuite ecrire le message
        //        }

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
    }
}