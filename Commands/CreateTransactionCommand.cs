using ConsoleAppT1.Interfaces;
using ConsoleAppT1.Models;

namespace ConsoleAppT1.Commands
{
    public sealed class CreateTransactionCommand : ICommand
    {
        public CreateTransactionCommand(Transaction newTransaction)
        {
            Entity = newTransaction;
        }

        public Transaction Entity { get; }
    }
}
