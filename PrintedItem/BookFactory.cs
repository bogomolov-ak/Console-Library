using System.Collections.Generic;

namespace PrintedItem
{
    public class BookFactory
    {
        public static int BookId { get; set; }
        static BookFactory()
        {
            BookId = 0;
        }
        public static Book CreateBook(Dictionary<string, string> arguments)
        {
            ++BookId;
            return new Book(arguments["name"], arguments["author"], arguments["style"], arguments["year"], arguments["publisher"]);             
        }
    }
}
