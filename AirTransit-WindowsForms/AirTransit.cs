using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using AirTransit_Core;
//using Message = Air;

namespace AirTransit_WindowsForms
{
    public partial class AirTransit : Form
    {
//        Core airCore;
        public AirTransit()
        {
            InitializeComponent();
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
    }
}