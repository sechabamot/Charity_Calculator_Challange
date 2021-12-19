using Charity_Calculator_Challange.Data;
using Charity_Calculator_Challange.Interfaces;
using Charity_Calculator_Challange.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Charity_Calculator_Challange.Services
{
    public class UserService : IUserService
    {
        private readonly IConfiguration _configuration;
        private readonly string _secrete;
        private ApplicationDbContext _dbContext;
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;

        public UserService(ApplicationDbContext dbContext,
            IConfiguration configuration,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _secrete = configuration["Application:Secret"];
            _dbContext = dbContext;
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        public async Task<string> Create(string email, string password)
        {
            if (await _userManager.FindByEmailAsync(email) != null)
            {
                return "Sorry, email already taken";
            }

            IdentityResult result = await _userManager.CreateAsync(new ApplicationUser(email), password);
            if (result.Succeeded == true)
            {
                return string.Empty;
            }

            return "Something went wrong and it's not your fault";
        }
       
        public async Task<string> Authenticate(string email, string password)
        {

            SignInResult signIn = await _signInManager.PasswordSignInAsync(email, password, false, false);
            if (signIn.Succeeded)
            {
                return ApplicationUser.CreateJWTAuthenticationToken(email, _secrete);
            }

            return string.Empty;
        }

        public async Task<string> GetUserRole(string email)
        {
            ApplicationUser user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
               if(await _userManager.IsInRoleAsync(user, "Admin")) return "Admin";
               if(await _userManager.IsInRoleAsync(user, "Donor")) return "Donor";
               if(await _userManager.IsInRoleAsync(user, "Promoter")) return "Promoter";
            }
            return string.Empty;
        }
    }
}
