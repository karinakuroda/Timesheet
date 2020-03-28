namespace Timesheet.ApplicationServices
{
    using System;
    using System.Collections.Generic;
    using Timesheet.ApplicationServices.DTO;
    using Timesheet.ApplicationServices.Interfaces;

    public class AppointmentValidator : IAppointmentValidator
    {
        private AppointmentDTO appointmentDto;

        public AppointmentValidator()
        {
            this.ErrorList = new List<string>();
        }

        public List<string> ErrorList { get; set; }

        public bool IsValid(AppointmentDTO dto)
        {
            this.appointmentDto = dto;

            return this.HasValidObject() && this.HasValidDates() && this.HasValidProjectId();
        }

        private bool HasValidObject()
        {
            if (this.appointmentDto != null)
            {
                return true;
            }

            this.ErrorList.Add("Invalid Appointment");
            return false;
        }

        private bool HasValidProjectId()
        {
            if (this.appointmentDto.ProjectId > 0)
            {
                return true;
            }

            this.ErrorList.Add("Invalid Project Id");
            return false;
        }

        private bool HasValidDates()
        {
            var startIsValid = this.appointmentDto.Start != default(DateTime);

            if (!this.appointmentDto.End.HasValue && startIsValid)
            {
                return true;
            }

            var endIsValid = this.appointmentDto.End != default(DateTime);

            if (startIsValid && endIsValid)
            {
                return true;
            }

            this.ErrorList.Add("Invalid Dates");
            return false;
        }
    }
}
