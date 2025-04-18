namespace BankManager.Models {
    public class Account (Person owner, string accountNumber) {

        public Person Owner { get; } = owner;
        public string AccountNumber { get; } = accountNumber;
        public double Amount { get; set; } = 0.00;

        public void DepositMoney(double amount) {
            Amount += amount;
        }
    }
}