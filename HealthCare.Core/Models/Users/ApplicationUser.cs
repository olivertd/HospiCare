using Microsoft.AspNetCore.Identity;

namespace HealthCare.Core.Models.Users
{
    public class ApplicationUser : IdentityUser
    {
        public List<Appointment> WorkerAppointments { get; set; }
        public List<Appointment> PatientAppointments { get; set; }

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

    public enum UserRole
    {
        Patient,
        Doctor,
        Nurse,
    }
}
