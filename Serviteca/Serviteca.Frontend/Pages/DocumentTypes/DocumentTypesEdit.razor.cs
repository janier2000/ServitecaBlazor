using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using MudBlazor;
using Serviteca.Frontend.Repositories;
using System.Diagnostics.Metrics;
using E = Serviteca.Shared.Entities;

namespace Serviteca.Frontend.Pages.DocumentTypes;

public partial class DocumentTypesEdit
{
    private E.DocumentType? documentTypeENT;
    private DocumentTypesForm? documentTypesForm;

    [Inject] private NavigationManager NavigationManager { get; set; } = null!;
    [Inject] private IRepository Repository { get; set; } = null!;
    [Inject] private ISnackbar Snackbar { get; set; } = null!;

    [Parameter] public int Id { get; set; }

    protected override async Task OnInitializedAsync()
    {
        var responseHttp = await Repository.GetAsync<E.DocumentType>($"api/DocumentTypes/{Id}");

        if (responseHttp.Error)
        {
            if (responseHttp.HttpResponseMessage.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                NavigationManager.NavigateTo("DocumentTypes");
            }
            else
            {
                var messageError = await responseHttp.GetErrorMessageAsync();
                Snackbar.Add(messageError, Severity.Error);
            }
        }
        else
        {
            documentTypeENT = responseHttp.Response;
        }
    }

    private async Task EditAsync()
    {
        var responseHttp = await Repository.PutAsync("api/DocumentTypes", documentTypeENT);

        if (responseHttp.Error)
        {
            var messageError = await responseHttp.GetErrorMessageAsync();
            Snackbar.Add(messageError, Severity.Error);
            return;
        }

        Return();
        Snackbar.Add("Tipo documento modificado con éxito.", Severity.Success);
    }

    private void Return()
    {
        documentTypesForm!.FormPostedSuccessfully = true;
        NavigationManager.NavigateTo("DocumentTypes");
    }
}