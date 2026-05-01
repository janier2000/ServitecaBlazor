using Serviteca.Frontend.Shared;
using Microsoft.AspNetCore.Components;
using Serviteca.Frontend.Repositories;
using CurrieTechnologies.Razor.SweetAlert2;

namespace Serviteca.Frontend.Pages.VehicleType
{
    public partial class TypeCreate
    {

        private Serviteca.Shared.Entities.VehicleType vehicleTypeENT = new();
        private FormWithName<Serviteca.Shared.Entities.VehicleType>? VehicleTypeFORM;

        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;


        private async Task CreateAsync()
        {
            var responseHttp = await Repository.PostAsync("/api/VehType", vehicleTypeENT);
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
            await toast.FireAsync(icon: SweetAlertIcon.Success, message: "Tipo vehiculo creado con éxito.");
        }

        private void Return()
        {
            VehicleTypeFORM!.FormPostedSuccessfully = true;
            NavigationManager.NavigateTo("/VehType");
        }
    }
}