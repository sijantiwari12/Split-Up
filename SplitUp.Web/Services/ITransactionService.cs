using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SplitUp.Web.Services
{
    public interface ITransactionService
    {
        int SaveTransaction(Transaction transaction);

        double GetAmountToReceive(int UserId);

        IEnumerable<Transaction> GetAllBills(int userId);

        Transaction SetBillDetails(int transactionId);

        void DeleteTransaction(int transactionId);

        IEnumerable<Transaction> GetCreditorIncludedTransaction(int[] transactionId);
    }
}
