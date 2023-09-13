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

   
    static void StringStructure()
    {
        var sentence = "  Today its gonna be a GOOD DAY.  " + "I do NOT wanna stress me out. " + "  I want a car, a game and a ball.";

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

    }
}