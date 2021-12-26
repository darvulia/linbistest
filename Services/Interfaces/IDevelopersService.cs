using Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IDevelopersService
    {
        Task<Developer> GetById(int id);
        Task CreateDeveloper(Developer developer);
        Task<Developer> UpdateDeveloper(Developer developer);
        Task DeleteDeveloper(Developer developer);
    }
}
