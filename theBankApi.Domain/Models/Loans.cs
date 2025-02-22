using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace theBankApi.Domain.Models
{
    public class Loans
    {
        public int LoanId { get; set; }
        public int AccountId { get; set; } //fk
        public DateOnly Date {  get; set; }
        public decimal Amount { get; set; }
        public int Duration { get; set; }
        public decimal Payments { get; set; }
        public string Status { get; set; } = null!;

        public Accounts Accounts { get; set; } = null!; //set in accounts
    }
}
