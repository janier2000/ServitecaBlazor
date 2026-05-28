using Microsoft.AspNetCore.Components;
using MudBlazor;
using Serviteca.Frontend.Repositories;
using Serviteca.Shared.Entities;

namespace Serviteca.Frontend.Pages.Brands;

public partial class BrandsCreate
{
    private Brand brandENT = new();
    private BrandsForm? brandsForm;
    [Inject] private IRepository Repository { get; set; } = null!;
    [Inject] private NavigationManager NavigationManager { get; set; } = null!;
    [Inject] private ISnackbar Snackbar { get; set; } = null!;

    private async Task CreateAsync()
    {
        var responseHttp = await Repository.PostAsync("/api/Brands", brandENT);
        if (responseHttp.Error)
        {
            var message = await responseHttp.GetErrorMessageAsync();
            Snackbar.Add(message, Severity.Error);
            return;
        }
        Return();
        Snackbar.Add("Marca vehiculo creado con éxito.", Severity.Success);
    }

    private void Return()
    {
        brandsForm!.FormPostedSuccessfully = true;
        NavigationManager.NavigateTo("/Brands");
    }
}