namespace Timesheet.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Timesheet.ApplicationServices.DTO;
    using Timesheet.Domain;

    public class TimesheetRepository : ITimesheetRepository
    {
        private readonly TimesheetContext context;

        public TimesheetRepository(TimesheetContext context)
        {
            this.context = context;
        }

        public async Task<Timesheet> PostAsync(Timesheet timesheet)
        {
            this.context.Add(timesheet);
            await this.context.SaveChangesAsync();
            return timesheet;
        }

        public Task<Timesheet> GetAsync(Guid id)
        {
            return this.context.Timesheets.FindAsync(id);
        }

        public Task<List<Timesheet>> GetAllAsync(TimesheetFilter filter)
        {
            return this.context.Timesheets
                .Where(a => a.UserName == filter.Username || string.IsNullOrWhiteSpace(filter.Username))
                .ToListAsync();
        }
    }
}
