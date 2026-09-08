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
            //trygetvalue over contains key so that actual object is handed and u.Name/u.Password be used
            if (!_users.TryGetValue(id, out User u))
                {
                    Console.WriteLine("login failed.\nid doesn't exist\n");
                    return null;
                }
            else
                { 
                 if (u.Name != name || u.Password != password)
                    {
                        Console.WriteLine("login failed.\nincorrect name or password\n");
                        return null;
                    }
                 else
                    {
                        Console.WriteLine("successful login");
                        return u;
                    }
                 }
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
            Console.WriteLine("update failed, product id doesn't exist");
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
            Console.WriteLine("delete failed, product id doesn't exist");
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
            Console.WriteLine("cannot checkout: cart is empty");
            return null;
        }

        decimal total = cart.CalculateTotal();

        bool paymentSuccess = paymentmethod.Pay(total);

        if (!paymentSuccess)
        {
            Console.WriteLine("payment failed, order not created");
            return null;
        }

        Order order = new Order(cart.GetItems(), total, paymentmethod.MethodName);

        SendNotification(order);

        return order;
    }

    private void SendNotification(Order order)
    {
        Console.WriteLine($"order {order.Id} is placed successfully, total is: {order.TotalPrice}");
    }
}
internal class Program
        {
            static void Main(string[] args)
            {
                UserManager manager = new UserManager();

                while (true)
                {
                    Console.WriteLine("enter your id");
                    string idInput = Console.ReadLine();

                    if (!int.TryParse(idInput, out int id))
                    {
                        Console.WriteLine("invalid id format");
                        continue;
                    }

                    Console.WriteLine("enter your name");
                    string name = Console.ReadLine();

                    Console.WriteLine("enter your password");
                    string password = Console.ReadLine();

                }
            }
        } 

