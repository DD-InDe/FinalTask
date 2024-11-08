using Database.Models;
using Microsoft.AspNetCore.Mvc;

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
}