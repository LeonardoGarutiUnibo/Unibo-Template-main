using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Template.Services.Shared
{
    public class AddOrUpdateTeamCommand {
        public Guid? Id { get; set; }
        public string Name { get; set; }
    }
    

    public partial class SharedService
    {
        public async Task<Guid> HandleTeam(AddOrUpdateTeamCommand cmd)
        {
            var team = await _dbContext.Teams
                .Where(x => x.Id == cmd.Id)
                .FirstOrDefaultAsync();

            if (team == null)
            {
                team = new Team
                {
                    Name = cmd.Name,
                };

                _dbContext.Teams.Add(team);
            }

            await _dbContext.SaveChangesAsync();

            return team.Id;
        }
    }
}