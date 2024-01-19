namespace Timesheet.Controllers
{
    using System;
    using System.Net.Mime;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Timesheet.ApplicationServices.DTO;
    using Timesheet.ApplicationServices.Interfaces;
    using Timesheet.Domain;

    [Route("api/[controller]")]
    public class TimesheetsController : Controller
    {
        private readonly ITimesheetService timesheetService;

        public TimesheetsController(ITimesheetService timesheetService)
        {
            this.timesheetService = timesheetService;
        }
        
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Appointment), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var timesheet = await this.timesheetService.GetAsync(id);

            if (timesheet == null)
            {
                return this.NotFound();
            }

            return this.Ok(timesheet);
        }
        
        [HttpGet]
        [ProducesResponseType(typeof(Appointment), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllAsync([FromQuery] TimesheetFilterDTO filterDto)
        {
            var result = await this.timesheetService.GetAllAsync(filterDto);
            
            if (result == null)
            {
                return this.NotFound();
            }

            return this.Ok(result);
        }
        
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostAsync([FromBody] Timesheet request)
        {
            if (string.IsNullOrWhiteSpace(request.UserName))
            {
                return this.BadRequest("Invalid UserName");
            }

            var result = await this.timesheetService.PostAsync(request);

            return this.CreatedAtAction(nameof(this.GetAsync), new { id = result.Id }, result.Id);
        }
    }
}
