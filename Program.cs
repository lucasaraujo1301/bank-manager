using BankManager.Models;

namespace BankManager
{
    public class BankManager {
        private static List<Bank> Banks { get; set; } = [];

        public static void Main () {
            WriteMainPageConsole();

            string? choice;

            do {
                WriteMainOptionsConsole();
                choice = Console.ReadLine();

                switch (choice) {
                    case "1":
                        Bank? bank = SelectBank();

                        // Statement to check if user typed 'Exit'.
                        if (bank == null) {
                            break;
                        }
                        SelectBankFlow(bank);
                        break;
                    case "2":
                        CreateBank();
                        break;
                    case "3":
                        Console.WriteLine("Goodbye!");
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            } while (choice != "3");

        }

        private static string GetRequiredInput(string description, bool canExit = false) {
            string message = $"\nEnter {description}:";
            if (canExit) {
                message = message.Replace(":", " (Enter 'Exit' to back to the menu):");
            }

            Console.WriteLine(message);
            string? input = Console.ReadLine();

            while (string.IsNullOrEmpty(input)) {
                Console.WriteLine($"\nThe {description} can not be null or empty.");
                input = Console.ReadLine();
            }

            return input.Trim();
        }

        private static void WriteMainPageConsole () {
            Console.WriteLine("======================================");
            Console.WriteLine("        Welcome to BankManager        ");
            Console.WriteLine("======================================");
            Console.WriteLine("A simple and powerful console app to");
            Console.WriteLine("manage bank accounts, transactions,");
            Console.WriteLine("and customer information.");
        }

        private static void WriteMainOptionsConsole () {
            Console.WriteLine("\nPlease select an option:");
            Console.WriteLine("1. Select an Existing Bank");
            Console.WriteLine("2. Create a New Bank");
            Console.WriteLine("3. Exit\n");
        }

        private static void WriteSelectedBankOptionConsole () {
            Console.WriteLine("\nPlease select an option:");
            Console.WriteLine("1. Select an Existing Account");
            Console.WriteLine("2. Create a New Account");
            Console.WriteLine("3. Main menu\n");
        }

        private static void WriteSelectedAccountOptionConsole () {
            Console.WriteLine("\nPlease select an option:");
            Console.WriteLine("1. Deposit Money");
            Console.WriteLine("2. Withdraw Money");
            Console.WriteLine("3. Make an Transfer");
            Console.WriteLine("4. Show account info.");
            Console.WriteLine("5. Main menu\n");
        }

        private static bool HasBank(string bankName) {
            return Banks.Exists(b => b.Name == bankName);
        }

        public static Bank? GetBank (string bankName) {
            return Banks.FirstOrDefault(b => b.Name == bankName);
        }

        public static void AddBank (string bankName) {
            if (HasBank(bankName)) {
                throw new Exception("Bank already exist.");
            }

            Bank bank = new(bankName);
            Banks.Add(bank);
        }

        private static void CreateBank () {
            string bankName = GetRequiredInput("Bank name", true);

            if (bankName.Equals("exit", StringComparison.CurrentCultureIgnoreCase)) {
                return;
            }

            try {
                AddBank(bankName);
                Console.WriteLine("\nBank created successfully\n");
            } catch (Exception ex) {
                Console.WriteLine($"\n{ex.Message}");
            }
        }

        private static Bank? SelectBank() {

            Bank? bank = null;

            while (bank == null) {
                string bankName = GetRequiredInput("Bank name", true);

                if (bankName.Equals("exit", StringComparison.CurrentCultureIgnoreCase)) {
                    return null;
                }

                bank = GetBank(bankName);

                if (bank == null) {
                    Console.WriteLine("\nBank not found.");
                    Console.WriteLine("Try another name.");
                }
            }

            return bank;
        }

        private static void SelectBankFlow (Bank bank) {
            string? choice;

            do {
                WriteSelectedBankOptionConsole();
                choice = Console.ReadLine();

                switch (choice) {
                    case "1":
                        Account? account = SelectAccount(bank);

                        // Statement to check if user typed 'Exit'.
                        if (account == null) {
                            break;
                        }
                        SelectAccountFlow(account, bank);
                        break;
                    case "2":
                        CreateAccount(bank);
                        break;
                    case "3":
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            } while (choice != "3");
            
        }

        private static DateOnly ParseBirthdate(string birthdate) {
            DateOnly birthdateParsed;

            while (true) {
                if (DateOnly.TryParse(birthdate, out DateOnly date)) {
                    birthdateParsed = date;
                    break;
                }

                birthdate = GetRequiredInput("the birthdate in the follow format (yyyy-MM-DD)");
            }

            return birthdateParsed;
        }

        private static Account? SelectAccount(Bank bank, string message = "the document number") {
            Account? account = null;

            while (account == null) {
                string documentNumber = GetRequiredInput(message, true);
                if (documentNumber.Equals("exit", StringComparison.CurrentCultureIgnoreCase)) {
                    return null;
                }

                account = bank.GetAccount(documentNumber);

                if (account == null) {
                    Console.WriteLine("\nDocument number not found.");
                }
            }

            return account;
        }

        private static void SelectAccountFlow (Account account, Bank bank) {
            string? choice;

            do {
                WriteSelectedAccountOptionConsole();
                choice = Console.ReadLine();

                switch (choice) {
                    case "1":
                        DepositMoney(account);
                        break;
                    case "2":
                        WithdrawMoney(account);
                        break;
                    case "3":
                        TransferMoney(account, bank);
                        break;
                    case "4":
                        Console.WriteLine($"\nAccount number: {account.AccountNumberToDisplay()}");
                        Console.WriteLine($"First Name: {account.Owner.FirstName}");
                        Console.WriteLine($"Document number: {account.Owner.DocumentNumber}");
                        Console.WriteLine($"Balance: ${account.Balance()}");
                        break;
                    case "5":
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }

            } while (choice != "5");
        }

        private static void DepositMoney(Account account) {
            double amount;
            while (true) {
                string amountString = GetRequiredInput("the amount you want to deposit", true);

                if (amountString.Equals("exit", StringComparison.CurrentCultureIgnoreCase)) {
                    return;
                }

                if (double.TryParse(amountString, out double result)) {
                    amount = result;
                    if (amount > 0) {
                        break;
                    }
                }

                Console.WriteLine("The amount to be deposit must be great than 0.");
            }

            account.DepositMoney(amount);
            Console.WriteLine("Deposit successfully!");
        }

        private static void WithdrawMoney(Account account) {
            double amount;
            while (true) {
                string amountString = GetRequiredInput("the amount you want to withdraw", true);

                if (amountString.Equals("exit", StringComparison.CurrentCultureIgnoreCase)) {
                    return;
                }

                if (double.TryParse(amountString, out double result)) {
                    amount = result;
                    if (amount > 0) {
                        break;
                    }
                }

                Console.WriteLine("The amount to be withdrawn must be greater than 0.");
            }

            try {
                account.WithdrawMoney(amount);
                Console.WriteLine("Withdrawal successful!");
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
        }

        private static void CreateAccount(Bank bank) {
            string documentNumber = GetRequiredInput("the document number", true);

            if (documentNumber.Equals("exit", StringComparison.CurrentCultureIgnoreCase)) {
                return;
            }

            while(bank.AlreadyHasAccount(documentNumber)) {
                Console.WriteLine("Document number already in use.");
                documentNumber = GetRequiredInput("the document number", true);

                if (documentNumber.Equals("exit", StringComparison.CurrentCultureIgnoreCase)) {
                    return;
                }
            }

            string firstName = GetRequiredInput("your first name");
            string lastName = GetRequiredInput("your last name");
            string birthdateString = GetRequiredInput("your birthdate in the follow format (yyyy-MM-DD)");
            DateOnly birthdate = ParseBirthdate(birthdateString);

            Person owner = new(firstName, lastName, birthdate, documentNumber);

            bank.CreateAccount(owner);
            Console.WriteLine("Account created successfully!");
        }

        private static void TransferMoney(Account account, Bank bank) {
            string inputDescription = "the document number of the account you want to transfer the money to";
            Account? destinyAccount = SelectAccount(bank, inputDescription);

            if (destinyAccount == null) {
                return;
            }

            double amount;
            while (true) {
                string amountString = GetRequiredInput("the amount you want to make the transfer", true);

                if (amountString.Equals("exit", StringComparison.CurrentCultureIgnoreCase)) {
                    return;
                }

                if (double.TryParse(amountString, out double result)) {
                    amount = result;
                    if (amount > 0) {
                        break;
                    }
                }

                Console.WriteLine("The amount to be transferred must be greater than 0.");
            }

            try {
                account.MakeTransfer(amount, destinyAccount);
                Console.WriteLine("Transfer created successfully!");
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }

        }
    }
}