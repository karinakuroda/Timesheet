namespace Timesheet.ApplicationServices
{
    using System;
    using System.Threading.Tasks;
    using Timesheet.ApplicationServices.Interfaces;
    using Timesheet.Data;
    using Timesheet.Domain;

    public class TimesheetService : ITimesheetService
    {
        private readonly ITimesheetRepository timesheetRepository;

        public TimesheetService(ITimesheetRepository timesheetRepository)
        {
            this.timesheetRepository = timesheetRepository;
        }

        public Task<Timesheet> ProcessAsync(Timesheet timesheet)
        {
            return this.timesheetRepository.ProcessAsync(timesheet);
        }

        public Task<Timesheet> GetAsync(Guid id)
        {
            return this.timesheetRepository.GetAsync(id);
        }
    }
}
