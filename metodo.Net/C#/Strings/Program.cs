using System.Linq;

class Program
{
    static void Main()
    {
        StringStructure();
    }

    //Lenght: returns characters size
    //Empty: returns empty characters
    //ToLower: returns a copy of the string in lowercase
    //ToUpper: returns a copy of the string in uppercase
    //Split: gets a string with a separator and split it in an array of elements
    //Trim: removes white space
    //TrimEnd: removes white space in the end
    //TrimStart: removes white space in the start
    //IsNullOrWhiteSpace: its a boolean and returns if it is null or if it has white spaces
    //Replace: gets 2 carachteres and replace one by other

    //IndexOf (String) or (Char): returns the position/index of the first string or char 
    //LastIndexOf (String) or (Char): returns last the position/index string or char
    //StartsWith (String) or (Char): boolean - returns if starts with string or char or not
    //Substring (Int32) or (Int32, Int32): retuns the string from the indexOf position
    //Contains (String) or (Char): boolean.. returns its true or false

    static void StringStructure()
    {
        var sentence = "  Today its gonna be a GOOD DAY.  " + " Today I do NOT wanna stress me out. " + "  I want a car, a game and a ball.";

        Console.Write("Lenght: ");
        Console.WriteLine(sentence.Length);

        Console.Write("\nEmpty: ");
        Console.WriteLine(string.Empty);

        Console.Write("\nToLower: ");
        Console.WriteLine(sentence.ToLower());

        Console.Write("\nToUpper: ");
        Console.WriteLine(sentence.ToUpper());

        Console.Write("\nSplit: ");
        Console.WriteLine(sentence.Split('.'));

        Console.Write("\nTrim: ");
        Console.WriteLine(sentence.Trim());

        Console.Write("\nTrimEnd: ");
        Console.WriteLine(sentence.TrimEnd());

        Console.Write("\nTrimStart: ");
        Console.WriteLine(sentence.TrimStart());

        Console.Write("\nIsNullOrWhiteSpace: ");
        Console.WriteLine(string.IsNullOrWhiteSpace(sentence));

        Console.Write("\nReplace: ");
        Console.WriteLine(sentence.Replace('.', '!'));

        Console.Write("\nIndexOf: ");
        Console.WriteLine(sentence.IndexOf("Today"));

        Console.Write("\nLastIndexOf: ");
        Console.WriteLine(sentence.LastIndexOf("Today"));

        Console.Write("\nStartsWith: ");
        Console.WriteLine(sentence.StartsWith("Go"));

        Console.Write("\nIndexOf: ");
        var indexOfToday = sentence.IndexOf("Today");
        Console.WriteLine(indexOfToday);

        Console.Write("\nSubstring: ");
        var substringOfToday = sentence.Substring(indexOfToday, 5);
        Console.WriteLine(substringOfToday);

        Console.Write("\nContains: ");
        var containsWanna = sentence.Contains("wanna", StringComparison.OrdinalIgnoreCase);
        Console.WriteLine(containsWanna);

        Console.Write("\nContains Exact word: ");
        var containsExact = sentence.Contains("wanna");
        Console.WriteLine(containsExact);

        Console.Write("\nContains Ruim, sem retorno: ");
        var containsBad = sentence.Contains("palco");
        Console.WriteLine(containsBad);
    }
}