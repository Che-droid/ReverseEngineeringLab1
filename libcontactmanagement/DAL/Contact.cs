namespace LibContactManagement.DAL
{
    public class Contact
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string DateTime { get; set; }
        public string Email { get; set; }

        public Contact() { }

        public Contact(string name, string phoneNumber, string dateTime = null, string email = null)
        {
            Name = name;
            PhoneNumber = phoneNumber;
            DateTime = dateTime;
            Email = email;
        }
    }
}
