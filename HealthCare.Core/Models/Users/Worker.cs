namespace HealthCare.Core.Models.Users
{
    public class Worker : User
    {
        public WorkerType Type { get; set; }
    }

    public enum WorkerType
    {
        Doctor,
        Nurse,
    }
}
