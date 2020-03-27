namespace Timesheet.ApplicationServices.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Timesheet.ApplicationServices.DTO;
    using Timesheet.Domain;

    public interface IAppointmentService
    {
        Task<Appointment> ProcessAsync(Guid timesheetId, AppointmentDTO appointment);

        Task<Appointment> GetByIdAsync(Guid timesheetId, Guid id);

        Task<List<Appointment>> GetAllAsync(Guid timesheetId);
    }
}
