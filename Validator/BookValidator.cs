using System.Collections.Generic;

namespace Validator
{
    public class BookValidator
    {
        public static bool IsValidBook(Dictionary<string, string> arguments)
        {
            var isValidBook = true;
            var bookType = typeof(PrintedItem.Book);
            var BookPropertiesArray = bookType.GetProperties();            

            foreach (var property in BookPropertiesArray)
            {
                if (!arguments.ContainsKey(property.Name.ToLower()))
                    isValidBook = false;
            }

            return isValidBook;
        }
    }
}
