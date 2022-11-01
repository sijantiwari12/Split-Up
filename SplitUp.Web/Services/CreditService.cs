using Microsoft.EntityFrameworkCore;
using SplitUp.Web.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SplitUp.Web.Services
{
    public class CreditService : ICreditService
    {
        private readonly DataContext _dataContext;

        private readonly IUserService _userService;

        public CreditService(DataContext dbContext, IUserService userService)
        {
            _dataContext = dbContext;
            _userService = userService;
        }

        public void InsertCreditors(Credit details, string creditorEmail)
        {
            details.CreditorId = _userService.GetUserByEmail(creditorEmail).Id;
             
            _dataContext.Add(details);
            _dataContext.SaveChanges();
        }

        public double GetAmountToPay(int UserId) => _dataContext.Creditors.Where(u => u.CreditorId == UserId && u.Status == 0 && u.Transaction.UserId != UserId).Sum(t => t.AmountToPay);

        public void DeleteCreditors(int transactionId)
        {
            _dataContext.RemoveRange(this.GetCreditorsByTransaction(transactionId));
            _dataContext.SaveChanges();
        }

        public IEnumerable<Credit> GetCreditorsByTransaction(int transactionId)
        {
            var credtiors = _dataContext.Creditors.AsNoTracking().Include(c => c.Creditor).Include(t => t.Transaction).Where(t => t.TransactionId == transactionId).ToList();
            return credtiors;
        }

        public void UpdateCreditorTransaction(int transactionId,int creditorId)
        {
            var creditor = _dataContext.Creditors.Where(u => u.CreditorId == creditorId && u.TransactionId == transactionId).FirstOrDefault();
            creditor.UpdatedDate = new DateTime();
            creditor.Status = 1;

            _dataContext.SaveChanges();
        }

        public int[] GetUserIncludedTransactionIds(int userId) => _dataContext.Creditors.Where(a => a.CreditorId == userId).Select(t => t.TransactionId).ToArray<int>();

        public void Ping(int transactionId, int userId)
        {
            var pingUser = _dataContext.Creditors.Where(u => u.CreditorId == userId && u.TransactionId == transactionId).FirstOrDefault();
            pingUser.Ping = 1;

            _dataContext.SaveChanges();
        }

        public void CancelPing(int transactionId, int userId)
        {
            var pingUser = _dataContext.Creditors.Where(u => u.CreditorId == userId && u.TransactionId == transactionId).FirstOrDefault();
            pingUser.Ping = 0;

            _dataContext.SaveChanges();
        }
    }
}
