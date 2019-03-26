using DataBaseAccessLayer.Data.Repository.Contracts;
using DataBaseAccessLayer.Data.Repository.GenericRepository;
using DataBaseAccessLayer.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using BusinessLogic.Models;
using System.Text.RegularExpressions;
using System.Linq.Dynamic;
using AutoMapper;
using BusinessLogic.Services.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using BusinessLogic.Models.Account;
using System.Threading.Tasks;
using BusinessLogic.Utils;

namespace BusinessLogic.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<DataBaseAccessLayer.Data.Entities.User> _userManager;
        private readonly SignInManager<DataBaseAccessLayer.Data.Entities.User> _signInManager;
        //private readonly IEmailSender _emailSender;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public AccountService(UserManager<DataBaseAccessLayer.Data.Entities.User> userManager,
                               SignInManager<DataBaseAccessLayer.Data.Entities.User> signInManager,
                               //IEmailSender emailSender,
                               ILogger logger,
                               IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            //_emailSender = emailSender;
            _logger = logger;
            _mapper = mapper;
        }        

        public async Task<int> LoginUser(LoginModel user)
        {
            if (isLoginModelValid(user))
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(user.UserName, user.Password, user.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    _logger.LogInformation(1, "User logged in.");
                    return (int)AccountStatusCodes.Ok;
                }
                
                if (result.IsLockedOut)
                {
                    _logger.LogWarning(2, "User account locked out.");
                    return (int)AccountStatusCodes.UserLocked;
                }
                else
                {
                    _logger.LogWarning(2, "Invalid login attempt.");
                    return (int)AccountStatusCodes.WrongCredentials;
                }
            }

            return (int)AccountStatusCodes.Failed;
        }

        public async Task<int> RegisterUser(RegisterModel user)
        {
            if (isRegisterModelValid(user))
            {
                var userEntity = new DataBaseAccessLayer.Data.Entities.User
                {
                    UserName = user.Email,
                    Email = user.Email,
                    Password = user.Password
                };

                var result = await _userManager.CreateAsync(_mapper.Map<DataBaseAccessLayer.Data.Entities.User>(user), userEntity.Password);
                if (result.Succeeded)
                {                    
                    //await _signInManager.SignInAsync(userEntity, isPersistent: false);
                    _logger.LogInformation(3, "User created a new account with password.");
                    return (int)AccountStatusCodes.Ok;
                }
            }

            return (int)AccountStatusCodes.Failed;
        }

        public async Task<int> ResetPassword(ResetPasswordModel model)
        {
            if (!isResetPasswordModelValid(model))
            {
                return (int)AccountStatusCodes.Failed;
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return (int)AccountStatusCodes.UserNotExist;
            }

            var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
            if (result.Succeeded)
            {
                return (int)AccountStatusCodes.Ok;
            }

            return (int)AccountStatusCodes.Failed;
        }

        public async Task<int> LogOff()
        {
            try
            {
                await _signInManager.SignOutAsync();
                _logger.LogInformation(4, "User logged out.");
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
            return (int)AccountStatusCodes.Ok;
        }

        private bool isLoginModelValid(LoginModel user)
        {
            return true;
        }

        private bool isRegisterModelValid(RegisterModel user)
        {
            return true;
        }

        private bool isResetPasswordModelValid(ResetPasswordModel user)
        {
            return true;
        }
    }
}
