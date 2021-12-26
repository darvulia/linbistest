using Data;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementations
{
    public class ProjectsService : IProjectsService
    {
        private readonly AppDbContext appContext;

        public ProjectsService(AppDbContext appContext)
        {
            this.appContext = appContext;
        }

        public async Task<Project> GetById(int id)
        {
            var res = await appContext.Projects.Where(x => x.id == id).FirstOrDefaultAsync();
            return res;
        }

        public async Task CreateProject(Project project)
        {
            using (var transaction = appContext.Database.BeginTransaction())
            {
                try
                {
                    appContext.Add(project);
                    await appContext.SaveChangesAsync();
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw new Exception(e.Message);
                }
            }
        }


        public async Task<Project> UpdateProject(Project project)
        {
            using (var transaction = appContext.Database.BeginTransaction())
            {
                try
                {
                    appContext.Entry(project).State = EntityState.Modified;
                    await appContext.SaveChangesAsync();
                    transaction.Commit();
                    return project;
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw new Exception(e.Message);

                }
            }

        }


        public async Task DeleteProject(Project project)
        {
            using (var transaction = appContext.Database.BeginTransaction())
            {
                try
                {
                    appContext.Remove(project);
                    await appContext.SaveChangesAsync();
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw new Exception(e.Message);

                }
            }
        }

    }
}
