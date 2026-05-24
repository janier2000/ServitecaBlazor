using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.Extensions.Localization;
using MudBlazor;
using Serviteca.Frontend.Repositories;
using Serviteca.Shared.DTOs;
using Serviteca.Shared.Entities;
using Serviteca.Shared.Enums;

using E = Serviteca.Shared.Entities;

namespace Serviteca.Frontend.Pages.Customers;

public partial class CustomersForm
{
    private string? isActiveMessage;
    private EditContext editContext = null!;
    private DocumentType selectedDocumentType = new();
    private List<DocumentType>? LstDocumentType;
    private List<EnumGenericDTO>? LstGenderENT;

    [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
    [Inject] private IRepository Repository { get; set; } = null!;
    [Inject] private ISnackbar Snackbar { get; set; } = null!;
    [EditorRequired, Parameter] public CustomerDTO CustomerDtoENT { get; set; } = null!;
    [EditorRequired, Parameter] public EventCallback OnValidSubmit { get; set; }
    [EditorRequired, Parameter] public EventCallback ReturnAction { get; set; }
    private DateTime? selectedDate { get; set; } = DateTime.Now.Date;
    private EnumGenericDTO? selectedGender { get; set; }

    public bool FormPostedSuccessfully { get; set; } = false;

    protected override async Task OnInitializedAsync()
    {
        editContext = new(CustomerDtoENT);
        await LoadDocumentTypeAsync();
        LoadGenderAsync();
        LoadCustomers();
    }

    private async Task LoadDocumentTypeAsync()
    {
        var responseHttp = await Repository.GetAsync<List<DocumentType>>("/api/DocumentTypes/combo");
        if (responseHttp.Error)
        {
            var message = await responseHttp.GetErrorMessageAsync();
            await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
            return;
        }
        LstDocumentType = responseHttp.Response;
        if (CustomerDtoENT.DocumentTypeId != 0)
        {
            selectedDocumentType = LstDocumentType!.FirstOrDefault(x => x.Id == CustomerDtoENT.DocumentTypeId)!;
        }
    }

    private void LoadGenderAsync()
    {
        // Obtener todos los valores del enum en una lista
        List<string> LstGender = new List<string>(Enum.GetNames(typeof(Gender)));
        LstGenderENT = new List<EnumGenericDTO>();
        EnumGenericDTO genero = new EnumGenericDTO();
        //genero = new EnumGenericDTO
        //{
        //    Id = -1,
        //    Name = "-- Seleccione género --"
        //};
        //LstGenderENT.Add(genero);

        foreach (var item in LstGender)
        {
            genero = new EnumGenericDTO
            {
                Id = (int)Enum.Parse(typeof(Gender), item),
                Name = item
            };
            LstGenderENT.Add(genero);
        }

        if (CustomerDtoENT.gender != 0)
        {
            selectedGender = LstGenderENT!.FirstOrDefault(x => x.Id == CustomerDtoENT.gender)!;
        }
        //selectedGender = LstGenderENT.First();

        //List<GenderDTO> LstGenderDTO = new List<GenderDTO>((Gender[])Enum.GetValues(typeof(Gender)));
    }

    private void LoadCustomers()
    {
        if (CustomerDtoENT.ClientSince != default)
        {
            selectedDate = CustomerDtoENT.ClientSince;
        }
        else
        {
            selectedDate = DateTime.Now;
        }
    }

    protected override void OnParametersSet()
    {
        //base.OnParametersSet();
        //if (!string.IsNullOrEmpty(CustomerDTOENT.Image))
        //{
        //    CustomerDTOENT.Image = null;
        //}
        //isActiveMessage = CustomerDTOENT.IsActive ? $"{Localizer["Group"]} {Localizer["Active"]}" : $"{Localizer["Group"]} {Localizer["Inactive"]}";
    }

    private void OnToggledChanged(bool toggled)
    {
        //CustomerDTOENT.IsActive = toggled;
        //isActiveMessage = CustomerDTOENT.IsActive ? $"{Localizer["Group"]} {Localizer["Active"]}" : $"{Localizer["Group"]} {Localizer["Inactive"]}";
    }

    private void OnInvalidSubmit(EditContext editContext)
    {
        var messages = editContext.GetValidationMessages();

        foreach (var message in messages)
        {
            Snackbar.Add(message, Severity.Error);
        }
    }

    private async Task<IEnumerable<DocumentType>> SearchDocumentType(string searchText, CancellationToken cancellationToken)
    {
        await Task.Delay(5);
        if (string.IsNullOrWhiteSpace(searchText))
        {
            return LstDocumentType!;
        }

        return LstDocumentType!.Where(x => x.Name.Contains(searchText, StringComparison.InvariantCultureIgnoreCase)).ToList();
    }

    private void OnDateChanged(DateTime? newDate)
    {
        selectedDate = newDate;
        CustomerDtoENT.ClientSince = selectedDate!.Value;
    }

    private async Task OnBeforeInternalNavigation(LocationChangingContext context)
    {
        var formWasEdited = editContext.IsModified();
        if (!formWasEdited || FormPostedSuccessfully)
        {
            return;
        }

        var result = await SweetAlertService.FireAsync(new SweetAlertOptions
        {
            Title = "Confirmación",
            Text = "¿Deseas abandonar la página y perder los cambios?",
            Icon = SweetAlertIcon.Warning,
            ShowCancelButton = true,
            CancelButtonText = "Cancelar",
        });

        var confirm = !string.IsNullOrEmpty(result.Value);
        if (confirm)
        {
            return;
        }
        context.PreventNavigation();
    }

    private async Task<IEnumerable<DocumentType>> SearchFuncDocumentType(string searchText, CancellationToken cancellationToken)
    {
        await Task.Delay(5);
        if (string.IsNullOrWhiteSpace(searchText))
        {
            return LstDocumentType!;
        }

        return LstDocumentType!.Where(x => x.Name.Contains(searchText, StringComparison.InvariantCultureIgnoreCase)).ToList();
    }

    private void ChangedDocumentType(DocumentType documentTypeEnt)
    {
        selectedDocumentType = documentTypeEnt;
        CustomerDtoENT.DocumentTypeId = documentTypeEnt.Id;
    }

    private async Task<IEnumerable<EnumGenericDTO>> SearchFuncGender(string searchText, CancellationToken cancellationToken)
    {
        await Task.Delay(5);
        if (string.IsNullOrWhiteSpace(searchText))
        {
            return LstGenderENT!;
        }
        return LstGenderENT!.Where(x => x.Name.Contains(searchText, StringComparison.InvariantCultureIgnoreCase)).ToList();
    }

    private void ChangedGender(EnumGenericDTO genderDTOEnt)
    {
        selectedGender = genderDTOEnt;
        CustomerDtoENT.gender = genderDTOEnt.Id;
    }
}