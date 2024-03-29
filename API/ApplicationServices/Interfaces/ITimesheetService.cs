﻿namespace Timesheet.ApplicationServices.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Timesheet.ApplicationServices.DTO;
    using Timesheet.Domain;

    public interface ITimesheetService
    {
        Task<Timesheet> PostAsync(Timesheet timesheet);

        ValueTask<Timesheet> GetAsync(Guid id);

        Task<List<Timesheet>> GetAllAsync(TimesheetFilterDTO filterDto);
    }
}
