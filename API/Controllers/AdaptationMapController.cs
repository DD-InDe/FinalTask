using System.Reflection;
using Database.APIModels;
using Database.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace API.Controllers;

[ApiController]
[Route("api")]
public class AdaptationMapController(EmployeeAdaptationSystemDbContext context) : ControllerBase
{
    [HttpGet("Map/{employeeId:int}")]
    public async Task<IActionResult> GetMap(int employeeId)
    {
        try
        {
            AdaptationMapModel mapModel = new AdaptationMapModel();

            List<AdaptationProgramModule> list = await context
                .AdaptationProgramModules
                .Include(c => c.Module)
                .Include(c => c.AdaptationProgram)
                .Include(c => c.Mentor)
                .Where(c => c.AdaptationProgram.EmployeeId == employeeId)
                .ToListAsync();
            
            if (list.IsNullOrEmpty())
                return NoContent();
            
            mapModel.AdaptationProgramId = list.First()
                .AdaptationProgramId.Value;
            list.ForEach(c => { mapModel.Modules.Add(c.Module); });

            return Ok(mapModel);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Conflict(e);
        }
    }
}