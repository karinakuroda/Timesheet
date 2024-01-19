namespace Timesheet.ApplicationServices.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.JsonPatch;
    using Timesheet.ApplicationServices.DTO;
    using Timesheet.Domain;

    public interface IAppointmentService
    {
        Task<Appointment> PostAsync(Guid timesheetId, AppointmentDTO appointment);

        Task<Appointment> GetByIdAsync(Guid timesheetId, Guid id);

        Task<List<Appointment>> GetAllAsync(Guid timesheetId, AppointmentFilterDTO filterDto);

        Task DeleteAsync(Guid timesheetId, Guid id);

        Task PatchAsync(Guid timesheetId, Guid id, JsonPatchDocument<Appointment> patchDocument);

        Task PutAsync(Guid timesheetId, Guid id, AppointmentDTO appointment);
    }
}
