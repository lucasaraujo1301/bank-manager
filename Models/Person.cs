namespace BankManager.Models {
    public class Person (string firstName, string lastName, DateOnly birthdate, string documentNumber) {
        public string FirstName { get; } = firstName;
        public string LastName { get; } = lastName;
        public string DocumentNumber { get; } = documentNumber;
        public DateOnly Birthdate { get; } = birthdate;

        public List<Address> Addresses { get; set; } = [];
    }
}