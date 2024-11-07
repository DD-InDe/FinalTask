using System.Net.Http.Json;
using System.Text.Json;
using Database.Models;

namespace Database.Services;

public abstract class FormationAdaptionProgramService : ApiBase
{
    #region Other

    public static async Task<List<ModuleStatus>?> GetModuleStatusesList()
    {
        try
        {
            var message = await Client.GetAsync(BaseUrl + $"api/FormationProgram/Statuses");
            if (message.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<List<ModuleStatus>>(
                    await message.Content.ReadAsStreamAsync(),
                    Options);
            }

            return null;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public static async Task<bool> AddModuleHistory(ModuleEditHistory history)
    {
        try
        {
            var message = await Client.PostAsJsonAsync(BaseUrl + $"api/FormationProgram/ModuleHistory", history);
            Console.WriteLine(await message.Content.ReadAsStringAsync());
            return message.IsSuccessStatusCode;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    #endregion

    #region Collaborations

    public static async Task<List<Module>?> GetEmployeeDevelopModules(int employeeId)
    {
        try
        {
            var message = await Client.GetAsync(BaseUrl + $"api/FormationProgram/EmployeeDevelopModules/{employeeId}");
            if (message.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<List<Module>>(
                    await message.Content.ReadAsStreamAsync(),
                    Options);
            }

            return null;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public static async Task<List<Module>?> GetEmployeeAccessModules(int employeeId)
    {
        try
        {
            var message = await Client.GetAsync(BaseUrl + $"api/FormationProgram/EmployeeAccessModules/{employeeId}");
            if (message.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<List<Module>>(
                    await message.Content.ReadAsStreamAsync(),
                    Options);
            }

            return null;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public static async Task<List<Collaboration>?> GetCollaborations()
    {
        try
        {
            var message = await Client.GetAsync(BaseUrl + $"api/FormationProgram/Collaborations");
            if (message.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<List<Collaboration>>(
                    await message.Content.ReadAsStreamAsync(),
                    Options);
            }

            return null;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public static async Task<List<Collaboration>?> GetModuleDevelopers(int moduleId)
    {
        try
        {
            var message = await Client.GetAsync(BaseUrl + $"api/FormationProgram/ModuleDevelopers/{moduleId}");
            if (message.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<List<Collaboration>>(
                    await message.Content.ReadAsStreamAsync(),
                    Options);
            }

            return null;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public static async Task<bool> RejectModule(int moduleId)
    {
        try
        {
            var message = await Client.PostAsync(BaseUrl + $"api/FormationProgram/RejectModule", null);
            return message.IsSuccessStatusCode;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public static async Task<bool> AcceptModule(int moduleId, int employeeId)
    {
        try
        {
            var message = await Client.PostAsync(BaseUrl + $"api/FormationProgram/AcceptModule/{moduleId},{employeeId}",
                null);
            return message.IsSuccessStatusCode;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    #endregion

    #region Modules

    public static async Task<List<Module>?> GetModulesList()
    {
        try
        {
            var message = await Client.GetAsync(BaseUrl + "api/FormationProgram/Modules");
            if (message.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<List<Module>>(await message.Content.ReadAsStreamAsync(),
                    Options);
            }

            return null;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public static async Task<Module?> GetModuleById(int moduleId)
    {
        try
        {
            var message = await Client.GetAsync(BaseUrl + $"api/FormationProgram/Modules/{moduleId}");
            if (message.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<Module>(await message.Content.ReadAsStreamAsync(),
                    Options);
            }

            return null;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public static async Task<bool?> UpdateModule(Module module)
    {
        try
        {
            module.Status = null;
            var message = await Client.PutAsJsonAsync(BaseUrl + $"api/FormationProgram/Modules", module);
            return message.IsSuccessStatusCode;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    #endregion

    #region Positions

    public static async Task<List<Position>?> GetPositionsIncludedInModule(int moduleId)
    {
        try
        {
            var message = await Client.GetAsync(BaseUrl + $"api/FormationProgram/Positions/{moduleId}");
            if (message.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<List<Position>>(await message.Content.ReadAsStreamAsync(),
                    Options);
            }

            return null;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public static async Task<bool> IncludePositionsInModule(int moduleId, List<Position> positions)
    {
        try
        {
            var message =
                await Client.PostAsJsonAsync(BaseUrl + $"api/FormationProgram/Positions/{moduleId}", positions);
            return message.IsSuccessStatusCode;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    #endregion

    #region Events

    public static async Task<List<Event>?> GetModuleEvents(int moduleId)
    {
        try
        {
            var message = await Client.GetAsync(BaseUrl + $"api/FormationProgram/Events/{moduleId}");
            if (message.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<List<Event>>(await message.Content.ReadAsStreamAsync(),
                    Options);
            }

            return null;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public static async Task<bool> AddEvent(Event @event)
    {
        try
        {
            var message = await Client.PostAsJsonAsync(BaseUrl + $"api/FormationProgram/Events", @event);
            return message.IsSuccessStatusCode;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public static async Task<List<EventType>?> GetEventTypesList()
    {
        try
        {
            var message = await Client.GetAsync(BaseUrl + $"api/FormationProgram/EventTypes");
            if (message.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<List<EventType>>(await message.Content.ReadAsStreamAsync(),
                    Options);
            }

            return null;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    #endregion

    #region Materials

    public static async Task<List<Material>?> GetModuleMaterials(int moduleId)
    {
        try
        {
            var message = await Client.GetAsync(BaseUrl + $"api/FormationProgram/Materials/{moduleId}");
            if (message.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<List<Material>>(await message.Content.ReadAsStreamAsync(),
                    Options);
            }

            return null;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public static async Task<bool> AddMaterial(Material material)
    {
        try
        {
            var message = await Client.PostAsJsonAsync(BaseUrl + $"api/FormationProgram/Materials", material);
            return message.IsSuccessStatusCode;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    #endregion

    #region Questions

    public static async Task<List<TestQuestion>?> GetModuleQuestions(int moduleId)
    {
        try
        {
            var message = await Client.GetAsync(BaseUrl + $"api/FormationProgram/Questions/{moduleId}");
            if (message.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<List<TestQuestion>>(
                    await message.Content.ReadAsStreamAsync(),
                    Options);
            }

            return null;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public static async Task<bool> AddModuleQuestion(int moduleId, int typeId, TestQuestion question)
    {
        try
        {
            var message = await Client.PostAsJsonAsync(BaseUrl + $"api/FormationProgram/Questions/{typeId},{moduleId}",
                question);
            return message.IsSuccessStatusCode;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    #endregion
}