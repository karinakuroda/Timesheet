namespace Timesheet.ApplicationServices.Interfaces
{
    using System.Collections.Generic;
    using Timesheet.ApplicationServices.DTO;

    public interface IAppointmentValidator
    {
        List<string> ErrorList { get; set; }

        bool IsValid(AppointmentDTO appointmentDto);
    }
}
