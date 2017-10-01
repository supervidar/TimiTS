using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimiTS.Models.IRepository;

namespace TimiTS.Models.EFRepository
{
    public class EFWorkParticipationRepository : IWorkParticipationRepository
    {
        private ApplicationDbContext _context;
        private int prosjektarbeid = 1;
        private int timearbeid = 2;
        private int egenmelding = 3;
        private int egenmeldigSyktBarn = 4;
        private int sykemelding = 5;
     

        public EFWorkParticipationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<WorkParticipation> WorkParticipations => _context.WorkParticipations.Where(t => t.WorkTypeId == prosjektarbeid || t.WorkTypeId == timearbeid)
            .Include(u=>u.Id)
            .Include(c=>c.CId)
            .Include(t=>t.WTId)
            .Include(wc=>wc.WCId);

        public IEnumerable<WorkParticipation> Absneces => _context.WorkParticipations.Where(t => t.WorkTypeId == egenmelding || t.WorkTypeId == egenmeldigSyktBarn || t.WorkTypeId == sykemelding)
            .Include(u => u.Id)
            .Include(c => c.CId)
            .Include(t => t.WTId)
            .Include(wc => wc.WCId);

        public IEnumerable<WorkParticipation> GetAllWorkParticipation => _context.WorkParticipations;

        // public IEnumerable<WorkParticipation> WorkParticipationsUM => _context.WorkParticipations.Where().Where(t => t.WTId.WTType.Contains(ta)).Where(t => t.WTId.WTType.Contains(pa));

        public void SaveWorkParticipation(WorkParticipation workParticipation)
        {
            if (workParticipation.WPId == 0)
            {
                _context.WorkParticipations.Add(workParticipation);
            }
            else
            {

                WorkParticipation dbEntry = _context.WorkParticipations
                    .FirstOrDefault(p => p.WPId == workParticipation.WPId);
                if (dbEntry != null)
                {
                    dbEntry.WPId = workParticipation.WPId;
                    dbEntry.DateTimeIn = workParticipation.DateTimeIn;
                    dbEntry.DateTimeOut = workParticipation.DateTimeOut;
                    dbEntry.Hours = workParticipation.Hours;
                    dbEntry.WPBreak = workParticipation.WPBreak;
                    dbEntry.Comment = workParticipation.Comment;
                    dbEntry.ActiveSession = workParticipation.ActiveSession;
                    dbEntry.ContracterId = workParticipation.ContracterId;
                    dbEntry.ProjectId = workParticipation.ProjectId;
                    dbEntry.UserId = workParticipation.UserId;
                    dbEntry.WorkCategoryId = workParticipation.WorkCategoryId;
                    dbEntry.WorkTypeId = workParticipation.WorkTypeId;
                    //dbEntry.Verified = workParticipation.Verified;

                }
            }
            //_context.Update(workParticipation);
            _context.SaveChanges();
        }
        

        public void CreateWorkParticipation(WorkParticipation workParticipation)
        {
            _context.WorkParticipations.Add(workParticipation);
            _context.SaveChanges();
        }

        public WorkParticipation DeleteWorkParticipation(int id)
        {
            WorkParticipation dbEntry = _context.WorkParticipations.SingleOrDefault(m => m.WPId == id);
            if (dbEntry != null)
            {
                _context.WorkParticipations.Remove(dbEntry);
                _context.SaveChanges();
            }
            return dbEntry;
        }

        public WorkParticipation FindWorkParticipation(int? id)
        {
            
            WorkParticipation dbEntry = _context.WorkParticipations.Include(c => c.CId).Include(t => t.WTId).Include(wc => wc.WCId).SingleOrDefault(wp => wp.WPId == id);

            return dbEntry;
        }

    }
}
