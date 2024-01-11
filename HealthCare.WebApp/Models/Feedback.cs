using HealthCare.Core.Models;

namespace HealthCare.WebApp.Models
{
    public class Feedback
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public int Rating { get; set; }
        public Appointment Appointment { get; set; }
        public int AppointmentID { get; set; }
    }
}
