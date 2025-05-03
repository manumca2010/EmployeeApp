using EmployeeRepository.Interfaces;
using EmployeeRepository.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _repository;
        public EmployeeController(IEmployeeRepository repository)
        {
            _repository = repository;
        }
        [HttpGet("GetEmpDetails")]
       // [Route("GetEmpDetails")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetAll()
            => Ok(await _repository.GetAllAsync());

        [HttpGet("GetEmpDetailsById/{id}")]
        //[Route("GetEmpDetailsById")]
        public async Task<ActionResult<Employee>> GetById(int id)
        {
            var employee = await _repository.GetByIdAsync(id);
            if (employee == null) return NotFound();
            return Ok(employee);
        }

        [HttpPost("SaveEmpDetails")]
        //[Route("SaveEmpDetails")]
        public async Task<ActionResult<Employee>> Create(Employee employee)
        {
            var created = await _repository.AddAsync(employee);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("UpdateEmpDetails/{id}")]
        //[Route("UpdateEmpDetails")]
        public async Task<IActionResult> Update(int id, Employee employee)
        {
            if (id != employee.Id) return BadRequest();
            await _repository.UpdateAsync(employee);
            return NoContent();
        }

        [HttpDelete("DeleteEmpDetails/{id}")]
        //[Route("DeleteEmpDetails")]
        public async Task<IActionResult> Delete(int id)
        {
            await _repository.DeleteAsync(id);
            return NoContent();
        }

    }
}
