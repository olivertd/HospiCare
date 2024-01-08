using HealthCare.Core.Models;
namespace HealthCare.Core
{
    public class BookingService
    {
        private List<Appointment> bookings = new List<Appointment>();

        public BookingService()
        {
            

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

