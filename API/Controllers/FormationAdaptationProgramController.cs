using Database.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("api/FormationProgram")]
public class FormationAdaptationProgramController(EmployeeAdaptationSystemDbContext context) : ControllerBase
{
    #region Other

    [HttpGet("Collaborations/{id:int}")]
    public async Task<IActionResult> GetEmployeeCollaborationsList(int id)
    {
        try
        {
            var collaborations =
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


    [HttpPost("ModuleHistory")]
    public async Task<IActionResult> AddHistoryRow([FromBody] ModuleEditHistory history)
    {
        try
        {
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
            return Ok(await context.Modules.Include(c => c.Status).Include(c => c.ResponsiblePerson)
                .Include(c => c.Previous).ToListAsync());
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
            return Ok(await context.Modules.Include(c => c.Status).Include(c => c.ResponsiblePerson)
                .Include(c => c.Previous).FirstOrDefaultAsync(c => c.Id == moduleId));
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
            return Ok(await context.ModuleAccesses.Where(c => c.ModuleId == moduleId).Select(c => c.Position)
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
            return Ok(await context.Events.Include(c => c.Type).Where(c => c.ModuleId == moduleId).ToListAsync());
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
            return Ok(await context.Materials.Where(c => c.ModuleId == moduleId).ToListAsync());
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

    [HttpGet("EntranceQuestions/{moduleId:int}")]
    public async Task<IActionResult> GetModuleEntranceQuestions(int moduleId)
    {
        try
        {
            int testId = (await context.Testings.FirstAsync(c => c.ModuleId == moduleId && c.TypeId == 1)).Id;
            return Ok(await context.TestQuestions.Where(c => c.TestingId == testId).ToListAsync());
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Conflict(e);
        }
    }

    [HttpPost("EntranceQuestions/{moduleId:int}")]
    public async Task<IActionResult> AddModuleEntranceQuestions([FromBody] TestQuestion question,
        [FromRoute] int moduleId)
    {
        try
        {
            question.TestingId = (await context.Testings.FirstAsync(c => c.ModuleId == moduleId && c.TypeId == 1)).Id;
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

    [HttpGet("FinalQuestions/{moduleId:int}")]
    public async Task<IActionResult> GetModuleFinalQuestions(int moduleId)
    {
        try
        {
            int testId = (await context.Testings.FirstAsync(c => c.ModuleId == moduleId && c.TypeId == 2)).Id;
            return Ok(await context.TestQuestions.Where(c => c.TestingId == testId).ToListAsync());
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Conflict(e);
        }
    }

    [HttpPost("FinalQuestions/{moduleId:int}")]
    public async Task<IActionResult> AddModuleFinalQuestions([FromBody] TestQuestion question, [FromRoute] int moduleId)
    {
        try
        {
            question.TestingId = (await context.Testings.FirstAsync(c => c.ModuleId == moduleId && c.TypeId == 2)).Id;
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