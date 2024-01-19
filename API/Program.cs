using Microsoft.EntityFrameworkCore;
using Timesheet.ApplicationServices;
using Timesheet.ApplicationServices.DTO;
using Timesheet.ApplicationServices.Interfaces;
using Timesheet.Data;
using Timesheet.Domain.Builders;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IAppointmentService, AppointmentService>();
builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddScoped<IAppointmentBuilder, AppointmentBuilder>();
builder.Services.AddScoped<IAppointmentValidator, AppointmentValidator>();
builder.Services.AddScoped<ITimesheetService, TimesheetService>();
builder.Services.AddScoped<ITimesheetRepository, TimesheetRepository>();
var connection = builder.Configuration.GetConnectionString("DefaultConnectionPsg");
builder.Services.AddDbContext<TimesheetContext>(opt => opt.UseNpgsql(connection));
// builder.Services.AddDatabaseDeveloperPageExceptionFilter();
var app = builder.Build();

app.MapGet("/Timesheets", async (ITimesheetService timesheetService) =>
{
    var result = await timesheetService.GetAllAsync(new TimesheetFilterDTO());
    return result == null ? Results.NotFound() : Results.Ok(result);
});

app.Run();