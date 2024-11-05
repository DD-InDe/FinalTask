using Database.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CompanyController(EmployeeAdaptationSystemDbContext context) : ControllerBase
{
    [HttpGet("Employees")]
    public async Task<IActionResult> GetEmployeesList()
    {
        try
        {
            return Ok(await context.Employees.ToListAsync());
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Conflict(e);
        }
    }


    [HttpGet("Positions")]
    public async Task<IActionResult> GetPositionsList()
    {
        try
        {
            return Ok(await context.Positions.ToListAsync());
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Conflict(e);
        }
    }


    [HttpGet("Departments")]
    public async Task<IActionResult> GetDepartmentsList()
    {
        try
        {
            return Ok(await context.Departments.ToListAsync());
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Conflict(e);
        }
    }
}