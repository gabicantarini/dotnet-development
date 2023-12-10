using System;
namespace AdvancedCsharp.Shared
{
    public class UserViewModel
	{
		public UserViewModel()
		{
		}

        public string Id { get; set; }
		public string Name { get; set; }
		public string Avatar { get; set; }
		public DateTime CreatedAt { get; set; }
	}
}

