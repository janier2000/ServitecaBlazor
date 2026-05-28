using Microsoft.AspNetCore.Components;
using MudBlazor;
using Serviteca.Frontend.Repositories;
using Serviteca.Shared.Entities;

namespace Serviteca.Frontend.Pages.Brands;

public partial class BrandsEdit
{
    private Brand? brandENT;
    private BrandsForm? brandsForm;

    [Inject] private NavigationManager NavigationManager { get; set; } = null!;
    [Inject] private IRepository Repository { get; set; } = null!;
    [Inject] private ISnackbar Snackbar { get; set; } = null!;

    [Parameter] public int Id { get; set; }

    protected override async Task OnInitializedAsync()
    {
        var responseHttp = await Repository.GetAsync<Brand>($"api/Brands/{Id}");

        if (responseHttp.Error)
        {
            if (responseHttp.HttpResponseMessage.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                NavigationManager.NavigateTo("/Brands");
            }
            else
            {
                var messageError = await responseHttp.GetErrorMessageAsync();
                Snackbar.Add(messageError, Severity.Error);
            }
        }
        else
        {
            brandENT = responseHttp.Response;
        }
    }

    private async Task EditAsync()
    {
        var responseHttp = await Repository.PutAsync("api/Brands", brandENT);

        if (responseHttp.Error)
        {
            var messageError = await responseHttp.GetErrorMessageAsync();
            Snackbar.Add(messageError, Severity.Error);
            return;
        }

        Return();
        Snackbar.Add("Marca de vehículo modificada con éxito.", Severity.Success);
    }

    private void Return()
    {
        brandsForm!.FormPostedSuccessfully = true;
        NavigationManager.NavigateTo("/Brands");
    }
}