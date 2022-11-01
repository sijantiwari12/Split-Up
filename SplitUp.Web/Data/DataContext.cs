using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SplitUp.Web.Data
{
    public class DataContext : DbContext
    {
        //constructor
        public DataContext() { }

        public DataContext(IDbContextOptions options) { }

        public DbSet<User> Users { get; set; }

        public DbSet<Transaction> Transactions { get; set; }

        public DbSet<Credit> Creditors { get; set; } 

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //Server=(localdb)\\mssqllocaldb;Database=SplitUp;Trusted_Connection=True
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=SplitUp;Trusted_Connection=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().
                HasMany(t => t.Transactions).
                WithOne(u => u.User);

            modelBuilder.Entity<User>().
                HasMany(t => t.Creditors).
                WithOne(u => u.Creditor);

            modelBuilder.Entity<Transaction>().
                HasOne(u => u.User).
                WithMany(t => t.Transactions).
                HasForeignKey(f => f.UserId).
                OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Credit>().
                 HasOne(u => u.Creditor).
                 WithMany(c => c.Creditors).
                 HasForeignKey(f => f.CreditorId).
                 OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Credit>().
                 HasOne(u => u.Transaction).
                 WithMany(c => c.Creditors).
                 HasForeignKey(f => f.TransactionId).
                 OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    FullName = "Bibesh KC",
                    Password = "bafal123",
                    Email = "bibesh.kc@selu.edu",
                    Gender = 'M',
                    Token = Guid.NewGuid().ToString(),
                });

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 2,
                    FullName = "Pratikshya Timalsina",
                    Password = "dallu123",
                    Email = "pratikshya.timalsina@selu.edu",
                    Gender = 'F',
                    Token = Guid.NewGuid().ToString(),
                });

            modelBuilder.Entity<Transaction>().HasData(
                new Transaction
                {
                    Id = 1,
                    BillName = "Walmart",
                    AmountPaid = 44.4,
                    NoOfIndividuals = 2,
                    UserId = 2,
                    Memo = " This is a demo Bill."
                });

            modelBuilder.Entity<Credit>().HasData(
                new Credit
                {
                    Id = 1,
                    Status = 0,
                    CreditorId = 2,
                    TransactionId = 1,
                    AmountToPay = 22.2,
                    UpdatedDate = new DateTime(),
                });
        }
    }
}
