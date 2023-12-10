using System.Reflection;

namespace LuisDev.Reflections.OnlyReflection
{
    internal class OnlyReflectionService
    {
        public void Example1()
        {
            Person person = new() { Name = "João", Age = 30 };
            Console.WriteLine($"Type: Person");
            Console.WriteLine($"name: {person.Name}");
            Console.WriteLine($"age: {person.Age}");
         
        }

        public void Example2_Audit()
        {
            Person person = new() { Name = "João", Age = 30 };

            Type personType = person.GetType();

            PropertyInfo[] properties = personType.GetProperties();

            foreach (var property in properties)
            {
                string propertyName = property.Name;
                object propertyValue = property.GetValue(person)!;

                Console.WriteLine($"Name: {propertyName}");
                Console.WriteLine($"Type: {property.PropertyType}");
                Console.WriteLine($"Value: {propertyValue}");
                Console.WriteLine();
            }
        }

        public void Example3_AuditGeneric()
        {
            Person person = new() { Name = "João", Age = 30 };
            Product product = new () { Id = 1, Name = "Car", Price = 10000, Description = "a car very fast" };
            GenerateGenericAudit(person);
            GenerateGenericAudit(product);
        }
        
        private void GenerateGenericAudit(object objectToAudit)
        {

            var type = objectToAudit.GetType();
            var properties = type.GetProperties();
            Console.WriteLine($"------LOG TO:{type.Name}-----------");

            foreach (var property in properties)
            {
                var propertyName = property.Name;
                var propertyValue = property.GetValue(objectToAudit);
                Console.WriteLine($"property type: {property.PropertyType}");
                Console.WriteLine($"property name: {propertyName}");
                Console.WriteLine($"property value: {propertyValue}");
                Console.WriteLine($"-----------------------------------");
            }
            Console.WriteLine($"\n ");

        }
    }
}
