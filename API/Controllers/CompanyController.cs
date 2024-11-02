using Database.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

/// <summary>
/// Контроллер для управления сущностями системы.
/// Обрабатывает запросы на получение информации о различных сущностях.
/// </summary>
/// <param name="context">Контекст базы данных</param>
[ApiController]
[Route("api/[controller]")]
public class CompanyController(EmployeeAdaptationSystemDbContext context) : ControllerBase
{
    /// <summary>
    /// Вывод всех сотрудников
    /// </summary>
    /// <returns>Коллекция сотрудников</returns>
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

    /// <summary>
    /// Вывод всех должностей
    /// </summary>
    /// <returns></returns>
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

    /// <summary>
    /// Вывод всех отделов
    /// </summary>
    /// <returns></returns>
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