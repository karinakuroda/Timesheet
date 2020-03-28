namespace UnitTests
{
    using System;
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

    public class AppointmentValidatorTests
    {
        private readonly Fixture fixture;
        private readonly AppointmentValidator appointmentValidator;

        public AppointmentValidatorTests()
        {
            this.fixture = new Fixture();
            this.appointmentValidator = new AppointmentValidator();
        }

        [Fact]
        public void IsValid_WithEmptyObject_ShouldReturnFalse()
        {
            // Act;
            var result = this.appointmentValidator.IsValid(null);
            
            // Assert
            Assert.False(result);
            this.appointmentValidator.ErrorList.Should().HaveCount(1).And.Contain("Invalid Appointment");
        }

        [Fact]
        public void IsValid_WithEmptyProjectId_ShouldReturnFalse()
        {
            // Arrange
            var appointmentDto = this.fixture.Build<AppointmentDTO>().With(w => w.ProjectId, 0).Create();

            // Act;
            var result = this.appointmentValidator.IsValid(appointmentDto);

            // Assert
            Assert.False(result);
            this.appointmentValidator.ErrorList.Should().HaveCount(1).And.Contain("Invalid Project Id");
        }

    }
}
