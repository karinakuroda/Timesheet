namespace UnitTests
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using AutoFixture;
    using FluentAssertions;
    using Timesheet.ApplicationServices;
    using Timesheet.ApplicationServices.DTO;
    using Xunit;

    [ExcludeFromCodeCoverage]
    public class AppointmentBuilderTests
    {
        private readonly Fixture fixture;
        private readonly AppointmentValidator appointmentValidator;

        public AppointmentBuilderTests()
        {
            this.fixture = new Fixture();
            this.appointmentValidator = new AppointmentValidator();
        }

        //[Fact]
        //public void IsValid_WithEmptyObject_ShouldReturnFalse()
        //{
        //    // Act;
        //    var result = this.appointmentValidator.IsValid(null);

        //    // Assert
        //    result.Should().BeFalse();
        //    this.appointmentValidator.ErrorList.Should().HaveCount(1).And.Contain("Invalid Appointment");
        //}

        //[Fact]
        //public void IsValid_WithEmptyProjectId_ShouldReturnFalse()
        //{
        //    // Arrange
        //    var appointmentDto = this.fixture.Build<AppointmentDTO>().With(w => w.ProjectId, 0).Create();

        //    // Act;
        //    var result = this.appointmentValidator.IsValid(appointmentDto);

        //    // Assert
        //    result.Should().BeFalse();
        //    this.appointmentValidator.ErrorList.Should().HaveCount(1).And.Contain("Invalid Project Id");
        //}

        //[Fact]
        //public void IsValid_WithEmptyDates_ShouldReturnFalse()
        //{
        //    // Arrange
        //    var appointmentDto = this.fixture.Build<AppointmentDTO>().Without(w => w.Start).Without(w => w.End).Create();
            
        //    // Act;
        //    var result = this.appointmentValidator.IsValid(appointmentDto);

        //    // Assert
        //    result.Should().BeFalse();
        //    this.appointmentValidator.ErrorList.Should().HaveCount(1).And.Contain("Invalid Dates");
        //}
    }
}
