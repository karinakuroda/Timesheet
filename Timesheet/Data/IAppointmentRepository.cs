namespace Timesheet.Data
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Timesheet.ApplicationServices.DTO;
    using Timesheet.Domain;

    public interface IAppointmentRepository
    {
        Task<Appointment> AddAsync(Appointment appointment);

        Task<Appointment> GetByIdAsync(Guid timesheetId, Guid id);

        Task<List<Appointment>> GetAllAsync(Guid timesheetId, AppointmentFilterDTO filterDto);

        Task DeleteAsync(Guid timesheetId, Guid id);

        Task PartialUpdateAsync(Guid timesheetId, Guid id, Appointment appointment);

        Task UpdateAsync(Guid timesheetId, Guid id, Appointment appointment);
    }
}
