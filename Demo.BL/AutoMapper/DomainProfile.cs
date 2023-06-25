using AutoMapper;
using Demo.BL.ModelVM;
using Demo.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BL.AutoMapper
{
    public class DomainProfile :Profile
    {
        public DomainProfile()
        {
            // from entity to VM Retrive
            CreateMap<Department, DepartmentVM>();
            // from VM to entity (create - edit - delete)
            CreateMap<DepartmentVM, Department>();
            
            CreateMap<Employee, EmployeeVM>();
            CreateMap<EmployeeVM, Employee>();

            CreateMap<Country, CountryVM>();
            CreateMap<CountryVM, Country>();

            CreateMap<City, CityVM>();
            CreateMap<CityVM, City>();

            CreateMap<District, DistrictVM>();
            CreateMap<DistrictVM, District>();

        }

    }

}
