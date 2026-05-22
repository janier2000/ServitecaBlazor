using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.Extensions.Localization;
using MudBlazor;

using Serviteca.Frontend.Repositories;
using Serviteca.Shared.DTOs;
using Serviteca.Shared.Entities;

using E = Serviteca.Shared.Entities;

namespace Serviteca.Frontend.Pages.Customers;

public partial class CustomersCreate
{
    private CustomersForm? customersForm;
    private CustomerDTO customerDtoENT = new();
    [Inject] private IRepository Repository { get; set; } = null!;
    [Inject] private NavigationManager NavigationManager { get; set; } = null!;
    [Inject] private ISnackbar Snackbar { get; set; } = null!;
    [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;

    private async Task CreateAsync()
    {
        var responseHttp = await Repository
            .PostAsync<CustomerDTO, E.Customer>("/api/Customers/Create", customerDtoENT);
        if (responseHttp.Error)
        {
            var message = await responseHttp.GetErrorMessageAsync();
            Snackbar.Add(message, Severity.Error);
            return;
        }
        var customerResponse = responseHttp.Response;

        Return();
        Snackbar.Add("Cliente creado con éxito.", Severity.Success);
    }

    private void Return()
    {
        customersForm!.FormPostedSuccessfully = true;
        NavigationManager.NavigateTo("/Customers");
    }
}