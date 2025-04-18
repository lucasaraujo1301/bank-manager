namespace BankManager.Models {
    public class Bank(string name) {
        private static List<Account> Accounts = [];
        public string Name { get; } = name;

        private static string GenerateAccountNumber() {
            if (Accounts.Count == 0) {
                return "0000001";
            }
            Accounts.Sort();
            string lastAccountNumber = Accounts[^1].AccountNumber;
            int lastAccountNumberParsed = int.Parse(lastAccountNumber);

            lastAccountNumberParsed++;

            return lastAccountNumberParsed.ToString("D7");
        }

        private static bool AlreadyHasAccount(string cpf) {
            return Accounts.Exists(
                a => a.Owner.Cpf == cpf
            );
        }

        public static Account GetAccount(string cpf) {
            if (!AlreadyHasAccount(cpf)) {
                throw new Exception("Account not found for this CPF.");
            }
            return Accounts.First(a => a.Owner.Cpf == cpf);
        }

        private static DateOnly ParseBirthdate(string birthdate) {
            DateOnly birthdateParsed;

            while (true) {
                if (DateOnly.TryParse(birthdate, out DateOnly date)) {
                    birthdateParsed = date;
                    break;
                }

                Console.WriteLine("Please, enter your Birthdate in the follow format (yyyy-MM-DD).");
                birthdate = Console.ReadLine()!;
            }

            return birthdateParsed;
        }

        public static (string cpf, string firstName, string lastName, string birthdate) GetUserInformation() {
            Console.WriteLine("Enter your CPF.");
            string? cpf = Console.ReadLine();

            while (cpf == null || AlreadyHasAccount(cpf)) {
                Console.WriteLine("CPF can not be null or already exist an account.");
                cpf = Console.ReadLine();
            }

            Console.WriteLine("Enter your First Name.");
            string? firstName = Console.ReadLine();

            while (firstName == null) {
                Console.WriteLine("Please, enter your First Name, can not be null.");
                firstName = Console.ReadLine();
            }

            Console.WriteLine("Enter your Last Name.");
            string? lastName = Console.ReadLine();

            while (lastName == null) {
                Console.WriteLine("Please, enter your Last Name, can not be null.");
                lastName = Console.ReadLine();
            }

            Console.WriteLine("Enter your Birthdate (yyyy-MM-dd).");
            string? birthdateString = Console.ReadLine();

            while (birthdateString == null) {
                Console.WriteLine("Please, enter your Birthdate, can not be null.");
                birthdateString = Console.ReadLine();
            }

            return (cpf, firstName, lastName, birthdateString);
        }

        public static void CreateAccount() {
            var (cpf, firstName, lastName, birthdateString) = GetUserInformation();

            DateOnly birthdate = ParseBirthdate(birthdateString);

            Person owner = new(firstName, lastName, birthdate, cpf);

            string accountNumber = GenerateAccountNumber();

            Account account = new(owner, accountNumber);

            Accounts.Add(account);

            Console.WriteLine("Account created successfully.");
        }

    }
}