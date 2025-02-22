using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace theBankApi.Domain.Requests
{
    public class SendMoneyRequest
    {
        public int Toaccountid { get; set; }
        public int Fromaccountid { get; set; }
        public decimal Amount { get; set; }

    }
}
