// See https://aka.ms/new-console-template for more information
using DotLive01.Entities;


var student1 = new Student("Luis", "Dev", 7.5m, new Address("Avenida ABC", "São Paulo", "SP", "12345"));
var student2 = new Student { FirstName = "Dev", LastName = "Luis", Score = 5.5m, Address = new Address("Avenida ABC", "São Paulo", "SP", "12345") };

var students = new List<Student> { student1, student2 };

Console.WriteLine($"Equal Addresses? {student1.Address == student2.Address}");

// Tuples
student1.Update((3, new Address("Rua ABC", "São Paulo", "SP", "12345-678")));

var info = student1.GetInfo();
var (Nome, Score) = student1.GetInfo();
(string name, decimal score) = student1.GetInfo();
var nonNamedInfo = student1.GetInfoNonNamed();

Console.WriteLine($"Name: {info.Name}, Score: {info.Score}");
Console.WriteLine($"Name: {Nome}, Score: {Score}");
Console.WriteLine($"Name: {name}, Score: {score}");
Console.WriteLine($"Name: {nonNamedInfo.Item1}, Score: {nonNamedInfo.Item2}");

List<(string firstName, decimal? score)> studentTuples = students
    .Select(s => (s.FirstName, s.Score))
    .ToList();

foreach (var (nome, nota) in studentTuples)
{
    Console.WriteLine($"First Name: {nome}, Score: {nota}");
}

// Records

// Null Reference Types
student1.SetLastName(student2.LastName);

var firstName1 = student1.FirstName.Length;
var lastName1 = student1.LastName.Length;
var lastName2 = student2.LastName.Length;

Student? luis = students.SingleOrDefault(s => s.FirstName == "Luis");

Console.WriteLine(luis.FirstName);

student1.PrintInfo();
