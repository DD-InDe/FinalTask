using Database.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("api/FormationProgram")]
public class FormationAdaptationProgramController(EmployeeAdaptationSystemDbContext context) : ControllerBase
{
    #region Collaborations

    [HttpGet("EmployeeDevelopModules/{employeeId:int}")]
    public async Task<IActionResult> GetEmployeeDevelopModules(int employeeId)
    {
        try
        {
            var collaborations =
                await context.Collaborations
                    .Where(c => c.EmployeeId == employeeId && c.RoleId == 1)
                    .Include(c => c.Module)
                    .Include(c => c.Role)
                    .Select(c => c.Module)
                    .ToListAsync();

            return Ok(collaborations);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Conflict(e);
        }
    }

    [HttpGet("Collaborations")]
    public async Task<IActionResult> GetCollaborations()
    {
        try
        {
            var collaborations =
                await context.Collaborations
                    .Include(c => c.Employee)
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

    [HttpGet("ModuleDevelopers/{moduleId:int}")]
    public async Task<IActionResult> GetDevelopers(int moduleId)
    {
        try
        {
            var collaborations =
                await context.Collaborations
                    .Include(c => c.Employee)
                    .Include(c => c.Module)
                    .Include(c => c.Role)
                    .Where(c => c.RoleId == 1 && c.ModuleId == moduleId)
                    .ToListAsync();

            return Ok(collaborations);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Conflict(e);
        }
    }

    [HttpGet("EmployeeAccessModules/{employeeId:int}")]
    public async Task<IActionResult> GetEmployeeAccessModules(int employeeId)
    {
        try
        {
            var collaborations =
                await context.Collaborations
                    .Include(c => c.Module)
                    .Include(c => c.Role)
                    .Where(c => c.EmployeeId == employeeId && c.RoleId == 2 && c.Module.StatusId == 4)
                    .Select(c => c.Module)
                    .ToListAsync();

            return Ok(collaborations);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Conflict(e);
        }
    }

    [HttpPost("RejectModule/{moduleId:int}")]
    public async Task<IActionResult> RejectModule(int moduleId)
    {
        try
        {
            Module module = await context.Modules.FirstOrDefaultAsync(c => c.Id == moduleId);
            module.StatusId = 2;

            List<Collaboration> collaborations = await context.Collaborations
                .Where(c => c.ModuleId == moduleId && c.RoleId == 2)
                .ToListAsync();
            foreach (var collaboration in collaborations)
                collaboration.IsAccepted = false;

            await context.SaveChangesAsync();
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Conflict(e);
        }
    }

    [HttpPost("AcceptModule/{moduleId:int},{employeeId:int}")]
    public async Task<IActionResult> AcceptModule(int moduleId, int employeeId)
    {
        try
        {
            Collaboration collaboration =
                await context.Collaborations.FirstOrDefaultAsync(c =>
                    c.ModuleId == moduleId && c.EmployeeId == employeeId);
            if (collaboration != null)
            {
                collaboration.IsAccepted = true;
                await context.SaveChangesAsync();

                if ((await context.Collaborations.Where(c => c.ModuleId == moduleId && c.RoleId == 2)
                        .ToListAsync()).All(c => c.IsAccepted == true))
                {
                    Module module = await context.Modules.FirstAsync(c => c.Id == moduleId);
                    module.StatusId = 3;
                    module.IsReleased = true;
                    await context.SaveChangesAsync();
                }

                return Ok();
            }

            return NotFound();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Conflict(e);
        }
    }

    #endregion

    #region Other

    [HttpPost("ModuleHistory")]
    public async Task<IActionResult> AddHistoryRow([FromBody] ModuleEditHistory history)
    {
        try
        {
            context.Attach(history.Module);
            context.Attach(history.Employee);
            await context.ModuleEditHistories.AddAsync(history);
            await context.SaveChangesAsync();
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Conflict(e);
        }
    }

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

    #endregion

    #region Modules

    [HttpGet("Modules")]
    public async Task<IActionResult> GetModulesList()
    {
        try
        {
            return Ok(await context.Modules.Include(c => c.Status)
                .Include(c => c.ResponsiblePerson)
                .Include(c => c.Previous)
                .ToListAsync());
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Conflict(e);
        }
    }

    [HttpGet("Modules/{moduleId}")]
    public async Task<IActionResult> GetModuleById(int moduleId)
    {
        try
        {
            return Ok(await context.Modules.Include(c => c.Status)
                .Include(c => c.ResponsiblePerson)
                .Include(c => c.Previous)
                .FirstOrDefaultAsync(c => c.Id == moduleId));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Conflict(e);
        }
    }

    [HttpPut("Modules")]
    public async Task<IActionResult> UpdateModule([FromBody] Module module)
    {
        try
        {
            module.ResponsiblePerson = null;
            module.Previous = null;

            context.Modules.Update(module);
            await context.SaveChangesAsync();
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Conflict(e);
        }
    }

    #endregion

    #region Positions

    [HttpGet("Positions/{moduleId}")]
    public async Task<IActionResult> GetPositionsIncludedInModule(int moduleId)
    {
        try
        {
            return Ok(await context.ModuleAccesses.Where(c => c.ModuleId == moduleId)
                .Select(c => c.Position)
                .ToListAsync());
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Conflict(e);
        }
    }

    [HttpPost("Positions/{moduleId}")]
    public async Task<IActionResult> IncludePositionsInModule([FromBody] List<Position> positions,
        [FromRoute] int moduleId)
    {
        try
        {
            context.ModuleAccesses.RemoveRange(await context.ModuleAccesses.Where(c => c.ModuleId == moduleId)
                .ToListAsync());
            foreach (var position in positions)
                await context.ModuleAccesses.AddAsync(new() { ModuleId = moduleId, PositionId = position.Id });

            await context.SaveChangesAsync();
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Conflict(e);
        }
    }

    #endregion

    #region Events

    [HttpGet("Events/{moduleId}")]
    public async Task<IActionResult> GetModuleEventsList(int moduleId)
    {
        try
        {
            return Ok(await context.Events.Include(c => c.Type)
                .Where(c => c.ModuleId == moduleId)
                .ToListAsync());
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Conflict(e);
        }
    }

    [HttpPost("Events")]
    public async Task<IActionResult> AddEvent([FromBody] Event @event)
    {
        try
        {
            await context.Events.AddAsync(@event);
            await context.SaveChangesAsync();
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Conflict(e);
        }
    }

    [HttpGet("EventTypes")]
    public async Task<IActionResult> GetEventTypesList()
    {
        try
        {
            return Ok(await context.EventTypes.ToListAsync());
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Conflict(e);
        }
    }

    #endregion

    #region Materials

    [HttpGet("Materials/{moduleId}")]
    public async Task<IActionResult> GetModuleMaterialList(int moduleId)
    {
        try
        {
            return Ok(await context.Materials.Where(c => c.ModuleId == moduleId)
                .ToListAsync());
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Conflict(e);
        }
    }


    [HttpPost("Materials")]
    public async Task<IActionResult> AddMaterial([FromBody] Material material)
    {
        try
        {
            await context.Materials.AddAsync(material);
            await context.SaveChangesAsync();
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Conflict(e);
        }
    }

    #endregion

    #region Questions

    [HttpGet("Questions/{moduleId:int}")]
    public async Task<IActionResult> GetModuleEntranceQuestions(int moduleId)
    {
        try
        {
            return Ok(await context.TestQuestions.Include(c => c.Testing)
                .Where(c => c.Testing.ModuleId == moduleId)
                .ToListAsync());
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Conflict(e);
        }
    }

    [HttpPost("Questions/{typeId:int},{moduleId:int}")]
    public async Task<IActionResult> AddModuleEntranceQuestions([FromBody] TestQuestion question,
        [FromRoute] int typeId, [FromRoute] int moduleId)
    {
        try
        {
            question.TestingId = (await context.Testings.FirstAsync(c => c.ModuleId == moduleId && c.TypeId == typeId))
                .Id;
            await context.TestQuestions.AddAsync(question);
            await context.SaveChangesAsync();
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Conflict(e);
        }
    }

    #endregion
}