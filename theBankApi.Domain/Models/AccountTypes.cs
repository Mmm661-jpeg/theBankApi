using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace theBankApi.Domain.Models
{
    public class AccountTypes
    {
        public int AccountTypeId { get; set; }
        public string TypeName { get; set; } = null!;

        public string Description { get; set; }

        public ICollection<Accounts> Accounts { get; set; } = new HashSet<Accounts>();
    }
}
