using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SplitUp.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SplitUp.Web.Controllers
{
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        private readonly ICreditService _creditService;

        public TransactionController(ITransactionService transactionService, ICreditService creditService)
        {
            _transactionService = transactionService;
            _creditService = creditService;
        }

        [HttpPost("SaveTransaction")]
        public IActionResult SaveTransaction([FromBody]TransactionDto transactionDetails)
        {
            int transactionId = _transactionService.SaveTransaction(transactionDetails.Transaction);
            for (int i=0; i< transactionDetails.Emails.Length; i++)
            {
                Credit details = new Credit()
                {
                    AmountToPay = transactionDetails.Transaction.AmountPaid / transactionDetails.Transaction.NoOfIndividuals,
                    TransactionId = transactionId,
                    CreditorId = 0,
                    Status = 0,
                    UpdatedDate = null,
                };
                _creditService.InsertCreditors(details, transactionDetails.Emails[i]);
            }

            return Ok(this._transactionSuccessMessage());
        }

        private String _transactionSuccessMessage()
        {
            return JsonConvert.SerializeObject(new
            {
                status = "Success",
                message = "Transaction has been succesfully added.",
            });
        }

        [HttpGet("GetAllBills/{userId}")]
        public IActionResult GetAllBills(int userId) {
            var billDetails = _transactionService.GetAllBills(userId);
            return Ok(billDetails.Reverse());
        }

        [HttpGet("GetBillDetails/{transactionId}")]
        public IActionResult GetBillDetails(int transactionId)
        {
            Transaction billDetail = _transactionService.SetBillDetails(transactionId);

            return Ok(billDetail);
        }

        [HttpDelete("DeleteTransaction/{transactionId}")]
        public IActionResult DeleteTransaction(int transactionId)
        {
            _creditService.DeleteCreditors(transactionId);
            _transactionService.DeleteTransaction(transactionId);

            return Ok(this._transactionDeleteMessage());
        }

        private String _transactionDeleteMessage()
        {
            return JsonConvert.SerializeObject(new
            {
                status = "Success",
                message = "Transaction has been succesfully deleted.",
            });
        }

        [HttpGet("GetCreditorsByTransaction/{transactionId}")]
        public IActionResult GetCreditorsByTransaction(int transactionId) => Ok(_creditService.GetCreditorsByTransaction(transactionId));

        [HttpGet("GetUserIncludedTransactions/{userId}")]
        //Include User
        public IActionResult GetUserIncludedTransaction(int userId) => Ok(_transactionService.GetCreditorIncludedTransaction(_creditService.GetUserIncludedTransactionIds(userId)));
    }
}
