using Microsoft.AspNetCore.Components;
using MudBlazor;
using Serviteca.Frontend.Repositories;
using Serviteca.Shared.Entities;

namespace Serviteca.Frontend.Pages.Customers;

public partial class CustomersDetail
{
    private Customer? customerENT;
    [Inject] private NavigationManager NavigationManager { get; set; } = null!;
    [Inject] private IRepository Repository { get; set; } = null!;
    [Inject] private ISnackbar Snackbar { get; set; } = null!;

    [Parameter] public int Id { get; set; }
    [Parameter] public bool IsAnonymouns { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        await LoadGroupAsync();
        //await CheckPredictionsForAllMatchesAsync();
    }

    //private async Task CheckPredictionsForAllMatchesAsync()
    //{
    //    var responseHttp = await Repository.GetAsync($"api/groups/CheckPredictionsForAllMatches/{Id}");
    //}

    private async Task LoadGroupAsync()
    {
        var responseHttp = await Repository.GetAsync<Customer>($"api/Customers/{Id}");
        if (responseHttp.Error)
        {
            if (responseHttp.HttpResponseMessage.StatusCode != System.Net.HttpStatusCode.NotFound)
            {
                var messageError = await responseHttp.GetErrorMessageAsync();
                Snackbar.Add(messageError, Severity.Error);
            }
            NavigationManager.NavigateTo("/customers");
            return;
        }
        customerENT = responseHttp.Response;
    }

    private void Return()
    {
        //customersForm!.FormPostedSuccessfully = true;
        NavigationManager.NavigateTo("/Customers");
    }
}