using System;
using System.Collections.Generic;
using System.Threading.Channels;

class Program
{
    static void Main()
    {
        //#region Arrays
        ArrayStructure();
        ListStructure();
        //#endregion Arrays

    }

    static void ArrayStructure()
    {
        #region Arrays
        int[] numberInt = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 }; //forma de inicializar um array
        var numbersCopy = new int[10]; //forma de inicializar e instanciar um array
        var number = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 }; //forma de inicializar e instanciar um array com valor declarado

        for (int i = 0; i < numbersCopy.Length; i++)
        {
            numbersCopy[i] = numberInt[i];
        }
        Console.WriteLine("Numbers Copy: " + string.Join(", ", numbersCopy)); //By using string.Join, you will convert the array elements into a comma-separated string, which can be concatenated to the other string for printing.
        Console.WriteLine("Numbers Int: " + string.Join(", ", numberInt));

        var numbersString = "1 2 3 4 5 6";
        var numbersStringSplit = numbersString.Split(' ');
        var numberStringConvert = Array.ConvertAll(numbersStringSplit, Convert.ToInt32);
        Console.Write("\n number String Convert: " + string.Join(" , ", numberStringConvert));
        Console.ReadKey();

        #endregion Arrays
    }

    static void ListStructure()
    {
        //Methods and Properties
        //Count: contador da lista
        //Add: add an element in a list
        //AddRange: add a collection in a list
        //Contains: boolean - verify if there are some element in a list
        //Sort: ordered a list
        //Reverse: inverse order
        //ForeEach: execute a block of code for an iten in a list
        //Remove: remove an element from the list
        //RemoveAll: remove many elements
        //Clear: clear elements in a list
        int[] numberInt = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };

        var list = new List<int> {0, 1, 2, 3, 4 }; //forma de inicializar uma lista
        var list2 = new List<int> (numberInt); //forma de inicializar uma lista

        
        list.Add(5);
        list.AddRange(new List<int> { 6, 7});
        list.AddRange(new int[] { 8, 9 });

        //Reverse List
        Console.WriteLine("Reverse List: ");
        list.Reverse();
        list.ForEach(l => Console.WriteLine(l)); //lambda expression

        //Ordered List
        Console.WriteLine("Ordered List: ");
        list.Sort();
        list.ForEach(l => Console.WriteLine(l)); //lambda expression

        list.Remove(4);
        list.RemoveAll(l => l > 5); //lambda expression to remove less than 5

        list.Clear();
    }


}