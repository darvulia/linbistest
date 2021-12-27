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
    public class DevelopersService : IDevelopersService
    {
        private readonly AppDbContext appContext;

        public DevelopersService(AppDbContext appContext)
        {
            this.appContext = appContext;
        }

        public async Task<Project> GetById(int id)
        {
            var res = await appContext
                            .Projects
                            .Include(x=> x.Developers)
                            .Where(x => x.id == id).ToListAsync();

            var devs = res.SelectMany(x=> x.Developers);


            var pro = res.Select(x => new Project
            {
                name = x.name,
                isActive = x.isActive,
                addedDate = x.addedDate,
                effortRequireInDays = x.effortRequireInDays,
                developmentCost = (double)devs.Sum(x => x.costByDay) * x.effortRequireInDays,
                Developers = devs.ToList()
            }).FirstOrDefault();


            return pro;
        }

        public async Task CreateDeveloper(Developer developer)
        {
            using (var transaction = appContext.Database.BeginTransaction())
            {
                try
                {
                    appContext.Add(developer);
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


        public async Task<Developer> UpdateDeveloper(Developer developer)
        {
            using (var transaction = appContext.Database.BeginTransaction())
            {
                try
                {
                    appContext.Entry(developer).State = EntityState.Modified;
                    await appContext.SaveChangesAsync();
                    transaction.Commit();
                    return developer;
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw new Exception(e.Message);

                }
            }

        }


        public async Task DeleteDeveloper(Developer developer)
        {
            using (var transaction = appContext.Database.BeginTransaction())
            {
                try
                {
                    appContext.Remove(developer);
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


        public async Task<Project> projectExist(int id)
        {
            var res = await appContext.Projects.Where(x => x.id == id).FirstOrDefaultAsync();
            return res;
        }


    }
}
