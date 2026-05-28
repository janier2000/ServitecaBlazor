using Microsoft.AspNetCore.Components;
using MudBlazor;
using Serviteca.Frontend.Repositories;
using Serviteca.Shared.Entities;

namespace Serviteca.Frontend.Pages.Types;

public partial class TypesEdit
{
    private TypeV? typeENT;
    private TypesForm? typesForm;

    [Inject] private NavigationManager NavigationManager { get; set; } = null!;
    [Inject] private IRepository Repository { get; set; } = null!;
    [Inject] private ISnackbar Snackbar { get; set; } = null!;

    [Parameter] public int Id { get; set; }

    protected override async Task OnInitializedAsync()
    {
        var responseHttp = await Repository.GetAsync<TypeV>($"api/Types/{Id}");

        if (responseHttp.Error)
        {
            if (responseHttp.HttpResponseMessage.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                NavigationManager.NavigateTo("Types");
            }
            else
            {
                var messageError = await responseHttp.GetErrorMessageAsync();
                Snackbar.Add(messageError, Severity.Error);
            }
        }
        else
        {
            typeENT = responseHttp.Response;
        }
    }

    private async Task EditAsync()
    {
        var responseHttp = await Repository.PutAsync("api/Types", typeENT);

        if (responseHttp.Error)
        {
            var messageError = await responseHttp.GetErrorMessageAsync();
            Snackbar.Add(messageError, Severity.Error);
            return;
        }

        Return();
        Snackbar.Add("Tipo vehiculo modificado con éxito.", Severity.Success);
    }

    private void Return()
    {
        typesForm!.FormPostedSuccessfully = true;
        NavigationManager.NavigateTo("/Types");
    }
}