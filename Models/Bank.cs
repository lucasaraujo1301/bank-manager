namespace BankManager.Models {
    public class Bank(string name) {
        private List<Account> Accounts { get; set; } = [];
        public string Name { get; } = name;

        private int GenerateAccountNumber() {
            if (Accounts.Count == 0) {
                return 1;
            }
            int lastAccountNumber = Accounts.Max(a => a.AccountNumber);
            return ++lastAccountNumber;
        }

        public bool AlreadyHasAccount(string documentNumber) {
            return Accounts.Exists(
                a => a.Owner.DocumentNumber == documentNumber
            );
        }

        public Account? GetAccount(string documentNumber) {
            return Accounts.FirstOrDefault(a => a.Owner.DocumentNumber == documentNumber);
        }

        public void CreateAccount(Person owner) {
            int accountNumber = GenerateAccountNumber();

            Account account = new(owner, accountNumber);

            Accounts.Add(account);
        }

    }
}