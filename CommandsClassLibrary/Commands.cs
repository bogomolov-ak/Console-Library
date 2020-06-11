using System;
using System.Collections.Generic;
using System.Reflection;
using PrintedItem;
using static Validator.BookValidator;
using static Validator.AmountValidator;
using static Validator.IdValidator;

namespace CommandsClassLibrary
{
    public static class CommandNames
    {
        public const string Help = "-help";
        public const string Add = "-add";
        public const string Delete = "-delete";
        public const string Update = "-update";
        public const string Find = "-find";
        public const string End = "-end";
    }

    public static class CommandDescriptions
    {
        public const string Help = "вывод списка команд и описания их функций.";
        public const string Add = "добавление книги или журнала в библиотеку.\n" +
            "Для добавления книги: (-add /code= /name= /author= /style= /year= /publisher= /amount=)\n" +
            "Для добавления журнала: (-add /code= /name= /period= /publisher= /year= /number= /amount=)";
        public const string Delete = "удаление книги или журнала из библиотеки.\n" +
            "Для удаления книги: (-delete /bookid= /amount=)\n" +
            "Для удаления журнала: (-delete /magazineid= /amount=)";
        public const string Update = "обновление сведений о книге или журнале в библиотеке.\n" +
            "Для обновления книги: (-update /bookid= /code= /name= /author= /style= /year= /publisher=\n" +
            "Для обновления журнала: (-update /magazineid= /code= /name= /period= /publisher= /year= /number=)";
        public const string Find = "поиск книги или журнала по названию в библиотеке.\n" +
            "(-find /name=)";
        public const string End = "завершение работы программы.";
    }
    public class HelpCommand : Command
    {        
        public HelpCommand()
        {                   
            CommandName = CommandNames.Help;
            CommandDescription = CommandDescriptions.Help;
        }        

        public void Execute()
        {
            var CommandNames = Assembly.GetExecutingAssembly().GetType("CommandsClassLibrary.CommandNames", false, true).GetFields();
            var CommandDescriptions = Assembly.GetExecutingAssembly().GetType("CommandsClassLibrary.CommandDescriptions", false, true).GetFields();
            var commandCount = CommandNames.Length;

            for (int i = 0; i < commandCount; ++i)
            {
                Console.WriteLine($"{CommandNames[i].GetValue(this)} : {CommandDescriptions[i].GetValue(this)}");
            }
        }
    }

    public class AddCommand : Command
    {
        private Dictionary<string, string> arguments;     

        public AddCommand(Dictionary<string, string> arguments)
        {
            CommandName = CommandNames.Add;
            CommandDescription = CommandDescriptions.Add;
            this.arguments = arguments;
        }

        public void Execute()
        {            
            if (IsValidBook(arguments) && IsValidAmount(arguments["amount"], out var amount))
            {                
                var addedBook = BookFactory.CreateBook(arguments);

                BookRepository.Add(addedBook, amount);
                
                return;         
            }

            throw new Exception("Ошибка, введены некорректные параметры книги или журнала.");            
        }        
    }

    public class DeleteCommand : Command
    {
        private Dictionary<string, string> arguments;

        public DeleteCommand(Dictionary<string, string> arguments)
        {
            CommandName = CommandNames.Delete;
            CommandDescription = CommandDescriptions.Delete;
            this.arguments = arguments;
        }

        public void Execute()
        {
            if (arguments.ContainsKey("bookid") && IsValidAmount(arguments["amount"], out var amount) && IsValidId(arguments["bookid"], out var deletedBookId))
            {   
                BookRepository.Delete(deletedBookId, amount);

                Console.WriteLine($"Книга ID : {deletedBookId}\nуспешно удалена из библиотеки в количестве {amount}.");

                return;
            }

            throw new Exception("Ошибка, ведены некорректные параметры книги или журнала.");
        }
    }

    public class UpdateCommand : Command
    {
        private Dictionary<string, string> arguments;
        public UpdateCommand(Dictionary<string, string> arguments)
        {
            CommandName = CommandNames.Update;
            CommandDescription = CommandDescriptions.Update;
            this.arguments = arguments;
        }

        public void Execute()
        {    
            if (arguments.ContainsKey("bookid") && IsValidId(arguments["bookid"], out var updatedBookId))
            {      
                BookRepository.Update(updatedBookId, arguments);
                
                Console.WriteLine($"Сведения о книге ID : {updatedBookId}\nуспешно обновлены.");

                return;
            }

            throw new Exception("Ошибка, не удалось корректно обновить сведения о книге.");
        }
    }

    public class FindCommand : Command
    {
        private Dictionary<string, string> arguments;
        public FindCommand(Dictionary<string, string> arguments)
        {
            CommandName = CommandNames.Find;
            CommandDescription = CommandDescriptions.Find;
            this.arguments = arguments;
        }

        public void Execute()
        {
            if (arguments.ContainsKey("name"))
            {
                BookRepository.FindBookAndPrintInConsole(arguments["name"]);                
            }
            else
                throw new Exception("Отсутвует аргумент, соответствующий имени искомых книги или журнала.");
        }
    }

    public class EndCommand : Command
    {        
        public EndCommand()        
        {            
            CommandName = CommandNames.End;
            CommandDescription = CommandDescriptions.End;
        }        
        public void Execute()
        {
            Console.WriteLine("Завершение работы программы.");
            Console.ReadKey();
        }
    }
}
