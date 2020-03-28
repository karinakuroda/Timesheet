namespace Timesheet.ApplicationServices
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Timesheet.ApplicationServices.DTO;
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

        public Task<Timesheet> PostAsync(Timesheet timesheet)
        {
            return this.timesheetRepository.PostAsync(timesheet);
        }

        public Task<Timesheet> GetAsync(Guid id)
        {
            return this.timesheetRepository.GetAsync(id);
        }

        public Task<List<Timesheet>> GetAllAsync(TimesheetFilter filter)
        {
            return this.timesheetRepository.GetAllAsync(filter);
        }
    }
}
