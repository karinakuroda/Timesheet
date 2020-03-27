namespace Timesheet.Data
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Timesheet.Domain;

    public interface IAppointmentRepository
    {
        Task<Appointment> ProcessAsync(Appointment appointment);

        Task<Appointment> GetByIdAsync(Guid timesheetId, Guid id);

        Task<List<Appointment>> GetAllAsync(Guid timesheetId);
    }
}
