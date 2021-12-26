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

        public async Task<Developer> GetById(int id)
        {
            var res = await appContext.Developers.Where(x => x.id == id).FirstOrDefaultAsync();
            return res;
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

    }
}
