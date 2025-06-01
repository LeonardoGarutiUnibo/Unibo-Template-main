using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Template.Infrastructure;

namespace Template.Services.Shared
{
    public class TimesheetsSelectQuery
    {
        public Guid IdCurrentTimesheet { get; set; }
        public string Filter { get; set; }
    }

    public class TimesheetsSelectDTO
    {
        public IEnumerable<Timesheet> Timesheets { get; set; }
        public int Count { get; set; }

        public class Timesheet
        {
            public Guid Id { get; set; }
            public string WeekDay { get; set; }
            public string Name { get; set; }
        }
    }

    public class TimesheetsIndexQuery
    {
        public Guid IdCurrentTimesheet { get; set; }
        public string Filter { get; set; }

        public Paging Paging { get; set; }
    }


    public class TimesheetsIndexDTO
    {
        public IEnumerable<Timesheet> Timesheets { get; set; }
        public int Count { get; set; }
        public class Timesheet
        {
            public Guid Id { get; set; }
            public string WeekDay { get; set; }
            public string Name { get; set; }
        }
    }

    public class TimesheetDetailQuery
    {
        public Guid Id { get; set; }
    }

    public class TimesheetDetailDTO
    {
        public Guid Id { get; set; }
        public string WeekDay { get; set; }
        public string Name { get; set; }
    }

    public partial class SharedService
    {
        /// <summary>
        /// Returns users for a select field
        /// </summary>
        /// <param name="qry"></param>
        /// <returns></returns>
        public async Task<TimesheetsSelectDTO> QueryTimesheet(TimesheetsSelectQuery qry)
        {
            var queryable = _dbContext.Timesheets
                .Where(x => x.Id != qry.IdCurrentTimesheet);


            return new TimesheetsSelectDTO
            {
                Timesheets = await queryable
                .Select(x => new TimesheetsSelectDTO.Timesheet
                {
                    Id = x.Id,
                    WeekDay = x.WeekDay,
                    Name = x.Name
                }).ToArrayAsync(),
                Count = await queryable.CountAsync(),
            };
        }

        /// <summary>
        /// Returns users for an index page
        /// </summary>
        /// <param name="qry"></param>
        /// <returns></returns>
        public async Task<TimesheetsIndexDTO> Query(TimesheetsIndexQuery qry)
        {
            var queryable = _dbContext.Timesheets
                .Where(x => x.Id != qry.IdCurrentTimesheet);
            
            return new TimesheetsIndexDTO
            {
                Timesheets = await queryable
                    .ApplyPaging(qry.Paging)
                    .Select(x => new TimesheetsIndexDTO.Timesheet
                    {
                        Id = x.Id,
                        Name = x.Name,
                        WeekDay = x.WeekDay,
                    })
                    .ToArrayAsync(),
                Count = await queryable.CountAsync()
            };
        }

        /// <summary>
        /// Returns the detail of the user who matches the Id passed in the qry parameter
        /// </summary>
        /// <param name="qry"></param>
        /// <returns></returns>
        public async Task<TimesheetDetailDTO> Query(TimesheetDetailQuery qry)
        {
            return await _dbContext.Timesheets
                .Where(x => x.Id == qry.Id)
                .Select(x => new TimesheetDetailDTO{
                    Id = x.Id,
                    WeekDay = x.WeekDay,
                    Name = x.Name
                })
                .FirstOrDefaultAsync();
        }
    }
}
