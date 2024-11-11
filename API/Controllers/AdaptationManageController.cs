using Database.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AdaptationManageController(EmployeeAdaptationSystemDbContext context) : ControllerBase
{
    [HttpPost("Collaborations")]
    public async Task<IActionResult> AddModuleAndCollaborations([FromBody] List<Collaboration> collaborations)
    {
        try
        {
            Module module = collaborations.First()
                .Module;
            collaborations.ForEach(c => c.Module = module);

            await context.Collaborations.AddRangeAsync(collaborations);
            await context.SaveChangesAsync();

            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Conflict(e);
        }
    }

    [HttpGet("Modules/positionId={positionId:int}")]
    public async Task<IActionResult> GetModulesByPosition(int positionId)
    {
        try
        {
            return Ok(await context
                .ModuleAccesses.Where(c => c.PositionId == positionId)
                .Include(c => c.Module)
                .Include(c => c.Module.ResponsiblePerson)
                .Select(c => c.Module)
                .ToListAsync());
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Conflict(e);
        }
    }

    [HttpPost("AdaptationPrograms")]
    public async Task<IActionResult> AddAdaptationProgram(
        [FromBody] List<AdaptationProgramModule> adaptationProgramModules)
    {
        try
        {
            AdaptationProgram program = adaptationProgramModules.First()
                .AdaptationProgram;
            await context.AdaptationPrograms.AddAsync(program);
            adaptationProgramModules.ForEach(c => c.AdaptationProgram = program);
            await context.AdaptationProgramModules.AddRangeAsync(adaptationProgramModules);
            await context.SaveChangesAsync();

            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Conflict(e);
        }
    }
}