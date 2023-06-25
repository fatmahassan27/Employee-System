using Demo.BL.Interface;
using Demo.BL.ModelVM;
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
    public class EmployeeRep : IEmployee
    {
        private readonly ApplicationDBContext db;
        public EmployeeRep(ApplicationDBContext db)
        {
            this.db = db;
        }
        public async Task<IEnumerable<Employee>> GetAsync(Expression<Func<Employee, bool>> filter = null)
        {
            if(filter !=null)
                return await db.Employee
                    .Where(filter)
                    .Include("Department")
                    .Include("District")
                    .ToListAsync();
            else
                return await db.Employee.Include("Department").Include("District").ToListAsync();
        }

        public async Task<Employee> GetByIdAsync(Expression<Func<Employee, bool>> filter = null)
        {
            var data = await db.Employee.Where(filter)
                .Include("Department")
                .Include("District")
                .FirstOrDefaultAsync();
            return data;
        }
        
        public async Task<Employee> CreateAsync(Employee obj)
        {
            await db.Employee.AddAsync(obj);
            await db.SaveChangesAsync();
            var result = await db.Employee.OrderByDescending(x => x.Id).FirstOrDefaultAsync();
            return result;
        }
        public async Task UpdateAsync(Employee obj)
        {
            obj.UpdatedDate = DateTime.Now;
            obj.IsUpdated= true;
            db.Entry(obj).State = EntityState.Modified;
            await db.SaveChangesAsync();

        }
        public async Task DeleteAsync(Employee obj)
        {

            var oldData = db.Employee.Find(obj);
            //oldData.IsDeleted = true;
            //oldData.DeletedDate=DateTime.Now;
            db.Employee.Remove(oldData);
            await db.SaveChangesAsync();

        }

     
    }
}
