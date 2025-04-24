namespace BankManager.Models {
    public class Account (Person owner, string accountNumber) {

        public Person Owner { get; } = owner;
        public string AccountNumber { get; } = accountNumber;
        public double Amount { get; set; } = 0.00;

        private bool HasEnoughMoney(double amount) {
            return Amount > amount;
        }

        public void DepositMoney(double amount) {
            Amount += amount;
        }

        public void WithdrawMoney(double amount) {
            if (!HasEnoughMoney(amount)) {
                throw new Exception("You do not have enought money for this action.");
            }
            Amount -= amount;
        }

        public void MakeTransfer(double amount, Account destinyAccount) {
            if (!HasEnoughMoney(amount)) {
                throw new Exception("You do not have enought money for this action.");
            }
            WithdrawMoney(amount);
            destinyAccount.DepositMoney(amount);
        }
    }
}