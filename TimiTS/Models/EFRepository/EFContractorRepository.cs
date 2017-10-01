using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimiTS.Models.IRepository;

namespace TimiTS.Models.EFRepository
{
    public class EFContractorRepository : IContractorRepository
    {
        private ApplicationDbContext _context;

        public EFContractorRepository(ApplicationDbContext context)
        {
            _context = context;
        }
       

        public IEnumerable<Contractor> GetAllContractors => _context.Contractors;

        public void CreateContractor(Contractor contractor)
        {
            _context.Contractors.Add(contractor);
            _context.SaveChanges();
        }

        public Contractor DeleteContractor(int id)
        {
            Contractor dbEntry = _context.Contractors.SingleOrDefault(m => m.CId == id);
            if (dbEntry != null)
            {
                _context.Contractors.Remove(dbEntry);
                _context.SaveChanges();
            }
            return dbEntry;
        }

        public void SaveContractor(Contractor contractor)
        {
            if (contractor.CId == 0)
            {
                _context.Contractors.Add(contractor);
            }
            else
            {
                Contractor dbEntry = _context.Contractors
                    .FirstOrDefault(p => p.CId == contractor.CId);
                if (dbEntry != null)
                {
                    dbEntry.CId = contractor.CId;
                    dbEntry.CName = contractor.CName;
                    dbEntry.CEmail = contractor.CEmail;
                    dbEntry.CStreetAddress = contractor.CStreetAddress;
                    dbEntry.CPostalCode = contractor.CPostalCode;
                    dbEntry.CPostalAddress = contractor.CPostalAddress;
                    dbEntry.COrgNr = contractor.COrgNr;
                   
                }
            }
            _context.SaveChanges();
        }

        
    }
}
