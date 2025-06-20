﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Template.Infrastructure;

namespace Template.Services.Shared
{
    public class UsersSelectQuery
    {
        public Guid IdCurrentUser { get; set; }
        public string Filter { get; set; }
    }

    public class UsersSelectDTO
    {
        public IEnumerable<User> Users { get; set; }
        public int Count { get; set; }

        public class User
        {
            public Guid Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string NickName { get; set; }
            public Guid TeamId { get; set; }
            public string Email { get; set; }
            public string Role { get; set; }
            public string TimesheetWeekDay { get; set; }
            public TimeSpan TimesheetStartTime { get; set; }
            public TimeSpan TimesheetEndTime { get; set; }
        }
    }

    public class UsersIndexQuery
    {
        public Guid IdCurrentUser { get; set; }
        public string Filter { get; set; }

        public Paging Paging { get; set; }
    }

    public class UsersIndexDTO
    {
        public IEnumerable<User> Users { get; set; }
        public int Count { get; set; }

        public class User
        {
            public Guid Id { get; set; }
            public string Email { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string NickName { get; set; }
            public string Role { get; set; }
            public Guid TeamId { get; set; }
            public Guid TimesheetId { get; set; }
            public string TimesheetWeekDay { get; set; }
            public TimeSpan TimesheetStartTime { get; set; }
            public TimeSpan TimesheetEndTime { get; set; }
        }
    }

    public class UsersByTimesheetQuery
    {
        public Guid TimesheetId { get; set; }
    }

    public class UserDetailQuery
    {
        public Guid Id { get; set; }
    }

    public class UserDetailDTO
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NickName { get; set; }
        public string Role { get; set; }
        public string Password { get; set; }
        public Guid TeamId { get; set; }
        public Guid TimesheetId { get; set; }
        public string TimesheetWeekDay { get; set; }
        public TimeSpan TimesheetStartTime { get; set; }
        public TimeSpan TimesheetEndTime { get; set; }
    }

    public class CheckLoginCredentialsQuery
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public partial class SharedService
    {
        /// <summary>
        /// Returns users for a select field
        /// </summary>
        /// <param name="qry"></param>
        /// <returns></returns>
        public async Task<UsersSelectDTO> Query(UsersSelectQuery qry)
        {
            var queryable = _dbContext.Users
                .Where(x => x.Id != qry.IdCurrentUser);

            if (string.IsNullOrWhiteSpace(qry.Filter) == false)
            {
                queryable = queryable.Where(x => x.Email.Contains(qry.Filter, StringComparison.OrdinalIgnoreCase));
            }

            return new UsersSelectDTO
            {
                Users = await queryable
                .Include(x => x.Timesheet)
                .Select(x => new UsersSelectDTO.User
                {
                    Id = x.Id,
                    Email = x.Email,
                    Role =  x.Role,
                    FirstName = x.FirstName,
                    LastName  = x.LastName,
                    NickName  = x.NickName,
                    TeamId  = x.TeamId,
                    TimesheetWeekDay = x.Timesheet.WeekDay,   
                    TimesheetStartTime = x.Timesheet.StartTime, 
                    TimesheetEndTime = x.Timesheet.EndTime
                })
                .ToArrayAsync(),
                Count = await queryable.CountAsync(),
            };
        }
        
        /// <summary>
        /// Returns users for an index page
        /// </summary>
        /// <param name="qry"></param>
        /// <returns></returns>
        public async Task<UsersIndexDTO> Query(UsersIndexQuery qry)
        {
            var queryable = _dbContext.Users
                .Where(x => x.Id != qry.IdCurrentUser);

            if (string.IsNullOrWhiteSpace(qry.Filter) == false)
            {
                queryable = queryable.Where(x => x.Email.Contains(qry.Filter, StringComparison.OrdinalIgnoreCase));
            }

            return new UsersIndexDTO
            {
                Users = await queryable
                    .ApplyPaging(qry.Paging)
                    .Include(x => x.Timesheet)
                    .Select(x => new UsersIndexDTO.User
                    {
                        Id = x.Id,
                        Email = x.Email,
                        Role = x.Role,
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        NickName = x.NickName,
                        TeamId = x.TeamId,
                        TimesheetId = x.TimesheetId,
                        TimesheetWeekDay = x.Timesheet.WeekDay,
                        TimesheetStartTime = x.Timesheet.StartTime, 
                        TimesheetEndTime = x.Timesheet.EndTime
                    })
                    .ToArrayAsync(),
                Count = await queryable.CountAsync()
            };
        }

        /// <summary>
        /// Returns the detail of the user who matches the Id passed in the qry parameter
        /// </summary>
        /// <param name="qry"></param>
        /// <returns></returns>
        public async Task<UserDetailDTO> Query(UserDetailQuery qry)
        {
            return await _dbContext.Users
            .Where(u => u.Id == qry.Id)
            .Include(u => u.Timesheet)
            .Select(u => new UserDetailDTO
            {
                Id = u.Id,
                Email = u.Email,
                Role = u.Role,
                FirstName = u.FirstName,
                LastName = u.LastName,
                NickName = u.NickName,
                TeamId = u.TeamId,
                TimesheetId = u.TimesheetId,
                TimesheetWeekDay = u.Timesheet.WeekDay,
                TimesheetStartTime = u.Timesheet.StartTime, 
                TimesheetEndTime = u.Timesheet.EndTime
            })
    .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Check if credentials passed in the query are valid for a user present in the database
        /// </summary>
        /// <param name="qry"></param>
        /// <returns>User data if user has been found and credentials are valid</returns>
        /// <exception cref="LoginException">Invalid credentials</exception>
        public async Task<UserDetailDTO> Query(CheckLoginCredentialsQuery qry)
        {

            var user = await _dbContext.Users
                .Where(x => x.Email == qry.Email)
                .FirstOrDefaultAsync();
            if (user == null || user.IsMatchWithPassword(qry.Password) == false)
                throw new LoginException("Email o password errate");

            return new UserDetailDTO
            {
                Id = user.Id,
                Email = user.Email,
                Role =  user.Role,
                FirstName = user.FirstName,
                LastName = user.LastName,
                NickName = user.NickName,
                TeamId = user.TeamId
            };
        }

        public async Task<Dictionary<Guid, string>> QueryUserFullNamesByIds(List<Guid> userIds)
        {
            return await _dbContext.Users
                .Where(x => userIds.Contains(x.Id))
                .Select(x => new { x.Id, FullName = x.FirstName + " " + x.LastName })
                .ToDictionaryAsync(x => x.Id, x => x.FullName);
        }

        public async Task<UsersSelectDTO> Query(UsersByTimesheetQuery qry)
        {
            var queryable = _dbContext.Users
                .Where(u => u.TimesheetId == qry.TimesheetId);
        
            var users = await queryable
                .Select(u => new UsersSelectDTO.User
                {
                    Id = u.Id,
                    Email = u.Email,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    NickName = u.NickName,
                    Role = u.Role,
                    TeamId = u.TeamId,
                    TimesheetWeekDay = u.Timesheet.WeekDay,
                    TimesheetStartTime = u.Timesheet.StartTime,
                    TimesheetEndTime = u.Timesheet.EndTime
                })
                .ToArrayAsync();
        
            return new UsersSelectDTO
            {
                Users = users,
                Count = users.Length
            };
        }
    }
}
