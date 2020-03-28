namespace Timesheet.Domain.Builders
{
    using System;

    public class AppointmentBuilder : IAppointmentBuilder
    {
        private readonly Appointment appointment;

        public AppointmentBuilder()
        {
            this.appointment = new Appointment();
        }

        public AppointmentBuilder SetId(Guid id)
        {
            this.appointment.Id = id;
            return this;
        }

        public AppointmentBuilder SetTimesheetId(Guid timesheetId)
        {
            this.appointment.TimesheetId = timesheetId;
            return this;
        }

        public AppointmentBuilder SetStart(DateTime dateTime)
        {
            this.appointment.Start = dateTime;
            return this;
        }

        public AppointmentBuilder SetEnd(DateTime? dateTime)
        {
            if (dateTime.HasValue)
            {
                this.appointment.End = dateTime;
            }

            return this;
        }

        public AppointmentBuilder SetProjectId(int projectId)
        {
            this.appointment.ProjectId = projectId;
            return this;
        }

        public AppointmentBuilder SetDescription(string description)
        {
            this.appointment.Description = description;
            return this;
        }

        public Appointment Build()
        {
            this.appointment.Validate();

            return this.appointment;
        }
    }
}
