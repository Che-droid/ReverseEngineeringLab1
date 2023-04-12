using System;
using System.Configuration;
using LibContactManagement.DAL;

namespace ConContactBook
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("q to quit \t c to create contact \t u to update contact \t d to delete contact \t s to list all contacts");

            SqliteDBprovider provider = new SqliteDBprovider(GetConnectionString());
            var presentationLayerManager = new PresentationLayerManager(provider);

            try
            {
                while (true)
                {
                    char command = Console.ReadKey().KeyChar;
                    switch (command)
                    {
                        case 'q':
                            Console.WriteLine("\n quit.....");
                            return;
                        case 'c':
                            presentationLayerManager.CreateContact();
                            break;
                        case 'u':
                            presentationLayerManager.UpdateContact();
                            break;
                        case 'd':
                            presentationLayerManager.DeleteContact();
                            break;
                        case 's':
                            presentationLayerManager.ListAllContacts();
                            break;
                        default:
                            Console.WriteLine("\n enter valid command");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Terminating error: " + ex.Message);
                Console.WriteLine("Press any key to close the program...");
                Console.Read();
            }
        }

        private static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["default"].ConnectionString;
        }
    }
}
