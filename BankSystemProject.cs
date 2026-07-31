using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace project_2
{
    internal class BankSystem
    {
        //lists for creating an account 
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
            Console.WriteLine("your account number is: " + accountnumber);

            Console.WriteLine("enter account name");
            string name = Console.ReadLine();
            Console.WriteLine("enter pin");
            string pin = Console.ReadLine();
            Console.WriteLine("enter balance");
            double balance = double.Parse(Console.ReadLine());

            accnumbers.Add(accountnumber);
            accnames.Add(name);
            accpins.Add(pin);
            accbalance.Add(balance);
            histories.Add("");
        }
        static void Login()
        {
            Console.WriteLine("enter account number ");
            int number = int.Parse((Console.ReadLine()));

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

            Console.WriteLine("Wrong account number or pin.");
        }
        static void Deposit()
        {
            Console.WriteLine("enter amount to deposit ");
            double amount = double.Parse(Console.ReadLine());
            accbalance[currentuser] += amount;
            histories[currentuser] += $"deposit: {amount} , balance now : {accbalance[currentuser]}\n"; ;
        }

        static void Withdraw()
        {
            Console.WriteLine("enter amount to withdraw ");
            double withdrawamount = double.Parse(Console.ReadLine());
            if (accbalance[currentuser] < withdrawamount)
            {
                Console.WriteLine("current balance is not enough");
            }
            else
            {
                accbalance[currentuser] -= withdrawamount;
                histories[currentuser] += $"withdraw: {withdrawamount} , balance now : {accbalance[currentuser]}\n"; ;
            }
        }
            static void CheckBalance()
        {
            Console.WriteLine("current balance is: " + accbalance[currentuser]);
        }

        static void ViewHistory()
        {
            int accnum = accnumbers[currentuser];
            Console.WriteLine($"history of {accnum} is {histories[currentuser]}\n") ;
        }
        static void Main(string[] args)
        {
            bool exit = false;

            while (!exit)
            {
                if (currentuser == -1)
                {
                    Console.WriteLine("Menu is 1. create account\n 2.login\n 3.exit");
                    int choice = int.Parse((Console.ReadLine()));

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
                    int choice = int.Parse(Console.ReadLine());

                    switch (choice)
                    {
                        case 1:
                            Console.WriteLine("balance is: " + accbalance[currentuser]);
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
                            Console.WriteLine("logged out.");
                            break;
                    }
                }
            }
        

    }
       
    }
}
