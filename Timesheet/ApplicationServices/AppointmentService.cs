namespace Timesheet.ApplicationServices
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.JsonPatch;
    using Timesheet.ApplicationServices.DTO;
    using Timesheet.ApplicationServices.Interfaces;
    using Timesheet.Data;
    using Timesheet.Domain;
    using Timesheet.Domain.Builders;

    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository appointmentRepository;

        private readonly IAppointmentBuilder appointmentBuilder;

        public AppointmentService(IAppointmentRepository appointmentRepository, IAppointmentBuilder appointmentBuilder)
        {
            this.appointmentRepository = appointmentRepository;
            this.appointmentBuilder = appointmentBuilder;
        }

        public Task<Appointment> PostAsync(Guid timesheetId, AppointmentDTO appointmentDTO)
        {
            var appointment = this.appointmentBuilder
                .SetProjectId(appointmentDTO.ProjectId)
                .SetDescription(appointmentDTO.Description)
                .SetTimesheetId(timesheetId)
                .SetStart(appointmentDTO.Start)
                .SetEnd(appointmentDTO.End)
                .Build();

            return this.appointmentRepository.AddAsync(appointment);
        }

        public Task<Appointment> GetByIdAsync(Guid timesheetId, Guid id)
        {
            return this.appointmentRepository.GetByIdAsync(timesheetId, id);
        }

        public Task<List<Appointment>> GetAllAsync(Guid timesheetId, AppointmentFilterDTO filterDto)
        {
            return this.appointmentRepository.GetAllAsync(timesheetId, filterDto);
        }

        public Task DeleteAsync(Guid timesheetId, Guid id)
        {
            return this.appointmentRepository.DeleteAsync(timesheetId, id);
        }

        public async Task PatchAsync(Guid timesheetId, Guid id, JsonPatchDocument<Appointment> patchDocument)
        {
            var appointment = await this.GetByIdAsync(timesheetId, id);
            patchDocument.ApplyTo(appointment);

            await this.appointmentRepository.PartialUpdateAsync(timesheetId, id, appointment);
        }

        public Task PutAsync(Guid timesheetId, Guid id, AppointmentDTO appointmentDto)
        {
            var appointment = this.appointmentBuilder
                .SetId(id)
                .SetTimesheetId(timesheetId)
                .SetProjectId(appointmentDto.ProjectId)
                .SetDescription(appointmentDto.Description)
                .SetStart(appointmentDto.Start)
                .SetEnd(appointmentDto.End)
                .Build();

            return this.appointmentRepository.UpdateAsync(timesheetId, id, appointment);
        }
    }
}
