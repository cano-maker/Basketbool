using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;
using Basketbool.Web.Data.Entities;
using Basketbool.Web.Enums;
using Basketbool.Web.Models;

namespace Basketbool.Web.Interfaces
{
    public interface IUserHelper
    {
        Task<UserEntity> GetUserAsync(string email);
        Task<IdentityResult> AddUserAsync(UserEntity user, string password);
        Task CheckRoleAsync(string roleName);
        Task AddUserToRoleAsync(UserEntity user, string roleName);
        Task<bool> IsUserInRoleAsync(UserEntity user, string roleName);
        Task<SignInResult> LoginAsync(LoginViewModel model);
        Task LogoutAsync();
        Task<UserEntity> AddUserAsync(AddUserViewModel model, Guid imageId);
        Task<IdentityResult> ChangePasswordAsync(UserEntity user, string oldPassword, string newPassword);
        Task<IdentityResult> UpdateUserAsync(UserEntity user);
        Task<UserEntity> GetUserAsync(Guid userId);
        Task<string> GenerateEmailConfirmationTokenAsync(UserEntity user);
        Task<IdentityResult> ConfirmEmailAsync(UserEntity user, string token);
        Task<string> GeneratePasswordResetTokenAsync(UserEntity user);
        Task<IdentityResult> ResetPasswordAsync(UserEntity user, string token, string password);
        Task<SignInResult> ValidatePasswordAsync(UserEntity user, string password);
        Task<UserEntity> AddUserAsync(AddUserViewModel model, Guid imageId, UserType userType);
    }
}
