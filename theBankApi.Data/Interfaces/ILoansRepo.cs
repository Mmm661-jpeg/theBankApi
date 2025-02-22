using theBankApi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace theBankApi.Data.Interfaces
{
    public interface ILoansRepo
    {
        //User has 1 column for loan so update loan after created no duplicate
        bool CreateLoan(int accountid, decimal amount, int duration, string status);
        //kan ta bort payments och sätta amount/duration=payments inne?

        Loans GetLoan(int accountid);

        List<Loans> GetAllLoans();

        bool UpdateLoanStatus(int accountid, decimal? amount = null, int? duration = null, string? status = null);
        //if != null 
        //payments updatedar ifall amount eller duration ändras
        

     
    }
}
