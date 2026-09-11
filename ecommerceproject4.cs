using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using project4c_andoop;
using static project4c_andoop.UserManager;

namespace project4c_andoop
{
    //loose coupling
    //where users are stored and how you find them
    internal class UserManager
    {
        //id is the key
        private Dictionary<int, User> _users = new Dictionary<int, User>();

        public Customer RegisterCustomer(string name, string password)
        {
            Customer c = new Customer(name, password);
            //store with this id customer c in the dictionary
            _users[c.Id] = c;
            //return customer object
            return c;
        }
        public Admin RegisterAdmin(string name, string password)
        {
            Admin a = new Admin(name, password);
            //store with this id customer c in the dictionary
            _users[a.Id] = a;
            //return admin object
            return a;
        }

        public User Login(int id, string name, string password)
        {
            if (!_users.TryGetValue(id, out User u))
            {
                Console.WriteLine("login failed.\nid doesn't exist\n");
                return null;
            }

            if (u.Name != name || u.Password != password)
            {
                Console.WriteLine("login failed.\nincorrect name or password\n");
                return null;
            }

            Console.WriteLine("successful login\n");
            return u;
        }
    }

}
    //just representing what a user (customer/admin) is
    internal class User
    {
            protected int _id;
            //increment in constructor
            protected static int _nextidnumber = 1;
            protected string _name;
            protected string _password;

            public int Id { get { return _id; } }
            public string Name
            {
                get { return _name; }
                set
                {
                    if (string.IsNullOrEmpty(value))
                    {
                        throw new ArgumentException("name can't be empty");
                    }
                    _name = value;
                }
            }
            public string Password
            {
                get { return _password; }
                set
                {
                    if (string.IsNullOrEmpty(value) || value.Length < 8)
                    {
                        throw new ArgumentException("password can't be empty or be less than 8 characters");
                    }

                    _password = value;
                }
            }

            public User(string name, string password)
            {
                _id = _nextidnumber++;
                Name = name;
                Password = password;
            }
        }
        internal class Customer : User
        {
            public Customer(string name, string password) : base(name, password)
            {
            }

        }
        internal class Admin : User
        {
            public Admin(string name, string password) : base(name, password)
            {
            }
        }
internal class Product
{
    protected static int _nextidnumber = 1;
    protected int _id;
    protected string _name;
    protected decimal _price;
    protected int _stockquantity;

    public int Id { 
        get { return _id; } 
    }

    public string Name
    {
        get { return _name; }
        set
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("product name can't be empty");
            _name = value;
        }
    }

    public decimal Price
    {
        get { return _price; }
        set
        {
            if (value < 0)
                throw new ArgumentException("price can't be negative");
            _price = value;
        }
    }

    public int StockQuantity
    {
        get { return _stockquantity; }
        set
        {
            if (value < 0)
                throw new ArgumentException("stock quantity can't be negative");
            _stockquantity = value;
        }
    }

    public Product(string name, decimal price, int stockQuantity)
    {
        _id = _nextidnumber++;
        Name = name;
        Price = price;
        StockQuantity = stockQuantity;
    }
}
internal class ProductManager
{
    private Dictionary<int, Product> _products = new Dictionary<int, Product>();

    public Product AddProduct(string name, decimal price, int stockQuantity)
    {
        Product p = new Product(name, price, stockQuantity);
        _products[p.Id] = p;
        return p;
    }

    public bool UpdateProduct(int id, string name, decimal price, int stockQuantity)
    {
        if (!_products.TryGetValue(id, out Product p))
        {
            Console.WriteLine("update failed\nproduct id doesn't exist");
            return false;
        }

        p.Name = name;
        p.Price = price;
        p.StockQuantity = stockQuantity;
        return true;
    }

    public bool DeleteProduct(int id)
    {
        if (!_products.ContainsKey(id))
        {
            Console.WriteLine("delete failed\nproduct id doesn't exist");
            return false;
        }

        _products.Remove(id);
        return true;
    }

    public Product GetProduct(int id)
    {
        _products.TryGetValue(id, out Product p);
        return p;
    }

    //lets users browse a list with no key
    public List<Product> GetAllProducts()
    {
        return _products.Values.ToList();
    }
}
//compostion CartItem has a Product
internal class CartItem
{
    private Product _product;
    private int _quantity;

    public Product Product { 
        get { return _product; }
    }

    public int Quantity
    {
        get { return _quantity; }
        set
        {
            if (value <= 0)
                throw new ArgumentException("quantity must be greater than 0");
            _quantity = value;
        }
    }

    public CartItem(Product product, int quantity)
    {
        if (product == null)
            throw new ArgumentException("product can't be null");
        _product = product;
        Quantity = quantity;
    }

