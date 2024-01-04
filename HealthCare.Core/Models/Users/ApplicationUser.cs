using Microsoft.AspNetCore.Identity;

namespace HealthCare.Core.Models.Users
{
    public class ApplicationUser : IdentityUser
    {
        public UserType Type { get; set; } = UserType.Patient;

        public List<Appointment> Appointments { get; set; }

        public static List<ApplicationUser> GenerateMockDoctors()
        {
            // Mock data: List of doctors for demonstration
            return new List<ApplicationUser>
            {
                new ApplicationUser { UserName = "DrSmith" },
                new ApplicationUser { UserName = "DrJohnson" },
                // Add more doctors as needed
            };
        }
    }

    public enum UserType
    {
        Patient,
        Doctor,
        Nurse,
    }
}
