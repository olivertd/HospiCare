using Microsoft.AspNetCore.Identity;

namespace HealthCare.Core.Models.Users
{
    public class ApplicationUser : IdentityUser
    {
        public List<Appointment> Appointments { get; set; }
    }

    public enum UserRole
    {
        Patient,
        Doctor,
        Nurse,
    }
}
