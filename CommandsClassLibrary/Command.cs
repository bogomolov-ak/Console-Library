namespace CommandsClassLibrary
{
    public abstract class Command
    {
        public string CommandName { get ; protected set; }
        public string CommandDescription { get; protected set; }      
    }
}
