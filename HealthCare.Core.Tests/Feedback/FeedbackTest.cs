using HealthCare.Core.Data;
using HealthCare.Core.Models;
using HealthCare.Core.Models.Users;
using HealthCare.WebApp.Pages.Appointment;
using HealthCare.WebApp.Shared;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Data.Entity;
using System.Security.Claims;

namespace HealthCare.Core.Tests.Feedback
{
    public class FeedbackTest
    {
        [Fact]
        public async Task NewFeedbackTest()
        {
            var authenticationStateProviderMock = new Mock<AuthenticationStateProvider>();

            // Set up a mock current user
            var currentUser = new ApplicationUser { Id = "userId123" };
            var authState = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.Name, currentUser.Id) }, "mock")));
            authenticationStateProviderMock.Setup(x => x.GetAuthenticationStateAsync()).ReturnsAsync(authState);

            var contextOptions = new DbContextOptionsBuilder<Context>().UseInMemoryDatabase(databaseName: "TestDb").Options;
            var context = new Context(contextOptions);

            var userManager = new UserManager<ApplicationUser>(new Mock<IUserStore<ApplicationUser>>().Object,
                                                       null, null, null, null, null, null, null, null);

            Appointment appointment = new Appointment{
                AppointmentStart = DateTime.Parse("2024-01-09 09:00:00"),
                AppointmentEnd = DateTime.Parse("2024-01-09 10:00:00"),
                PatientId = currentUser.Id,
                PatientText = "Some patient text",
                WorkerId = "workerID123",
                typeOfService = "General Checkup",
            };
            context.Appointments.Add(appointment);
            context.SaveChanges();

            var feedbackService = new FeedbackService(context);
            var page = new DashboardComponent()
            {
                UserManagers = userManager,
                AuthenticationStateProviders = authenticationStateProviderMock.Object,
                FeedbackServices = feedbackService
            };

            string body = "Some text.";
            int rating = 5;

            await page.FeedbackServices.SaveFeedback(body, rating, appointment.Id);

            var newFeedback = context.Feedbacks.SingleOrDefault(x => x.AppointmentID == appointment.Id);
            Assert.NotNull(newFeedback);

        }
    }
}
