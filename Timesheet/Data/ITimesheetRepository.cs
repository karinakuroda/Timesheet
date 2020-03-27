namespace Timesheet.Data
{
    using System;
    using System.Threading.Tasks;
    using Timesheet.Domain;

    public interface ITimesheetRepository
    {
        Task<Timesheet> ProcessAsync(Timesheet timesheet);

        Task<Timesheet> GetAsync(Guid id);
    }
}
