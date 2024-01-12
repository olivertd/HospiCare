using HealthCare.WebApp.Areas.Identity.Pages.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HealthCare.Core.Tests.Auth
{
    public class AuthTest
    {
        private string returnUrl = "https://localhost:7139/";

        [Fact]
        public async Task RegisterAccount_Success()
        {
            // Arrange
            var registerModel = new AuthTestHelpers().CreateRegisterModel();

            registerModel.Input = new RegisterModel.InputModel
            {
                Email = "test@example.com",
                Password = "Test123!",
                ConfirmPassword = "Test123!",
                FirstName = "Skalman",
                LastName = "Sköldpadda"
            };

            // Act
            var result = await registerModel.OnPostAsync(returnUrl);

            // Assert
            Assert.IsType<LocalRedirectResult>(result);
        }

        [Fact]
        public async Task RegisterAccount_Failure_InvalidEmail()
        {
            //Arrange
            var registerModel = new AuthTestHelpers().CreateRegisterModel();

            registerModel.Input = new RegisterModel.InputModel
            {
                Email = "invalidemail",
                Password = "Test123!",
                ConfirmPassword = "Test123!",
                FirstName = "Skalman",
                LastName = "Sköldpadda"
            };

            registerModel.ModelState.AddModelError("Email", "Invalid email"); // ALEX: We need to add this error manually since we are not using the UI. See this dicussion: https://github.com/dotnet/aspnetcore/issues/4966

            // Act
            var result = await registerModel.OnPostAsync(returnUrl);

            // Assert
            Assert.IsType<PageResult>(result);
            Assert.False(registerModel.ModelState.IsValid);
            Assert.Contains(registerModel.ModelState.Keys, key => key.Contains("Email"));
        }

        [Fact]
        public async Task RegisterAccount_Failure_WeakPassword()
        {
            // Arrange
            var registerModel = new AuthTestHelpers().CreateRegisterModel();

            registerModel.Input = new RegisterModel.InputModel
            {
                Email = "test@example.com",
                Password = "weak",
                ConfirmPassword = "weak",
                FirstName = "Skalman",
                LastName = "Sköldpadda"
            };

            registerModel.ModelState.AddModelError("Password", "Weak password"); // ALEX: We need to add this error manually since we are not using the UI. See this dicussion: https://github.com/dotnet/aspnetcore/issues/4966

            // Act
            var result = await registerModel.OnPostAsync(returnUrl);


            // Assert
            Assert.IsType<PageResult>(result);
            Assert.False(registerModel.ModelState.IsValid);
            Assert.Contains(registerModel.ModelState.Keys, key => key.Contains("Password"));
        }

        [Fact]
        public async Task RegisterAccount_Failure_PasswordMismatch()
        {
            // Arrange
            var registerModel = new AuthTestHelpers().CreateRegisterModel();

            registerModel.Input = new RegisterModel.InputModel
            {
                Email = "test@example.com",
                Password = "Test123!",
                ConfirmPassword = "NoMatch!",
                FirstName = "Skalman",
                LastName = "Sköldpadda"
            };

            registerModel.ModelState.AddModelError("ConfirmPassword", "Password doesnt match"); // ALEX: We need to add this error manually since we are not using the UI. See this dicussion: https://github.com/dotnet/aspnetcore/issues/4966

            // Act
            var result = await registerModel.OnPostAsync(returnUrl);

            // Assert
            Assert.IsType<PageResult>(result);
            Assert.False(registerModel.ModelState.IsValid);
            Assert.Contains(registerModel.ModelState.Keys, key => key.Contains("ConfirmPassword"));
        }
    }
}