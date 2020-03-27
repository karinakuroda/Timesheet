namespace Timesheet.Domain
{
    using System;

    public class Appointment
    {
        public Guid Id { get; set; }

        public Guid TimesheetId { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public TimeSpan TimeSpent
        {
            get
            {
                TimeSpan diff = this.End - this.Start;
                return diff;
            }
        }

        public int ProjectId { get; set; }

        public Project Project { get; set; }

        public string Description { get; set; }
    }
}
