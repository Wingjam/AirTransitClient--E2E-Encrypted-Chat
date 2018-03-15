using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AirTransit_Core.Models;
using AirTransit_Core;

namespace AirTransit_Console
{
    public class AirTransitConsole
    {
        private readonly CoreServices _coreServices;
        private Contact _currentContact;
        private const bool TEXTINPUTFOLLOWING = true;
        private const string VERSION = "1.0";

        public AirTransitConsole()
        {
            _coreServices = new CoreServices();
        }
        
        public void AppLoop()
        {
            var mustQuit = false;
            var loggedIn = false;
            
            WriteDecoratedText($"AirTransit V{VERSION}");
            
            while (!loggedIn)
            {
                var phoneNumber = GetPhoneInput("Please enter your phone number (10 digits): ");
                loggedIn = _coreServices.Init(phoneNumber);
            }
            
            ShowMenuOptions();
            
            while (!mustQuit)
            {
                var command = GetTextInput("\nEnter your command: ").ToUpper();
                
                switch (command)
                {
                    case "SM": //SendMessage
                        SendMessage();
                        break;
                    case "FM": //FetchMessages
                        FetchMessages();
                        break;
                    case "SC": //SelectContact
                        SelectContact();
                        FetchMessages();
                        break;
                    case "AC": //AddContact
                        AddContact();
                        break;
                    case "ST": //ShowContacts
                        ShowContacts();
                        break;
                    case "DC": //DeleteContact
                        DeleteContact();
                        break;
                    case "MO": //ShowMenuOptions
                    case "HELP":
                        ShowMenuOptions();
                        break;
                    case "QQ": //Quit
                        WriteToConsole("AirTransit closing...");
                        mustQuit = true;
                        break;
                    default: //Invalid
                        WriteInvalidInput();
                        break;
                }
            }
            
            Environment.Exit(0);
        }

        private static string GetTextInput(string text)
        {
            string result;
            
            do
            {
                WriteToConsole(text, TEXTINPUTFOLLOWING);
                result = Console.ReadLine();
            } while (string.IsNullOrEmpty(result));
            
            return result;
        }
        
        private static int GetIntInput(string text)
        {
            var result = 0;
            string input;
            
            do
            {
                WriteToConsole(text, TEXTINPUTFOLLOWING);
                input = Console.ReadLine();
            } while (!int.TryParse(input, out result));
            
            return result;
        }
        
        private static string GetPhoneInput(string text)
        {
            string result;
            
            do
            {
                WriteToConsole(text, TEXTINPUTFOLLOWING);
                result = Console.ReadLine();
            } while (string.IsNullOrEmpty(result) || result.Length != 10 || !result.All(char.IsDigit));
            
            return result;
        }

        private void SendMessage()
        {
            if (_currentContact == null)
            {
                SelectContact();
                if (_currentContact == null)
                {
                    return;
                }
            }
            
            var answer = GetTextInput($"Send message to {_currentContact.Name} " +
                                      $"({_currentContact.PhoneNumber})? (y/n): ").ToLower();
            if (answer == "n")
            {
                SelectContact();
            }
            
            var message = GetTextInput($"Message to {_currentContact.Name}: ");
            _coreServices.MessageService.SendMessage(_currentContact, message);
        }

        private void FetchMessages()
        {
            if (_currentContact == null)
            {
                SelectContact();
                if (_currentContact == null)
                {
                    return;
                }
            }
            
            WriteContactHeader();
            
            var fetchMoreMessages = true;
            var messagesToFetch = 10;
            
            do
            {
                var messages = messagesToFetch == -1
                    ? _coreServices.MessageRepository.GetMessages(_currentContact)?.ToList()
                    : _coreServices.MessageRepository.GetMessages(_currentContact, messagesToFetch)?.ToList();
                if (messages == null || !messages.Any())
                {
                    WriteToConsole("No messages.\nUse SM to send some :)");
                    break;
                }

                foreach (var message in messages)
                {
                    WriteToConsole($"{message.Timestamp} - {message.Content}");
                }

                var getLongerHistory = GetTextInput($"Get longer message history? (y/n/all): ").ToLower();
                switch (getLongerHistory)
                {
                    case "y":
                        messagesToFetch += 10;
                        WriteContactHeader();
                        break;
                    case "all":
                        messagesToFetch = -1;
                        WriteContactHeader();
                        break;
                    default:
                        fetchMoreMessages = false;
                        break;
                }
            } while (fetchMoreMessages);
        }

