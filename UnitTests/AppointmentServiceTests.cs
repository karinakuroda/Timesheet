namespace UnitTests
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;
    using AutoFixture;
    using FluentAssertions;
    using Microsoft.AspNetCore.JsonPatch;
    using Moq;
    using Timesheet.ApplicationServices;
    using Timesheet.ApplicationServices.DTO;
    using Timesheet.Data;
    using Timesheet.Domain;
    using Timesheet.Domain.Builders;
    using Xunit;

    [ExcludeFromCodeCoverage]
    public class AppointmentServiceTests
    {
        private readonly Fixture fixture;
        private readonly Mock<IAppointmentRepository> appointmentRepositoryMock;
        private readonly AppointmentService appointmentService;
        private readonly AppointmentBuilder appointmentBuilder;

        public AppointmentServiceTests()
        {
            this.fixture = new Fixture();
            this.appointmentRepositoryMock = new Mock<IAppointmentRepository>();
            this.appointmentBuilder = new AppointmentBuilder();
            this.appointmentService = new AppointmentService(
                this.appointmentRepositoryMock.Object,
                this.appointmentBuilder);
        }

        [Fact]
        public async Task PostAsync_WithValidAppointment_ShouldCallRepositoryPostAsync()
        {
            // Arrange
            var timesheetId = this.fixture.Create<Guid>();
            var appointmentDto = this.fixture.Build<AppointmentDTO>().With(w => w.Start, DateTime.Now).Without(w => w.End).Create();
            var appointment = this.fixture.Create<Appointment>();

            this.appointmentRepositoryMock.Setup(s => s.AddAsync(It.IsAny<Appointment>())).ReturnsAsync(appointment);

            // Act
            var result = await this.appointmentService.PostAsync(timesheetId, appointmentDto);

            // Assert
            this.appointmentRepositoryMock.Verify(v => v.AddAsync(It.IsAny<Appointment>()), Times.Once);
        }

        [Fact]
        public async Task PatchAsync_WithValidAppointment_ShouldCallGetAndPatch()
        {
            // Arrange
            var timesheetId = this.fixture.Create<Guid>();
            var id = this.fixture.Create<Guid>();
            var appointment = this.fixture.Create<Appointment>();
            var doc = new JsonPatchDocument<Appointment>();

            this.appointmentRepositoryMock.Setup(s => s.GetByIdAsync(timesheetId, id)).ReturnsAsync(appointment).Verifiable();
            this.appointmentRepositoryMock.Setup(s => s.PartialUpdateAsync(timesheetId, id, It.IsAny<Appointment>())).Verifiable();

            // Act
            await this.appointmentService.PatchAsync(timesheetId, id, doc);

            // Assert
            this.appointmentRepositoryMock.VerifyAll();
        }

        [Fact]
        public async Task PostAsync_WithValidAppointment_ShouldReturnAsExpected()
        {
            // Arrange
            var timesheetId = this.fixture.Create<Guid>();
            var appointmentDto = this.fixture.Build<AppointmentDTO>().With(w => w.Start, DateTime.Now).Without(w => w.End).Create();
            var appointment = this.fixture.Create<Appointment>();

            this.appointmentRepositoryMock.Setup(s => s.AddAsync(It.IsAny<Appointment>())).ReturnsAsync(appointment);

            // Act
            var result = await this.appointmentService.PostAsync(timesheetId, appointmentDto);

            // Assert
            result.Should().BeEquivalentTo(appointment);
        }

    }
}
