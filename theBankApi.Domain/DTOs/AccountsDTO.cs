using theBankApi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace theBankApi.Domain.DTOs
{
    public class AccountsDTO
    {
        public int AccountId { get; set; }
        public string Frequency { get; set; } = null!;

        public DateOnly Created { get; set; }

        public decimal Balance { get; set; }

        public int AccountTypesId { get; set; }



        //Annledning:system.Text.Json.JsonException:
        //A possible object cycle was detected.
        //This can either be due to a cycle or if the object depth is
        //larger than the maximum allowed depth of 32.
        //Consider using ReferenceHandler.Preserve on JsonSerializerOptions to support cycles.


        //Vill koppla bort dessa:


        // public AccountTypes AccountTypes { get; set; } = null!; //set in accoutns
        //public ICollection<Dispositions> Dispositions { get; set; } = new HashSet<Dispositions>(); //set in accounts

       // public ICollection<Loans> Loans { get; set; } = new HashSet<Loans>(); //set in accounts

       // public ICollection<Transactions> Transactions { get; set; } = new HashSet<Transactions>(); /
       
    }
}
