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
            if (this.IsNull() || 
                this.StartBiggerThanEnd() ||
                this.DatesBiggerThanNow())
            {
                this.ErrorList.Add("Invalid Date");
                return false;
            }

            return true;
        }

        private bool StartBiggerThanEnd()
        {
            if (this.appointmentDto.Start >= this.appointmentDto.End)
            {
                return true;
            }

            return false;
        }

        private bool DatesBiggerThanNow()
        {
            if (this.appointmentDto.Start > DateTime.Now || this.appointmentDto.End > DateTime.Now)
            {
                return true;
            }

            return false;
        }

        private bool IsNull()
        {
            if (this.appointmentDto.End == default(DateTime) || this.appointmentDto.Start == default(DateTime))
            {
                return true;
            }

            return false;
        }
    }
}
