using Serviteca.Frontend.Shared;
using Microsoft.AspNetCore.Components;
using Serviteca.Frontend.Repositories;
using CurrieTechnologies.Razor.SweetAlert2;

namespace Serviteca.Frontend.Pages.VehicleBrand
{
    public partial class BrandCreate
    {

        private Serviteca.Shared.Entities.VehicleBrand vehicleBrandENT = new();

        private FormWithName<Serviteca.Shared.Entities.VehicleBrand>? vehicleBrandFORM;

        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;

        private async Task CreateAsync()
        {
            var responseHttp = await Repository.PostAsync("/api/VehBrand", vehicleBrandENT);
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }

            Return();

            var toast = SweetAlertService.Mixin(new SweetAlertOptions
            {
                Toast = true,
                Position = SweetAlertPosition.BottomEnd,
                ShowConfirmButton = true,
                Timer = 3000
            });
            await toast.FireAsync(icon: SweetAlertIcon.Success, message: "Marca vehiculo creado con éxito.");
        }

        private void Return()
        {
            vehicleBrandFORM!.FormPostedSuccessfully = true;
            NavigationManager.NavigateTo("/VehBrand");
        }
    }
}