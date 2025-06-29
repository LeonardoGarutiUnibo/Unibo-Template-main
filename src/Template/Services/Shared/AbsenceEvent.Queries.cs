using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Template.Infrastructure;

namespace Template.Services.Shared
{
    public class AbsenceEventsSelectQuery
    {
        public Guid IdCurrentAbsenceEvent { get; set; }
        public string Filter { get; set; }
    }

    public class AbsenceEventsSelectDTO
    {
        public IEnumerable<AbsenceEvent> AbsenceEvents { get; set; }
        public int Count { get; set; }
        public class AbsenceEvent{
            public Guid Id { get; set; }
            public Guid UserId { get; set; }
            public DateTime RequestDate { get; set; }
            public DateTime StartEventDate { get; set; }
            public DateTime EndEventDate { get; set; }
            public string EventType { get; set; }
            public string EventState { get; set; }
        }
    }

    public class AbsenceEventsIndexQuery
    {
        public Guid IdCurrentAbsenceEvent { get; set; }
        public string Filter { get; set; }
        public Paging Paging { get; set; }

        public DateTime? StartEventDate { get; set; }
        public DateTime? EndEventDate { get; set; }
        public string EventType { get; set; }
        public string EventState { get; set; }
        public List<Guid> UserId { get; set; }
    }


    public class AbsenceEventsIndexDTO
    {
        public IEnumerable<AbsenceEvent> AbsenceEvents { get; set; }
        public int Count { get; set; }
        public class AbsenceEvent
        {
            public Guid Id { get; set; }
            public Guid UserId { get; set; }
            public DateTime RequestDate { get; set; }
            public DateTime StartEventDate { get; set; }
            public DateTime EndEventDate { get; set; }
            public string EventType { get; set; }
            public string EventState { get; set; }
        }
    }

    public class AbsenceEventDetailQuery
    {
        public Guid Id { get; set; }
    }

    public class AbsenceEventDetailDTO {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime StartEventDate { get; set; }
        public DateTime EndEventDate { get; set; }
        public string EventType { get; set; }
        public string EventState { get; set; }
    }
    public partial class SharedService
    {
        /// <summary>
        /// Returns users for a select field
        /// </summary>
        /// <param name="qry"></param>
        /// <returns></returns>
        public async Task<AbsenceEventsSelectDTO> QueryTeam(AbsenceEventsSelectQuery qry)
        {
            var queryable = _dbContext.AbsenceEvents
                .Where(x => x.Id != qry.IdCurrentAbsenceEvent);


            return new AbsenceEventsSelectDTO
            {
                AbsenceEvents = await queryable
                .Select(x => new AbsenceEventsSelectDTO.AbsenceEvent
                {
                    Id = x.Id,
                }).ToArrayAsync(),
                Count = await queryable.CountAsync(),
            };
        }

        /// <summary>
        /// Returns users for an index page
        /// </summary>
        /// <param name="qry"></param>
        /// <returns></returns>
        public async Task<AbsenceEventsIndexDTO> Query(AbsenceEventsIndexQuery qry)
        {
            var queryable = _dbContext.AbsenceEvents.AsQueryable();

            if (qry.IdCurrentAbsenceEvent != Guid.Empty)
                queryable = queryable.Where(x => x.Id != qry.IdCurrentAbsenceEvent);

            if (qry.StartEventDate.HasValue)
                queryable = queryable.Where(x => x.StartEventDate >= qry.StartEventDate.Value);

            if (qry.EndEventDate.HasValue)
                queryable = queryable.Where(x => x.EndEventDate <= qry.EndEventDate.Value);

            if (!string.IsNullOrEmpty(qry.EventType))
                queryable = queryable.Where(x => x.EventType == qry.EventType);

            if (qry.UserId != null && qry.UserId.Any())
                queryable = queryable.Where(x => qry.UserId.Contains(x.UserId));

            var items = await queryable
                .ApplyPaging(qry.Paging)
                .Select(x => new AbsenceEventsIndexDTO.AbsenceEvent
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    RequestDate = x.RequestDate,
                    StartEventDate = x.StartEventDate,
                    EndEventDate = x.EndEventDate,
                    EventType = x.EventType,
                    EventState = x.EventState
                })
                .ToArrayAsync();

            return new AbsenceEventsIndexDTO
            {
                AbsenceEvents = items,
                Count = await queryable.CountAsync()
            };
        }

        /// <summary>
        /// Returns the detail of the user who matches the Id passed in the qry parameter
        /// </summary>
        /// <param name="qry"></param>
        /// <returns></returns>
        public async Task<AbsenceEventDetailDTO> Query(AbsenceEventDetailQuery qry)
        {
            return await _dbContext.AbsenceEvents
                .Where(x => x.Id == qry.Id)
                .Select(x => new AbsenceEventDetailDTO
                {
                    Id = x.Id,
                })
                .FirstOrDefaultAsync();
        }
        public async Task SaveAbsenceEvent(AbsenceEvent absenceEvent)
        {
            _dbContext.AbsenceEvents.Add(absenceEvent);
            await _dbContext.SaveChangesAsync();
        }
    }
}
