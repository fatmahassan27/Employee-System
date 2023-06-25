using AutoMapper;
using Demo.BL.Helper;
using Demo.BL.Interface;
using Demo.BL.ModelVM;
using Demo.DAL.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Demo.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployee employee;
        private readonly IMapper mapper;

        public EmployeeController(IEmployee employee ,IMapper mapper)
        {
            this.employee = employee;
            this.mapper = mapper;
        }
        [Route("~/api/Empolyee/GetEmployees")]
        public  async Task<IActionResult> GetEmployees()
        {
            try
            {
                var data = await employee.GetAsync(x => x.IsDeleted == false && x.IsActive == true);
                var result = mapper.Map<IEnumerable<EmployeeVM>>(data);
                return Ok(new ApiResponse<IEnumerable<EmployeeVM>>()
                {
                    Code = 200,
                    Status = "ok",
                    Message = "data is found",
                    Data = result
                });
            }catch(Exception ex)
            { 
                return NotFound(new ApiResponse<string>()
                {
                    Code = 200,
                    Status = "owsome",
                    Message = "data not found",
                    Data = ex.Message
                });

            }
         

        }

        [HttpGet]
        [Route("~/api/Empolyee/GetEmployeebyId/{id}")]
        public async Task<IActionResult> GetEmployeebyId(int id)
        {
            try
            {
                var data = employee.GetByIdAsync(x => x.IsDeleted == false && x.IsActive == true && x.Id == id);
                var result = mapper.Map<EmployeeVM>(data);
                return Ok(new ApiResponse<EmployeeVM>()
                {
                    Code = 200,
                    Status = "ok",
                    Message = "data is found",
                    Data = result
                });
            }catch(Exception ex)
            {
                return NotFound(new ApiResponse<string>()
                {
                    Code = 200,
                    Status = "owsome",
                    Message = "data not found",
                    Data = ex.Message
                });
            }
           
        }


        [HttpPost]
        [Route("~/api/Empolyee/PostEmployee")]
        public async Task<IActionResult> PostEmployee(EmployeeVM model)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    var data=mapper.Map<Employee>(model);
                    var result = await employee.CreateAsync(data);
                    return Ok(new ApiResponse<Employee>()
                    {
                        Code = 201,
                        Status = "Created",
                        Message = "data saved ",
                        Data = result
                    });
                   
                }
                return BadRequest(new ApiResponse<string>()
                {
                    Code = 404,
                    Status = "BadRequest",
                    Message = "data not saved "
                });
            }
            catch(Exception ex)
            {
                return BadRequest(new ApiResponse<string>()
                {
                    Code = 404,
                    Status = "BadRequest",
                    Message = "data not saved ",
                    Data= ex.Message
                });
            }
        }

        [HttpPost]
        [Route("~/api/Empolyee/PutEmpolyee")]
        public async Task<IActionResult> PutEmpolyee(EmployeeVM model)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    var data = mapper.Map<Employee>(model);
                    await employee.UpdateAsync(data);
                    return Ok(new ApiResponse<string>()
                    {
                        Code = 201,
                        Status = "Updated",
                        Message = "data updated ",
                        Data = "No result to return "
                    });

                }
                return BadRequest(new ApiResponse<string>()
                {
                    Code = 400,
                    Status = "Bad Request",
                    Message = "Validation Error"
                });

            }
            catch(Exception ex)
            {
                return BadRequest(new ApiResponse<string>()
                {
                    Code = 404,
                    Status = "BadRequest",
                    Message = "data not updated ",
                    Data = ex.Message
                });
            }
        }


        [HttpDelete]
        [Route("~/api/Empolyee/DeleteEmployee")]
        public async Task<IActionResult> DeleteEmpolyee(EmployeeVM model)
        {
            try
            {
                var data = mapper.Map<Employee>(model);
                await employee.DeleteAsync(data);
                return Ok(new ApiResponse<string>()
                {
                    Code = 201,
                    Status = "Deleted",
                    Message = "Data Deleted",
                    Data = "No result return "
                });
            }catch(Exception ex)
            {
                return BadRequest(new ApiResponse<string>()
                {
                    Code=404,
                    Status="Not Deleted",
                    Message="Data Not Deleted",
                    Data = ex.Message

                });
            }
        }
    }
}
