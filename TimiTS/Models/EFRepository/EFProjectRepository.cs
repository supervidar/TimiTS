using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimiTS.Models.IRepository;

namespace TimiTS.Models.EFRepository
{
    public class EFProjectRepository : IProjectRepository
    {
        private  ApplicationDbContext _context;

        public EFProjectRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public IEnumerable<Project> GetAllProjects => _context.Projects;

        public void SaveProject(Project project)
        {
            if (project.PId == 0)
            {
                _context.Projects.Add(project);
            }
            else
            {
                Project dbEntry = _context.Projects
                    .FirstOrDefault(p => p.PId == project.PId);
                if (dbEntry != null )
                {
                    dbEntry.PId = project.PId;
                    dbEntry.PName = project.PName;
                    dbEntry.PStartDate = project.PStartDate;
                    dbEntry.PEndDate = project.PEndDate;
                    dbEntry.PEstimateMasonry = project.PEstimateMasonry;
                    dbEntry.PEstimateTile = project.PEstimateTile;
                    dbEntry.PEstimateStructural = project.PEstimateStructural;
                    dbEntry.PEstimateExternal = project.PEstimateExternal;
                    dbEntry.PEstimatePlating = project.PEstimatePlating;
                    dbEntry.PEstimateStender = project.PEstimateStender;
                    dbEntry.PEstimateFinalWork = project.PEstimateFinalWork;
                    dbEntry.PEstimateGarage = project.PEstimateGarage;
                    dbEntry.PEstimateAssembly = project.PEstimateAssembly;
                    dbEntry.PEstimateOther = project.PEstimateOther;
                }
            }
            _context.SaveChanges();
        }

        public Project DeleteProject(int id)
        {
            Project dbEntry = _context.Projects.SingleOrDefault(m => m.PId == id);
            if (dbEntry != null)
            {
                _context.Projects.Remove(dbEntry);
                _context.SaveChanges();
            }
            return dbEntry;
                        
        }

        public void CreateProject(Project project)
        {           
            _context.Projects.Add(project);
            _context.SaveChanges();
        }

        public Project FindProject(int id)
        {

            Project dbEntry = _context.Projects.SingleOrDefault(m => m.PId == id);

            return dbEntry;
        }
    }
}
