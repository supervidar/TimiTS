using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimiTS.Models.IRepository
{
    public interface IWorkParticipationRepository 
    {
       
        IEnumerable<WorkParticipation> WorkParticipations { get; }

        IEnumerable<WorkParticipation> GetAllWorkParticipation { get; }
        //IEnumerable<WorkParticipation> WorkParticipations { get; }

        IEnumerable<WorkParticipation> Absneces { get; }

        void SaveWorkParticipation(WorkParticipation workParticipation);

        void CreateWorkParticipation(WorkParticipation workParticipation);


        WorkParticipation DeleteWorkParticipation(int id);

        WorkParticipation FindWorkParticipation(int? id);


    }
}
