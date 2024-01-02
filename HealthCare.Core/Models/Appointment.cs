using HealthCare.Core.Models.Users;

namespace HealthCare.Core.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public DateTime AppointmentStart { get; set; }
        public DateTime AppointmentEnd { get; set; }
        public string PatientText { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        // AG: Ersätt IdentityUser med Patient senare

        // AG: Koppla varje besök till en specific vårdarbetare? Eller inte?
        //public int WorkerId { get; set; }
        //public Worker Worker { get; set; }
    }
}

