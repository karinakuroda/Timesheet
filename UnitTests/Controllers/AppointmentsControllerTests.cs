namespace UnitTests.Controllers
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;
    using AutoFixture;
    using FluentAssertions;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using Timesheet.ApplicationServices.DTO;
    using Timesheet.ApplicationServices.Interfaces;
    using Timesheet.Controllers;
    using Timesheet.Domain;
    using Xunit;

    [ExcludeFromCodeCoverage]
    public class AppointmentsControllerTests
    {
        private readonly Fixture fixture;
        private readonly Mock<IAppointmentService> appointmentserviceMock;
        private readonly Mock<IAppointmentValidator> appointmentValidatorMock;

        private readonly AppointmentsController appointmentsController;

        public AppointmentsControllerTests()
        {
            this.fixture = new Fixture();
            this.appointmentserviceMock = new Mock<IAppointmentService>();
            this.appointmentValidatorMock = new Mock<IAppointmentValidator>();
            this.appointmentsController = new AppointmentsController(this.appointmentserviceMock.Object, this.appointmentValidatorMock.Object);
        }

        [Fact]
        public async Task GetAsync_WithValidParameters_ShouldReturnOk()
        {
            // Arrange
            var timesheetId = this.fixture.Create<Guid>();
            var id = this.fixture.Create<Guid>();

            this.appointmentserviceMock.Setup(s => s.GetByIdAsync(timesheetId, id)).ReturnsAsync(this.fixture.Create<Appointment>());

            // Act
            var result = await this.appointmentsController.GetByIdAsync(timesheetId, id);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task GetAsync_WithInvalidParameters_ShouldReturnNotFound()
        {
            // Arrange
            var timesheetId = this.fixture.Create<Guid>();
            var id = this.fixture.Create<Guid>();

            // Act
            var result = await this.appointmentsController.GetByIdAsync(timesheetId, id);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task PostAsync_WithValidParameters_ShouldReturnCreatedResult()
        {
            // Arrange
            var timesheetId = this.fixture.Create<Guid>();
            
            var dto = this.fixture.Create<AppointmentDTO>();

            this.appointmentValidatorMock.Setup(s => s.IsValid(dto)).Returns(true);

            this.appointmentserviceMock.Setup(s => s.PostAsync(timesheetId, dto)).ReturnsAsync(this.fixture.Create<Appointment>());
            
            // Act
            var result = await this.appointmentsController.PostAsync(timesheetId, dto);

            // Assert
            result.Should().BeOfType<CreatedAtActionResult>();
        }

        [Fact]
        public async Task PostAsync_WithValidParameters_ShouldCallServiceOnce()
        {
            // Arrange
            var timesheetId = this.fixture.Create<Guid>();
            var dto = this.fixture.Create<AppointmentDTO>();

            this.appointmentValidatorMock.Setup(s => s.IsValid(dto)).Returns(true);
            this.appointmentserviceMock.Setup(s => s.PostAsync(timesheetId, dto)).ReturnsAsync(this.fixture.Create<Appointment>());

            // Act
            await this.appointmentsController.PostAsync(timesheetId, dto);

            // Assert
            this.appointmentserviceMock.Verify(v => v.PostAsync(timesheetId, dto), Times.Once);
        }

        [Fact]
        public async Task PostAsync_WithInvalidParameters_ShouldReturnBadRequest()
        {
            // Arrange
            var timesheetId = this.fixture.Create<Guid>();
            var dto = this.fixture.Create<AppointmentDTO>();

            this.appointmentValidatorMock.Setup(s => s.IsValid(dto)).Returns(false);

            // Act
            var result = await this.appointmentsController.PostAsync(timesheetId, dto);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }
    }
}
