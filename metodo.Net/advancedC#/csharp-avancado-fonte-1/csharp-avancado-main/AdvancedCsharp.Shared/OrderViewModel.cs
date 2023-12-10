namespace AdvancedCsharp.Shared
{
    public class OrderViewModel
	{
        public OrderViewModel()
        {

        }

        public OrderViewModel(decimal total, List<OrderProductViewModel> products, DateTime orderCreated, Guid userId)
        {
            Total = total;
            Products = products;
            OrderCreated = orderCreated;
            UserId = userId;
        }

        public decimal Total { get; set; }
		public List<OrderProductViewModel> Products { get; set; }
		public DateTime OrderCreated { get; set; }
		public Guid UserId { get; set; }
	}
}

