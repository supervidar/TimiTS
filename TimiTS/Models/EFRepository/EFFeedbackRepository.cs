using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimiTS.Models.IRepository;

namespace TimiTS.Models.EFRepository
{
    public class EFFeedbackRepository : IFeedbackRepository
    {
        private ApplicationDbContext _context;

        public EFFeedbackRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public IEnumerable<Feedback> GetAllFeedback => _context.Feedbacks;

        public void CreateFeedback(Feedback feedback)
        {
            _context.Feedbacks.Add(feedback);
            _context.SaveChanges();
        }

        public Feedback DeleteFeedback(int id)
        {
            Feedback dbEntry = _context.Feedbacks.SingleOrDefault(m => m.FId == id);
            if (dbEntry != null)
            {
                _context.Feedbacks.Remove(dbEntry);
                _context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
