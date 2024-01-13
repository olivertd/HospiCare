using HealthCare.Core.Data;
using HealthCare.WebApp.Models;
using System;
namespace HealthCare.Core
{
    public class FeedbackService
    {
        private readonly Context database;

        public FeedbackService(Context database)
        {
            this.database = database;
        }

        public FeedbackService()
        {
        }

        

        public async Task SaveFeedback(string body, int rating, int appointmentID)
        {
            var feedback = new Feedback
            {
                Rating = rating,
                AppointmentID = appointmentID,
                Body = body
            };
            database.Feedbacks.Add(feedback);
            await database.SaveChangesAsync();
        }

        public List<Feedback> GetAllFeedback()
        {

            return database.Feedbacks.ToList();
        }
    }
}



