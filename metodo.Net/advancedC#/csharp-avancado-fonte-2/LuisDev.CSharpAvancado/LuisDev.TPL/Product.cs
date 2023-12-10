namespace LuisDev.TPL
{
    internal class Product
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public double Price { get; set; }

        public bool IsValid()
        {
            Task.Delay(200).Wait();
            return true;
        }
    }
}