        private void AddContact()
        {
            WriteDecoratedText("Add a contact");
            
            var name = GetTextInput("Contact name: ");
            var phoneNumber = GetPhoneInput("Phone number (10 digits): ");
            _coreServices.ContactRepository.AddContact(new Contact(phoneNumber, name));
            
            WriteToConsole($"{name} added to your contacts.");
        }

        private void SelectContact()
        {
            WriteDecoratedText("Select a contact");
            
            var contact = ContactSelection();
            if (contact == null) return;
            _currentContact = contact;
            
            WriteToConsole($"{_currentContact.Name} selected.");
        }

        private void DeleteContact()
        {
            WriteDecoratedText("Delete a contact");
            
            var contact = ContactSelection();
            if (contact == null) return;
            _coreServices.ContactRepository.DeleteContact(contact);
            
            WriteToConsole($"{contact.Name} removed from your contacts.");
        }

        private Contact ContactSelection()
        {
            Contact result = null;
            var contacts = _coreServices.ContactRepository.GetContacts().ToList();
            if (contacts.Count <= 0)
            {
                WriteToConsole("No contacts.\nUse AC to add some :)");
                return null;
            }
            
            do
            {
                for (var i = 0; i < contacts.Count; ++i)
                {
                    WriteToConsole($"{i}. {contacts[i].Name} ({contacts[i].PhoneNumber})");
                }
                
                var contactId = GetIntInput($"Choose a contact (0-{contacts.Count - 1})(-1 to cancel): ");
                if (contactId == -1)
                {
                    break;
                }
                
                if (contactId < 0 || contactId > contacts.Count - 1)
                {
                    WriteInvalidInput();
                    continue;
                }; 
                
                result = contacts[contactId];
            } while (result == null);

            return result;
        }

        private void ShowContacts()
        {
            WriteDecoratedText("Your contacts");
            
            var contacts = _coreServices.ContactRepository.GetContacts().ToList();
            if (!contacts.Any())
            {
                WriteToConsole("No contacts.\nUse AC to add some :)");
            }
            
            foreach (var contact in contacts)
            {
                WriteToConsole($"{contact.Name} ({contact.PhoneNumber})");
            }
        }
        
        private static string DecorateText(string text)
        {
            const string DECORATION = "------------------------------";
            
            var sbuild = new StringBuilder();
            sbuild.AppendLine(DECORATION);
            sbuild.AppendLine(text.PadLeft(text.Length+(30-text.Length)/2));
            sbuild.AppendLine(DECORATION);
            
            return sbuild.ToString();
        }

        private static void WriteToConsole(string text, bool textInputFollowing = false)
        {
            if (string.IsNullOrEmpty(text)) return;
            if (!textInputFollowing)
            {
                Console.WriteLine(text);
            }
            else
            {
                Console.Write(text);
            }
        }

        private void WriteContactHeader()
        {
            if (_currentContact != null)
            {
                WriteDecoratedText($"{_currentContact.Name} ({_currentContact.PhoneNumber})");
            }
        }

        private static void WriteInvalidInput()
        {
            WriteToConsole("Invalid input.");
        }

        private static void WriteDecoratedText(string text)
        {
            WriteToConsole(DecorateText(text));
        }

        private static void ShowMenuOptions()
        {
            var sbuild = new StringBuilder(DecorateText("Menu options"));
            var options = new List<string>
            {
                "SM - Send Message",
                "FM - Fetch messages",
                "SC - Select contact",
                "AC - Add contact",
                "ST - Show contacts",
                "DC - Delete contact",
                "MO - Show menu options",
                "QQ - Quit"
            };
            
            foreach (var option in options)
            {
                sbuild.AppendLine(option);
            }
            
            WriteToConsole(sbuild.ToString(), true);
        }
    }
}
