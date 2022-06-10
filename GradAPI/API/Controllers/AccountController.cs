using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using API.Entities;
using API.Data;
namespace API.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;

        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IRepositoryWrapper _context;

        public AccountController(
         UserManager<IdentityUser> userManager,
         SignInManager<IdentityUser> signInManager, ILogger<AccountController> logger, 
         IRepositoryWrapper context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _context = context;
        }
        [HttpPost("fetch-user")]
        public IActionResult ExternalLogin()
        {
            // Request a redirect to the external login provider.
            try{
                var returnUrl = "https://localhost:5001/api/Account/signin";
            var redirectUrl = Url.Action(nameof(ExternalLoginCallback), "Account", new { returnUrl });
            string provider =  "Google";
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return Challenge(properties, provider);
            }catch(Exception ex){
                return BadRequest(ex.Message);
            }
            return BadRequest("Unkown Error Occured!");
        }

        [HttpGet("signin")]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        {
            if (remoteError != null)
            {
                return BadRequest($"Error from external provider: {remoteError}");
            }
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return BadRequest("User Info not found");
            }
            // Sign in the user with this external login provider if the user already has a login.
            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
            if (result.Succeeded)
            {
                //return Redirect(returnUrl);
                _logger.LogInformation("User logged in with {Name} provider.", info.LoginProvider);
                return StatusCode(200, "Login success");
            }
            if (result.IsLockedOut)
            {
                return StatusCode(400, "User lockedout");
            }
            else
            {  //get google login user infromation like that.
            string email = "", firstName = "", lastName = "";
                if (info.Principal.HasClaim(c => c.Type == ClaimTypes.Email))
                {
                     email = info.Principal.FindFirstValue(ClaimTypes.Email);
                }
                if (info.Principal.HasClaim(c => c.Type == ClaimTypes.GivenName))
                {
                     firstName = info.Principal.FindFirstValue(ClaimTypes.GivenName);
                }
                if (info.Principal.HasClaim(c => c.Type == ClaimTypes.GivenName))
                {
                     lastName = info.Principal.FindFirstValue(ClaimTypes.Surname);
                } 
                var user = new Grads { FirstName = firstName, Email = email, LastName = lastName };
                if(!UserExists(user)){
                   _context.Grads.Create(user);
                   _context.Grads.SaveChanges();
                }
                 _logger.LogInformation($"details: {email} {firstName} {lastName}");
                return StatusCode(200, "User created");
            }
         return BadRequest("Something went wrong!");
        }
        private bool UserExists(Grads user){
            //return _context.Grads.GetAll().Contains(user) ? true : false;
            foreach (var item in _context.Grads.GetAll().ToList())
            {
                if(item.Email.Equals(user.Email))
                  return true;
            }
            return false;
        }

    }
}