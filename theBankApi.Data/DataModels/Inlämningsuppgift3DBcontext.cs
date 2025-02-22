using theBankApi.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace theBankApi.Data.DataModels
{
    public class theBankApiDBcontext:DbContext
    {
        public theBankApiDBcontext()
        {

        }

        public theBankApiDBcontext(DbContextOptions<theBankApiDBcontext> options) : base(options) { }

        public virtual DbSet<Users> Users { get; set; } = null!;

        public virtual DbSet<Customers> Customers { get; set; } = null!;
        public virtual DbSet<Accounts> Accounts { get; set; } = null!;
        public virtual DbSet<AccountTypes> AccountTypes { get; set; } = null!;
        public virtual DbSet<Dispositions> Dispositions { get; set; } = null!;
        public virtual DbSet<Loans> Loans { get; set; } = null!;
        public virtual DbSet<Transactions> Transactions { get; set; } = null!;


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=BankAppData;Integrated Security=SSPI; TrustServerCertificate=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>(entity =>
            {
                entity.ToTable("Users");

                entity.Property(e => e.UserID).IsRequired()
                .UseIdentityColumn().HasColumnName("UserID");

                entity.Property(e => e.Username).IsRequired()
                .HasColumnName("Username")
                .IsUnicode(false)
                .HasMaxLength(50);

                entity.Property(e => e.Password).IsRequired()
               .HasColumnName("Password")
               .IsUnicode(false)
               .HasMaxLength(72);

                entity.Property(e => e.CustomerId).IsRequired()
                .HasColumnName("CustomerId");

                entity.HasKey(e => e.UserID);

                entity.HasOne(u => u.Customers) //  Users -> Customers
                .WithOne(c => c.Users)
                .HasForeignKey<Users>(u => u.CustomerId);

                entity.HasKey(e => e.UserID);  //set p key


            });

            modelBuilder.Entity<Customers>(entity =>
            {
                entity.ToTable("Customers");

                entity.Property(c => c.CustomerId).IsRequired()
                .HasColumnName("CustomerId");

                entity.Property(c => c.Gender).IsRequired()
                .IsUnicode(true)
                .HasMaxLength(6);

                entity.Property(c=>c.Givenname).IsRequired()
                .IsUnicode (true)
                .HasMaxLength(100);

                entity.Property(c=>c.Surname).IsRequired()
                .IsUnicode(true)
                .HasMaxLength(100);

                entity.Property(c=>c.Streetaddress).IsRequired()
                .IsUnicode(true)
                .HasMaxLength (100);

                entity.Property(c => c.City).IsRequired()
               .IsUnicode(true)
               .HasMaxLength(100);

                entity.Property(c => c.Zipcode).IsRequired()
                 .IsUnicode(true)
                 .HasMaxLength(15);

                entity.Property(c => c.Country).IsRequired()
                 .IsUnicode(true)
                 .HasMaxLength(100);

                entity.Property(c => c.CountryCode).IsRequired()
               .IsUnicode(true)
               .HasMaxLength(2);

                entity.Property(c => c.Birthday)
                .HasColumnName("Birthday");

                entity.Property(c => c.Telephonecountrycode)
               .IsUnicode(true)
               .HasMaxLength(10);

                entity.Property(c => c.Telephonenumber)
              .IsUnicode(true)
              .HasMaxLength(25);

                entity.Property(c => c.Emailaddress)
             .IsUnicode(true)
             .HasMaxLength(100);

                entity.HasMany(c => c.Dispositions)  //  Dispositions -> Customer
                .WithOne(d => d.Customer)
                .HasForeignKey(d => d.CustomerId);

                entity.HasKey(c => c.CustomerId);  //set p key

            });

            modelBuilder.Entity<Accounts>(entity =>
            {
                entity.ToTable("Accounts");

                entity.Property(a => a.AccountId).IsRequired()
                .HasColumnName("AccountId");

                entity.Property(a=>a.Frequency).IsRequired()
                .IsUnicode(true)
                .HasMaxLength(50);

                entity.Property(a => a.Created).HasColumnName("Created")
                .IsRequired();

                entity.Property(a => a.Balance).IsRequired()
                 .HasColumnType("decimal(13,2)");

                entity.Property(a => a.AccountTypesId).HasColumnName("AccountTypesId");

                entity.HasOne(a => a.AccountTypes) // AccountTypes -> Accounts
                .WithMany(at => at.Accounts)
                .HasForeignKey(a => a.AccountTypesId); 

                entity.HasMany(a => a.Dispositions) //  Dispositions -> Account
                .WithOne(d => d.Account)
                .HasForeignKey(d => d.AccountId);

                entity.HasMany(a=>a.Loans) //  Loans -> Accounts
                .WithOne(l=>l.Accounts)
                .HasForeignKey(l => l.AccountId);

                entity.HasMany(a=>a.Transactions) //  Transactions -> Accounts
                .WithOne(t=>t.Accounts)
                .HasForeignKey(t => t.AccountId);

                entity.HasKey(a => a.AccountId);  //set p key


            });

            modelBuilder.Entity<AccountTypes>(entity =>
            {
                entity.ToTable("AccountTypes");

                entity.Property(at => at.AccountTypeId)
                .IsRequired()
                .HasColumnName("AccountTypeId");
               

                entity.Property(at => at.TypeName)
                .IsRequired()
                .HasColumnName("TypeName")
                .IsUnicode(true)
                .HasMaxLength(50);

                entity.Property(at => at.Description)
                .HasColumnName("Description")
                .IsUnicode (true)
                .HasMaxLength(500);  
                
                entity.HasKey(at => at.AccountTypeId); //set p key

            });

            modelBuilder.Entity<Dispositions>(entity =>
            {
                entity.ToTable("Dispositions");

                entity.Property(d=>d.DispositionId)
                .IsRequired();

                entity.Property(d=>d.CustomerId) //set in customers
                .IsRequired();

                entity.Property(d=>d.AccountId)  //set in accounts
                .IsRequired();

                entity.Property(d=>d.Type)
                .IsRequired()
                .IsUnicode(true)
                .HasMaxLength (50);

                entity.HasKey (d => d.DispositionId);  //set p key



            });

            modelBuilder.Entity<Loans>(entity =>
            {
                entity.ToTable("Loans");

                entity.Property(l=>l.LoanId)
                .IsRequired();

                entity.Property(l=>l.AccountId)
                .IsRequired();

                entity.Property(l=>l.Date)
                .IsRequired();

                entity.Property(l => l.Amount)
                .IsRequired()
                .HasColumnType("decimal(13,2)");

                entity.Property(l => l.Duration)
                .IsRequired()
                .HasColumnType("integer");

                entity.Property(l => l.Payments)
                .IsRequired()
                .HasColumnType("decimal(13,2)");

                entity.Property(l => l.Status)
                .IsRequired()
                .IsUnicode(true)
                .HasMaxLength(50);

                entity.HasKey(l => l.LoanId);  //set p key



            });

            modelBuilder.Entity<Transactions>(entity =>
            {
                entity.ToTable("Transactions");

                entity.Property(t=>t.TransactionId)
                .IsRequired();

                entity.Property(t=>t.AccountId)
                .IsRequired();

                entity.Property(t=>t.Date)
                .IsRequired();

                entity.Property(t => t.Type)
                .IsRequired()
                .IsUnicode(true)
                .HasMaxLength (50);

                entity.Property(t => t.Operation)
                .IsRequired()
                .IsUnicode(true)
                .HasMaxLength(50);

                entity.Property(t => t.Amount)
                .IsRequired()
                .HasColumnType("decimal(13,2)");

                entity.Property(t => t.Balance)
                .IsRequired()
                .HasColumnType("decimal(13,2)");

                entity.Property(t => t.Symbol)
                .IsUnicode (true)
                .HasMaxLength(50)
                .HasColumnName("Symbol");

                entity.Property(t => t.Bank)
                .IsUnicode(true)
                .HasMaxLength(50)
                .HasColumnName("Bank");

                entity.Property(t => t.Account)
                .IsUnicode()
                .HasMaxLength(50);

                entity.HasKey(t => t.TransactionId);  //set p key

            });
        }
    }
}
