using DigitalFolder.Models.Enums;
using System;

namespace DigitalFolder.Data.Dtos.Filters.Transactions
{
    public class FilterTransactionsRequest
    {
        public TransactionType? Type { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
