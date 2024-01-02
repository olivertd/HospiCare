using HealthCare.Core.Models;
namespace HealthCare.Core
{
    public class BookingService
    {
        private List<Appointment> bookings = new List<Appointment>();

        public BookingService()
        {
            // mock data
            bookings.Add(new Appointment
            {
                Id = 1,
                AppointmentStart = DateTime.Now.AddHours(2),
                AppointmentEnd = DateTime.Now.AddHours(2),
                PatientId = 1,
                PatientText = "General Checkup"
            });
            bookings.Add(new Appointment
            {
                Id = 2,
                AppointmentStart = DateTime.Now.AddHours(2),
                AppointmentEnd = DateTime.Now.AddHours(2),
                PatientId = 2,
                PatientText = "General Checkup"
            });

        }

        public IEnumerable<Appointment> GetBookings()
        {
            return bookings;
        }

        public void AddBooking(Appointment booking)
        {
            bookings.Add(booking);
        }
    }
}

