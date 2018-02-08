using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace AirTransit_Core.Models
{
    public class Contact
    {
        public Contact(string phoneNumber, string name)
        {
            PhoneNumber = phoneNumber;
            Name = name;
        }
        
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public string Name { get; set; }
        public ICollection<Message> Messages { get; }
    }
}
