using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using MudBlazor;
using Serviteca.Frontend.Repositories;
using Serviteca.Frontend.Shared;
using Serviteca.Shared.DTOs;
using Serviteca.Shared.Entities;
using System.Drawing;

namespace Serviteca.Frontend.Pages.Soats;

public partial class SoatsForm
{
    private EditContext editContext = null!;

    private List<Insurer>? LstInsurer;
    private Insurer selectedInsurer = new();

    private List<VehicleDTO>? LstVehicle;
    private VehicleDTO selectedVehicle = new();

    [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
    [Inject] private IRepository Repository { get; set; } = null!;
    [Inject] private ISnackbar Snackbar { get; set; } = null!;
    [EditorRequired, Parameter] public SoatDTO SoatDtoENT { get; set; } = null!;
    [EditorRequired, Parameter] public EventCallback OnValidSubmit { get; set; }
    [EditorRequired, Parameter] public EventCallback ReturnAction { get; set; }
    private DateTime? selectedDate { get; set; } = DateTime.Now.Date;
    private string? accion { get; set; } = null!;
    public bool FormPostedSuccessfully { get; set; } = false;

    protected override async Task OnInitializedAsync()
    {
        editContext = new(SoatDtoENT);
        await LoadInsurerAsync();
        await LoadVehicleAsync();
        LoadDateAsync();
        LoadData();
    }

    private void LoadData()
    {
        accion = SoatDtoENT.Id == 0 ? "Crear" : "Editar";
    }

    private async Task LoadInsurerAsync()
    {
        var responseHttp = await Repository.GetAsync<List<Insurer>>("/api/Insurers/combo");
        if (responseHttp.Error)
        {
            var message = await responseHttp.GetErrorMessageAsync();
            await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
            return;
        }
        LstInsurer = responseHttp.Response;
        if (SoatDtoENT.InsurerId != 0)
        {
            selectedInsurer = LstInsurer!.FirstOrDefault(x => x.Id == SoatDtoENT.InsurerId)!;
        }
    }

    private async Task LoadVehicleAsync()
    {
        var responseHttp = await Repository.GetAsync<List<Vehicle>>("/api/Vehicles/combo");
        if (responseHttp.Error)
        {
            var message = await responseHttp.GetErrorMessageAsync();
            await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
            return;
        }
        List<Vehicle> p = responseHttp.Response!;

        foreach (var item in p)
        {
            LstVehicle ??= new List<VehicleDTO>();
            LstVehicle.Add(new VehicleDTO
            {
                Id = item.Id,
                Plate = item.Plate,
                Model = item.Model,
                BrandName = item.Brand!.Name,
                CustomerName = item.Customer!.FirstName + " " + item.Customer.LastName
            });
        }

        if (SoatDtoENT.VehicleId != 0)
        {
            selectedVehicle = LstVehicle!.FirstOrDefault(x => x.Id == SoatDtoENT.VehicleId)!;
        }
    }

    private void LoadDateAsync()
    {
        if (SoatDtoENT.Date != default)
        {
            selectedDate = SoatDtoENT.Date;
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

    private void ChangedInsurer(Insurer insurerEnt)
    {
        selectedInsurer = insurerEnt;
        SoatDtoENT.InsurerId = insurerEnt.Id;
    }

    private void ChangedVehicle(VehicleDTO vehicleEnt)
    {
        selectedVehicle = new VehicleDTO();
        if (vehicleEnt != null)
        {
            selectedVehicle = vehicleEnt;
            SoatDtoENT.VehicleId = vehicleEnt.Id;
        }
    }

    private void ChangedDate(DateTime? newDate)
    {
        selectedDate = newDate;
        SoatDtoENT.Date = selectedDate.HasValue ? selectedDate.Value : default;
    }

    private async Task<IEnumerable<Insurer>> SearchInsurer(string searchText, CancellationToken cancellationToken)
    {
        await Task.Delay(5);
        if (string.IsNullOrWhiteSpace(searchText))
        {
            return LstInsurer!;
        }

        return LstInsurer!.Where(x => x.Name.Contains(searchText, StringComparison.InvariantCultureIgnoreCase)).ToList();
    }

    private async Task<IEnumerable<VehicleDTO>> SearchVehicle(string searchText, CancellationToken cancellationToken)
    {
        LstVehicle = new List<VehicleDTO>();
        if (searchText != "" && searchText != " -  - " && searchText != null)
        {
            var responseHttp = await Repository.GetAsync<List<Vehicle>>($"api/Vehicles/GetByFilter?searchText={searchText}");
            if (!responseHttp.Error)
            {
                List<Vehicle> p = responseHttp.Response!;

                foreach (var item in p)
                {
                    LstVehicle.Add(new VehicleDTO
                    {
                        Id = item.Id,
                        Plate = item.Plate,
                        Model = item.Model,
                        BrandName = item.Brand!.Name,
                        CustomerName = item.Customer!.FirstName + " " + item.Customer.LastName
                    });
                }
            }
        }
        return LstVehicle;
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
}

//await Task.Delay(5);
//if (string.IsNullOrWhiteSpace(searchText))
//{
//    return LstVehicle!;
//}

//List<VehicleDTO> lista = LstVehicle!.FindAll(p => string.Concat(p.Plate, p.Model, p.BrandName).Contains(searchText, StringComparison.InvariantCultureIgnoreCase));
//return lista;