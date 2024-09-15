using ConsoleAppT1.Commands;
using ConsoleAppT1.Commands.Handlers;
using ConsoleAppT1.Interfaces;
using ConsoleAppT1.Models;
using ConsoleAppT1.Queries;
using ConsoleAppT1.Queries.Handlers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Globalization;
using System.Text.Json;
using static System.Console;

// Create the DI container
using var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        // Add memory cache to store data
        services.AddMemoryCache();
        services.AddScoped<ICommandHandler<CreateTransactionCommand>, CreateTransactionCommandHandler>();
        services.AddScoped<IQueryHandler<GetTransactionById, Transaction>, GetTransactionByIdHandler>();
    })
    .Build();

using var serviceScope = host.Services.CreateScope();
var provider = serviceScope.ServiceProvider;

try
{
    // Initialize variables 
    var dateTimeFormat = "dd.MM.yyyy";
    var invalidOperationExceptionText = "Invalid value";
    var createTransactionCommandHandler = provider.GetService<ICommandHandler<CreateTransactionCommand>>();
    var getTransactionByIdHandler = provider.GetService<IQueryHandler<GetTransactionById, Transaction>>();
    var jsonSerializerOptions = new JsonSerializerOptions
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    while (true)
    {
        WriteLine("Enter a command (add, get, exit):");
        var command = ReadLine();

        switch (command?.ToLower())
        {
            case "add":
                AddTranCommand();
                break;
            case "get":
                GetTranCommand();
                break;
            case "exit":
                WriteLine("Exit the application");
                Environment.Exit(0);
                return;
            default:
                WriteLine("Unknown command. Please try again\n");
                break;
        }
    }

    void AddTranCommand()
    {
        TryExecuteHandling(() =>
        {
            WriteLine("Enter id:");
            var id = int.Parse(ReadLine() ?? throw new InvalidOperationException(invalidOperationExceptionText));

            WriteLine($"Enter the date (format: {dateTimeFormat}):");
            var date = DateTime.ParseExact(
                ReadLine() ?? throw new InvalidOperationException(invalidOperationExceptionText), dateTimeFormat,
                CultureInfo.InvariantCulture);

            WriteLine("Enter amount:");
            var amount =
                decimal.Parse(ReadLine() ?? throw new InvalidOperationException(invalidOperationExceptionText),
                NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands,
                CultureInfo.InvariantCulture);

            var transaction = new Transaction
            {
                Id = id,
                TransactionDate = date,
                Amount = amount
            };

            var command = new CreateTransactionCommand(transaction);
            createTransactionCommandHandler!.Handle(command);
        });
    }

    void GetTranCommand()
    {
        TryExecuteHandling(() =>
        {
            WriteLine("Enter Id:");
            var id = int.Parse(ReadLine() ?? throw new InvalidOperationException(invalidOperationExceptionText));
            var query = new GetTransactionById(id);
            var transaction = getTransactionByIdHandler!.Handle(query);

            WriteLine(transaction!.SerializeToJson(jsonSerializerOptions));
        });
    }

    void TryExecuteHandling(Action action)
    {
        try
        {
            action();
            WriteLine("[OK]\n");
        }
        catch (Exception ex)
        {
            WriteLine($"Error: {ex.Message}\n");
        }
    }
}
catch (Exception ex)
{
    WriteLine($"Fatal error: {ex.Message}, {ex.StackTrace}", ConsoleColor.Red);
}