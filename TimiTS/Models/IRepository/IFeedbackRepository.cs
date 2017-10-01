using System.Collections.Generic;

namespace TimiTS.Models.IRepository
{
    public interface IFeedbackRepository
    {
        IEnumerable<Feedback> GetAllFeedback { get; }       

        void CreateFeedback(Feedback feedback);

        Feedback DeleteFeedback(int id);

    }
}
