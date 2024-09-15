namespace ConsoleAppT1.Models
{
    /// <summary>
    /// Transaction model
    /// </summary>
    public class Transaction
    {
        public int Id { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal Amount { get; set; }

    }
}
