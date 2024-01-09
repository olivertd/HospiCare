using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HealthCare.Core.Data;
using HealthCare.Core.Models;
using HealthCare.Core.Models.Users;
using HealthCare.WebApp.Shared;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace HealthCare.Core.Tests.Booking
{
    public class BookingTest
    {
        [Fact]
        public async Task NewAppointment()
        {
            // Arrange: Create mock dependencies
            var authenticationStateProviderMock = new Mock<AuthenticationStateProvider>();

            // Set up a mock current user
            var currentUser = new ApplicationUser { Id = "userId123" };
            var authState = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.Name, currentUser.Id) }, "mock")));
            authenticationStateProviderMock.Setup(x => x.GetAuthenticationStateAsync()).ReturnsAsync(authState);

            var contextOptions = new DbContextOptionsBuilder<Context>().UseInMemoryDatabase(databaseName: "TestDb").Options;
            var context = new Context(contextOptions);

            var userManager = new UserManager<ApplicationUser>(new Mock<IUserStore<ApplicationUser>>().Object,
                                                       null, null, null, null, null, null, null, null);


            // Populate the database with test data
            context.Appointments.Add(new Appointment
            {
                AppointmentStart = DateTime.Parse("2024-01-09 09:00:00"),
                AppointmentEnd = DateTime.Parse("2024-01-09 10:00:00"),
                PatientId = currentUser.Id,
                PatientText = "Some patient text",
                WorkerId = "b7b29523-b37d-454a-b46b-d8834f1bc4c0",
                typeOfService = "General Checkup"
            });
            context.SaveChanges();

            var bookingService = new BookingService(context);
            var page = new BookingComponent(context, null, null)
            {
                UserManagers = userManager,
                AuthenticationStateProviders = authenticationStateProviderMock.Object,
                BookingServices = bookingService
            };

            // Act: Call the method or property that you want to test
            // Assuming you have a method like BookAppointment, call it here
            await page.BookingServices.BookAppointment("2024-01-09 11:00:00", "b7b29523-b37d-454a-b46b-d8834f1bc4c0", currentUser.Id, "No description was provided.", "General Checkup");

            // Assert: Verify the expected result
            // Check if the appointment was added to the database, or any other relevant assertions
            var bookedAppointment = context.Appointments.FirstOrDefault(a => a.PatientId == currentUser.Id);
            Assert.NotNull(bookedAppointment);
        }

        [Fact]
        public async Task AppointmentAlreadyExists()
        {
            // Arrange: Create mock dependencies
            var authenticationStateProviderMock = new Mock<AuthenticationStateProvider>();

            // Set up a mock current user
            var currentUser = new ApplicationUser { Id = "userId123" };
            var authState = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.Name, currentUser.Id) }, "mock")));
            authenticationStateProviderMock.Setup(x => x.GetAuthenticationStateAsync()).ReturnsAsync(authState);

            var contextOptions = new DbContextOptionsBuilder<Context>().UseInMemoryDatabase(databaseName: "TestDb").Options;
            var context = new Context(contextOptions);

            var userManager = new UserManager<ApplicationUser>(new Mock<IUserStore<ApplicationUser>>().Object,
                                                       null, null, null, null, null, null, null, null);


            // Populate the database with test data
            context.Appointments.Add(new Appointment
            {
                AppointmentStart = DateTime.Parse("2024-01-09 09:00:00"),
                AppointmentEnd = DateTime.Parse("2024-01-09 10:00:00"),
                PatientId = currentUser.Id,
                PatientText = "Some patient text",
                WorkerId = "b7b29523-b37d-454a-b46b-d8834f1bc4c0",
                typeOfService = "General Checkup"
            });
            context.SaveChanges();

            var bookingService = new BookingService(context);
            var page = new BookingComponent(context, null, null)
            {
                UserManagers = userManager,
                AuthenticationStateProviders = authenticationStateProviderMock.Object,
                BookingServices = bookingService
            };

            // Act: Call the method or property that you want to test
            // Assuming you have a method like BookAppointment, call it here
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                await page.BookingServices.BookAppointment("2024-01-09 09:00:00", "b7b29523-b37d-454a-b46b-d8834f1bc4c0", currentUser.Id, "No description was provided.", "General Checkup");
            });

            // Assert: Verify the expected result
            // Check if the exception is of the expected type
            Assert.NotNull(exception);
        }
    }
}
