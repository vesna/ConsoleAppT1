using AutoMapper;
using ConsoleAppT1.Interfaces;
using ConsoleAppT1.Models;
using Microsoft.Extensions.Caching.Memory;

namespace ConsoleAppT1.Queries.Handlers
{
    public sealed class GetTransactionByIdHandler : IQueryHandler<GetTransactionById, Transaction>
    {
        private readonly IMemoryCache _memoryCache;

        public GetTransactionByIdHandler(IMemoryCache cache)
        {
            _memoryCache = cache;
        }
        public Transaction Handle(GetTransactionById query)
        {
            if (!_memoryCache.TryGetValue(query.TransactionId, out Transaction? transaction))
            {
                throw new ArgumentException("The transaction doesn't exist.");
            }

            return transaction!;
        }
    }
}
