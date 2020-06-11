using System;

namespace PrintedItem
{
    public class Book
    { 
        [Updatable]
        public string Name { get; set; }
        [Updatable]
        public string Year { get; set; }
        [Updatable]
        public string Publisher { get; set; }
        [Updatable]
        public string Author { get; set; }
        [Updatable]
        public string Style { get; set; }       

        public Book(string name, string year, string publisher, string author, string style)
        {            
            Name = name;
            Year = year;
            Publisher = publisher;
            Author = author;
            Style = style;
        }

        public override string ToString()
        {
            return $"{Name}\n{Year}\n{Publisher}\n{Author}\n{Style}\n";
        }

        public override bool Equals(object obj)
        {
            return obj is Book book &&                  
                   Name == book.Name &&
                   Year == book.Year &&
                   Publisher == book.Publisher &&
                   Author == book.Author &&
                   Style == book.Style;
        }
    }

    public class Updatable : Attribute
    {
    }
}
