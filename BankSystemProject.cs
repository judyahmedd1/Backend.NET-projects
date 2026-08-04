using System; 
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace project_2
{
    enum MainMenu
    {
        CreateAccount = 1,
        Login,
        Exit
    }

    enum UserMenu
    {
        CheckBalance = 1,
        Deposit,
        Withdraw,
        TransactionHistory,
        Logout
    }
    internal class BankSystem
    {

        static Dictionary<int, string> accnames = new Dictionary<int, string>();
        static Dictionary<int, double> accbalance = new Dictionary<int, double>();
        static Dictionary<int, string> accpins = new Dictionary<int, string>();
        static Dictionary<int, string> histories = new Dictionary<int, string>();

        static Random random = new Random();
        static int currentuser = -1;
        static void CreateAccount()
        {
            int accountnumber = random.Next(1, 1000);
            while (accnames.ContainsKey(accountnumber))
            {
                accountnumber = random.Next(1, 1000);
            }
            Console.WriteLine("your account number is: " + accountnumber);

            Console.WriteLine("enter account name");
            string name = Console.ReadLine();

            string pin;
            while (true)
            {
                Console.WriteLine("enter a 4-digit pin: ");
                pin = Console.ReadLine();
                if (pin.Length == 4 && int.TryParse(pin, out _))
                    break;
                Console.WriteLine("pin must be 4 digits, re-enter");
            }

            Console.WriteLine("enter balance");
            double balance;
            while (!double.TryParse(Console.ReadLine(), out balance) || balance <= 0)
            {
                Console.WriteLine("invalid balance, must be a positive number");
            }

            accnames.Add(accountnumber, name);
            accpins.Add(accountnumber, pin);
            accbalance.Add(accountnumber, balance);
            histories.Add(accountnumber, "");
        }
        static void Login()
        {
            Console.WriteLine("enter account number ");
            int number;
            while (!int.TryParse(Console.ReadLine(), out number) || number <= 0)
            {
                Console.WriteLine("invalid account number, must be a positive number");
            }

            Console.WriteLine("enter pin ");
            string pin = Console.ReadLine();

            if (accpins.ContainsKey(number) && accpins[number] == pin)
            {
                currentuser = number;
                Console.WriteLine("login successful");
            }
            else
            {
                Console.WriteLine("wrong account number or pin");
            }

        }
        static void Deposit()
        {
            Console.WriteLine("enter amount to deposit ");
            double amount;
            while (!double.TryParse(Console.ReadLine(), out amount) || amount <= 0)
            {
                Console.WriteLine("invalid deposit, must be positive number");
            }
            accbalance[currentuser] += amount;
            histories[currentuser] += $"deposit: {amount} , balance now : {accbalance[currentuser]}\n";
            CheckBalance();
        }

        static void Withdraw()
        {
            Console.WriteLine("enter amount to withdraw ");
            double withdrawamount;
            while (!double.TryParse(Console.ReadLine(), out withdrawamount) || withdrawamount <= 0)
            {
                Console.WriteLine("invalid withdraw amount, must be positive number");
            }
            if (accbalance[currentuser] < withdrawamount)
            {
                Console.WriteLine("current balance is not enough");
            }
            else
            {
                accbalance[currentuser] -= withdrawamount;
                histories[currentuser] += $"withdraw: {withdrawamount} , balance now : {accbalance[currentuser]}\n";
            }
            CheckBalance();
        }
        static void CheckBalance()
        {
            Console.WriteLine($"Current balance is: {accbalance[currentuser]:F2}");
        }

        static void ViewHistory()
        {
            Console.WriteLine($"History of account {currentuser}:\n{histories[currentuser]}\n");
        }
        static void Main(string[] args)
        {
            bool exit = false;

            while (!exit)
            {
                if (currentuser == -1)
                {
                    Console.WriteLine("Menu is 1. create account\n 2.login\n 3.exit");
                    int choice;
                    while (!int.TryParse(Console.ReadLine(), out choice))
                    {
                        Console.WriteLine("invalid choice");
                    }

                    switch ((MainMenu)choice)
                    {
                        case MainMenu.CreateAccount:
                            CreateAccount();
                            break;

                        case MainMenu.Login:
                            Login();
                            break;

                        case MainMenu.Exit:
                            exit = true;
                            break;

                        default:
                            Console.WriteLine("invalid choice");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("1.check balance");
                    Console.WriteLine("2.deposit");
                    Console.WriteLine("3.withdraw");
                    Console.WriteLine("4.transaction history");
                    Console.WriteLine("5.logout");
                    Console.WriteLine("enter your choice: ");
                    int choice;
                    while (!int.TryParse(Console.ReadLine(), out choice))
                    {
                        Console.WriteLine("invalid choice");
                    }

                    switch ((UserMenu)choice)
                    {
                        case UserMenu.CheckBalance:
                            CheckBalance();
                            break;

                        case UserMenu.Deposit:
                            Deposit();
                            break;

                        case UserMenu.Withdraw:
                            Withdraw();
                            break;

                        case UserMenu.TransactionHistory:
                            ViewHistory();
                            break;

                        case UserMenu.Logout:
                            currentuser = -1;
                            Console.WriteLine("logged out");
                            break;

                        default:
                            Console.WriteLine("invalid choice");
                            break;
                    }
                }
            }


        }

    }
}
