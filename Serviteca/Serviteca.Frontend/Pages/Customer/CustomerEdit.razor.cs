using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Serviteca.Frontend.Pages.DocumentType;
using Serviteca.Frontend.Repositories;
using Serviteca.Shared.Entities;
using System.Net;
using e = Serviteca.Shared.Entities;

namespace Serviteca.Frontend.Pages.Customer
{
    public partial class CustomerEdit
    {
        private e.Customer? customerENT;
        private List<e.DocumentType>? lstDocumentType;
        private bool loading = true;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
        [Inject] private IRepository Repository { get; set; } = null!;
        [EditorRequired, Parameter] public int Id { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            loading = true;
            var responseHttp = await Repository.GetAsync<e.Customer>($"/api/Customer/{Id}");
            if (responseHttp.Error)
            {
                loading = false;
                if (responseHttp.HttpResponseMessage.StatusCode == HttpStatusCode.NotFound)
                {
                    NavigationManager.NavigateTo("/Customer");
                }
                else
                {
                    var messsage = await responseHttp.GetErrorMessageAsync();
                    await SweetAlertService.FireAsync("Error", messsage, SweetAlertIcon.Error);
                }
            }
            else
            {
                customerENT = responseHttp.Response;
            }
            loading = false;
        }

        protected override async Task OnInitializedAsync()
        {
            await LoadDocumentTypeAsync();
        }

        private async Task LoadDocumentTypeAsync()
        {
            var responseHttp = await Repository.GetAsync<List<e.DocumentType>>("/api/DocType/combo");
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }
            lstDocumentType = responseHttp.Response;
        }

        private async Task EditAsync()
        {
            var responseHttp = await Repository.PutAsync("/api/Customer", customerENT);
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
            await toast.FireAsync(icon: SweetAlertIcon.Success, message: "Cliente modificado con éxito.");
        }

        private void Return()
        {
            NavigationManager.NavigateTo("/Customer");
        }

    }
}