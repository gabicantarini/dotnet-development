// See https://aka.ms/new-console-template for more information
using System.Reflection;
using Newtonsoft.Json;

Console.WriteLine("Hello, World!");


string json = @"{
            ""specifications"": [
                {
                    ""Field"": ""my_name"",
                    ""Destination"": ""Name""
                },
                {
                    ""Field"": ""my_age"",
                    ""Destination"": ""Age""
                }
            ],
            ""values"": [
                {
                    ""my_name"": ""Luis"",
                    ""my_age"": 30
                }
            ]
        }";

var data = JsonConvert.DeserializeObject<DataObject>(json);

List<Person> persons = new List<Person>();

PropertyInfo[] personProperties = typeof(Person).GetProperties();

Dictionary<string, PropertyInfo> specificationPropertyCache = new Dictionary<string, PropertyInfo>();

foreach (var spec in data.Specifications)
{
    var targetProperty = personProperties.FirstOrDefault(prop => prop.Name == spec.Destination);

    if (targetProperty != null)
    {
        specificationPropertyCache[spec.Field] = targetProperty;
    }
}

foreach (var value in data.Values)
{
    Person person = new Person();

    foreach (var spec in data.Specifications)
    {
        if (value.TryGetValue(spec.Field, out object fieldValue) && specificationPropertyCache.TryGetValue(spec.Field, out PropertyInfo targetProperty))
        {
            var convertedValue = Convert.ChangeType(fieldValue, targetProperty.PropertyType);
            targetProperty.SetValue(person, convertedValue);
        }
    }

    MethodInfo sayHelloMethod = typeof(Person).GetMethod("SayHello");
    sayHelloMethod.Invoke(person, null);

    MethodInfo sayHelloWithParamMethod = typeof(Person).GetMethod("SayHelloWithParam");
    sayHelloWithParamMethod.Invoke(person, new object[] { "Luis" });

    persons.Add(person);
}

Console.Read();

public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }

    public void SayHello()
    {
        Console.WriteLine($"SayHello(): Hello, I'm {Name}, I'm {Age} years old!");
    }

    public void SayHelloWithParam(string name)
    {
        Console.WriteLine($"SayHelloWithParam(string name): Hello, I'm {name}!");
    }
}

public class FieldSpecification
{
    public string Field { get; set; }
    public string Destination { get; set; }
}

public class DataObject
{
    public List<FieldSpecification> Specifications { get; set; }
    public List<Dictionary<string, object>> Values { get; set; }
}