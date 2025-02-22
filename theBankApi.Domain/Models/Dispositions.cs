using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace theBankApi.Domain.Models
{
    public class Dispositions
    {
        public int DispositionId { get; set; }
        public int CustomerId { get; set; }
        public int AccountId { get; set; }
        public string Type { get; set; } = null!;

        public Customers Customer { get; set; } = null!; //set in customers
        public Accounts Account { get; set; } = null!; //set in accounts

    }
}
