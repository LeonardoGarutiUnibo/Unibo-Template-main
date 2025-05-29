using System;
using System.ComponentModel.DataAnnotations;
using Template.Services.Shared;
using Template.Web.Infrastructure;

namespace Template.Web.Areas.Example.Teams
{
    [TypeScriptModule("Example.Teams.Server")]
    public class EditViewModel
    {
        public EditViewModel()
        {
        }

        public Guid? Id { get; set; }

        [Display(Name = "Nome")]
        public string Name { get; set; }

        public string ToJson()
        {
            return Infrastructure.JsonSerializer.ToJsonCamelCase(this);
        }

        public void SetTeam(TeamDetailDTO teamDetailDTO)
        {
            if (teamDetailDTO != null)
            {
                Id = teamDetailDTO.Id;
                Name = teamDetailDTO.Name;
            }
        }

        public AddOrUpdateTeamCommand ToAddOrUpdateTeamCommand()
        {
            return new AddOrUpdateTeamCommand
            {
                Id = Id,
                Name = Name
            };
        }
    }
}