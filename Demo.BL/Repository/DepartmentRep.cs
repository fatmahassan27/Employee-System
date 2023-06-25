using Demo.BL.Interface;
using Demo.BL.ModelVM;
using Demo.DAL.DataBase;
using Demo.DAL.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BL.Repository
{
    public class DepartmentRep : IDepartment
    {
        private readonly ApplicationDBContext db;
        public DepartmentRep(ApplicationDBContext db)
        {
            this.db = db;   
        }
        public async Task<IEnumerable<Department>> GetAsync()
        {
            var data = await db.Department.ToListAsync();
            return data;
        }

        public async Task<Department> GetByIdAsync(int id)
        {
            var data = await db.Department.Where(x => x.Id == id).FirstOrDefaultAsync();
            return data;
        }

        public async Task CreateAsync(Department obj)
        {    
          await db.Department.AddAsync(obj);
           await db.SaveChangesAsync();
        }
        public async Task UpdateAsync(Department obj)
        {
           db.Entry(obj).State= EntityState.Modified;   
            await db.SaveChangesAsync();

        }
        public  async Task DeleteAsync(int id)
        {

            var oldData = db.Department.Find(id);
            db.Department.Remove(oldData);
            await db.SaveChangesAsync();
                    
        }



    }
}
