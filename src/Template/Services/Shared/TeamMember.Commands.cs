using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Template.Services.Shared
{
    public class AddOrUpdateTeamMemberCommand
    {
        public Guid? Id { get; set; }
        public string UserId { get; set; }
        public string TeamId { get; set; }
        public bool IsManager { get; set; }
    }

    public partial class SharedService
    {
        public async Task<Guid> HandleTeamMember(AddOrUpdateTeamMemberCommand cmd)
        {
            var teamMember = await _dbContext.TeamMembers
                .Where(x => x.Id == cmd.Id)
                .FirstOrDefaultAsync();

            if (teamMember == null)
            {
                teamMember = new TeamMember
                {
                    UserId = cmd.UserId,
                    TeamId = cmd.TeamId,
                    IsManager = cmd.IsManager,
                };

                _dbContext.TeamMembers.Add(teamMember);
            }

            await _dbContext.SaveChangesAsync();

            return teamMember.Id;
        }
    }
}