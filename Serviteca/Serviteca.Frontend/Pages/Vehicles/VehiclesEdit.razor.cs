using Microsoft.AspNetCore.Components;
using MudBlazor;
using Serviteca.Frontend.Pages.Customers;
using Serviteca.Frontend.Repositories;
using Serviteca.Shared.DTOs;
using E = Serviteca.Shared.Entities;

namespace Serviteca.Frontend.Pages.Vehicles;

public partial class VehiclesEdit
{
    private VehiclesForm? vehiclesForm;
    private VehicleDTO? vehicleDtoENT;

    [Inject] private NavigationManager NavigationManager { get; set; } = null!;
    [Inject] private IRepository Repository { get; set; } = null!;
    [Inject] private ISnackbar Snackbar { get; set; } = null!;

    [Parameter] public int Id { get; set; }

    protected override async Task OnInitializedAsync()
    {
        var responseHttp = await Repository.GetAsync<E.Vehicle>($"api/Vehicles/{Id}");
        if (responseHttp.Error)
        {
            if (responseHttp.HttpResponseMessage.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                NavigationManager.NavigateTo("Vehicles");
            }
            else
            {
                var messageError = await responseHttp.GetErrorMessageAsync();
                Snackbar.Add(messageError, Severity.Error);
            }
        }
        else
        {
            var vehicle = responseHttp.Response;
            vehicleDtoENT = new VehicleDTO()
            {
                Id = vehicle!.Id,
                Model = vehicle.Model,
                CurrentKm = vehicle.CurrentKm,
                Plate = vehicle.Plate,
                ReturnDate = Convert.ToDateTime(vehicle.ReturnDate),
                CustomerId = vehicle.CustomerId,
                VehicleBrandId = vehicle.VehicleBrandId,
                VehicleTypeId = vehicle.VehicleTypeId,
                VehicleUseId = vehicle.VehicleUseId,
            };
        }
    }

    private async Task EditAsync()
    {
        var responseHttp = await Repository.PutAsync("api/Vehicles/Edit", vehicleDtoENT);
        if (responseHttp.Error)
        {
            var messageError = await responseHttp.GetErrorMessageAsync();
            Snackbar.Add(messageError, Severity.Error);
            return;
        }

        Return();
        Snackbar.Add("Vehículo modificado con éxito.", Severity.Success);
    }

    private void Return()
    {
        vehiclesForm!.FormPostedSuccessfully = true;
        NavigationManager.NavigateTo("/Vehicles");
    }
}