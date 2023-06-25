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
    public class DistrictRep : IDistrict
    {
        private readonly ApplicationDBContext db;
        public DistrictRep(ApplicationDBContext db)
        {
            this.db = db;
        }
        public async Task<IEnumerable<District>> GetAsync(Expression<Func<District, bool>> filter = null)
        {
            if (filter != null)
                return
                    await db.District.Where(filter).Include("City").ToListAsync();
            else
                return
                await db.District.Include("City").ToListAsync();

        }
    }
}
