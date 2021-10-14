using ConnectToDB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Models.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MicrosoftIdentityTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly IServiceProvider _service;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        public HomeController(IServiceProvider service, 
            UserManager<User> userManager, 
            SignInManager<User> signInManager)
        {
            _service = service;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<string> Login([FromForm] string username, [FromForm] string password)
        {
            var user = await _userManager.FindByNameAsync(username);
            var result = await _signInManager.PasswordSignInAsync(user, password, false, false);

            if (result.Succeeded) return "access";
            else return "deny";
        }
        [HttpGet]
        [Route("Logout")]
        public async Task<string> Logout()
        {
            await _signInManager.SignOutAsync();

            return "deny";
        }
        [HttpGet]
        [Route("Create")]
        public User GetCreate()
        {
            var userManager = _service.GetService<UserManager<User>>();
            User user = new User()
            {
                UserName = "User1",
                LastName = "LastName1",
                FirstName = "FirstName1"
            };
            
            IdentityResult result = userManager.CreateAsync(user, "123qwe").GetAwaiter().GetResult();
            userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "User")).GetAwaiter().GetResult();
            if (result.Succeeded)
            {
                //userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "User")).GetAwaiter().GetResult();
            }

            return user;
        }
    }
}
