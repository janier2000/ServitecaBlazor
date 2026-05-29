using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Serviteca.Frontend.Pages.Vehicles;
using Serviteca.Frontend.Repositories;
using Serviteca.Shared.DTOs;
using Serviteca.Shared.Entities;

namespace Serviteca.Frontend.Pages.Soats;

public partial class SoatsCreate
{
    private SoatsForm? soatsForm;
    private SoatDTO soatDtoENT = new();
    [Inject] private IRepository Repository { get; set; } = null!;
    [Inject] private NavigationManager NavigationManager { get; set; } = null!;
    [Inject] private ISnackbar Snackbar { get; set; } = null!;
    [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;

    private async Task CreateAsync()
    {
        var responseHttp = await Repository
            .PostAsync<SoatDTO, Soat>("/api/Soats/Create", soatDtoENT);
        if (responseHttp.Error)
        {
            var message = await responseHttp.GetErrorMessageAsync();
            Snackbar.Add(message, Severity.Error);
            return;
        }
        var vehicleResponse = responseHttp.Response;

        Return();
        Snackbar.Add("Soat creado con éxito.", Severity.Success);
    }

    private void Return()
    {
        soatsForm!.FormPostedSuccessfully = true;
        NavigationManager.NavigateTo("/Soats");
    }
}