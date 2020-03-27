namespace Timesheet.Data
{
    using System;
    using System.Threading.Tasks;
    using Timesheet.Domain;

    public class TimesheetRepository : ITimesheetRepository
    {
        private readonly TimesheetContext context;

        public TimesheetRepository(TimesheetContext context)
        {
            this.context = context;
        }

        public async Task<Timesheet> ProcessAsync(Timesheet timesheet)
        {
            this.context.Add(timesheet);
            await this.context.SaveChangesAsync();
            return timesheet;
        }

        public Task<Timesheet> GetAsync(Guid id)
        {
            return this.context.Timesheets.FindAsync(id);
        }
    }

}
