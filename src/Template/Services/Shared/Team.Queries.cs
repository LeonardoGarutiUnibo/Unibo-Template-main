using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Template.Infrastructure;

namespace Template.Services.Shared
{
    public class TeamsSelectQuery
    {
        public Guid IdCurrentTeam { get; set; }
        public string Filter { get; set; }
    }

    public class TeamsSelectDTO
    {
        public IEnumerable<Team> Teams { get; set; }
        public int Count { get; set; }

        public class Team{
            public Guid Id { get; set; }
            public string Name { get; set; }
        }
    }

    public class TeamsIndexQuery
    {
        public Guid IdCurrentTeam { get; set; }
        public string Filter { get; set; }

        public Paging Paging { get; set; }
    }


    public class TeamsIndexDTO
    {
        public IEnumerable<Team> Teams { get; set; }
        public int Count { get; set; }
        public class Team
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
        }
    }

    public class TeamDetailQuery
    {
        public Guid Id { get; set; }
    }

    public class TeamDetailDTO {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
    public partial class SharedService
    {
        /// <summary>
        /// Returns users for a select field
        /// </summary>
        /// <param name="qry"></param>
        /// <returns></returns>
        public async Task<TeamsSelectDTO> QueryTeam(TeamsSelectQuery qry)
        {
            var queryable = _dbContext.Teams
                .Where(x => x.Id != qry.IdCurrentTeam);


            return new TeamsSelectDTO
            {
                Teams = await queryable
                .Select(x => new TeamsSelectDTO.Team
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToArrayAsync(),
                Count = await queryable.CountAsync(),
            };
        }

        /// <summary>
        /// Returns users for an index page
        /// </summary>
        /// <param name="qry"></param>
        /// <returns></returns>
        public async Task<TeamsIndexDTO> Query(TeamsIndexQuery qry)
        {
            var queryable = _dbContext.Teams
                .Where(x => x.Id != qry.IdCurrentTeam);
            
            return new TeamsIndexDTO
            {
                Teams = await queryable
                    .ApplyPaging(qry.Paging)
                    .Select(x => new TeamsIndexDTO.Team
                    {           
                        Id = x.Id,
                        Name = x.Name
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
        public async Task<TeamDetailDTO> Query(TeamDetailQuery qry)
        {
            return await _dbContext.Teams
                .Where(x => x.Id == qry.Id)
                .Select(x => new TeamDetailDTO
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .FirstOrDefaultAsync();
        }
    }
}
