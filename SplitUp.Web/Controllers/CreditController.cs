using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SplitUp.Core.Models.Transaction;
using SplitUp.Web.Services;

namespace SplitUp.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreditController : ControllerBase
    {
        private readonly ICreditService _creditService;

        private readonly IUserService _userService;

        public CreditController(ICreditService creditService, IUserService userService)
        {
            _creditService = creditService;
            _userService = userService;
        }

        [HttpGet("GetCreditorsByTransaction/{transactionId}")]
        public IActionResult GetCreditorsByTransaction(int transactionId) => Ok(_creditService.GetCreditorsByTransaction(transactionId));

        [HttpGet("UpdateCreditorTransaction/{transactionId}/{creditorId}")]
        public IActionResult UpdateCreditorTransaction(int transactionId, int creditorId)
        {
            _creditService.UpdateCreditorTransaction(transactionId, creditorId);
            return Ok(this._transactionUpdatedMessage());
        }

        private String _transactionUpdatedMessage()
        {
            return JsonConvert.SerializeObject(new
            {
                status = "Success",
                message = "Transaction with the user has been succesfully completed.",
            });
        }

        [HttpGet("PingTheUser/{transactionId}/{userId}")]
        public IActionResult PingTheUser(int transactionId,int userId)
        {
            _creditService.Ping(transactionId, userId);

            return Ok(JsonConvert.SerializeObject(new
            {
                status = "Success",
                message = "User has been Pinged.",
            }));
        }

        [HttpGet("CancelPing/{transactionId}/{userId}")]
        public IActionResult CancelPing(int transactionId, int userId)
        {
            _creditService.CancelPing(transactionId, userId);

            return Ok(JsonConvert.SerializeObject(new
            {
                status = "Success",
                message = "You have cancelled user's Ping.",
            }));
        }
    }
}
