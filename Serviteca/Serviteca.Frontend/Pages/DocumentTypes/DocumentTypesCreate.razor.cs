using Microsoft.AspNetCore.Components;
using MudBlazor;
using Serviteca.Frontend.Repositories;
using Serviteca.Shared.Entities;

namespace Serviteca.Frontend.Pages.DocumentTypes;

public partial class DocumentTypesCreate
{
    private DocumentType documentTypeENT = new();
    private DocumentTypesForm? documentTypesForm;

    [Inject] private IRepository Repository { get; set; } = null!;
    [Inject] private NavigationManager NavigationManager { get; set; } = null!;
    [Inject] private ISnackbar Snackbar { get; set; } = null!;

    private async Task CreateAsync()
    {
        var responseHttp = await Repository.PostAsync("/api/DocumentTypes", documentTypeENT);
        if (responseHttp.Error)
        {
            var message = await responseHttp.GetErrorMessageAsync();
            Snackbar.Add(message, Severity.Error);
            return;
        }
        Return();
        Snackbar.Add("Tipo documento creado con éxito.", Severity.Success);
    }

    private void Return()
    {
        documentTypesForm!.FormPostedSuccessfully = true;
        NavigationManager.NavigateTo("/DocumentTypes");
    }
}