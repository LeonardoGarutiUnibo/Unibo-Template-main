using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Template.Services.Shared
{
    public class AddOrUpdateUserCommand
    {
        public Guid? Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NickName { get; set; }
        public string Role { get; set; }
        public string Password { get; set; }
        public Guid TeamId { get; set; }
        public Guid TimesheetId { get; set; }
    }

    public partial class SharedService
    {
        public async Task<Guid> HandleUser(AddOrUpdateUserCommand cmd)
        {
            Console.WriteLine("Secondo Role ricevuto: " + cmd.Role);

            var user = await _dbContext.Users
                .Where(x => x.Id == cmd.Id)
                .FirstOrDefaultAsync();

            if (user == null)
            {   
                user = new User
                {
                    FirstName = cmd.FirstName,
                    LastName = cmd.LastName,
                    NickName = cmd.NickName,
                    Email = cmd.Email,
                    Role = cmd.Role,
                    TeamId = cmd.TeamId,
                    TimesheetId = cmd.TimesheetId
                };
                
                if (!string.IsNullOrEmpty(cmd.Password))
                {
                    var passwordHasher = new PasswordHasher<User>();
                    user.Password = passwordHasher.HashPassword(user, cmd.Password);
                }
                _dbContext.Users.Add(user);
            }

            user.FirstName = cmd.FirstName;
            user.LastName = cmd.LastName;
            user.NickName = cmd.NickName;
            user.Role = cmd.Role;
            user.TeamId = cmd.TeamId;
            user.TimesheetId = cmd.TimesheetId;

            await _dbContext.SaveChangesAsync();

            return user.Id;
        }

        public async Task DeleteUser(Guid id)
        {
            var user = await _dbContext.Users
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
            if (user != null)
            {
                _dbContext.Users.Remove(user);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}