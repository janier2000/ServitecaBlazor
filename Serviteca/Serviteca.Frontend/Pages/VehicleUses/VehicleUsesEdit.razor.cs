using Microsoft.AspNetCore.Components;
using MudBlazor;
using Serviteca.Frontend.Pages.VehicleTypes;
using Serviteca.Frontend.Repositories;
using E = Serviteca.Shared.Entities;

namespace Serviteca.Frontend.Pages.VehicleUses;

public partial class VehicleUsesEdit
{
    private E.VehicleUse? vehicleUseENT;
    private VehicleUsesForm? vehicleUsesForm;

    [Inject] private NavigationManager NavigationManager { get; set; } = null!;
    [Inject] private IRepository Repository { get; set; } = null!;
    [Inject] private ISnackbar Snackbar { get; set; } = null!;

    [Parameter] public int Id { get; set; }

    protected override async Task OnInitializedAsync()
    {
        var responseHttp = await Repository.GetAsync<E.VehicleUse>($"api/VehicleUses/{Id}");

        if (responseHttp.Error)
        {
            if (responseHttp.HttpResponseMessage.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                NavigationManager.NavigateTo("VehicleUses");
            }
            else
            {
                var messageError = await responseHttp.GetErrorMessageAsync();
                Snackbar.Add(messageError, Severity.Error);
            }
        }
        else
        {
            vehicleUseENT = responseHttp.Response;
        }
    }

    private async Task EditAsync()
    {
        var responseHttp = await Repository.PutAsync("api/VehicleUses", vehicleUseENT);

        if (responseHttp.Error)
        {
            var messageError = await responseHttp.GetErrorMessageAsync();
            Snackbar.Add(messageError, Severity.Error);
            return;
        }

        Return();
        Snackbar.Add("Uso de vehículo modificado con éxito.", Severity.Success);
    }

    private void Return()
    {
        vehicleUsesForm!.FormPostedSuccessfully = true;
        NavigationManager.NavigateTo("VehicleUses");
    }
}