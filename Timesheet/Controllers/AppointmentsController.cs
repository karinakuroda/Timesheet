namespace Timesheet.Controllers
{
    using System;
    using System.Net.Mime;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.JsonPatch;
    using Microsoft.AspNetCore.Mvc;
    using Timesheet.ApplicationServices.DTO;
    using Timesheet.ApplicationServices.Interfaces;
    using Timesheet.Domain;

    public class AppointmentsController : Controller
    {
        private readonly IAppointmentService appointmentService;

        private readonly IAppointmentValidator appointmentValidator;

        public AppointmentsController(IAppointmentService appointmentService, IAppointmentValidator appointmentValidator)
        {
            this.appointmentService = appointmentService;
            this.appointmentValidator = appointmentValidator;
        }

        [HttpGet("timesheets/{timesheetId}/appointments")]
        [ProducesResponseType(typeof(Appointment), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllAsync(Guid timesheetId)
        {
            var appointments = await this.appointmentService.GetAllAsync(timesheetId);

            if (appointments == null)
            {
                return this.NotFound();
            }

            return this.Ok(appointments);
        }

        [HttpGet("timesheets/{timesheetId}/appointments/{id}")]
        [ProducesResponseType(typeof(Appointment), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByIdAsync(Guid timesheetId, Guid id)
        {
            var appointment = await this.appointmentService.GetByIdAsync(timesheetId, id);

            if (appointment == null)
            {
                return this.NotFound();
            }

            return this.Ok(appointment);
        }

        [HttpPost("timesheets/{timesheetId}/appointments")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostAsync([FromRoute] Guid timesheetId, [FromBody] AppointmentDTO request)
        {
            var isValidAppointment = this.appointmentValidator.IsValid(request);

            if (!isValidAppointment)
            {
                return this.BadRequest(this.appointmentValidator.ErrorList);
            }

            var result = await this.appointmentService.PostAsync(timesheetId, request);

            var routeValues = new
            {
                timesheetId = timesheetId,
                id = result.Id
            };

            return this.CreatedAtAction(nameof(this.GetByIdAsync), routeValues, result);
        }

        [HttpDelete("timesheets/{timesheetId}/appointments/{id}")]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteAsync(Guid timesheetId, Guid id)
        {
            await this.appointmentService.DeleteAsync(timesheetId, id);

            return this.NoContent();
        }

        [HttpPatch("timesheets/{timesheetId}/appointments/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PatchAsync([FromRoute]Guid timesheetId, [FromRoute]Guid id, [FromBody] JsonPatchDocument<Appointment> patchDoc)
        {
            if (patchDoc == null)
            {
                return this.BadRequest();
            }

            await this.appointmentService.PatchAsync(timesheetId, id, patchDoc);

            return this.NoContent();
        }

        [HttpPut("timesheets/{timesheetId}/appointments/{id}")]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put([FromRoute]Guid timesheetId, [FromRoute]Guid id, [FromBody] AppointmentDTO request)
        {
            var isValidAppointment = this.appointmentValidator.IsValid(request);

            if (!isValidAppointment)
            {
                return this.BadRequest(this.appointmentValidator.ErrorList);
            }

            await this.appointmentService.PutAsync(timesheetId, id, request);
            
            return this.NoContent();
        }
    }
}
