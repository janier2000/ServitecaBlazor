using Microsoft.AspNetCore.Components;
using MudBlazor;
using Serviteca.Frontend.Repositories;
using Serviteca.Shared.Entities;

namespace Serviteca.Frontend.Pages.Types;

public partial class TypesCreate
{
    private TypeV typeENT = new();
    private TypesForm? typesForm;
    [Inject] private IRepository Repository { get; set; } = null!;
    [Inject] private NavigationManager NavigationManager { get; set; } = null!;
    [Inject] private ISnackbar Snackbar { get; set; } = null!;

    private async Task CreateAsync()
    {
        var responseHttp = await Repository.PostAsync("/api/Types", typeENT);
        if (responseHttp.Error)
        {
            var message = await responseHttp.GetErrorMessageAsync();
            Snackbar.Add(message, Severity.Error);
            return;
        }
        Return();
        Snackbar.Add("Tipo vehiculo creado con éxito.", Severity.Success);
    }

    private void Return()
    {
        typesForm!.FormPostedSuccessfully = true;
        NavigationManager.NavigateTo("/Types");
    }
}