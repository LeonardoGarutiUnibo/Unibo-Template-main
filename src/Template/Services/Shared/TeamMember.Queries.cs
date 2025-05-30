using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Template.Infrastructure;

namespace Template.Services.Shared
{
    public class TeamMembersSelectQuery
    {
        public Guid IdCurrentTeamMember { get; set; }
        public string Filter { get; set; }
    }

    public class TeamMembersSelectDTO
    {
        public IEnumerable<TeamMember> TeamMembers { get; set; }
        public int Count { get; set; }

        public class TeamMember
        {
            public Guid Id { get; set; }
            public Guid UserId { get; set; }
            public Guid TeamId { get; set; }
            public bool IsManager { get; set; }
        }
    }

    public class TeamMembersIndexQuery
    {
        public Guid IdCurrentTeamMember { get; set; }
        public string Filter { get; set; }

        public Paging Paging { get; set; }
    }


    public class TeamMembersIndexDTO
    {
        public IEnumerable<TeamMember> TeamMembers { get; set; }
        public int Count { get; set; }
        public class TeamMember {
            public Guid Id { get; set; }
            public Guid UserId { get; set; }
            public Guid TeamId { get; set; }
            public bool IsManager { get; set; }
        }
    }

    public class TeamMemberDetailQuery
    {
        public Guid Id { get; set; }
    }

    public class TeamMemberDetailDTO {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid TeamId { get; set; }
        public bool IsManager { get; set; }
    }
    public partial class SharedService
    {
        /// <summary>
        /// Returns users for a select field
        /// </summary>
        /// <param name="qry"></param>
        /// <returns></returns>
        public async Task<TeamMembersSelectDTO> QueryTeamMember(TeamMembersSelectQuery qry)
        {
            var queryable = _dbContext.TeamMembers
                .Where(x => x.Id != qry.IdCurrentTeamMember);


            return new TeamMembersSelectDTO
            {
                TeamMembers = await queryable
                .Select(x => new TeamMembersSelectDTO.TeamMember
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    TeamId = x.TeamId,
                    IsManager = x.IsManager
                }).ToArrayAsync(),
                Count = await queryable.CountAsync(),
            };
        }

        /// <summary>
        /// Returns users for an index page
        /// </summary>
        /// <param name="qry"></param>
        /// <returns></returns>
        public async Task<TeamMembersIndexDTO> Query(TeamMembersIndexQuery qry)
        {
            var queryable = _dbContext.TeamMembers
                .Where(x => x.Id != qry.IdCurrentTeamMember);
            
            return new TeamMembersIndexDTO
            {
                TeamMembers = await queryable
                    .ApplyPaging(qry.Paging)
                    .Select(x => new TeamMembersIndexDTO.TeamMember
                    {
                        Id = x.Id,
                        UserId = x.UserId,
                        TeamId = x.TeamId,
                        IsManager = x.IsManager
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
        public async Task<TeamMemberDetailDTO> Query(TeamMemberDetailQuery qry)
        {
            return await _dbContext.TeamMembers
                .Where(x => x.Id == qry.Id)
                .Select(x => new TeamMemberDetailDTO
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    TeamId = x.TeamId,
                    IsManager = x.IsManager
                })
                .FirstOrDefaultAsync();
        }
    }
}
