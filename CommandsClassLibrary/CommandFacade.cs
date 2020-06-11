using System;
using System.Collections.Generic;

namespace CommandsClassLibrary
{
    public class CommandFacade
    {
        public static void Help()
        {
            new HelpCommand().Execute();
        }

        public static void Add(Dictionary<string, string> args)
        {            
            new AddCommand(args).Execute();
        }

        public static void Delete(Dictionary<string, string> args)
        {
            new DeleteCommand(args).Execute(); 
        }

        public static void Update(Dictionary<string, string> args)
        {
            new UpdateCommand(args).Execute();
        }
        public static void Find(Dictionary<string, string> args)
        {
            new FindCommand(args).Execute();
        }

        public static void End()
        {
            new EndCommand().Execute();
        }
    }
}
