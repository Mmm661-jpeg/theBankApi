using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace theBankApi.Domain.DTOs
{
    public class LoansDTO
    {
        public int LoanId { get; set; }
        public int AccountId { get; set; } //fk
        public DateOnly Date { get; set; }
        public decimal Amount { get; set; }
        public int Duration { get; set; }
        public decimal Payments { get; set; }
        public string Status { get; set; } = null!;
    }
}
