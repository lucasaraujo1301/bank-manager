namespace BankManager.Models {
    public class Person (string firstName, string lastName, DateOnly birthdate, string cpf) {
        public string FirstName { get; } = firstName;
        public string LastName { get; } = lastName;
        public string Cpf { get; } = cpf;
        public DateOnly Birthdate { get; } = birthdate;

        public List<Address> Addresses { get; set; } = [];
    }
}