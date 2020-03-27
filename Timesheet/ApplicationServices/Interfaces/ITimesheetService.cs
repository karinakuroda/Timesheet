namespace Timesheet.ApplicationServices.Interfaces
{
    using System;
    using System.Threading.Tasks;
    using Timesheet.Domain;

    public interface ITimesheetService
    {
        Task<Timesheet> ProcessAsync(Timesheet timesheet);

        Task<Timesheet> GetAsync(Guid id);
    }
}
