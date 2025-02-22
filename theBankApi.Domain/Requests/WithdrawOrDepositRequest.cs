using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace theBankApi.Domain.Requests
{
    public class WithdrawOrDepositRequest
    {
        public int AccountID { get; set; }
        public decimal Amount { get; set; }
    }
}
