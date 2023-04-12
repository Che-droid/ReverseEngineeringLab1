using System;
using LibContactManagement.DAL;
using System.Collections.Generic;

namespace LibContactManagement.BL
{
    public class ContactManager
    {
        public enum Field { Name, PhoneNumber, Email };

        private readonly IProvider _provider;

        public ContactManager(IProvider provider)
        {
            _provider = provider;
        }

        public List<Contact> GetContact(string name, Field field)
        {
            switch (field)
            {
                case Field.Name:
                    return _provider.NameRetrieve(name);
                case Field.PhoneNumber:
                    return _provider.PhoneRetrieve(name);
                case Field.Email:
                    return _provider.EmailRetrieve(name);
            }
            return new List<Contact>();
        }

        public int Update(string name, string phoneNumber, string newName)
        {
            return _provider.Update(new Contact(name, phoneNumber), newName, DateTime.Now.ToString());
        }

        public int Create(string name, string phonenumber, string email)
        {
            return _provider.Save(new Contact(name, phonenumber, DateTime.Now.ToString(), email));
        }

        public int Delete(string name, string phonenumber)
        {
            return _provider.Delete(name, phonenumber);
        }
    }
}
