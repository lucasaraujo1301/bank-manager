namespace BankManager.Models {
    public class Account (Person owner, string accountNumber) {

        public Person Owner { get; } = owner;
        public string AccountNumber { get; } = accountNumber;
    }
}