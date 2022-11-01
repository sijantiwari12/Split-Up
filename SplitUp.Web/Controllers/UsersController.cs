using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SplitUp.Core.Validators;
using SplitUp.Web.Services;
namespace SplitUp.Web.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        private readonly ITransactionService _transactionService;

        private readonly ICreditService _creditService;


        public UsersController(IUserService userService, ITransactionService transactionService, ICreditService creditService)
        {
            _userService = userService;
            _transactionService = transactionService;
            _creditService = creditService;
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] User currentUser)
        {
            UserLoginValidator validator = new UserLoginValidator();
            var result = validator.Validate(currentUser);
            if (result.IsValid)
            {
                var loggedInUser = _userService.Login(currentUser.Email, currentUser.Password);
                if (loggedInUser == null)
                {
                    return Ok(this._sendIncorrectLoginMessage());
                }
            //Use Session later when project is finished
                //HttpContext.Session.SetInt32("UserId", loggedInUser.Id);
                return Ok(loggedInUser);
            }

            var errors = result.Errors;
            return Ok(errors);
        }

        private String _sendIncorrectLoginMessage()
        {
            return JsonConvert.SerializeObject(new
            {
                status = "Error",
                message = "Login Credentials Incorrect. Please try again.",
            });
        }

        [HttpPost("Register")]
        public IActionResult Register([FromBody] User newUser)
        {
            UserRegisterValidator validator = new UserRegisterValidator();
            var result = validator.Validate(newUser);
            if (result.IsValid)
            {
                if (this._checkUserDuplication(newUser.Email))
                {
                    _userService.Register(newUser);

                    return Ok(this._registerSuccessMessage());
                }
                return Ok(this._registerUserDuplicationMessage());
            }
            var error = JsonConvert.SerializeObject(result);
            return Ok(error);
        }

        private bool _checkUserDuplication(String email)
        {
            return (_userService.GetUserByEmail(email) == null) ? true : false;
        }

        private String _registerSuccessMessage()
        {
            return JsonConvert.SerializeObject(new
            {
                status = "Success",
                message = "User Successfully registered. Please log in using the previously entered credentials",
            });
        }

        private String _registerUserDuplicationMessage()
        {
            return JsonConvert.SerializeObject(new
            {
                status = "Error",
                message = "User Registration Failed. The user with the email address already exist. Please try using another Email.",
            });
        }

        [HttpGet("GetAllUsersEmail")]
        public IActionResult GetAllUsersEmail() => Ok(_userService.GetAllUsersEmail());

        [HttpGet("GetAmounts/{userId}")]
        public IActionResult GetAmounts(int userId)
        {
           var amounts = JsonConvert.SerializeObject(new
           {
               amountToPay = _creditService.GetAmountToPay(userId),
               amountToReceive = _transactionService.GetAmountToReceive(userId),
           });

            return Ok(amounts);
        }

        [HttpGet("GetNameById/{userId}")]
        public IActionResult GetNameById(int userId) {
            return Ok(JsonConvert.SerializeObject(new
            {
                name = _userService.GetUserById(userId).FullName
            }));
        }
    }
}