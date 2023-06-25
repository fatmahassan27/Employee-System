using Demo.DAL.Entity;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BL.ModelVM
{
    public class EmployeeVM
    {
        public EmployeeVM()
        {
            IsActive = true;
            IsDeleted = false;
            CreationDate= DateTime.Now;
        }
        [Key]
        public int Id { get; set; }
        [Required, StringLength(20)]

        public string Name { get; set; }
        [Range(1000,100000,ErrorMessage ="Between 1000 to 100000")]
        public double Salary { get; set; }
        public string Address { get; set; }
        public DateTime HireDate { get; set; }
        public string? Notes { get; set; }
        [EmailAddress(ErrorMessage ="Email Invalid")]
        public string? Email { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public bool IsUpdated { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime DeletedDate { get; set; }
        //navigation property 1:M
        public int DepartmentId { get; set; }
        public int DistrictId { get; set; }
        public Department? Department { get; set; }
        public DistrictVM? District { get; set; }
        public string? ImgName { get; set; }
        public string? CVName { get; set; }
        public IFormFile Img { get; set; }
        public IFormFile CV { get; set; }


    }
}
