using Microsoft.AspNetCore.Components;
using MudBlazor;
using Serviteca.Frontend.Pages.VehicleTypes;
using Serviteca.Frontend.Repositories;
using E = Serviteca.Shared.Entities;

namespace Serviteca.Frontend.Pages.VehicleBrands;

public partial class VehicleBrandsCreate
{
    private E.VehicleBrand vehicleBrandENT = new();
    private VehicleBrandsForm? vehicleBrandsForm;
    [Inject] private IRepository Repository { get; set; } = null!;
    [Inject] private NavigationManager NavigationManager { get; set; } = null!;
    [Inject] private ISnackbar Snackbar { get; set; } = null!;

    private async Task CreateAsync()
    {
        var responseHttp = await Repository.PostAsync("/api/VehicleBrands", vehicleBrandENT);
        if (responseHttp.Error)
        {
            var message = await responseHttp.GetErrorMessageAsync();
            Snackbar.Add(message, Severity.Error);
            return;
        }
        Return();
        Snackbar.Add("Marca vehiculo creado con éxito.", Severity.Success);
    }

    private void Return()
    {
        vehicleBrandsForm!.FormPostedSuccessfully = true;
        NavigationManager.NavigateTo("/VehicleBrands");
    }
}