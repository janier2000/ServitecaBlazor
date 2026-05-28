using Microsoft.AspNetCore.Components;
using MudBlazor;
using Serviteca.Frontend.Repositories;
using Serviteca.Shared.DTOs;
using Serviteca.Shared.Entities;

namespace Serviteca.Frontend.Pages.Customers;

public partial class CustomersEdit
{
    private CustomersForm? customersForm;
    private CustomerDTO? customerDtoENT;

    [Inject] private NavigationManager NavigationManager { get; set; } = null!;
    [Inject] private IRepository Repository { get; set; } = null!;
    [Inject] private ISnackbar Snackbar { get; set; } = null!;

    [Parameter] public int Id { get; set; }

    protected override async Task OnInitializedAsync()
    {
        var responseHttp = await Repository.GetAsync<Customer>($"api/Customers/{Id}");
        if (responseHttp.Error)
        {
            if (responseHttp.HttpResponseMessage.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                NavigationManager.NavigateTo("Customers");
            }
            else
            {
                var messageError = await responseHttp.GetErrorMessageAsync();
                Snackbar.Add(messageError, Severity.Error);
            }
        }
        else
        {
            var customer = responseHttp.Response;
            customerDtoENT = new CustomerDTO()
            {
                Id = customer!.Id,
                DocumentTypeId = customer.DocumentTypeId,
                Document = customer.Document,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                gender = customer.gender,
                phone = customer.phone,
                ClientSince = Convert.ToDateTime(customer.ClientSince)
            };
        }
    }

    private async Task EditAsync()
    {
        var responseHttp = await Repository.PutAsync("api/Customers/Edit", customerDtoENT);

        if (responseHttp.Error)
        {
            var messageError = await responseHttp.GetErrorMessageAsync();
            Snackbar.Add(messageError, Severity.Error);
            return;
        }

        Return();
        Snackbar.Add("Cliente modificado con exito.", Severity.Success);
    }

    private void Return()
    {
        customersForm!.FormPostedSuccessfully = true;
        NavigationManager.NavigateTo("/Customers");
    }
}