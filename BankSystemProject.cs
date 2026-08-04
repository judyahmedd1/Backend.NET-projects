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
        
        static List<int> accnumbers = new List<int>();
        static List<string> accnames = new List<string>();
        static List<double> accbalance = new List<double>();
        static List<string> accpins = new List<string>();
        static List<string> histories = new List<string>();

        static Random random = new Random();
        static int currentuser = -1;
        static void CreateAccount()
        {
            int accountnumber = random.Next(1, 1000);
            while (accnumbers.Contains(accountnumber))
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
            while (!double.TryParse(Console.ReadLine(), out balance))
            {
                Console.WriteLine("invalid balance");
            }

            accnumbers.Add(accountnumber);
            accnames.Add(name);
            accpins.Add(pin);
            accbalance.Add(balance);
            histories.Add("");
        }
        static void Login()
        {
            Console.WriteLine("enter account number ");
            int number;
            while (!int.TryParse(Console.ReadLine(), out number))
            {
                Console.WriteLine("invalid account number");
            }

            Console.WriteLine("enter pin ");
            string pin = Console.ReadLine();

            for (int i = 0; i < accnumbers.Count; i++)
            {
                if (accnumbers[i] == number && accpins[i] == pin)
                {
                    currentuser = i;
                    Console.WriteLine("login done");
                    return;
                }
            }

            Console.WriteLine("Wrong account number or pin");
        }
        static void Deposit()
        {
            Console.WriteLine("enter amount to deposit ");
            double amount;
            while (!double.TryParse(Console.ReadLine(),out amount) || amount <= 0)
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
            while(!double.TryParse(Console.ReadLine(),out withdrawamount)|| withdrawamount <= 0){
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
            Console.WriteLine("current balance is: " + accbalance[currentuser]);
        }

        static void ViewHistory()
        {
            int accnum = accnumbers[currentuser];
            Console.WriteLine($"history of {accnum} is {histories[currentuser]}\n");
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

                    switch (choice)
                    {
                        case 1:
                            CreateAccount();
                            break;

                        case 2:
                            Login();
                            break;

                        case 3:
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

                    switch (choice)
                    {
                        case 1:
                            CheckBalance();
                            break;

                        case 2:
                            Deposit();
                            break;

                        case 3:
                            Withdraw();
                            break;

                        case 4:
                            ViewHistory();
                            break;

                        case 5:
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
