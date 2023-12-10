using System.Reflection;

namespace LuisDev.Reflections.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    internal class EqualValue : Attribute
    {
        public EqualValue(string propertyToComapare)
        {
            PropertyToComapare = propertyToComapare;
        }

        public string PropertyToComapare { get; }

        public bool ArePropertiesEqual(object obj, PropertyInfo currentProperty)
        {
            PropertyInfo propertyToCompareInfo = obj.GetType().GetProperty(PropertyToComapare)!;
            if (propertyToCompareInfo == null)
            {
                return false;
            }
            var propertyValue = currentProperty.GetValue(obj);
            var propertyToCompareValue = propertyToCompareInfo.GetValue(obj);
            return propertyValue != null && propertyValue.Equals(propertyToCompareValue);
        }
    }
}
