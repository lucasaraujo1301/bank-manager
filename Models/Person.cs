namespace BankManager.Models {
    public class Person (string firstName, string lastName, DateOnly birthdate) {
        public string FirstName { get; } = firstName;
        public string LastName { get; } = lastName;
        public DateOnly Birthdate { get; } = birthdate;

        public List<Address> Addresses { get; set; } = [];
    }
}