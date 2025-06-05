using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Template.Services.Shared
{
    public class AddOrUpdateAbsenceEventCommand
    {
        public Guid? Id { get; set; }
        
        public Guid UserId { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime StartEventDate { get; set; }

        public DateTime EndEventDate { get; set; }
        public string EventType { get; set; }
        public string EventState { get; set; }
    }

    public partial class SharedService
    {
        public async Task<Guid> HandleAbsenceEvent(AddOrUpdateAbsenceEventCommand cmd)
        {
            var absenceEvent = await _dbContext.AbsenceEvents
                .Where(x => x.Id == cmd.Id)
                .FirstOrDefaultAsync();

            if (absenceEvent == null)
            {
                absenceEvent = new AbsenceEvent
                {
                    UserId       = cmd.UserId,
                    RequestDate     = cmd.RequestDate,
                    StartEventDate  = cmd.StartEventDate,
                    EndEventDate    = cmd.EndEventDate,
                    EventType       = cmd.EventType,
                    EventState      = cmd.EventState,
                };

                _dbContext.AbsenceEvents.Add(absenceEvent);
            }

            await _dbContext.SaveChangesAsync();

            return absenceEvent.Id;
        }
    }
}