namespace BankManager.Models {
    public class Bank (string name) {
        private static List<Account> Accounts = [];
        public string Name { get; } = name;
    }
}