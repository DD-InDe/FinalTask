using System.ComponentModel.DataAnnotations;
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

            foreach (var item in list)
            {
                mapModel.Modules.Add(new()
                {
                    Module = item.Module,
                    Mentor = item.Mentor,
                    Progress = await GetProgress(employeeId, item.ModuleId.Value),
                    Rating = await GetRating(employeeId, item.ModuleId.Value),
                });
            }

            return Ok(mapModel);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Conflict(e);
        }
    }

    private async Task<int> GetRating(int employeeId, int moduleId)
    {
        try
        {
            // находим ласт тест
            TestingResult? testingResult = await context
                .TestingResults.Include(c => c.Testing)
                .OrderBy(c => c.Id)
                .LastOrDefaultAsync(c => c.Testing.ModuleId == moduleId && c.EmployeeId == employeeId);

            if (testingResult != null)
            {
                int maxCorrect = (await context
                        .TestQuestions.Where(c => c.TestingId == testingResult.TestingId)
                        .ToListAsync())
                    .Count;

                switch (testingResult.CorrectAnswers.Value)
                {
                    case 0:
                        return 1;
                    case var value when value == maxCorrect:
                        return 3;
                    default:
                        return 2;
                }
            }

            return 0;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private async Task<float> GetProgress(int employeeId, int moduleId)
    {
        try
        {
            List<Material> materials = await context
                .Materials.Where(c => c.ModuleId == moduleId)
                .ToListAsync();
            int downloadedCount = 0;
            foreach (var item in materials)
            {
                if (await context.DownloadHistories.AnyAsync(c =>
                        c.MaterialId == item.Id && c.EmployeeId == employeeId))
                    downloadedCount++;
            }

            if (downloadedCount == 0) return 0;
            return (materials.Count / downloadedCount) * 100;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpPost("DownloadHistories")]
    public async Task<IActionResult> AddHistory([Required] int employeeId, [Required] int materialId)
    {
        try
        {
            if (!await context.DownloadHistories.AnyAsync(c =>
                    c.EmployeeId == employeeId && c.MaterialId == materialId))
            {
                await context.DownloadHistories.AddAsync(new() { EmployeeId = employeeId, MaterialId = materialId });
                await context.SaveChangesAsync();
            }

            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Conflict(e);
        }
    }
}