using Demo.BL.Interface;
using Demo.DAL.DataBase;
using Demo.DAL.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BL.Repository
{
    public class CityRep : ICity
    {
        private readonly ApplicationDBContext db;

        public CityRep(ApplicationDBContext db) 
        {
            this.db = db;
        }    
        public async Task<IEnumerable<City>> GetAsync(Expression<Func<City, bool>> filter = null)
        {
            if (filter != null)
                return
                    await db.City.Where(filter).Include("Country").ToListAsync();
            else
                return
                    await db.City.Include("Country").ToListAsync();
        }
    }
}
