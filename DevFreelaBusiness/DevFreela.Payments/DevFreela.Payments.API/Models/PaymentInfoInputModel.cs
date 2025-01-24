namespace DevFreela.Payments.API.Models
{
    public class PaymentInfoInputModel
    {
        public PaymentInfoInputModel(int idProject, string creditCardNumber, string cvv, string expiresAt, string fullName, decimal amount)
        {
            IdProject = idProject;
            CreditCardNumber = creditCardNumber;
            Cvv = cvv;
            ExpiresAt = expiresAt;
            FullName = fullName;
            Amount = amount;
        }

        public int IdProject { get; set; }
        public string CreditCardNumber { get; set; }
        public string Cvv { get;}
        public string ExpiresAt { get; set; }   
        public string FullName { get; set; }
        public  decimal  Amount { get; set; }
    }
}
