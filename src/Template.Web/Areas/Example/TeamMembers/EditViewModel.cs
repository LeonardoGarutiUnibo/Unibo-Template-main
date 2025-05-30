using System;
using System.ComponentModel.DataAnnotations;
using Template.Services.Shared;
using Template.Web.Infrastructure;

namespace Template.Web.Areas.Example.TeamMembers
{
    [TypeScriptModule("Example.TeamMembers.Server")]
    public class EditViewModel
    {
        public EditViewModel()
        {
        }

        public Guid? Id { get; set; }
        public Guid TeamId { get; set; }
        public Guid UserId { get; set; }
        public bool IsManager { get; set; }

        public string ToJson()
        {
            return Infrastructure.JsonSerializer.ToJsonCamelCase(this);
        }

        public void SetTeamMember(TeamMemberDetailDTO teamMemberDetailDTO)
        {
            if (teamMemberDetailDTO != null)
            {
                Id = teamMemberDetailDTO.Id;
                TeamId = teamMemberDetailDTO.TeamId; 
                UserId = teamMemberDetailDTO.UserId; 
                IsManager = teamMemberDetailDTO.IsManager;
            }
        }

        public AddOrUpdateTeamMemberCommand ToAddOrUpdateTeamMemberCommand()
        {
            return new AddOrUpdateTeamMemberCommand
            {
                Id = Id,
                TeamId = TeamId, 
                UserId = UserId, 
                IsManager = IsManager
            };
        }
    }
}