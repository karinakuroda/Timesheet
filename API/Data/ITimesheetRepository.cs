namespace Timesheet.Data
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Timesheet.ApplicationServices.DTO;
    using Timesheet.Domain;

    public interface ITimesheetRepository
    {
        Task<Timesheet> PostAsync(Timesheet timesheet);

        ValueTask<Timesheet> GetAsync(Guid id);

        Task<List<Timesheet>> GetAllAsync(TimesheetFilterDTO filterDto);
    }
}
