using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Template.Services.Shared
{
    public class AddOrUpdateTeamMemberCommand
    {
        public Guid? Id { get; set; }
        public Guid UserId { get; set; }
        public Guid TeamId { get; set; }
        public bool IsManager { get; set; }
    }

    public partial class SharedService
    {
        public async Task<Guid> HandleTeamMember(AddOrUpdateTeamMemberCommand cmd)
        {
           var teamMember = await _dbContext.TeamMembers
               .FirstOrDefaultAsync(x => x.UserId == cmd.UserId && x.TeamId == cmd.TeamId);

           if (teamMember == null)
           {
               Console.WriteLine("teamMember non trovato, lo creo");
               teamMember = new TeamMember
               {
                   UserId = cmd.UserId,
                   TeamId = cmd.TeamId,
                   IsManager = cmd.IsManager,
               };

               _dbContext.TeamMembers.Add(teamMember);
           }
           else
           {
               Console.WriteLine("teamMember già presente, lo aggiorno");
               teamMember.IsManager = cmd.IsManager;
           }
           if (!cmd.IsManager)
           {
               var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == cmd.UserId);
               if (user != null)
               {
                   user.TeamId = cmd.TeamId;
               }
           }

           await _dbContext.SaveChangesAsync();

           return teamMember.Id;
        }

        public async Task DeleteTeamMember(Guid teamMemberId)
        {
            var teamMember = await _dbContext.TeamMembers
                .FirstOrDefaultAsync(tm => tm.Id == teamMemberId);

            if (teamMember != null)
            {
                _dbContext.TeamMembers.Remove(teamMember);
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("TeamMember non trovato.");
            }
        }
    }
}