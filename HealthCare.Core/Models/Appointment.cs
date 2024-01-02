using HealthCare.Core.Models.Users;
namespace HealthCare.Core.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public DateTime AppointmentStart { get; set; }
        public DateTime AppointmentEnd { get; set; }
        public string PatientText { get; set; }
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
        public int WorkerId { get; set; }
        public Worker Worker { get; set; }
    }
}

