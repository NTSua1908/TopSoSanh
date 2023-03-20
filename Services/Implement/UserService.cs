using Hangfire;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using TopSoSanh.DTO;
using TopSoSanh.DTO.User;
using TopSoSanh.Entity;
using TopSoSanh.Helper;
using TopSoSanh.Services.Interface;
using Role = TopSoSanh.Entity.Role;

namespace TopSoSanh.Services.Implement
{
    public class UserService : IUserService
    {

        private readonly UserManager<User> _userManager;
        private readonly ApiDbContext _dbContext;
        private readonly RoleManager<Role> _roleManager;
        private readonly ISendMailService _sendMailService;

        public UserService(UserManager<User> userManager, ApiDbContext context, RoleManager<Role> roleManager, 
            ISendMailService sendMailService)
        {
            _userManager = userManager;
            _dbContext = context;
            _roleManager = roleManager;
            _sendMailService = sendMailService;
        }

        public async Task<ErrorModel> Register(RegisterModel model, string hostName)
        {
            ErrorModel error = new ErrorModel();
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                error.Add("Email already exists");
                return error;
            }

            var hasher = new PasswordHasher<User>();
            user = new User()
            {
                PasswordHash = hasher.HashPassword(null, model.Password),
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.Email,
            };

            var result = await _userManager.CreateAsync(user);
            if (result.Succeeded)
            {
                var role = await _roleManager.FindByNameAsync("User");
                await _userManager.AddToRoleAsync(user, role.Name);
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                await _sendMailService.SendMailConfirmAsync(new Helper.MailContent()
                {
                    Subject = "Xác nhận email",
                    To = user.Email
                }, hostName, user.LastName, token, user.Email);
                return error;
            }
            error.Add("Register Failed");
            return error;
        }

        public async Task<ErrorModel> ForgotPassword(string Email)
        {
            var errors = new ErrorModel();
            var user = _dbContext.Users.FirstOrDefault(x => x.Email.ToLower() == Email.ToLower());
            if (user == null)
            {
                errors.Add(ErrorResource.UserNotFound);
                return errors;
            }
            var hasher = new PasswordHasher<User>();
            string password = RandomString(12);
            user.PasswordHash = hasher.HashPassword(null, password);
            _dbContext.SaveChanges();
            await _sendMailService.SendMailResetPassword(new MailContent()
            {
                Subject = "Đặt lại mật khẩu",
                To = Email
            }, user.LastName, password);
            return errors;
        }

        private string RandomString(int length)
        {
            Random random = new Random();
            const string chars = "abcdefghijklmnopqrstuvxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
