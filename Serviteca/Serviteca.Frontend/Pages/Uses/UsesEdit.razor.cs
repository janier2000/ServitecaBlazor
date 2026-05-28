using Microsoft.AspNetCore.Components;
using MudBlazor;
using Serviteca.Frontend.Repositories;
using Serviteca.Shared.Entities;

namespace Serviteca.Frontend.Pages.Uses;

public partial class UsesEdit
{
    private Use? useENT;
    private UsesForm? usesForm;

    [Inject] private NavigationManager NavigationManager { get; set; } = null!;
    [Inject] private IRepository Repository { get; set; } = null!;
    [Inject] private ISnackbar Snackbar { get; set; } = null!;

    [Parameter] public int Id { get; set; }

    protected override async Task OnInitializedAsync()
    {
        var responseHttp = await Repository.GetAsync<Use>($"api/Uses/{Id}");

        if (responseHttp.Error)
        {
            if (responseHttp.HttpResponseMessage.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                NavigationManager.NavigateTo("/Uses");
            }
            else
            {
                var messageError = await responseHttp.GetErrorMessageAsync();
                Snackbar.Add(messageError, Severity.Error);
            }
        }
        else
        {
            useENT = responseHttp.Response;
        }
    }

    private async Task EditAsync()
    {
        var responseHttp = await Repository.PutAsync("api/Uses", useENT);

        if (responseHttp.Error)
        {
            var messageError = await responseHttp.GetErrorMessageAsync();
            Snackbar.Add(messageError, Severity.Error);
            return;
        }

        Return();
        Snackbar.Add("Uso de vehículo modificado con éxito.", Severity.Success);
    }

    private void Return()
    {
        usesForm!.FormPostedSuccessfully = true;
        NavigationManager.NavigateTo("/Uses");
    }
}