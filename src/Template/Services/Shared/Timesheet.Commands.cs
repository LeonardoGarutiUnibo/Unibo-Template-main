using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Template.Services.Shared
{
    public class AddOrUpdateTimesheetCommand
    {
        public Guid? Id { get; set; }
        public string WeekDay { get; set; }
        public string Name { get; set; }
    }

    public partial class SharedService
    {
        public async Task<Guid> HandleTimesheet(AddOrUpdateTimesheetCommand cmd)
        {
            var timesheet = await _dbContext.Timesheets
                .Where(x => x.Id == cmd.Id)
                .FirstOrDefaultAsync();

            if (timesheet == null)
            {
                timesheet = new Timesheet
                {
                    WeekDay = cmd.WeekDay,
                    Name = cmd.Name,
                };

                _dbContext.Timesheets.Add(timesheet);
            }

            await _dbContext.SaveChangesAsync();

            return timesheet.Id;
        }

        public async Task DeleteTimesheet(Guid id)
        {
            var timesheet = await _dbContext.Timesheets
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
            if (timesheet != null)
            {
                _dbContext.Timesheets.Remove(timesheet);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}