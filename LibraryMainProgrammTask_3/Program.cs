using System;
using System.Collections.Generic;
using CommandsClassLibrary;

namespace LibraryMainProgrammTask_3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Добрый день.");

            #region
            while (true)
            {
                Console.WriteLine($"Пожалуйста, введите команду, либо {CommandNames.Help} для вызова списка и описания команд...");

                var enteringCommand = Console.ReadLine();
                var commandTypeAndArguments = enteringCommand.Trim().Split(" ", 2);
                var commandType = commandTypeAndArguments[0].Trim();
                
                if (commandType.Equals(CommandNames.End))
                    break;

                switch (commandType)
                {
                    case CommandNames.Help:
                        CommandFacade.Help();
                        break;

                    case CommandNames.Add:
                        if (commandTypeAndArguments.Length > 1)
                        {
                            try
                            {
                                var commandArguments = FormatCommandKeyValuePairsOfArguments(commandTypeAndArguments[1].Trim().Split("/"));
                                CommandFacade.Add(commandArguments);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e);
                            }  
                        }
                        break;

                    case CommandNames.Delete:
                        if (commandTypeAndArguments.Length > 1)
                        {
                            try
                            {
                                var commandArguments = FormatCommandKeyValuePairsOfArguments(commandTypeAndArguments[1].Trim().Split("/"));
                                CommandFacade.Delete(commandArguments);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e);
                            }                            
                        }
                        break;

                    case CommandNames.Update:
                        if (commandTypeAndArguments.Length > 1)
                        {
                            try
                            {
                                var commandArguments = FormatCommandKeyValuePairsOfArguments(commandTypeAndArguments[1].Trim().Split("/"));
                                CommandFacade.Update(commandArguments);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e);
                            }
                        }
                        break;

                    case CommandNames.Find:
                        if (commandTypeAndArguments.Length > 1)
                        {
                            try
                            {
                                var commandArguments = FormatCommandKeyValuePairsOfArguments(commandTypeAndArguments[1].Trim().Split("/"));
                                CommandFacade.Find(commandArguments);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e);
                            }
                        }
                        break;

                    default:
                        Console.WriteLine("Не удалось корректно определить команду.");
                        break;
                }              
            }
            #endregion
        }

        private static Dictionary<string, string> FormatCommandKeyValuePairsOfArguments(string[] args)
        {
            var keyValueArgumentsPairs = new Dictionary<string, string>();

            foreach (var argument in args)
            {
                var keyValueArgumentPair = argument.Trim().Split("=");
                if (keyValueArgumentPair.Length == 2)
                {
                    keyValueArgumentsPairs.Add(keyValueArgumentPair[0].Trim(), keyValueArgumentPair[1].Trim());                    
                }                
            }

            if (keyValueArgumentsPairs.Count == 0)
                throw new Exception("Не удалось корректно определить набор параметров.");
           
            return keyValueArgumentsPairs;
        }
    }
}
