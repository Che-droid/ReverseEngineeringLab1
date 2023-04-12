using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace LibContactManagement.DAL
{
    public class SqliteDBprovider : IProvider
    {
        private readonly string _connecitonString;

        public SqliteDBprovider(string connectionString)
        {
            _connecitonString = connectionString;
        }

        public int Delete(string name, string phoneNumber)
        {
            using (SQLiteConnection cnn = new SQLiteConnection(_connecitonString))
            {
                try
                {
                    cnn.Open();
                    string sqlstatment = name.Equals("all") ? "delete from contact" : string.Format("delete from contact where name='{0}' and phonenumber='{1}'", name, phoneNumber);
                    var sqlcommand = new SQLiteCommand(sqlstatment, cnn);
                    return sqlcommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new SQLiteException("Error occured while processing DB request: " + ex.Message);
                }
            }
        }

        public List<Contact> Retrieve(string value, string field)
        {
            using (SQLiteConnection cnn = new SQLiteConnection(_connecitonString))
            {
                try
                {
                    cnn.Open();
                    string sqlstatement = string.Format("select name , phonenumber,datetime,email from contact where {1}='{0}';", value, field);
                    var sqlcommand = new SQLiteCommand(sqlstatement, cnn);
                    List<Contact> contacts = new List<Contact>();
                    using (SQLiteDataReader rdr = sqlcommand.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            Contact contact = new Contact();
                            contact.Name = rdr["name"].ToString();
                            contact.PhoneNumber = rdr["phonenumber"].ToString();
                            contact.DateTime = rdr["datetime"].ToString();
                            contact.Email = rdr["email"].ToString();
                            contacts.Add(contact);
                        }
                    }
                    return contacts;
                }
                catch (Exception ex)
                {
                    throw new SQLiteException("Error occured while processing DB request: " + ex.Message);
                }
            }
        }

        public int Save(Contact contact)
        {
            using (SQLiteConnection cnn = new SQLiteConnection(_connecitonString))
            {
                try
                {
                    cnn.Open();
                    string sqlstatement = string.Format("INSERT INTO contact (name,phonenumber,datetime,email) VALUES ('{0}','{1}','{2}','{3}');",
                                                        contact.Name, contact.PhoneNumber, contact.DateTime, contact.Email);
                    var sqlcommand = new SQLiteCommand(sqlstatement, cnn);
                    return sqlcommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new SQLiteException("Error occured while processing DB request: " + ex.Message);
                }
            }
        }

        public List<Contact> NameRetrieve(string name)
        {
            return Retrieve(name, "name");
        }

        public List<Contact> PhoneRetrieve(string phoneNumber)
        {
            return Retrieve(phoneNumber, "phonenumber");
        }

        public List<Contact> EmailRetrieve(string email)
        {
            return Retrieve(email, "email");
        }

        public int Update(Contact oldContact, string newName, string newDatetime)
        {
            using (SQLiteConnection cnn = new SQLiteConnection(_connecitonString))
            {
                try
                {
                    cnn.Open();
                    string sqlstatment = string.Format("update contact set name='{0}' ,datetime='{3}' where name='{1}' and phonenumber='{2}'",
                                         newName, oldContact.Name, oldContact.PhoneNumber, newDatetime
                                            );
                    var sqlcommand = new SQLiteCommand(sqlstatment, cnn);
                    return sqlcommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new SQLiteException("Error occured while processing DB request: " + ex.Message);
                }
            }
        }
    }
}
