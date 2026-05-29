using Microsoft.AspNetCore.Components;
using MudBlazor;
using Serviteca.Frontend.Repositories;
using Serviteca.Shared.Entities;

namespace Serviteca.Frontend.Pages.Vehicles;

public partial class VehiclesDetail
{
    private Vehicle? vehicleENT;
    [Inject] private NavigationManager NavigationManager { get; set; } = null!;
    [Inject] private IRepository Repository { get; set; } = null!;
    [Inject] private ISnackbar Snackbar { get; set; } = null!;

    [Parameter] public int Id { get; set; }
    [Parameter] public bool IsAnonymouns { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        await LoadGroupAsync();
    }

    private async Task LoadGroupAsync()
    {
        var responseHttp = await Repository.GetAsync<Vehicle>($"api/Vehicles/{Id}");
        if (responseHttp.Error)
        {
            if (responseHttp.HttpResponseMessage.StatusCode != System.Net.HttpStatusCode.NotFound)
            {
                var messageError = await responseHttp.GetErrorMessageAsync();
                Snackbar.Add(messageError, Severity.Error);
            }
            NavigationManager.NavigateTo("/Soats");
            return;
        }
        vehicleENT = responseHttp.Response;
    }

    private void Return()
    {
        NavigationManager.NavigateTo("/Vehicles");
    }
}