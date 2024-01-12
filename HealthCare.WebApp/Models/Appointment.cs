using HealthCare.Core.Models.Users;

namespace HealthCare.Core.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public DateTime AppointmentStart { get; set; }
        public DateTime AppointmentEnd { get; set; }
        public string PatientText { get; set; }
        public string WorkerId { get; set; }
        public ApplicationUser Worker { get; set; }
        public string PatientId { get; set; }
        public ApplicationUser Patient { get; set; }
        public string typeOfService { get; set; }


    }
}

