using System.Collections.Generic;

namespace LibContactManagement.DAL
{
    public interface IProvider
    {
        int Save(Contact contact);
        List<Contact> NameRetrieve(string name);
        List<Contact> PhoneRetrieve(string phoneNumber);
        List<Contact> EmailRetrieve(string email);
        int Delete(string name, string phoneNumber);
        int Update(Contact oldContact, string newName, string newDatetime);
    }
}
