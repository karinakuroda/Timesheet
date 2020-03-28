namespace Timesheet.ApplicationServices.DTO
{
    using System;

    public class AppointmentFilterDTO
    {
        public int ProjectId { get; set; }

        public string Description { get; set; }

        public DateTime? Start { get; set; }

        public DateTime? End { get; set; }
    }
}
