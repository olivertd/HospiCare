using HealthCare.Core.Data;
using HealthCare.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthCare.Core
{
    public class AppointmentService
    {
        private readonly Context database;

        private List<Appointment> bookings = new List<Appointment>();

        public AppointmentService(Context database)
        {
            this.database = database;
        }

        // This method fetches appointments for a worker.
        public async Task<IEnumerable<AppointmentDetails>> GetAppointmentsForWorker(string workerId)
        {
            var appointments = await database.Appointments
                .Include(a => a.Patient)  
                .Where(a => a.WorkerId == workerId)
                .Select(a => new AppointmentDetails
                {
                    Id = a.Id.ToString(),
                    PatientId = a.PatientId,
                    WorkerId = a.WorkerId,
                    AppointmentStart = a.AppointmentStart,
                    AppointmentEnd = a.AppointmentEnd, 
                    AppointmentType = a.typeOfService, 
                    PatientName = a.Patient.FirstName + " " + a.Patient.LastName 
                })
                .ToListAsync();

            return appointments;
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

        // The AppointmentDetails class used to structure the data for the Razor page.
        public class AppointmentDetails
        {
            public string Id { get; set; }
            public string PatientId { get; set; }
            public string WorkerId { get; set; }
            public string Details { get; set; }
            public string PatientName { get; set; } 
            public DateTime AppointmentStart { get; set; }
            public DateTime AppointmentEnd { get; set; }
            public string AppointmentType { get; set; } 
        }
    }
}
