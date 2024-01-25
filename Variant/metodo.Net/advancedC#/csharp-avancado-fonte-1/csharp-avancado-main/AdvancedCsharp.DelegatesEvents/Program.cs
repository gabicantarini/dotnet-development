// See https://aka.ms/new-console-template for more information
using System.Linq.Expressions;
using AdvancedCsharp.DelegatesEvents;
using AdvancedCsharp.Shared;

Console.WriteLine("Hello, World!");

BinaryOperation multiply = delegate (int x, int y)
{
    return x * y;
};

BinaryOperation sum = delegate (int x, int y)
{
    return x + y;
};

int result = multiply(3, 4);
int sumResult = sum(3, 4);

Action<string> printMessage = message => Console.WriteLine(message);

printMessage("Hello, World!");

Func<int, int> square = x => x * x;

int squaredValue = square(4);

Func<int, int, int> addNumbers = (a, b) => a + b;

int addNumbersResult = addNumbers(5, 7);

//List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6 };
//Predicate<int> isEven = number => number % 2 == 0;

var customers = new List<Customer>
{
    new Customer(1, "Luis"),
    new Customer(2, "Cassiano"),
    new Customer(3, "Jesse")
};

customers.Where(c => c.Name.Contains("Luis"));
customers.Where((c, i) => c.Name == "LuisDev");

// Filtragem
SimpleFilter(c => c.Id == 1);

void SimpleFilter(Func<Customer, bool> filter)
{
    var customer2 = customers.Single(filter);

    Console.WriteLine(customer2.Name);
}

customers.ForEach(c => Console.WriteLine(c.Name));

customers.Select(c => c.Name).ToList().ForEach(Console.WriteLine);

var dates = customers.Select(c => c.CreatedAt);

dates.Where(d => DateIsInFutureBool(d));
dates.Where(DateIsInFutureBool);
var names = customers.Select(c => c.Name);

// var nonNullNames = names.Where(n => string.IsNullOrWhiteSpace(n));
var nonNullNames = names.Where(string.IsNullOrWhiteSpace);

//customers.Select(c => c.CreatedAt).ToList().ForEach(DateIsInFuture);


var customersContainSLetter = FilterPredicate(customers, c => c.Name.Contains('s'));
var customerContainOLeter = Filter(customers, c => c.Name.Contains('o'));


IEnumerable<Customer> Filter(IEnumerable<Customer> customers, Func<Customer, bool> predicate)
{
    return customers.Where(predicate).ToList();
}

IEnumerable<Customer> FilterPredicate(List<Customer> customers, Predicate<Customer> predicate)
{
    return customers.FindAll(predicate);
}

var instrumentation = new InstrumentationService();

instrumentation.Measure(DoWork);

instrumentation.Measure(DoWorkWithParameter, 4);

instrumentation.Measure(CalculateVowels, "luisdev");

instrumentation.Measure(() => DateIsInFuture(new DateTime(1992, 01, 01)));
instrumentation.Measure(DateIsInFuture, new DateTime(1992, 01, 01));


// Events
var customerManager = new CustomerManager();
var notificationService = new NotificationService();
var integrationService = new IntegrationService();

// Subscribe to the event
customerManager.CustomerCreated += notificationService.OnCustomerCreated;
customerManager.CustomerCreated += integrationService.OnCustomerCreated;

// Create a customer
customerManager.CreateCustomer("John Doe");

int DoWork()
{
    return 123;
}

int DoWorkWithParameter(int number)
{
    Thread.Sleep(1000);

    return number * number;
}

int CalculateVowels(string word)
{
    if (word == null) return 0;

    char[] vowels = { 'a', 'e', 'i', 'o', 'u', 'A', 'E', 'I', 'O', 'U' };

    return word.Count(c => vowels.Contains(c));
}

void DateIsInFuture(DateTime date)
{
    var isFuture = date > DateTime.Now;

    if (isFuture)
        Console.WriteLine($"Date {date} is in future.");
    else
        Console.WriteLine($"Date {date} is in past");
}

bool DateIsInFutureBool(DateTime date)
{
    return date > DateTime.Now;
}

Console.Read();