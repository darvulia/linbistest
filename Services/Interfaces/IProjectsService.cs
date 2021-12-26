using Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IProjectsService
    {
        Task<Project> GetById(int id);
        Task<Project> UpdateProject(Project project);
        Task CreateProject(Project project);
        Task DeleteProject(Project project);
    }
}
