using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Serviteca.Frontend.Repositories;
using System.Diagnostics.Metrics;
using e = Serviteca.Shared.Entities;

namespace Serviteca.Frontend.Pages.Customer
{
    public partial class CustomerCreate
    {
        private e.Customer customerENT = new();

        private List<e.DocumentType>? lstDocumentType;

        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
        [Inject] private IRepository Repository { get; set; } = null!;

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

        private async Task CreateAsync()
        {
            var responseHttp = await Repository.PostAsync("/api/Customer", customerENT);
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
            await toast.FireAsync(icon: SweetAlertIcon.Success, message: "Cliente creado con éxito.");
        }

        private void Return()
        {
            NavigationManager.NavigateTo("/Customer");
        }
    }
}