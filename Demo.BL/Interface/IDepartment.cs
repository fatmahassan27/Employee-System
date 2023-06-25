using Demo.BL.ModelVM;
using Demo.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BL.Interface
{
    public interface IDepartment
    {
        Task<IEnumerable<Department>> GetAsync();
        Task<Department> GetByIdAsync(int id);
        Task CreateAsync(Department obj);
        Task UpdateAsync(Department obj);
        Task DeleteAsync(int id);

    }
}
