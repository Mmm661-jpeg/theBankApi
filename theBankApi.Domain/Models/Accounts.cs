using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace theBankApi.Domain.Models
{
    public class Accounts
    {
        public int AccountId { get; set; }
        public string Frequency { get; set; } = null!;

        public DateOnly Created { get; set; }

        public decimal Balance { get; set; }

        public int AccountTypesId { get; set; } //fk

        public AccountTypes AccountTypes { get; set; } = null!; //set in accoutns
        public ICollection<Dispositions> Dispositions { get; set; } = new HashSet<Dispositions>(); //set in accounts

        public ICollection<Loans> Loans { get; set; } = new HashSet<Loans>(); //set in accounts

        public ICollection<Transactions> Transactions { get; set; } = new HashSet<Transactions>(); //set in accounts
    }

}
