namespace Timesheet.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Timesheet.Domain;

    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly TimesheetContext context;

        public AppointmentRepository(TimesheetContext context)
        {
            this.context = context;
        }

        public async Task<Appointment> ProcessAsync(Appointment appointment)
        {
            this.context.Add(appointment);
            await this.context.SaveChangesAsync();
            return appointment;
        }

        public Task<Appointment> GetByIdAsync(Guid timesheetId, Guid id)
        {
            return this.context.Appointments.Where(w => w.TimesheetId == timesheetId && w.Id == id).SingleOrDefaultAsync();
        }

        public Task<List<Appointment>> GetAllAsync(Guid timesheetId)
        {
            return this.context.Appointments.Include(i => i.Project).Where(w => w.TimesheetId == timesheetId)
                .ToListAsync();
        }
    }
}
