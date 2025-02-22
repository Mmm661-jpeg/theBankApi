using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace theBankApi.Domain.Requests
{
    public class CreateLoanRequest
    {
        public int Accountid {  get; set; }
        public decimal Amount { get; set; }
        public int Duration { get; set; }
        public string Status { get; set; } = null!;

    }
}
