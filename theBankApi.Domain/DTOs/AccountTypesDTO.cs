using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace theBankApi.Domain.DTOs
{
    public class AccountTypesDTO
    {
        public int AccountTypeId { get; set; }
        public string TypeName { get; set; } = null!;

        public string Description { get; set; }
    }
}
