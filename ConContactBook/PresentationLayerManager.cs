using LibContactManagement.BL;
using LibContactManagement.DAL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConContactBook
{
    public class PresentationLayerManager
    {
        private readonly ContactManager _contactManager;

        public PresentationLayerManager(IProvider provider)
        {
            _contactManager = new ContactManager(provider);
        }

        public void ListAllContacts()
        {
            Console.Write("\n n:search by name \t p:search by phonenumber \t e:search by email\n ");
            List<Contact> contacts = null;
            char command = Console.ReadKey().KeyChar;
            switch (command)
            {
                case 'n':
                    Console.Write("\n enter the name: ");
                    string name = Console.ReadLine();
                    contacts = _contactManager.GetContact(name, ContactManager.Field.Name);
                    break;
                case 'p':
                    Console.Write("\n enter the phonenumber: ");
                    string phonenumber = Console.ReadLine();
                    contacts = _contactManager.GetContact(phonenumber, ContactManager.Field.PhoneNumber);
                    break;
                case 'e':
                    Console.Write("\n enter the email: ");
                    string email = Console.ReadLine();
                    contacts = _contactManager.GetContact(email, ContactManager.Field.Email);
                    break;
                default:
                    Console.WriteLine("invalid option");
                    return;
            }

            Console.Write("\n display alphabetcally (a)or by date(d): ");
            char sort = Console.ReadKey().KeyChar;
            switch (sort)
            {
                case 'd':
                    contacts = contacts.OrderBy(o => o.DateTime).ToList();
                    break;
                case 'a':
                    contacts = contacts.OrderBy(o => o.Name).ToList();
                    break;
                default:
                    Console.WriteLine("invalid character");
                    break;
            }

            foreach (var contact in contacts)
            {
                Console.WriteLine(string.Format("\n {0}\t{1}\t{2}\t{3}", contact.Name, contact.PhoneNumber, contact.DateTime, contact.Email));
            }
            Console.WriteLine(string.Format(" {0} total(s)", contacts.Count));
        }

        public void DeleteContact()
        {
            InputnameAndphnumb(out string name, out string phonenumber);
            int result = _contactManager.Delete(name, phonenumber);
            Console.WriteLine(string.Format("\n {0} contacts deleted", result));
        }

        public void UpdateContact()
        {
            InputnameAndphnumb(out string name, out string phonenumber);
            Console.Write("\n enter the new name  ");
            string newname = Console.ReadLine();

            int result = _contactManager.Update(name, phonenumber, newname);
            Console.WriteLine(string.Format("\n {0} contacts updated", result));
        }

        public void CreateContact()
        {
            InputnameAndphnumb(out string name, out string phonenumber);
            Console.Write("\n enter the email ");
            string email = Console.ReadLine();
            int result = _contactManager.Create(name, phonenumber, email);
            Console.WriteLine(string.Format("\n {0} contacts saved", result));
        }

        private void InputnameAndphnumb(out string name, out string phonenumber)
        {
            Console.Write("\n enter the name:  ");
            name = Console.ReadLine();

            Console.Write("\n enter the phone number:  ");
            phonenumber = Console.ReadLine();
        }
    }
}
