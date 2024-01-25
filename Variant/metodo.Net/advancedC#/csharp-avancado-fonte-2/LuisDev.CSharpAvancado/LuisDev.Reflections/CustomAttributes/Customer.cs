namespace LuisDev.Reflections.CustomAttributes
{
    internal class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [HiddenValue]
        public string Password { get; set; }
        [HiddenValue, EqualValue(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
