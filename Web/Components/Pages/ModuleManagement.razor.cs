using Database.Models;
using Database.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;

namespace Web.Components.Pages;

public partial class ModuleManagement : ComponentBase
{
    #region Parameters

    [Parameter] public int ModuleId { get; set; }

    private Module? _module;

    private List<Position> _allPositions = new();
    private List<EventType> _allTypes = new();
    private List<Employee> _employees = new();
    private List<Module> _otherModules = new();

    private List<Position> _includedPositions = new();
    private List<Access> _accesses = new();
    private List<Event> _events = new();
    private List<Material> _materials = new();

    private List<TestQuestion> _entranceQuestions = new();
    private List<TestQuestion> _finalQuestions = new();

    Exception? _exception;

    Event _newEvent = new Event();
    Material _newMaterial = new Material();

    #endregion

    protected override async Task OnInitializedAsync()
    {
        try
        {
            if (!UserService.Authorized())
                NavigationManager.NavigateTo("/");

            await LoadAllData();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            _exception = e;
        }
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (_exception != null)
            await JS.InvokeVoidAsync("showNotificationByAlert", $"Приозошла ошибка: {_exception.Message}");

        _exception = null;
    }

    private async Task LoadAllData()
    {
        try
        {
            _module = await FormationAdaptionProgramService.GetModuleById(ModuleId);

            if (_module == null)
                NavigationManager.NavigateTo("/ViewModules");

            _employees = await CompanyService.GetEmployeesList() ?? new();
            _otherModules = (await FormationAdaptionProgramService.GetModulesList()).Where(c => c.Id != ModuleId)
                .ToList() ?? new();
            _allPositions = await CompanyService.GetPositionsList() ?? new();
            _allTypes = await FormationAdaptionProgramService.GetEventTypesList() ?? new();

            _includedPositions = await FormationAdaptionProgramService.GetPositionsIncludedInModule(ModuleId) ?? new();
            _accesses = new();

            foreach (var position in _allPositions)
                _accesses.Add(new(position, _includedPositions.Find(c => c.Id == position.Id) != null));

            _events = await FormationAdaptionProgramService.GetModuleEvents(ModuleId) ?? new();
            _materials = await FormationAdaptionProgramService.GetModuleMaterials(ModuleId) ?? new();
            _entranceQuestions = await FormationAdaptionProgramService.GetModuleEntranceQuestions(ModuleId) ?? new();
            _finalQuestions = await FormationAdaptionProgramService.GetModuleFinalQuestions(ModuleId) ?? new();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }


    #region SaveChangesMethods

    private async Task AddEvent()
    {
        try
        {
            _newEvent.ModuleId = ModuleId;
            if (await FormationAdaptionProgramService.AddEvent(_newEvent)) await AddHistory();
            ;
            _newEvent = new Event();

            await LoadAllData();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            _exception = e;
        }
    }

    private async Task AddMaterial()
    {
        try
        {
            _newMaterial.ModuleId = ModuleId;

            if (_newMaterial.FileData != null)
            {
                if (await FormationAdaptionProgramService.AddMaterial(_newMaterial)) await AddHistory();
            }

            _newMaterial = new Material();

            await LoadAllData();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            _exception = e;
        }
    }

    private async Task LoadFile(InputFileChangeEventArgs obj)
    {
        try
        {
            long maxSize = 20000000;
            if (obj.File.Size > maxSize)
            {
                throw new Exception("Файл не может быть больше 20 Мбайт");
            }

            Stream stream = obj.File.OpenReadStream(maxSize);
            MemoryStream memoryStream = new MemoryStream();
            await stream.CopyToAsync(memoryStream);
            _newMaterial.FileData = memoryStream.ToArray();
            _newMaterial.FileName = obj.File.Name;
            stream.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            _exception = e;
        }
    }

    private async Task ChangeAccess(ChangeEventArgs obj, Access access)
    {
        try
        {
            access.IsChecked = (bool)obj.Value;

            if (await FormationAdaptionProgramService.IncludePositionsInModule(ModuleId,
                    _accesses.Where(c => c.IsChecked).Select(c => c.Position).ToList()))
                await AddHistory();

            await LoadAllData();
            StateHasChanged();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private async Task ChangeValue(ChangeEventArgs obj, Action<string> updateAction)
    {
        try
        {
            updateAction(obj.Value.ToString());

            await FormationAdaptionProgramService.UpdateModule(_module);
            await AddHistory();
            await LoadAllData();

            StateHasChanged();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private async Task ChangeName(ChangeEventArgs obj)
    {
        await ChangeValue(obj, value => _module.Name = value);
    }

    private async Task ChangeSource(ChangeEventArgs obj)
    {
        await ChangeValue(obj, value => _module.Source = value);
    }
    
    private async Task ChangeDeadline(ChangeEventArgs obj)
    {
        await ChangeValue(obj, value => _module.ModuleCompleteDeadline = int.Parse(value));
    }

    private async Task ChangePreviousModule(ChangeEventArgs obj)
    {
        await ChangeValue(obj, value => _module.PreviousId = int.Parse(value));
    }

    private async Task ChangeResponsiblePerson(ChangeEventArgs obj)
    {
        await ChangeValue(obj, value => _module.ResponsiblePersonId = int.Parse(value));
    }

    private async Task AddHistory()
    {
        try
        {
            await FormationAdaptionProgramService.AddModuleHistory(
                new() { ModuleId = ModuleId, EmployeeId = UserService.GetEmployee().Id, Datetime = DateTime.Now });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    #endregion


    class Access(Position position, bool isChecked)
    {
        public Position Position { get; set; } = position;
        public bool IsChecked { get; set; } = isChecked;
    }
}