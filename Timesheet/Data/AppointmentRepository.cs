namespace Timesheet.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Timesheet.ApplicationServices.DTO;
    using Timesheet.Domain;

    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly TimesheetContext context;

        public AppointmentRepository(TimesheetContext context)
        {
            this.context = context;
        }

        public async Task<Appointment> AddAsync(Appointment appointment)
        {
            this.context.Add(appointment);
            await this.context.SaveChangesAsync();
            return appointment;
        }

        public Task<Appointment> GetByIdAsync(Guid timesheetId, Guid id)
        {
            return this.context.Appointments.Where(w => w.TimesheetId == timesheetId && w.Id == id).SingleOrDefaultAsync();
        }

        public Task<List<Appointment>> GetAllAsync(Guid timesheetId, AppointmentFilterDTO filterDto)
        {
            return this.context.Appointments.Include(i => i.Project)
                .Where(w => w.TimesheetId == timesheetId || timesheetId == default(Guid))
                .Where(w => w.ProjectId == filterDto.ProjectId || filterDto.ProjectId == default(int))
                .Where(w => w.Description == filterDto.Description || filterDto.Description == default(string))
                .Where(w => w.Start >= filterDto.Start || filterDto.Start == default(DateTime?))
                .Where(w => w.End <= filterDto.End || filterDto.End == default(DateTime?))
                .OrderByDescending(o => o.Start).ToListAsync();
        }

        public async Task DeleteAsync(Guid timesheetId, Guid id)
        {
            var appointment = await this.GetByIdAsync(timesheetId, id);
            this.context.Remove(appointment);
            await this.context.SaveChangesAsync();
        }

        public Task PartialUpdateAsync(Guid timesheetId, Guid id, Appointment appointment)
        {
            this.context.Entry(appointment).State = EntityState.Modified;
            this.context.SaveChangesAsync();

            return Task.CompletedTask;
        }

        public async Task UpdateAsync(Guid timesheetId, Guid id, Appointment appointment)
        {
            var old = await this.GetByIdAsync(timesheetId, id);
            this.context.Entry(old).CurrentValues.SetValues(appointment);
            await this.context.SaveChangesAsync();
        }
    }
}
