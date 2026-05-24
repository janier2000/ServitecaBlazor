using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Serviteca.Frontend.Repositories;
using Serviteca.Shared.DTOs;
using E = Serviteca.Shared.Entities;

namespace Serviteca.Frontend.Pages.Vehicles;

public partial class VehiclesCreate
{
    private VehiclesForm? vehiclesForm;
    private VehicleDTO vehicleDtoENT = new();
    [Inject] private IRepository Repository { get; set; } = null!;
    [Inject] private NavigationManager NavigationManager { get; set; } = null!;
    [Inject] private ISnackbar Snackbar { get; set; } = null!;
    [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;

    private async Task CreateAsync()
    {
        var responseHttp = await Repository
            .PostAsync<VehicleDTO, E.Vehicle>("/api/Vehicles/Create", vehicleDtoENT);
        if (responseHttp.Error)
        {
            var message = await responseHttp.GetErrorMessageAsync();
            Snackbar.Add(message, Severity.Error);
            return;
        }
        var vehicleResponse = responseHttp.Response;

        Return();
        Snackbar.Add("Vehiculo creado con éxito.", Severity.Success);
    }

    private void Return()
    {
        vehiclesForm!.FormPostedSuccessfully = true;
        NavigationManager.NavigateTo("/Vehicles");
    }
}