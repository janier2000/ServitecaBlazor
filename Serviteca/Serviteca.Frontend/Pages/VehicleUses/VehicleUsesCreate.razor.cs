using Microsoft.AspNetCore.Components;
using MudBlazor;
using Serviteca.Frontend.Pages.VehicleTypes;
using Serviteca.Frontend.Repositories;
using E = Serviteca.Shared.Entities;

namespace Serviteca.Frontend.Pages.VehicleUses;

public partial class VehicleUsesCreate
{
    private E.VehicleUse vehicleUseENT = new();
    private VehicleUsesForm? vehicleUsesForm;
    [Inject] private IRepository Repository { get; set; } = null!;
    [Inject] private NavigationManager NavigationManager { get; set; } = null!;
    [Inject] private ISnackbar Snackbar { get; set; } = null!;

    private async Task CreateAsync()
    {
        var responseHttp = await Repository.PostAsync("/api/VehicleUses", vehicleUseENT);
        if (responseHttp.Error)
        {
            var message = await responseHttp.GetErrorMessageAsync();
            Snackbar.Add(message, Severity.Error);
            return;
        }
        Return();
        Snackbar.Add("Uso de vehículo creado con éxito.", Severity.Success);
    }

    private void Return()
    {
        vehicleUsesForm!.FormPostedSuccessfully = true;
        NavigationManager.NavigateTo("/VehicleUses");
    }
}