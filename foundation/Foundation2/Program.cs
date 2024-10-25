using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        // Create two products
        Product product1 = new Product("Laptop", "P123", 800.00, 1);
        Product product2 = new Product("Mouse", "P456", 25.00, 2);
        Product product3 = new Product("Keyboard", "P789", 50.00, 1);
        
        // Create customer addresses
        Address address1 = new Address("123 Main St", "New York", "NY", "USA");
        Address address2 = new Address("456 Elm St", "Toronto", "ON", "Canada");
        
        // Create customers
        Customer customer1 = new Customer("John Doe", address1);
        Customer customer2 = new Customer("Jane Smith", address2);
        
        // Create orders
        Order order1 = new Order(customer1);
        order1.AddProduct(product1);
        order1.AddProduct(product2);
        
        Order order2 = new Order(customer2);
        order2.AddProduct(product3);
        
        // Display information for each order
        Console.WriteLine("Order 1 Details:");
        Console.WriteLine("Packing Label:");
        Console.WriteLine(order1.GetPackingLabel());
        Console.WriteLine("Shipping Label:");
        Console.WriteLine(order1.GetShippingLabel());
        Console.WriteLine($"Total Price: ${order1.GetTotalPrice()}");

        Console.WriteLine("\nOrder 2 Details:");
        Console.WriteLine("Packing Label:");
        Console.WriteLine(order2.GetPackingLabel());
        Console.WriteLine("Shipping Label:");
        Console.WriteLine(order2.GetShippingLabel());
        Console.WriteLine($"Total Price: ${order2.GetTotalPrice()}");
    }
}

// Class for Product
class Product
{
    // Private member variables
    private string _name;
    private string _productId;
    private double _price;
    private int _quantity;

    // Constructor for Product class
    public Product(string name, string productId, double price, int quantity)
    {
        _name = name;
        _productId = productId;
        _price = price;
        _quantity = quantity;
    }

    // Method to calculate total cost of the product
    public double GetTotalCost()
    {
        return _price * _quantity;
    }

    // Getter methods for product details
    public string GetName()
    {
        return _name;
    }

    public string GetProductId()
    {
        return _productId;
    }
}

// Class for Customer
class Customer
{
    // Private member variables
    private string _name;
    private Address _address;

    // Constructor for Customer class
    public Customer(string name, Address address)
    {
        _name = name;
        _address = address;
    }

    // Method to check if the customer lives in the USA
    public bool LivesInUSA()
    {
        return _address.IsInUSA();
    }

    // Getter methods for customer details
    public string GetName()
    {
        return _name;
    }

    public Address GetAddress()
    {
        return _address;
    }
}

// Class for Address
class Address
{
    // Private member variables
    private string _streetAddress;
    private string _city;
    private string _state;
    private string _country;

    // Constructor for Address class
    public Address(string streetAddress, string city, string state, string country)
    {
        _streetAddress = streetAddress;
        _city = city;
        _state = state;
        _country = country;
    }

    // Method to check if the address is in the USA
    public bool IsInUSA()
    {
        return _country == "USA";
    }

    // Override ToString() method to return the address in formatted string
    public override string ToString()
    {
        return $"{_streetAddress}\n{_city}, {_state}\n{_country}";
    }
}

// Class for Order
class Order
{
    // Private member variables
    private List<Product> _products;
    private Customer _customer;
    private const double UsaShippingCost = 5.00;
    private const double InternationalShippingCost = 35.00;

    // Constructor for Order class
    public Order(Customer customer)
    {
        _customer = customer;
        _products = new List<Product>();
    }

    // Method to add a product to the order
    public void AddProduct(Product product)
    {
        _products.Add(product);
    }

    // Method to calculate total price of the order
    public double GetTotalPrice()
    {
        double total = 0;

        // Calculate total price of products
        foreach (var product in _products)
        {
            total += product.GetTotalCost();
        }

        // Add shipping cost
        if (_customer.LivesInUSA())
        {
            total += UsaShippingCost;
        }
        else
        {
            total += InternationalShippingCost;
        }

        return total;
    }

    // Method to get the packing label for the order
    public string GetPackingLabel()
    {
        string label = "";
        foreach (var product in _products)
        {
            label += $"{product.GetName()} (ID: {product.GetProductId()})\n";
        }
        return label;
    }

    // Method to get the shipping label for the order
    public string GetShippingLabel()
    {
        return $"{_customer.GetName()}\n{_customer.GetAddress()}";
    }
}
