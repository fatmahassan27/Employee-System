using AutoMapper;
using Demo.BL.Interface;
using Demo.BL.Helper;

using Demo.BL.ModelVM;
using Demo.DAL.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Runtime.Intrinsics.Arm;
using System.IO; 

namespace Demo.PL.Controllers
{
    public class EmployeeController : Controller
    {
        #region prop
        private readonly IEmployee employee;
        private readonly IMapper mapper;
        private readonly IDepartment department;
        private readonly ICity city;

        private readonly IDistrict district;

        #endregion
        #region Constructor
        public EmployeeController(IEmployee employee ,IMapper mapper ,IDepartment department ,ICity city,IDistrict district)
        {
            this.employee = employee;
            this.mapper = mapper;
            this.department = department;
            this.city = city;
            this.district= district;
        }
        #endregion
        #region Actions
        public async Task<IActionResult> Index()
        {
            var data = await employee.GetAsync(x=>x.IsActive==true && x.IsDeleted==false);
            var result = mapper.Map<IEnumerable<EmployeeVM>>(data);
            return View(result);
        }
        public async Task<IActionResult> Details(int id)
        {

            var data = await employee.GetByIdAsync(x=>x.Id==id && x.IsActive == true && x.IsDeleted == false);
            var result = mapper.Map<EmployeeVM>(data);
            return View(result);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var Dep = await department.GetAsync();
            ViewBag.DepartmentList = new SelectList(Dep ,"Id" ,"Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(EmployeeVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                   
                    model.CVName = FolderUpload.UploadFile("Docs",model.CV);
                    model.ImgName = FolderUpload.UploadFile("Images", model.Img); ;

                    var data = mapper.Map<Employee>(model);
                    await employee.CreateAsync(data);
                    return RedirectToAction(nameof(Index));
                }

            }catch(Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            var Dep = await department.GetAsync();

            ViewBag.DepartmentList = new SelectList(Dep, "Id", "Name",model.DepartmentId);

            return View(model);
          
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var data = await employee.GetByIdAsync(x => x.Id == id && x.IsActive == true && x.IsDeleted == false);
            var result=mapper.Map<EmployeeVM>(data);
            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> Update(EmployeeVM model)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    var data=mapper.Map<Employee>(model);
                    await employee.UpdateAsync(data);
                    return RedirectToAction("Index");
                }

            }catch(Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            var Dep = await department.GetAsync();

            ViewBag.DepartmentList = new SelectList(Dep, "Id", "Name", model.DepartmentId);
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await employee.GetByIdAsync(x => x.Id == id && x.IsActive == true && x.IsDeleted == false);
            var result = mapper.Map<EmployeeVM>(data);
            return View(result);
        }
        [HttpPost]
        [ActionName("Delete")]  
        public async Task<IActionResult> ConfirmDelete(EmployeeVM obj)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    FolderUpload.RemoveFile("Docs", obj.CVName);
                    FolderUpload.RemoveFile("Images", obj.ImgName);
                    var result = mapper.Map<Employee>(obj);
                    await employee.DeleteAsync(result);
                    return RedirectToAction("Index");
                }
            }catch(Exception ex)
            {
                TempData["Error"]=ex.Message;   
            }
            return View(); 
        }
        #endregion
        #region Ajax -retun json(Array of Objects)

        //1- Get Cities data based on country Id 
        [HttpPost]
        public async Task<JsonResult> GetCityByCntryId(int cntyId)
        {
            var data = await city.GetAsync(x=>x.CountryId== cntyId);
            var result = mapper.Map<IEnumerable<CityVM>>(data);
            return Json(result);
        }
        //2- Get District data based on City id
        [HttpPost]
        public async Task<JsonResult> GetDistByCityId(int CtyId)
        {
            var data =await district.GetAsync(x=>x.CityId== CtyId);
            var result = mapper.Map<IEnumerable<DistrictVM>>(data);
            return Json(result);
        }


       
        #endregion
    }
}
