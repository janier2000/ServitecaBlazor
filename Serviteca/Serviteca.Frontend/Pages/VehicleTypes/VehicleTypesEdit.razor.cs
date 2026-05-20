using Microsoft.AspNetCore.Components;
using MudBlazor;
using Serviteca.Frontend.Repositories;
using E = Serviteca.Shared.Entities;

namespace Serviteca.Frontend.Pages.VehicleTypes;

public partial class VehicleTypesEdit
{
    private E.VehicleType? vehicleTypeENT;
    private VehicleTypesForm? vehicleTypesForm;

    [Inject] private NavigationManager NavigationManager { get; set; } = null!;
    [Inject] private IRepository Repository { get; set; } = null!;
    [Inject] private ISnackbar Snackbar { get; set; } = null!;

    [Parameter] public int Id { get; set; }

    protected override async Task OnInitializedAsync()
    {
        var responseHttp = await Repository.GetAsync<E.VehicleType>($"api/VehicleTypes/{Id}");

        if (responseHttp.Error)
        {
            if (responseHttp.HttpResponseMessage.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                NavigationManager.NavigateTo("VehicleTypes");
            }
            else
            {
                var messageError = await responseHttp.GetErrorMessageAsync();
                Snackbar.Add(messageError, Severity.Error);
            }
        }
        else
        {
            vehicleTypeENT = responseHttp.Response;
        }
    }

    private async Task EditAsync()
    {
        var responseHttp = await Repository.PutAsync("api/VehicleTypes", vehicleTypeENT);

        if (responseHttp.Error)
        {
            var messageError = await responseHttp.GetErrorMessageAsync();
            Snackbar.Add(messageError, Severity.Error);
            return;
        }

        Return();
        Snackbar.Add("Tipo vehiculo modificado con éxito.", Severity.Success);
    }

    private void Return()
    {
        vehicleTypesForm!.FormPostedSuccessfully = true;
        NavigationManager.NavigateTo("VehicleTypes");
    }
}