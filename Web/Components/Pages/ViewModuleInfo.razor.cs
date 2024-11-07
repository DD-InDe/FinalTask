using Database.Models;
using Database.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Web.Components.Pages;

public partial class ViewModuleInfo : ComponentBase
{
    #region Parameters

    [Parameter] public int ModuleId { get; set; }

    private Module? _module;

    private List<Event> _events = new();
    private List<TestQuestion> _questions = new();
    private List<Material> _materials = new();
    private List<Position> _positions = new();
    private List<Employee> _developers = new();

    private string Why = string.Empty;
    private string Days = string.Empty;

    private Exception? _exception;

    #endregion

    protected override async Task OnInitializedAsync()
    {
        try
        {
            if (!UserService.Authorized()) NavigationManager.NavigateTo("/");

            _module = await FormationAdaptionProgramService.GetModuleById(ModuleId);
            _events = await FormationAdaptionProgramService.GetModuleEvents(ModuleId) ?? new();
            _questions = await FormationAdaptionProgramService.GetModuleQuestions(ModuleId) ?? new();
            _materials = await FormationAdaptionProgramService.GetModuleMaterials(ModuleId) ?? new();
            _positions = await FormationAdaptionProgramService.GetPositionsIncludedInModule(ModuleId) ?? new();

            _developers = (await FormationAdaptionProgramService.GetModuleDevelopers(ModuleId)).Select(c => c.Employee)
                .ToList();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            _exception = e;
        }
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        try
        {
            if (_exception != null) await JS.InvokeVoidAsync("showNotification", "Ошибка", _exception.Message);
            _exception = null;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            await JS.InvokeVoidAsync("showNotification", "Ошибка", e.Message);
        }
    }


    private async Task Reject()
    {
        try
        {
            await FormationAdaptionProgramService.RejectModule(ModuleId);
            NavigationManager.NavigateTo("PersonalArea");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            _exception = e;
        }
    }

    private async Task Accept()
    {
        try
        {
            await FormationAdaptionProgramService.AcceptModule(ModuleId, UserService.GetEmployee()
                .Id);
            NavigationManager.NavigateTo("PersonalArea");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            _exception = e;
        }
    }
}