using ConsoleAppT1.Interfaces;

namespace ConsoleAppT1.Queries
{
    public class GetTransactionById : IQuery
    {
        public GetTransactionById(int trId)
        {
            TransactionId = trId;
        }
        public int TransactionId { get; }
    }

}
