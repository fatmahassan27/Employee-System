using Demo.BL.ModelVM;
using Demo.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BL.Interface
{
    public interface IEmployee
    {
       public Task<IEnumerable<Employee>> GetAsync(Expression<Func<Employee, bool>> filter = null);
        public Task<Employee> GetByIdAsync(Expression<Func<Employee, bool>> filter = null);
        public Task<Employee> CreateAsync(Employee obj);
        public Task UpdateAsync(Employee obj);
        public Task DeleteAsync(Employee obj);
    }
}
