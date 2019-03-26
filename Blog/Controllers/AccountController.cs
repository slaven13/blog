using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.Models;
using BusinessLogic.Models.Account;
using BusinessLogic.Services;
using BusinessLogic.Services.Contracts;
using BusinessLogic.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Api.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {            
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel user)
        {
            var result = await _accountService.LoginUser(user);

            if (result == (int)AccountStatusCodes.Ok)
            {
                return Ok();
            }
            else if (result == (int)AccountStatusCodes.UserLocked)
            {
                throw new Exception("User is locked");
            }
            else if (result == (int)AccountStatusCodes.WrongCredentials)
            {
                throw new Exception("Wrong credentials");
            }
            else throw new Exception("Login failed");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register(string returnUrl = null)
        {
            return Ok();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel user)
        {
            var result = await _accountService.RegisterUser(user);

            if (result == (int)AccountStatusCodes.Ok)
            {
                return Ok();
            }
            else
            {
                throw new Exception("Something wrong with Registration process");
            }
        }

        [HttpPost("logoff")]
        public async Task<IActionResult> Logoff()
        {
            var result = await _accountService.LogOff();
            
            if (result == (int)AccountStatusCodes.Ok)
            {
                return Ok();
            }
            else
            {
                throw new Exception("Logoff failed");
            }
        }
    }
}