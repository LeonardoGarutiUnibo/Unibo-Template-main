using System;
using System.ComponentModel.DataAnnotations;
using Template.Services.Shared;
using Template.Web.Infrastructure;

namespace Template.Web.Areas.Example.Timesheets
{
    [TypeScriptModule("Example.Timesheets.Server")]
    public class EditViewModel
    {
        public EditViewModel()
        {
        }

        public Guid? Id { get; set; }

        [Display(Name = "Nome")]
        public string Name { get; set; }
        [Display(Name = "Giorni della settimana")]
        public string WeekDay { get; set; }

        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public string ToJson()
        {
            return Infrastructure.JsonSerializer.ToJsonCamelCase(this);
        }

        public void SetTimesheet(TimesheetDetailDTO timesheetDetailDTO)
        {
            if (timesheetDetailDTO != null)
            {
                Id = timesheetDetailDTO.Id;
                Name = timesheetDetailDTO.Name;
                WeekDay = timesheetDetailDTO.WeekDay;
            }
        }

        public AddOrUpdateTimesheetCommand ToAddOrUpdateTimesheetCommand()
        {
            return new AddOrUpdateTimesheetCommand
            {
                Id = Id,
                Name = Name,
                WeekDay = WeekDay,
                StartTime =  StartTime,
                EndTime = EndTime

            };
        }
    }
}