using System.Reflection;

namespace LuisDev.Reflections.CustomAttributes
{
    internal class CustomAttributesService
    {
        public void Example1()
        {
            var costumer = new Customer
            {
                Id = 1,
                Name = "Luis",
                Password = "123456"
            };
            GenerateGenericAudit(costumer);
        }
        private void GenerateGenericAudit(object objectToAudit)
        {

            var type = objectToAudit.GetType();
            var properties = type.GetProperties();
            Console.WriteLine($"------LOG TO:{type.Name}-----------");

            foreach (var property in properties)
            {
                var propertyName = property.Name;
                var shouldHideValue = property.GetCustomAttribute<HiddenValue>() != null;
                var propertyValue = shouldHideValue ? "****" : property.GetValue(objectToAudit);

                Console.WriteLine($"property type: {property.PropertyType}");
                Console.WriteLine($"property name: {propertyName}");
                Console.WriteLine($"property value: {propertyValue}");
                Console.WriteLine($"-----------------------------------");

            }
            Console.WriteLine($"\n \n \n");

        }
        public void Example2()
        {
            var costumer = new Customer
            {
                Id = 1,
                Name = "Luis",
                Password = "123456",
                ConfirmPassword = "1234567"
            };
            GenerateGenericAuditV2(costumer);
        }
        private void GenerateGenericAuditV2(object objectToAudit)
        {

            var type = objectToAudit.GetType();
            var properties = type.GetProperties();
            Console.WriteLine($"------LOG TO:{type.Name}-----------");

            foreach (var property in properties)
            {
                var propertyName = property.Name;

                var shouldHideValue = property.GetCustomAttribute<HiddenValue>() != null;
                var compareValueAttribute = property.GetCustomAttribute<EqualValue>();
                
                var shouldCompareValue = compareValueAttribute != null;
                var propertyValue = shouldHideValue ? "****" : property.GetValue(objectToAudit);

                Console.WriteLine($"property type: {property.PropertyType}");
                Console.WriteLine($"property name: {propertyName}");
                Console.WriteLine($"property value: {propertyValue}");

                if (shouldCompareValue)
                {
                    var areEqual = compareValueAttribute!.ArePropertiesEqual(objectToAudit, property);
                    Console.WriteLine($"the value is equal to {compareValueAttribute.PropertyToComapare}: {areEqual}");
                }
                Console.WriteLine($"-----------------------------------");

            }
            Console.WriteLine($"\n \n \n");

        }
    }
}
