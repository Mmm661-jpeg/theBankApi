using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace theBankApi.Domain.DTOs
{
    public class TransactionsDTO
    {
        public int TransactionId { get; set; }
        public int AccountId { get; set; } //fk
        public DateOnly Date { get; set; }
        public string Type { get; set; } = null!;
        public string Operation { get; set; } = null!;
        public decimal Amount { get; set; }
        public decimal Balance { get; set; }
     
    }
}