    public decimal Getsubtotal()
    {
        return _product.Price * _quantity;
    }
}
internal class Cart
{
    private List<CartItem> _items = new List<CartItem>();

    public void AddProduct(Product product, int quantity)
    {
        if (product == null)
            throw new ArgumentException("product can't be null");

        if (quantity > product.StockQuantity)
        {
            Console.WriteLine("not enough stock available");
            return;
        }

        CartItem item = new CartItem(product, quantity);
        _items.Add(item);
    }

    public void RemoveProduct(Product product)
    {
        CartItem itemToRemove = null;

        for (int i = 0; i < _items.Count; i++)
        {
            if (_items[i].Product.Id == product.Id)
            {
                itemToRemove = _items[i];
                break;
            }
        }

        if (itemToRemove == null)
        {
            Console.WriteLine("product not in cart");
            return;
        }

        _items.Remove(itemToRemove);
    }

    public List<CartItem> GetItems()
    {
        return _items;
    }

    public decimal CalculateTotal()
    {
        decimal total = 0;
        foreach (CartItem item in _items)
        {
            total += item.Getsubtotal();
        }
        return total;
    }
}
internal class Order
{
    protected static int _nextidnumber = 1;
    protected int _id;
    private List<CartItem> _items;
    private decimal _totalprice;
    private string _paymentmethodused;

    public int Id {
        get { return _id; }
    }
    public List<CartItem> Items { 
        get { return _items; } 
    }
    public decimal TotalPrice {
        get { return _totalprice; }
    }
    public string PaymentMethodUsed { 
        get { return _paymentmethodused; }
    }

    public Order(List<CartItem> items, decimal totalPrice, string paymentMethodUsed)
    {
        if (items == null || items.Count == 0)
            throw new ArgumentException("order must contain at least one item");

        _id = _nextidnumber++;
        _items = items;
        _totalprice = totalPrice;
        _paymentmethodused = paymentMethodUsed;
    }
}
//polymorphism
internal interface IPayment
{
    bool Pay(decimal amount);
    string MethodName { get; }
}
internal class CreditCardPayment : IPayment
{
    private string _cardnumber;
    public string MethodName { 
        get { return "Credit Card"; }
    }

    public CreditCardPayment(string cardNumber)
    {
        if (string.IsNullOrEmpty(cardNumber))
            throw new ArgumentException("card number can't be empty");
        _cardnumber = cardNumber;
    }

    public bool Pay(decimal amount)
    {
        Console.WriteLine($"payed {amount} with credit card");
        return true;
    }
}

internal class PayPalPayment : IPayment
{
    private string _email;
    public string MethodName {
        get { return "PayPal"; }
    }

    public PayPalPayment(string email)
    {
        if (string.IsNullOrEmpty(email))
            throw new ArgumentException("email can't be empty");
        _email = email;
    }

    public bool Pay(decimal amount)
    {
        Console.WriteLine($"payed {amount} with PayPal account");
        return true;
    }
}

internal class CashPayment : IPayment
{
    public string MethodName { 
        get { return "Cash"; } 
    }
    public bool Pay(decimal amount)
    {
        Console.WriteLine($"payed {amount} in cash");
        return true;
    }
}
internal class OrderService
{
    public Order ProcessOrder(Cart cart, IPayment paymentmethod)
    {
        if (cart == null || cart.GetItems().Count == 0)
        {
            Console.WriteLine("cannot checkout\ncart is empty");
            return null;
        }

        decimal total = cart.CalculateTotal();

        bool paymentSuccess = paymentmethod.Pay(total);

        if (!paymentSuccess)
        {
            Console.WriteLine("payment failed\norder not created");
            return null;
        }

        Order order = new Order(cart.GetItems(), total, paymentmethod.MethodName);

        SendNotification(order);

        return order;
    }

