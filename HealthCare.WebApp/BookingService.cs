using HealthCare.Core.Data;
using HealthCare.Core.Models;
using System.Security.Permissions;
namespace HealthCare.Core
{
    public class BookingService
    {
        private List<Appointment> bookings = new List<Appointment>();

        private readonly Context database;

        public BookingService(Context database)
        {
            this.database = database;
        }

        public BookingService()
        {
            

        }

        public async Task BookAppointment(
            string selectedTime,
            string selectedStaff,
            string patientId,
            string patientText,
            string selectedService)
        {
            var newBooking = new Appointment
            {
                AppointmentStart = DateTime.Parse(selectedTime),
                AppointmentEnd = DateTime.Parse(selectedTime).AddHours(1),
                PatientText = patientText,
                PatientId = patientId,
                WorkerId = selectedStaff,
                typeOfService = selectedService,
            };

            bool existingAppointment = database.Appointments.Where(a => a.AppointmentStart == newBooking.AppointmentStart && a.WorkerId == newBooking.WorkerId).Any();

            if(existingAppointment)
            {
                throw new InvalidOperationException("An appointment already exists for the selected worker and time.");
            }
            else
            {
                database.Appointments.Add(newBooking);
                await database.SaveChangesAsync();
            }
            
        }

        public async Task DeleteAppointment(Appointment appointment)
        {
            try
            {
                database.Appointments.Remove(appointment);
                await database.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw new InvalidOperationException("No such appointment exists.");
            }
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

