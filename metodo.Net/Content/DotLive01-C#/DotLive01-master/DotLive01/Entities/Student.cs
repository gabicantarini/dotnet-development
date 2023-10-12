using System.Diagnostics.CodeAnalysis;

namespace DotLive01.Entities
{
    public class Student
    {
        public Student()
        {
            FirstName = "";
            Status = StudentStatus.Active;
        }

        [SetsRequiredMembers]
        public Student(string firstName, string? lastName, decimal? score, Address? address)
        {
            FirstName = firstName;
            LastName = lastName;
            Score = score;
            Address = address;

            Status = StudentStatus.Active;
        }

        public required string FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public decimal? Score { get; set; }
        public Address? Address { get; set; }
        public StudentStatus Status { get; set; }

        public void Update((decimal? Score, Address? Address) info)
        {
            Score = info.Score;
            Address = info.Address;
        }

        public void SetLastName(string lastName)
        {

        }

        public (string Name, decimal Score) GetInfo()
        {
            return ($"{FirstName} {LastName}", Score ?? 0);
        }

        public (string, decimal) GetInfoNonNamed()
        {
            return ($"{FirstName} {LastName}", Score ?? 0);
        }

        public void PrintInfo()
        {
            var fullName = $"Name: {FirstName} {LastName}";
            var score = $"Score: {Score?.ToString("0.00") ?? "N/A"}";
            var address = Address?.GetFullAddress() ?? "N/A";

            Console.WriteLine(fullName);
            Console.WriteLine(score);
            Console.WriteLine(address);

            string result1 = Score switch
            {
                >= 7 => "Aprovado",
                >= 3 and < 7 => "Recuperação",
                _ => "Reprovado"
            };

            string result2 = this switch
            {
                { Score: >= 7, Status: StudentStatus.Active } => "Aprovado",
                { Score: >= 3 and < 7, Status: StudentStatus.Active } => "Recuperação",
                _ => "Reprovado"
            };

            Console.WriteLine($"Result: {result1}");
            Console.WriteLine($"Result: {result2}");

            using var streamWriter = new StreamWriter("student_log.txt", true);

            streamWriter.WriteLine($"{fullName}, {score}, {address}");
        }
    }
}
