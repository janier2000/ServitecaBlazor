using System.Net;
using Serviteca.Frontend.Shared;
using e = Serviteca.Shared.Entities;
using Microsoft.AspNetCore.Components;
using Serviteca.Frontend.Repositories;
using CurrieTechnologies.Razor.SweetAlert2;


namespace Serviteca.Frontend.Pages.DocumentType
{
    public partial class DocumentTypeEdit
    {

        private e.DocumentType? documentTypeENT;

        private FormWithName<e.DocumentType>? documentTypeForm;
        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [EditorRequired, Parameter] public int Id { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            var responseHttp = await Repository.GetAsync<e.DocumentType>($"/api/DocType/{Id}");
            if (responseHttp.Error)
            {
                if (responseHttp.HttpResponseMessage.StatusCode == HttpStatusCode.NotFound)
                {
                    NavigationManager.NavigateTo("/DocType");
                }
                else
                {
                    var message = await responseHttp.GetErrorMessageAsync();
                    EnviarMensaje("Error", message!);
                }
            }
            else
            {
                documentTypeENT = responseHttp.Response;
            }
        }

        private async Task EditAsync()
        {
            var responseHttp = await Repository.PutAsync("/api/DocType", documentTypeENT);
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                EnviarMensaje("Error", message!);
                return;
            }
            Return();
            EnviarMensaje("OK", "Tipo documento modificado con éxito."!);
        }

        private void Return()
        {
            documentTypeForm!.FormPostedSuccessfully = true;
            NavigationManager.NavigateTo("/DocType");
        }

        public void EnviarMensaje(string tipo, string message)
        {
            if (tipo == "Error")
            {
                SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
            }
            else
            {
                var toast = SweetAlertService.Mixin(new SweetAlertOptions
                {
                    Toast = true,
                    Position = SweetAlertPosition.BottomEnd,
                    ShowConfirmButton = true,
                    Timer = 3000
                });
                toast.FireAsync(icon: SweetAlertIcon.Success, message: message);
            }
        }
    }
}