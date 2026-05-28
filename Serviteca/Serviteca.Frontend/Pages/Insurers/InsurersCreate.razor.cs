using Microsoft.AspNetCore.Components;
using MudBlazor;
using Serviteca.Frontend.Pages.Brands;
using Serviteca.Frontend.Repositories;
using Serviteca.Shared.Entities;

namespace Serviteca.Frontend.Pages.Insurers;

public partial class InsurersCreate
{
    private Insurer insurerENT = new();
    private InsurersForm? insurersForm;
    [Inject] private NavigationManager NavigationManager { get; set; } = null!;
    [Inject] private IRepository Repository { get; set; } = null!;
    [Inject] private ISnackbar Snackbar { get; set; } = null!;

    private async Task CreateAsync()
    {
        var responseHttp = await Repository.PostAsync("/api/Insurers", insurerENT);
        if (responseHttp.Error)
        {
            var message = await responseHttp.GetErrorMessageAsync();
            Snackbar.Add(message, Severity.Error);
            return;
        }
        Return();
        Snackbar.Add("Aseguradora creada con éxito.", Severity.Success);
    }

    private void Return()
    {
        insurersForm!.FormPostedSuccessfully = true;
        NavigationManager.NavigateTo("/Insurers");
    }
}