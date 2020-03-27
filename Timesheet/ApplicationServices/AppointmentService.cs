namespace Timesheet.ApplicationServices
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
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

        public Task<Appointment> ProcessAsync(Guid timesheetId, AppointmentDTO appointmentDTO)
        {
            var appointment = this.appointmentBuilder
                .SetProjectId(appointmentDTO.ProjectId)
                .SetDescription(appointmentDTO.Description)
                .SetTimesheetId(timesheetId)
                .SetStart(appointmentDTO.Start)
                .SetEnd(appointmentDTO.End)
                .Build();

            return this.appointmentRepository.ProcessAsync(appointment);
        }

        public Task<Appointment> GetByIdAsync(Guid timesheetId, Guid id)
        {
            return this.appointmentRepository.GetByIdAsync(timesheetId, id);
        }

        public Task<List<Appointment>> GetAllAsync(Guid timesheetId)
        {
            return this.appointmentRepository.GetAllAsync(timesheetId);
        }
    }
}
