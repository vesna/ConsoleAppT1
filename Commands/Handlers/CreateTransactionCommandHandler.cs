using AutoMapper;
using ConsoleAppT1.Interfaces;
using ConsoleAppT1.Models;
using Microsoft.Extensions.Caching.Memory;

namespace ConsoleAppT1.Commands.Handlers
{
    public sealed class CreateTransactionCommandHandler : ICommandHandler<CreateTransactionCommand>
    {
        private readonly IMemoryCache _memoryCache;

        private readonly MemoryCacheEntryOptions _cacheOptions = new()
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1),
            SlidingExpiration = TimeSpan.FromMinutes(2)
        };

        public CreateTransactionCommandHandler(IMemoryCache cache)
        {
            _memoryCache = cache;
        }

        public void Handle(CreateTransactionCommand command)
        {
            _memoryCache.TryGetValue(command.Entity.Id, out Transaction? tran);
            if (tran is not null)
            {
                throw new ArgumentException("Transaction with the same id already exists.");
            }

            _memoryCache.Set(command.Entity.Id, command.Entity, _cacheOptions);
        }
    }
}
