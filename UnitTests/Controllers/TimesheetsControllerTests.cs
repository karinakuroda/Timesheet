namespace UnitTests.Controllers
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;
    using AutoFixture;
    using FluentAssertions;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using Timesheet.ApplicationServices.Interfaces;
    using Timesheet.Controllers;
    using Timesheet.Domain;
    using Xunit;

    [ExcludeFromCodeCoverage]
    public class TimesheetsControllerTests
    {
        private readonly Fixture fixture;

        private readonly Mock<ITimesheetService> timesheetServiceMock;

        private readonly TimesheetsController timesheetsController;

        public TimesheetsControllerTests()
        {
            this.fixture = new Fixture();
            this.timesheetServiceMock = new Mock<ITimesheetService>();
            this.timesheetsController = new TimesheetsController(this.timesheetServiceMock.Object);
        }

        [Fact]
        public async Task GetAsync_WithValidParameters_ShouldReturnOk()
        {
            // Arrange
            var id = this.fixture.Create<Guid>();

            this.timesheetServiceMock.Setup(s => s.GetAsync(id)).ReturnsAsync(this.fixture.Create<Timesheet>());

            // Act
            var result = await this.timesheetsController.GetAsync(id);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task GetAsync_WithInvalidParameters_ShouldReturnNotFound()
        {
            // Arrange
            var id = this.fixture.Create<Guid>();

            // Act
            var result = await this.timesheetsController.GetAsync(id);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task PostAsync_WithValidParameters_ShouldReturnCreatedResult()
        {
            // Arrange
            var timesheet = this.fixture.Create<Timesheet>();

            this.timesheetServiceMock.Setup(s => s.PostAsync(timesheet)).ReturnsAsync(timesheet);

            // Act
            var result = await this.timesheetsController.PostAsync(timesheet);

            // Assert
            result.Should().BeOfType<CreatedAtActionResult>();
        }

        [Fact]
        public async Task PostAsync_WithValidParameters_ShouldCallServiceOnce()
        {
            // Arrange
            var timesheet = this.fixture.Create<Timesheet>();

            this.timesheetServiceMock.Setup(s => s.PostAsync(timesheet)).ReturnsAsync(timesheet);

            // Act
            await this.timesheetsController.PostAsync(timesheet);

            // Assert
            this.timesheetServiceMock.Verify(v => v.PostAsync(timesheet), Times.Once);
        }

        [Fact]
        public async Task PostAsync_WithInvalidParameters_ShouldReturnBadRequest()
        {
            // Arrange
            var timesheet = this.fixture.Build<Timesheet>().With(w => w.UserName, string.Empty).Create();

            // Act
            var result = await this.timesheetsController.PostAsync(timesheet);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            var badRequest = result as BadRequestObjectResult;
            badRequest?.Value.ToString().Should().Be("Invalid UserName");
        }
    }
}