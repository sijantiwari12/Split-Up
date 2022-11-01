using Microsoft.EntityFrameworkCore;
using SplitUp.Web.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SplitUp.Web.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly DataContext _dataContext;

        public TransactionService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public int SaveTransaction(Transaction transaction)
        {
            _dataContext.Add(transaction);
            _dataContext.SaveChanges();

            return transaction.Id;
        }

        public double GetAmountToReceive(int userId)
        {
            var transactions = _dataContext.Transactions.Include(u => u.Creditors).Where(x => x.UserId == userId).ToList();
            double total = 0;

            foreach(var abc in transactions)
            {
                total += abc.Creditors.Where(s => s.Status == 0 && s.CreditorId != userId).Sum(a => a.AmountToPay);
            }
            return total;
        }

        public IEnumerable<Transaction> GetAllBills(int userId) =>  _dataContext.Transactions.Where(i => i.UserId == userId).ToList();

        public Transaction SetBillDetails(int transactionId) => _dataContext.Transactions.Include(c => c.Creditors).Where(t => t.Id == transactionId).Single();

        public void DeleteTransaction(int transactionId)
        {
            Transaction transactionToRemove = _dataContext.Transactions.Where(u => u.Id == transactionId).Single();
            _dataContext.Remove(transactionToRemove);

            _dataContext.SaveChanges();
        }

        public IEnumerable<Transaction> GetCreditorIncludedTransaction(int[] transactionId) =>  _dataContext.Transactions.Where(u => transactionId.Contains(u.Id)).ToList();
    }
}
