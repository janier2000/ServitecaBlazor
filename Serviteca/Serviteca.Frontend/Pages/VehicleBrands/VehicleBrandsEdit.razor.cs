using Microsoft.AspNetCore.Components;
using MudBlazor;
using Serviteca.Frontend.Pages.VehicleTypes;
using Serviteca.Frontend.Repositories;
using E = Serviteca.Shared.Entities;

namespace Serviteca.Frontend.Pages.VehicleBrands;

public partial class VehicleBrandsEdit
{
    private E.VehicleBrand? vehicleBrandENT;
    private VehicleBrandsForm? vehicleBrandsForm;

    [Inject] private NavigationManager NavigationManager { get; set; } = null!;
    [Inject] private IRepository Repository { get; set; } = null!;
    [Inject] private ISnackbar Snackbar { get; set; } = null!;

    [Parameter] public int Id { get; set; }

    protected override async Task OnInitializedAsync()
    {
        var responseHttp = await Repository.GetAsync<E.VehicleBrand>($"api/VehicleBrands/{Id}");

        if (responseHttp.Error)
        {
            if (responseHttp.HttpResponseMessage.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                NavigationManager.NavigateTo("VehicleBrands");
            }
            else
            {
                var messageError = await responseHttp.GetErrorMessageAsync();
                Snackbar.Add(messageError, Severity.Error);
            }
        }
        else
        {
            vehicleBrandENT = responseHttp.Response;
        }
    }

    private async Task EditAsync()
    {
        var responseHttp = await Repository.PutAsync("api/VehicleBrands", vehicleBrandENT);

        if (responseHttp.Error)
        {
            var messageError = await responseHttp.GetErrorMessageAsync();
            Snackbar.Add(messageError, Severity.Error);
            return;
        }

        Return();
        Snackbar.Add("Marca de vehículo modificada con éxito.", Severity.Success);
    }

    private void Return()
    {
        vehicleBrandsForm!.FormPostedSuccessfully = true;
        NavigationManager.NavigateTo("VehicleBrands");
    }
}