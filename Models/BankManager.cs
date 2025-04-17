namespace BankManager.Models {
    public class BankManager () {
        private static List<Bank> Banks = [];

        private static bool HasBank(string bankName) {
            return Banks.Exists(b => b.Name == bankName);
        }

        public static Bank GetBank (string bankName) {
            if (!HasBank(bankName)) {
                throw new Exception("Bank not found.");
            }

            return Banks.First(b => b.Name == bankName);
        }

        public static void AddBank (string bankName) {
            if (HasBank(bankName)) {
                throw new Exception("Bank already exist.");
            }

            Bank bank = new(bankName);
            Banks.Add(bank);

            Console.WriteLine("Bank created successfully");
        }
    }
}