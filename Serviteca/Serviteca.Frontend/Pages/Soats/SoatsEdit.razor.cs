using Microsoft.AspNetCore.Components;
using MudBlazor;
using Serviteca.Frontend.Pages.Vehicles;
using Serviteca.Frontend.Repositories;
using Serviteca.Shared.DTOs;
using Serviteca.Shared.Entities;

namespace Serviteca.Frontend.Pages.Soats;

public partial class SoatsEdit
{
    private SoatsForm? soatsForm;
    private SoatDTO? soatDtoENT;

    [Inject] private NavigationManager NavigationManager { get; set; } = null!;
    [Inject] private IRepository Repository { get; set; } = null!;
    [Inject] private ISnackbar Snackbar { get; set; } = null!;

    [Parameter] public int Id { get; set; }

    protected override async Task OnInitializedAsync()
    {
        var responseHttp = await Repository.GetAsync<Soat>($"api/soats/{Id}");
        if (responseHttp.Error)
        {
            if (responseHttp.HttpResponseMessage.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                NavigationManager.NavigateTo("/Soats");
            }
            else
            {
                var messageError = await responseHttp.GetErrorMessageAsync();
                Snackbar.Add(messageError, Severity.Error);
            }
        }
        else
        {
            var soat = responseHttp.Response;
            soatDtoENT = new SoatDTO()
            {
                Id = soat!.Id,
                Date = soat.Date,
                ExpirationDate = soat.ExpirationDate,
                RateCategory = soat.RateCategory,
                PolicyData = soat.PolicyData,
                Price = soat.Price,
                InsurerId = soat.InsurerId,
                VehicleId = soat.VehicleId,
            };
        }
    }

    private async Task EditAsync()
    {
        var responseHttp = await Repository.PutAsync("api/soats/Edit", soatDtoENT);
        if (responseHttp.Error)
        {
            var messageError = await responseHttp.GetErrorMessageAsync();
            Snackbar.Add(messageError, Severity.Error);
            return;
        }

        Return();
        Snackbar.Add("Soat modificado con éxito.", Severity.Success);
    }

    private void Return()
    {
        soatsForm!.FormPostedSuccessfully = true;
        NavigationManager.NavigateTo("/Soats");
    }
}