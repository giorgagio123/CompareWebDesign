using CompareWebDesign.Domain;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompareWebDesign.Services.Account
{
    public interface IAccountService
    {
        Task<IdentityResult> CreateUserAsync(ApplicationUser user, string password);

        Task<ApplicationUser> FindByNameAsync(string userName);

        Task<bool> RoleExistsAsync(string role);

        Task<IdentityResult> CreateRoleAsync(IdentityRole role);

        Task<IdentityResult> AddToRoleAsync(ApplicationUser user, string role);

        Task<SignInResult> PasswordSignInAsync(string userName, string password, bool isPersistent, bool lockoutOnfailure);

        Task SignOutAsync();

        bool IsInRole(string role);

        string GetUserId();
    }
}
