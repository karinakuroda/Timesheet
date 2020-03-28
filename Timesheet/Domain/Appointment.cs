namespace Timesheet.Domain
{
    using System;

    public class Appointment
    {
        public Guid Id { get; set; }

        public Guid TimesheetId { get; set; }

        public DateTime Start { get; set; }

        public DateTime? End { get; set; }

        public TimeSpan? TimeSpent
        {
            get
            {
                if (this.End.HasValue)
                {
                    TimeSpan diff = this.End.Value - this.Start;
                    return diff;
                }

                return null;
            }
        }

        public int ProjectId { get; set; }

        public Project Project { get; set; }

        public string Description { get; set; }

        public void Validate()
        {
            this.HasValidDates();
        }

        private void HasValidDates()
        {
            this.StartBiggerThanEnd();
            this.DatesBiggerThanNow();
            this.StartShouldLimit15daysFromNow();
        }

        private void StartBiggerThanEnd()
        {
            if (this.End.HasValue && this.Start > this.End)
            {
                throw new ArgumentException("Date start is bigger than date end");
            }
        }

        private void DatesBiggerThanNow()
        {
            if (this.Start > DateTime.Now ||
                (this.End.HasValue && this.End > DateTime.Now))
            {
                throw new ArgumentException("Date is bigger than now");
            }
        }

        private void StartShouldLimit15daysFromNow()
        {
            var dateLimit = DateTime.Now.AddDays(-15);
            
            if (dateLimit > this.Start)
            {
                throw new ArgumentException("Date start has a limit of 15 days from now'");
            }
        }
    }
}
