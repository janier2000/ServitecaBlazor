using System.Net;
using Serviteca.Frontend.Shared;
using Microsoft.AspNetCore.Components;
using Serviteca.Frontend.Repositories;
using CurrieTechnologies.Razor.SweetAlert2;

namespace Serviteca.Frontend.Pages.Brand
{
    public partial class BrandEdit
    {

        private Serviteca.Shared.Entities.VehicleBrand? vehicleBrandENT;

        private FormWithName<Serviteca.Shared.Entities.VehicleBrand>? vehicleBrandFORM;
        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [EditorRequired, Parameter] public int Id { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            var responseHttp = await Repository.GetAsync<Serviteca.Shared.Entities.VehicleBrand>($"/api/Brand/{Id}");
            if (responseHttp.Error)
            {
                if (responseHttp.HttpResponseMessage.StatusCode == HttpStatusCode.NotFound)
                {
                    NavigationManager.NavigateTo("/Brand");
                }
                else
                {
                    var messsage = await responseHttp.GetErrorMessageAsync();
                    await SweetAlertService.FireAsync("Error", messsage, SweetAlertIcon.Error);
                }
            }
            else
            {
                vehicleBrandENT = responseHttp.Response;
            }
        }

        private async Task EditAsync()
        {
            var responseHttp = await Repository.PutAsync("/api/Brand", vehicleBrandENT);
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
            await toast.FireAsync(icon: SweetAlertIcon.Success, message: "Marca vehiculo modificado con éxito.");
        }

        private void Return()
        {
            vehicleBrandFORM!.FormPostedSuccessfully = true;
            NavigationManager.NavigateTo("/Brand");
        }
    }
}