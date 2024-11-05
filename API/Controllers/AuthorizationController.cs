using Database.APIModels;
using Database.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthorizationController(EmployeeAdaptationSystemDbContext context) : ControllerBase
{
    [HttpPost("[action]")]
    public async Task<IActionResult> LogIn([FromBody] LogInModel logInModel)
    {
        try
        {
            Employee? employee = await context.Employees.FirstOrDefaultAsync(c =>
                c.Login == logInModel.Login && c.Password == logInModel.Password);
            if (employee != null) return Ok(employee);
            return NotFound("Пользователь не найден!");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Conflict(e);
        }
    }
}