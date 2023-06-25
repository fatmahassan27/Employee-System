using Demo.BL.ModelVM;
using Demo.DAL.Entity;
using Demo.BL.Interface;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Demo.BL.Repository;

namespace Demo.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartment department;//loosly couple depend on interface
        private readonly IMapper mapper;

        public DepartmentController(IDepartment department ,IMapper mapper)
        {
            this.department = department;
            this.mapper = mapper;
        }
        public async  Task<IActionResult> Index()
        {
            var data =  await department.GetAsync();
            var result= mapper.Map<IEnumerable<DepartmentVM>>(data);
            return View(result);
        }
        #region create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(DepartmentVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = mapper.Map<Department>(model);
                    await  department.CreateAsync(result);
                    return RedirectToAction("Index");

                }

            }catch(Exception ex)
            {
                TempData["error"] = ex.Message;

            }
            return RedirectToAction("Index");
        }
        #endregion

        public async Task<IActionResult> Details(int id)
        {

            var data = await department.GetByIdAsync(id);
            var result = mapper.Map<DepartmentVM>(data);
            return View(result);
        }
        [HttpGet]
        public  async Task<IActionResult> Update(int id)
        {
            var data = await department.GetByIdAsync(id);
            var result = mapper.Map<DepartmentVM>(data);
            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> Update(DepartmentVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = mapper.Map<Department>(model);
                    await department.UpdateAsync(result);
                    return RedirectToAction("Index");

                }

            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;

            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await department.GetByIdAsync(id);
            var result = mapper.Map<DepartmentVM>(data);
            return View(result);
        }
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await department.DeleteAsync(id);
                    return RedirectToAction("Index");
                }

            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
            }

            //ModelState.Clear();
            return View();

        }
    }
}
