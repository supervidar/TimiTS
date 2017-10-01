using System.Collections.Generic;

//This interface uses IEnumerable<T> to allow a caller to obtain a sequence of Project objects, without
//saying how or where the data is stored or retrieved. A class that depends on the IProjectRepository
//interface can obtain Project objects without needing to know anything about where they are coming
//from or how the implementation class will deliver them.

namespace TimiTS.Models.IRepository
{
    public interface IProjectRepository
    {
        IEnumerable<Project> GetAllProjects { get; }

        void SaveProject(Project project);

        void CreateProject(Project project);

        Project DeleteProject(int id);

        Project FindProject(int id);

    }
}
