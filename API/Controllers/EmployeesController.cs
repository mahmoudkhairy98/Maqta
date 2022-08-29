using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository _repo;

        public EmployeesController(IEmployeeRepository repo)
        {
            this._repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult<Employee>> GetEmployees()
        {
            var employees= await _repo.GetAsync();
            return Ok(employees);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            var employee = await _repo.GetByIdAsync(id);
            return Ok(employee);
        }
        [HttpPost]
        public async Task<ActionResult> Create([FromBody]Employee emp)
        {
            if (emp == null)
            {
                return NotFound();
            }
            try
            {
                await _repo.CreateAsync(emp);
                return Ok("Employee Added");
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        [HttpPut]
        public async Task<ActionResult> Update([FromBody] Employee emp)
        {
            if (emp == null)
            {
                return NotFound("Getting null for employee");
            }

            await _repo.UpdateAsync(emp);
            return Ok("Employee Updated");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound("Getting null for employee id");
            }
            await _repo.DeleteAsync(id);
            return Ok("Employee Deleted");
        }
    }
}
