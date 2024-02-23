namespace Timesheet.Domain
{
    using System;
    using System.Collections.Generic;

    public class Timesheet
    {
        public Guid Id { get; set; }

        public string UserName { get; set; }

        public List<Appointment> Appointments { get; set; }
    }
}
