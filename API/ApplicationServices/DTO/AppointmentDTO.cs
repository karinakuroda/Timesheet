namespace Timesheet.ApplicationServices.DTO
{
    using System;

    public class AppointmentDTO
    {
        public DateTime Start { get; set; }

        public DateTime? End { get; set; }

        public int ProjectId { get; set; }

        public string Description { get; set; }
    }
}
