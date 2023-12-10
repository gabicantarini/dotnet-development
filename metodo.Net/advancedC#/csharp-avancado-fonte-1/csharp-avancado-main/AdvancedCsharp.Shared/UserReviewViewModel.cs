namespace AdvancedCsharp.Shared
{
    public class UserReviewViewModel
	{
        public UserReviewViewModel()
        {

        }

        public UserReviewViewModel(Guid userId, string text, int score, string product)
        {
            UserId = userId;
            Text = text;
            Score = score;
            Product = product;
        }

        public Guid UserId { get; set; }
		public string Text { get; set; }
		public int Score { get; set; }
		public string Product { get; set; }
	}
}

