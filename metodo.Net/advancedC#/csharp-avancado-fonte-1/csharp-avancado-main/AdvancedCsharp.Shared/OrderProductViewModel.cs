namespace AdvancedCsharp.Shared
{
    public class OrderProductViewModel
	{
		public OrderProductViewModel()
		{

		}

        public OrderProductViewModel(string product, int quantity, decimal subTotal)
        {
            Product = product;
            Quantity = quantity;
            SubTotal = subTotal;
        }

        public string Product { get; set; }
		public int Quantity { get; set; }
		public decimal SubTotal { get; set; }
	}
}

