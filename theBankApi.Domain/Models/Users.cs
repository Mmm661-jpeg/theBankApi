using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace theBankApi.Domain.Models
{
    public class Users
    {
        public int UserID { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int CustomerId { get; set; }

        public Customers Customers { get; set; } = null!;
    }
}
