using Database.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

/// <summary>
/// Контроллер для форматирования программ адаптации.
/// </summary>
/// <param name="context"></param>
[ApiController]
[Route("api/FormationProgram")]
public class FormationAdaptationProgramController(EmployeeAdaptationSystemDbContext context) : ControllerBase
{
    /// <summary>
    /// Метод для получения списка коллабораций в которых участвует сотрудник
    /// </summary>
    /// <param name="id">Идентификатор сотрудника</param>
    /// <returns>Список модулей</returns>
    [HttpGet("Collaborations/{id:int}")]
    public async Task<IActionResult> GetEmployeeCollaborationsList(int id)
    {
        try
        {
            List<Collaboration> collaborations =
                await context.Collaborations
                    .Where(c => c.EmployeeId == id)
                    .Include(c => c.Module)
                    .Include(c => c.Role)
                    .ToListAsync();

            return Ok(collaborations);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Conflict(e);
        }
    }

    /// <summary>
    /// Вывод всех модулей
    /// </summary>
    /// <returns></returns>
    [HttpGet("Modules")]
    public async Task<IActionResult> GetModulesList()
    {
        try
        {
            return Ok(await context.Modules.Include(c => c.Status).Include(c => c.ResponsiblePerson)
                .Include(c => c.Previous).ToListAsync());
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Conflict(e);
        }
    }

    /// <summary>
    /// Вывод конкретного модуля
    /// </summary>
    /// <param name="moduleId">Идентификатор модуля</param>
    /// <returns>Модуль</returns>
    [HttpGet("Modules/{moduleId}")]
    public async Task<IActionResult> GetModuleById(int moduleId)
    {
        try
        {
            return Ok(await context.Modules.Include(c => c.Status).Include(c => c.ResponsiblePerson)
                .Include(c => c.Previous).FirstOrDefaultAsync(c => c.Id == moduleId));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Conflict(e);
        }
    }

    /// <summary>
    /// Вывод всех статусов модуля
    /// </summary>
    /// <returns>Список статусов</returns>
    [HttpGet("Statuses")]
    public async Task<IActionResult> GetModuleStatusesList()
    {
        try
        {
            return Ok(await context.ModuleStatuses.ToListAsync());
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Conflict(e);
        }
    }

    /// <summary>
    /// Вывод мероприятий конкретного модуля
    /// </summary>
    /// <param name="moduleId">Идентификатор модуля</param>
    /// <returns></returns>
    [HttpGet("Events/{moduleId}")]
    public async Task<IActionResult> GetModuleEventsList(int moduleId)
    {
        try
        {
            return Ok(await context.Events.Where(c => c.ModuleId == moduleId).ToListAsync());
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Conflict(e);
        }
    }

    /// <summary>
    /// Вывод материалов конкркетного модуля
    /// </summary>
    /// <param name="moduleId">Идентификатор модуля</param>
    /// <returns>Список матераилов</returns>
    [HttpGet("Materials/{moduleId}")]
    public async Task<IActionResult> GetModuleMaterialList(int moduleId)
    {
        try
        {
            return Ok(await context.Materials.Where(c => c.ModuleId == moduleId).ToListAsync());
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Conflict(e);
        }
    }
}