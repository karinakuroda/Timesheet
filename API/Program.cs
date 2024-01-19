using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Timesheet.ApplicationServices;
using Timesheet.ApplicationServices.DTO;
using Timesheet.ApplicationServices.Interfaces;
using Timesheet.Data;
using Timesheet.Domain.Builders;
using Swashbuckle.AspNetCore.Swagger;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Timesheet API",
        Description = "Timesheet API"
    });
});
builder.Services.AddControllers();
builder.Services.AddScoped<IAppointmentService, AppointmentService>();
builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddScoped<IAppointmentBuilder, AppointmentBuilder>();
builder.Services.AddScoped<IAppointmentValidator, AppointmentValidator>();
builder.Services.AddScoped<ITimesheetService, TimesheetService>();
builder.Services.AddScoped<ITimesheetRepository, TimesheetRepository>();
var connection = builder.Configuration.GetConnectionString("DefaultConnectionPsg");
builder.Services.AddDbContext<TimesheetContext>(opt => opt.UseNpgsql(connection));

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.MapGet("/Timesheets", async (ITimesheetService timesheetService) =>
    {
        var result = await timesheetService.GetAllAsync(new TimesheetFilterDTO());
        return result == null ? Results.NotFound() : Results.Ok(result);
    }).WithDescription("Some Method Description")
    .WithOpenApi();
;

app.Run();