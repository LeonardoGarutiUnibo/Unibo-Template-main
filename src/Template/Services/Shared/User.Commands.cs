﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;
using System.Text;

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
                    var sha256 = SHA256.Create();
                    user.Password = System.Convert.ToBase64String(sha256.ComputeHash(Encoding.ASCII.GetBytes(cmd.Password)));
                }
                _dbContext.Users.Add(user);
            }

            user.FirstName = cmd.FirstName;
            user.LastName = cmd.LastName;
            user.NickName = cmd.NickName;
            user.Role = cmd.Role;
            if (cmd.TeamId != Guid.Parse("00000000-0000-0000-0000-000000000000")){
                user.TeamId = cmd.TeamId;
            }
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