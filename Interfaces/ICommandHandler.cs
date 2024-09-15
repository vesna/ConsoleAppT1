namespace ConsoleAppT1.Interfaces
{
    /// <summary>
    /// Used to mark command handler (Write operation)
    /// </summary>
    public interface ICommandHandler
    { }
    /// <summary>
    /// Generic class to implement command handler (Write operation)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICommandHandler<in T> : ICommandHandler where T : ICommand
    {
        void Handle(T command);
    }
}
