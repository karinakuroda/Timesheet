namespace Timesheet.Domain.Builders
{
    using System;

    public interface IAppointmentBuilder
    {
        AppointmentBuilder SetProjectId(int projectId);

        AppointmentBuilder SetDescription(string description);

        AppointmentBuilder SetId(Guid id);

        AppointmentBuilder SetTimesheetId(Guid id);

        AppointmentBuilder SetStart(DateTime dateTime);

        AppointmentBuilder SetEnd(DateTime? dateTime);

        Appointment Build();
    }
}
