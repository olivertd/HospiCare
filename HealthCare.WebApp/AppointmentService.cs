using HealthCare.Core.Data;
using HealthCare.Core.Models;
using System;
namespace HealthCare.Core
{
	public class AppointmentService
	{

        private List<Appointment> bookings = new List<Appointment>();

        private readonly Context database;

        public AppointmentService(Context database)
        {
            this.database = database;
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

       
    }
}

