using System;
using System.ComponentModel.DataAnnotations;
using Template.Services.Shared;
using Template.Web.Infrastructure;

namespace Template.Web.Areas.Example.AbsenceEvents
{
    [TypeScriptModule("Example.AbsenceEvents.Server")]
    public class EditViewModel
    {
        public EditViewModel()
        {
        }

        public Guid? Id { get; set; }
        public string UserId { get; set; }
        public string RequestDate { get; set; }
        public string StartEventDate { get; set; }

        public string EndDateEvent { get; set; }
        public string EventType { get; set; }
        public string EventState { get; set; }

        public string ToJson()
        {
            return Infrastructure.JsonSerializer.ToJsonCamelCase(this);
        }

        public void SetAbsenceEvent(AbsenceEventDetailDTO absenceEventDetailDTO)
        {
            if (absenceEventDetailDTO != null)
            {
                Id = absenceEventDetailDTO.Id;
                UserId = absenceEventDetailDTO.UserId;
                RequestDate =  absenceEventDetailDTO.RequestDate;
                StartEventDate = absenceEventDetailDTO.StartEventDate;
                EndDateEvent = absenceEventDetailDTO.EndDateEvent;
                EventType = absenceEventDetailDTO.EventType;
                EventState = absenceEventDetailDTO.EventState;

            }
        }

        public AddOrUpdateAbsenceEventCommand ToAddOrUpdateAbsenceEventCommand()
        {
            return new AddOrUpdateAbsenceEventCommand
            {
                Id = Id,
                UserId = UserId,
		        RequestDate = RequestDate,
                StartEventDate = StartEventDate,
                EndDateEvent = EndDateEvent,
                EventType = EventType,
                EventState = EventState
            };
        }
    }
}