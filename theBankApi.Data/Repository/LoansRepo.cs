using Inlämningsuppgift3.Data.DataModels;
using Inlämningsuppgift3.Data.Interfaces;
using theBankApi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inlämningsuppgift3.Data.Repository
{
    public class LoansRepo:ILoansRepo
    {
        private readonly Inlämningsuppgift3DBcontext db;
        public LoansRepo(Inlämningsuppgift3DBcontext db)
        {
            this.db = db;
        }

        public bool CreateLoan(int accountid, decimal amount, int duration, string status)
        {
            decimal payments = CalcPayments(amount, duration);
            var loan = new Loans()
            {
                AccountId = accountid,
                Amount = amount,
                Duration = duration,
                Status = status,
                Payments = payments,
                Date = DateOnly.FromDateTime(DateTime.Today)
            };

            using(var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    db.Loans.Add(loan);
                    db.SaveChanges();
                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
        }

        public Loans GetLoan(int accountid)
        {
            var result = db.Loans.FirstOrDefault(c=>c.AccountId == accountid);
            return result;
        }

        public List<Loans> GetAllLoans()
        {
            var result = db.Loans.ToList();
            return result;
        }

        public bool UpdateLoanStatus(int accountid, decimal? amount = null, int? duration = null, string? status = null)
        {
            decimal payments = 0;
            var loan = db.Loans.FirstOrDefault(c=>c.AccountId==accountid);

            if (loan == null)
            {
                return false;
            }

            if(amount.HasValue &&!duration.HasValue)
            {
                payments = CalcPayments(amount.Value, loan.Duration);
            }
            else if(duration.HasValue && !amount.HasValue) 
            {
                payments = CalcPayments(loan.Amount,duration.Value);
            }
            else if(amount.HasValue && duration.HasValue)
            {
                payments = CalcPayments(amount.Value,duration.Value);
            }
            else
            {
                payments = loan.Payments;
            }

            using(var transactions = db.Database.BeginTransaction())
            {
                try
                {
                    loan.Amount = amount ?? loan.Amount;
                    loan.Duration = duration ?? loan.Duration;
                    loan.Payments = payments;
                    loan.Status = status ?? loan.Status;

                    db.SaveChanges();
                    transactions.Commit();
                    return true;

                }
                catch (Exception ex)
                {
                    transactions.Rollback();
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }

        }

        private decimal CalcPayments(decimal amount, int duration)
        {
           try
            {
                if (duration <= 0)
                {
                    return amount;
                }
                else
                {
                    return amount / duration;
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
