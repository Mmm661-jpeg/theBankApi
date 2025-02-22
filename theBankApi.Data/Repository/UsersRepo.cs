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
    public class UsersRepo:IUsersRepo
    {
        private readonly Inlämningsuppgift3DBcontext db;

        public UsersRepo(Inlämningsuppgift3DBcontext db)
        {
            this.db = db;
        }

        public Users Login(string username, string password)
        {
            var result = db.Users.FirstOrDefault(u=>u.Username == username && u.Password == password);
            return result;
        }

        public bool Register(Users users, Customers customers)
        {
            using (var transaction = db.Database.BeginTransaction())
            {



                try
                {


                    bool isduplicate = db.Users.Any(c => c.Username == users.Username);
                    if (!isduplicate)
                    {

                        db.Customers.Add(customers);
                        db.SaveChanges();

                        int customerid = customers.CustomerId;

                        var standardAccount = new Accounts()
                        {
                            Frequency = "Monthly",
                            Created = DateOnly.FromDateTime(DateTime.Now.Date),
                            Balance = 0,
                            AccountTypesId = 1
                        };

                        db.Accounts.Add(standardAccount);
                        db.SaveChanges();

                        int standardAccountID = standardAccount.AccountId;

                        var savingsAccount = new Accounts()
                        {
                            Frequency = "Monthly",
                            Created = DateOnly.FromDateTime(DateTime.Now.Date),
                            Balance = 0,
                            AccountTypesId = 2

                        };

                        db.Accounts.Add(savingsAccount);
                        db.SaveChanges();
                        int savingsAccountID = savingsAccount.AccountId;

                        var disposition1 = new Dispositions()
                        {
                            CustomerId = customerid,
                            AccountId = standardAccountID,
                            Type = "OWNER"
                        };

                        db.Dispositions.Add(disposition1);
                        db.SaveChanges();

                        var dispositions2 = new Dispositions()
                        {
                            CustomerId = customers.CustomerId,
                            AccountId = savingsAccountID,
                            Type = "OWNER"
                        };
                        db.Dispositions.Add(dispositions2);
                        db.SaveChanges();

                        var useraccount = new Users()
                        {
                            Username = users.Username,
                            Password = users.Password,
                            CustomerId = customerid
                        };
                        db.Users.Add(useraccount);
                        db.SaveChanges();

                        transaction.Commit(); //

                        return true;


                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    //Log it if fails for now console.writeline?
                    Console.WriteLine(ex.ToString());
                    return false;
                }
            }
             

            //Validera username ska var unikt /
            //Sätt in ny customer först //
            //ta id lägg i variabel //
            //skapa 2 accounts ta deras idn
            //skapa connection i disposition med cid,aid1,aid2
            //skapa users
            //klar

        }
    }
}
