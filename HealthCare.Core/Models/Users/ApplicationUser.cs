using Microsoft.AspNetCore.Identity;

namespace HealthCare.Core.Models.Users
{
    public class ApplicationUser : IdentityUser
    {
        public UserType Type { get; set; } = UserType.Patient;

        public List<Appointment> Appointments { get; set; }
    }

    public enum UserType
    {
        Patient,
        Doctor,
        Nurse,
    }
}
