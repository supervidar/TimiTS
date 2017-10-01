using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimiTS.Models.IRepository
{
    public interface IContractorRepository
    {
        IEnumerable<Contractor> GetAllContractors { get; }

        void SaveContractor(Contractor contractor);

        void CreateContractor(Contractor contractor);

        Contractor DeleteContractor(int id);
    }
}