    private void SendNotification(Order order)
    {
        Console.WriteLine($"order {order.Id} is placed successfully\ntotal is: {order.TotalPrice}");
    }
}
internal class Program
{
    static void Main(string[] args)
    {
        UserManager userManager = new UserManager();
        ProductManager productManager = new ProductManager();
        OrderService orderService = new OrderService();

        Console.WriteLine("1.Register");
        Console.WriteLine("2.Login");
        string choice = Console.ReadLine();

        if (choice == "1")
        {
            Console.WriteLine("Register as: 1. Customer  2. Admin");
            string rolechoice = Console.ReadLine();

            Console.WriteLine("enter your name");
            string name = Console.ReadLine();

            Console.WriteLine("enter your password");
            string password = Console.ReadLine();

            if (rolechoice == "1")
            {
                Customer c = userManager.RegisterCustomer(name, password);
                Console.WriteLine($"registered\nyour id is {c.Id}");
            }
            else if (rolechoice == "2")
            {
                Admin a = userManager.RegisterAdmin(name, password);
                Console.WriteLine($"registered\nyour id is {a.Id}");
            }
        }
        else if (choice == "2")
        {
            Console.WriteLine("enter your id");
            string idInput = Console.ReadLine();

            if (!int.TryParse(idInput, out int id))
            {
                Console.WriteLine("invalid id format");
                return;
            }

            Console.WriteLine("enter your name");
            string name = Console.ReadLine();

            Console.WriteLine("enter your password");
            string password = Console.ReadLine();

            User loggedInUser = userManager.Login(id, name, password);

            if (loggedInUser == null)
            {
                return; 
            }
            //downcasting
            if (loggedInUser is Admin)
            {
                RunAdminMenu((Admin)loggedInUser, productManager);
            }
            else if (loggedInUser is Customer)
            {
                RunCustomerMenu((Customer)loggedInUser, productManager, orderService);
            }
        }
    }
    static void RunAdminMenu(Admin admin, ProductManager productManager)
    {
        while (true)
        {
            Console.WriteLine("1. Add Product  2. Update Product  3. Delete Product  4. Logout");
            string choice = Console.ReadLine();

            if (choice == "1")
            {
                Console.WriteLine("product name:");
                string name = Console.ReadLine();

                Console.WriteLine("price:");
                decimal.TryParse(Console.ReadLine(), out decimal price);

                Console.WriteLine("stock quantity:");
                int.TryParse(Console.ReadLine(), out int stock);

                Product p = productManager.AddProduct(name, price, stock);
                Console.WriteLine($"added product {p.Id}");
            }
            else if (choice == "2")
            {
                Console.WriteLine("product id to update:");
                int.TryParse(Console.ReadLine(), out int id);

                Console.WriteLine("new name:");
                string name = Console.ReadLine();

                Console.WriteLine("new price:");
                decimal.TryParse(Console.ReadLine(), out decimal price);

                Console.WriteLine("new stock:");
                int.TryParse(Console.ReadLine(), out int stock);

                productManager.UpdateProduct(id, name, price, stock);
            }
            else if (choice == "3")
            {
                Console.WriteLine("product id to delete:");
                int.TryParse(Console.ReadLine(), out int id);
                productManager.DeleteProduct(id);
            }
            else if (choice == "4")
            {
                return; 
            }
        }
    }
    static void RunCustomerMenu(Customer customer, ProductManager productManager, OrderService orderService)
    {
        Cart cart = new Cart();

        while (true)
        {
            Console.WriteLine("1. View Products  2. Add to Cart  3. View Cart  4. Checkout  5. Logout");
            string choice = Console.ReadLine();

            if (choice == "1")
            {
                List<Product> products = productManager.GetAllProducts();
                for (int i = 0; i < products.Count; i++)
                {
                    Product p = products[i];
                    Console.WriteLine($"{p.Id}: {p.Name} - {p.Price} (stock: {p.StockQuantity})");
                }
            }
            else if (choice == "2")
            {
                Console.WriteLine("product id");
                int.TryParse(Console.ReadLine(), out int id);

                Console.WriteLine("quantity");
                int.TryParse(Console.ReadLine(), out int qty);

                Product p = productManager.GetProduct(id);
                if (p == null)
                {
                    Console.WriteLine("product not found");
                    continue;
                }

                cart.AddProduct(p, qty);
            }
            else if (choice == "3")
            {
                List<CartItem> items = cart.GetItems();

                for (int i = 0; i < items.Count; i++)
                {
                    CartItem item = items[i];
                    Console.WriteLine($"{item.Product.Name} x{item.Quantity} = {item.Getsubtotal()}");
                }

                Console.WriteLine($"Total: {cart.CalculateTotal()}");
            }
            else if (choice == "4")
            {
                Console.WriteLine("Payment method: 1. Credit Card  2. PayPal  3. Cash");
                string payChoice = Console.ReadLine();

                IPayment payment;

                if (payChoice == "1")
                {
                    Console.WriteLine("card number");
                    payment = new CreditCardPayment(Console.ReadLine());
                }
                else if (payChoice == "2")
                {
                    Console.WriteLine("email");
                    payment = new PayPalPayment(Console.ReadLine());
                }
                else
                {
                    payment = new CashPayment();
                }

                Order order = orderService.ProcessOrder(cart, payment);

                if (order != null)
                {
                    // fresh empty cart after successful checkout
                        cart = new Cart(); 
                }
            }
            else if (choice == "5")
            {
                return;
            }
        }
    }
}
           
            

