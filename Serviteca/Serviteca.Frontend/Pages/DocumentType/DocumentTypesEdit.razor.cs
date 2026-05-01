using System.Net;
using Serviteca.Frontend.Shared;
using Microsoft.AspNetCore.Components;
using Serviteca.Frontend.Repositories;
using CurrieTechnologies.Razor.SweetAlert2;

namespace Serviteca.Frontend.Pages.DocumentType
{
    public partial class DocumentTypesEdit
    {

        private Serviteca.Shared.Entities.DocumentType? documentType;
        private FormWithName<Serviteca.Shared.Entities.DocumentType>? documentTypeForm;
        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [EditorRequired, Parameter] public int Id { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            var responseHttp = await Repository.GetAsync<Serviteca.Shared.Entities.DocumentType>($"/api/DocumentTypes/{Id}");
            if (responseHttp.Error)
            {
                if (responseHttp.HttpResponseMessage.StatusCode == HttpStatusCode.NotFound)
                {
                    NavigationManager.NavigateTo("/DocumentTypes");
                }
                else
                {
                    var messsage = await responseHttp.GetErrorMessageAsync();
                    await SweetAlertService.FireAsync("Error", messsage, SweetAlertIcon.Error);
                }
            }
            else
            {
                documentType = responseHttp.Response;
            }
        }

        private async Task EditAsync()
        {
            var responseHttp = await Repository.PutAsync("/api/DocumentTypes", documentType);
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
            await toast.FireAsync(icon: SweetAlertIcon.Success, message: "Cambios guardados con éxito.");
        }

        private void Return()
        {
            documentTypeForm!.FormPostedSuccessfully = true;
            NavigationManager.NavigateTo("/DocumentTypes");
        }
    }
}