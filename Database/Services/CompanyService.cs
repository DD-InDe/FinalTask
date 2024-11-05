using System.Net.Http.Json;
using System.Text.Json;
using Database.Models;

namespace Database.Services;

public class CompanyService : ApiBase
{
    public static async Task<List<Employee>?> GetEmployeesList()
    {
        try
        {
            var message = await Client.GetAsync(BaseUrl + "api/Company/Employees");
            if (message.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<List<Employee>>(await message.Content.ReadAsStreamAsync(),
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

    public static async Task<List<Position>?> GetPositionsList()
    {
        try
        {
            var message = await Client.GetAsync(BaseUrl + "api/Company/Positions");
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

    public static async Task<List<Department>?> GetDepartmentsList()
    {
        try
        {
            var message = await Client.GetAsync(BaseUrl + "api/Company/Departments");
            if (message.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<List<Department>>(
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
}