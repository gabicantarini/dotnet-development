using System;
using DocumentFormat.OpenXml.Office2010.Excel;
using System.Reflection.Metadata;

namespace Linq
{
    class Program
    {
        static void Main()
        {
            #region LINQ
            var students = new List<Student>
        {
            new Student(1, "Maria", "123456", 100),
            new Student(2, "João", "123456", 35),
            new Student(3, "Helena", "123456", 85),
            new Student(4, "Cecília", "123456", 70),
            new Student(5, "Leandro", "123456", 75),
        };

            var any = students.Any();
            var any100 = students.Any(s => s.Grade == 100);

            var singleOrDefault = students.SingleOrDefault();
            var single = students.Single(s => s.Id == 1);

            var first = students.First(s => s.FullName == "Cecília");
            var firstOrDefault = students.FirstOrDefault(s => s.Grade == 0);

            var orderByGrade = students.OrderBy(s => s.Grade);
            var orderByGradeDescending = students.OrderByDescending(s => s.Grade);

            var approvedStudents = students.Where(s => s.Grade >= 70);

            var grades = students.Select(s => s.Grade);

            var phoneNumbers = students.SelectMany(s => s.PhoneNumbers);

            var sum = students.Sum(s => s.Grade);
            var min = students.Min(s => s.Grade);
            var max = students.Max(s => s.Grade);
            var count = students.Count;

            #endregion LINQ
            Console.ReadKey();
        }


    }

    //LINQ: Language Integrated-Query
    //Methods
    //Any: verify if there are any elements with 
    //Single: metodo para busca
    //SingleOrDefault: se for single or default retorna valor + USADO
    //First: obtem primeiro elemento que atenda a condição
    //FirstOrDefault: obtem primeiro elemento ou o default que atenda a condição + USADO
    //OrderBy:permite que identifique o campo que quer ordenar
    //OrderByDescending:
    //Where: recebe uma condição e retorna os elementos da condição que atendam essa condição + USADO
    //Select: serve para fazer projeções de dados
    //SelectMany: serve para fazer projeções de dados e junta em uma coleção
    //Sum: soma baseada numa propriedade
    //Min: seleciona o valor minimo baseado numa propriedade
    //Max: seleciona o valor máximo baseado numa propriedade
    //Count:





    public class Student()
    {

        public Student(int id, string fullName, string document, int grade)
        {
            Id = id;
            FullName = fullName;
            Document = document;
            Grade = grade;

            PhoneNumbers = new List<string> { "236956245", "236956245", "236956245" };

        }


        public int Id { get; set; }
        public string FullName { get; set; }
        public string Document { get; set; }
        public int Grade { get; set; }
        public List<string> PhoneNumbers { get; set; }





    }

}