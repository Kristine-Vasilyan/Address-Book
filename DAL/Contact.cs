namespace WebAddressBook.DAL
{
    public class Contact
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;
        public int PhoneNumber { get; set; }
        public string? PhysicalAddress { get; set; }
    }
}
