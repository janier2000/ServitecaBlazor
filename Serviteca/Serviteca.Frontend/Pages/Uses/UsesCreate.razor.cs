using Microsoft.AspNetCore.Components;
using MudBlazor;
using Serviteca.Frontend.Repositories;
using Serviteca.Shared.Entities;

namespace Serviteca.Frontend.Pages.Uses;

public partial class UsesCreate
{
    private Use UseENT = new();
    private UsesForm? UsesForm;
    [Inject] private IRepository Repository { get; set; } = null!;
    [Inject] private NavigationManager NavigationManager { get; set; } = null!;
    [Inject] private ISnackbar Snackbar { get; set; } = null!;

    private async Task CreateAsync()
    {
        var responseHttp = await Repository.PostAsync("/api/Uses", UseENT);
        if (responseHttp.Error)
        {
            var message = await responseHttp.GetErrorMessageAsync();
            Snackbar.Add(message, Severity.Error);
            return;
        }
        Return();
        Snackbar.Add("Uso de vehículo creado con éxito.", Severity.Success);
    }

    private void Return()
    {
        UsesForm!.FormPostedSuccessfully = true;
        NavigationManager.NavigateTo("/Uses");
    }
}