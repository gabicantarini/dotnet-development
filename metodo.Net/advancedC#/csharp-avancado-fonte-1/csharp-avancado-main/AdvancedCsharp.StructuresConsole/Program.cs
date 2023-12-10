using System;

var book1 = new Book(
    "Lord of The Rings",
    Genre.Fantasy,
    new ShelfLocation { Row = 1, Column = 1 },
    new BookDimensions(5f, 7.5f, 1.2f)
);

var book2 = new Book(
    "Segredos da Mente Milionária",
    Genre.Fantasy,
    new ShelfLocation { Row = 3, Column = 2 },
    new BookDimensions(5f, 7.5f, 1.2f)
);

var book3 = new Book(
    "Clean Code",
    Genre.Fantasy,
    new ShelfLocation { Row = 2, Column = 2 },
    new BookDimensions(5f, 7.5f, 1.2f)
);

// DotLive 1: C# Moderno

Console.WriteLine(book2.Location.Equals(book3.Location));
Console.WriteLine(book2.Dimensions.Equals(book3.Dimensions));

Console.WriteLine(book1);
Console.WriteLine(book2);
Console.WriteLine(book3);

Console.ReadLine();

// 1. Enums
enum Genre
{
    Fiction = 1,
    NonFiction = 2,
    Biography = 3,
    Science = 4,
    Fantasy = 5
}

// 2. Record
record BookDimensions(float Width, float Height, float Thickness)
{
}

// 3. Struct
struct ShelfLocation : IEquatable<ShelfLocation>
{
    public int Row { get; set; }
    public int Column { get; set; }

    public bool Equals(ShelfLocation other)
    {
        return Row == other.Row && Column == other.Column;
    }
}

// 4. Class
class Book
{
    public Book(string title, Genre genre, ShelfLocation location, BookDimensions dimensions)
    {
        Title = title;
        Genre = genre;
        Location = location;
        Dimensions = dimensions;
    }

    public string Title { get; set; }
    public Genre Genre { get; set; }
    public ShelfLocation Location { get; set; }
    public BookDimensions Dimensions { get; set; }
}
