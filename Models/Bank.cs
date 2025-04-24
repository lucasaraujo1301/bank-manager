namespace BankManager.Models {
    public class Bank(string name) {
        private List<Account> Accounts { get; set; } = [];
        public string Name { get; } = name;

        private string GenerateAccountNumber() {
            if (Accounts.Count == 0) {
                return "0000001";
            }
            Accounts.Sort();
            string lastAccountNumber = Accounts[^1].AccountNumber;
            int lastAccountNumberParsed = int.Parse(lastAccountNumber);

            lastAccountNumberParsed++;

            return lastAccountNumberParsed.ToString("D7");
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
            string accountNumber = GenerateAccountNumber();

            Account account = new(owner, accountNumber);

            Accounts.Add(account);
        }

    }
}