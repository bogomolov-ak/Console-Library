using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace PrintedItem
{
    public static class BookRepository
    {
        public static List<BookWrapper> Books { get; private set; }

        static BookRepository()
        {
            Books = new List<BookWrapper>();
        }

        public static void Add(Book newBook, int amount)
        {
            var containsBook = ContainsBook(newBook);

            if (containsBook != null)
            {
                containsBook.Amount += amount;
                Console.WriteLine($"Книга{containsBook}успешно добавлена в библиотеку.");
            }
            else 
            {
                var newBookWrapper = new BookWrapper(newBook, amount);
                Books.Add(newBookWrapper);
                Console.WriteLine($"Книга{newBookWrapper}успешно добавлена в библиотеку.");
            }
        }

        public static void Delete(int bookId, int amount)
        {    
            var bookWrapperForDelete = Books.SingleOrDefault(book => book.BookId == bookId);

            if (bookWrapperForDelete == null)
                throw new Exception("Ошибка, удаляемая книга не найдена в библиотеке.");

            if (bookWrapperForDelete.Amount < amount)
                throw new Exception("Ошибка, количество недостаточно для удаления.");

            if (bookWrapperForDelete.Amount == amount)
            {
                Books.Remove(bookWrapperForDelete);
                return;
            }

            bookWrapperForDelete.Amount -= amount;   
        }

        //private static readonly Type BookType = typeof(Book);
        //private static Lazy<Dictionary<string, PropertyInfo>> Properties = new Lazy<Dictionary<string, PropertyInfo>>(InitProperties);

        //private static Dictionary<string, PropertyInfo> InitProperties()
        //{

        //    var typeProperties = BookType.GetProperties();
        //    var result = new Dictionary<string, PropertyInfo>(typeProperties.Length, StringComparer.OrdinalIgnoreCase);

        //    foreach (var typePropetry in typeProperties)
        //    {
        //        result.Add(typePropetry.Name, typePropetry);
        //    }

        //    return result;

        //}

        //public static void Update1(int bookId, Dictionary<string, string> args)
        //{
        //    var isUpdated = false;

        //    var bookWrapperForUpdate = Books.FirstOrDefault(book => book.BookId == bookId);

        //    if (ReferenceEquals(bookWrapperForUpdate, null))
        //        throw new InvalidOperationException("Книга с указанным ID не найдена.");

        //    var bookForUpdate = bookWrapperForUpdate.Book;

        //    foreach (var arg in args)
        //    {

        //        if (!Properties.Value.TryGetValue(arg.Key, out var property))
        //            throw new InvalidOperationException();

        //        property.SetValue(bookForUpdate, arg.Value);
        //    }

        //}

        public static void Update(int bookId, Dictionary<string, string> args)
        {
            var bookWrapperForUpdate = Books.SingleOrDefault(book => book.BookId == bookId);

            if (bookWrapperForUpdate == null)
                throw new Exception("Ошибка, обновляемая книга не найдена в библиотеке.");
            
            var properties = bookWrapperForUpdate.Book.GetType().GetProperties().Where(property => property.GetCustomAttributes().Contains(new Updatable()));            
            foreach (var property in properties)
            {
                var argKey = property.Name.ToLower();

                if (args.TryGetValue(argKey, out var updateValue))
                {
                    property.SetValue(bookWrapperForUpdate.Book, updateValue);
                    args.Remove(argKey);
                    Console.WriteLine($"Свойство книги {argKey} обновлено на {updateValue}");
                }
            }

            foreach (var arg in args)
                if (!arg.Key.Equals("bookid"))
                    Console.WriteLine($"Не удалось обнаружить связанные свойства книги: {arg.Key}={arg.Value}, либо нет прав на изменение.");            
        }

       
        public static BookWrapper ContainsBook(Book book)
        {            
            foreach (BookWrapper containsBook in Books)
            {
                if (containsBook.Book.Equals(book))
                    return containsBook;
            }
            return null;
        }

        public static void FindBookAndPrintInConsole(string name)
        {
            bool isFound = false;

            Books.ForEach(book =>
            {
                if (book.Book.Name.Equals(name))
                {
                    Console.WriteLine($"Найдена книга\n{book}");
                    isFound = true;
                }
            });            

            if (!isFound)
                Console.WriteLine("Книга с указанным именем не найдена.");
        }
    }
}
