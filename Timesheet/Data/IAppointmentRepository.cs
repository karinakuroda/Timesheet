namespace Timesheet.Data
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Timesheet.Domain;

    public interface IAppointmentRepository
    {
        Task<Appointment> PostAsync(Appointment appointment);

        Task<Appointment> GetByIdAsync(Guid timesheetId, Guid id);

        Task<List<Appointment>> GetAllAsync(Guid timesheetId);

        Task DeleteAsync(Guid timesheetId, Guid id);

        Task PatchAsync(Guid timesheetId, Guid id, Appointment appointment);

        Task PutAsync(Guid timesheetId, Guid id, Appointment appointment);
    }
}
