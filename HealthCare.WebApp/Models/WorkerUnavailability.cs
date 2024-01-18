using HealthCare.Core.Models.Users;

namespace HealthCare.WebApp.Models
{
    public class WorkerUnavailability
    {
        public int Id { get; set; }
        public ApplicationUser Worker { get; set; }

        public string WorkerId { get; set; }
        public DateTime UnavailableDate { get; set; }


    }
}