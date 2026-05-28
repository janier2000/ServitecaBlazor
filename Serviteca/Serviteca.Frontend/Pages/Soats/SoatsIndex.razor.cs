using Microsoft.AspNetCore.Components;
using MudBlazor;
using Serviteca.Frontend.Pages.Vehicles;
using Serviteca.Frontend.Repositories;
using Serviteca.Frontend.Shared;
using Serviteca.Shared.Entities;
using System.Net;

namespace Serviteca.Frontend.Pages.Soats;

public partial class SoatsIndex
{
    private List<Soat>? lstSoat { get; set; }
    private MudTable<Soat> table = new();
    private readonly int[] pageSizeOptions = { 10, 25, 50, int.MaxValue };
    private int totalRecords = 0;
    private bool loading;
    private string infoFormat = "{first_item}-{last_item} => {all_items}";

    [Inject] private IRepository Repository { get; set; } = null!;
    [Inject] private IDialogService DialogService { get; set; } = null!;
    [Inject] private ISnackbar Snackbar { get; set; } = null!;
    [Inject] private NavigationManager NavigationManager { get; set; } = null!;
    [Parameter, SupplyParameterFromQuery] public string Filter { get; set; } = string.Empty;

    protected override async Task OnInitializedAsync()
    {
        await LoadTotalRecordsAsync();
    }

    private async Task LoadTotalRecordsAsync()
    {
        loading = true;
        var url = $"api/Soats/TotalRecordsPaginated";

        if (!string.IsNullOrWhiteSpace(Filter))
        {
            url += $"?filter={Filter}";
        }

        var responseHttp = await Repository.GetAsync<int>(url);
        if (responseHttp.Error)
        {
            var message = await responseHttp.GetErrorMessageAsync();
            Snackbar.Add(message, Severity.Error);
            return;
        }
        totalRecords = responseHttp.Response;
        loading = false;
    }

    private async Task<TableData<Soat>> LoadListAsync(TableState state, CancellationToken cancellationToken)
    {
        int page = state.Page + 1;
        int pageSize = state.PageSize;
        var url = $"api/Soats/Paginated/?page={page}&recordsnumber={pageSize}";
        if (!string.IsNullOrWhiteSpace(Filter))
        {
            url += $"&filter={Filter}";
        }

        var responseHttp = await Repository.GetAsync<List<Soat>>(url);
        if (responseHttp.Error)
        {
            var message = await responseHttp.GetErrorMessageAsync();
            Snackbar.Add(message, Severity.Error);
            return new TableData<Soat>
            {
                Items = [],
                TotalItems = 0
            };
        }
        if (responseHttp.Response == null)
        {
            return new TableData<Soat>
            {
                Items = [],
                TotalItems = 0
            };
        }
        return new TableData<Soat>
        {
            Items = responseHttp.Response,
            TotalItems = totalRecords
        };
    }

    private async Task SetFilterValue(string value)
    {
        Filter = value;
        await LoadTotalRecordsAsync();
        await table.ReloadServerData();
    }

    private async Task ShowModalCreateAsync()
    {
        var options = new DialogOptions()
        {
            CloseOnEscapeKey = true,
            CloseButton = true
        };
        IDialogReference? dialog = DialogService.Show<SoatsCreate>($"Nuevo Soat", options);
        var result = await dialog.Result;
        if (result!.Canceled)
        {
            await LoadTotalRecordsAsync();
            await table.ReloadServerData();
        }
    }

    private async Task ShowModalEditAsync(int Id)
    {
        var options = new DialogOptions()
        {
            CloseOnEscapeKey = true,
            CloseButton = true
        };
        var parameters = new DialogParameters
        {
            { "Id", Id }
        };
        IDialogReference? dialog = DialogService.Show<SoatsEdit>($"Editar Soat", parameters, options);
        var result = await dialog.Result;
        if (result!.Canceled)
        {
            await LoadTotalRecordsAsync();
            await table.ReloadServerData();
        }
    }

    private async Task ShowModalDetailAsync(int Id)
    {
        var options = new DialogOptions()
        {
            CloseOnEscapeKey = true,
            CloseButton = true
        };
        var parameters = new DialogParameters
        {
            { "Id", Id }
        };
        IDialogReference? dialog = DialogService.Show<SoatsDetail>($"Detalle del soat", parameters, options);
        var result = await dialog.Result;
        if (result!.Canceled)
        {
            await LoadTotalRecordsAsync();
            await table.ReloadServerData();
        }
    }

    private async Task DeleteAsync(Soat soat)
    {
        var parameters = new DialogParameters
        {
            {
                "Message", $"¿Está seguro de borrar el soat: {soat.Insurer!.Name} para placa de vehiculo {soat.Vehicle!.Plate}?"
            }
        };
        var options = new DialogOptions
        {
            CloseButton = true,
            MaxWidth = MaxWidth.ExtraSmall,
            CloseOnEscapeKey = true
        };
        var dialog = DialogService.Show<ConfirmDialog>("Confirmación", parameters, options);
        var result = await dialog.Result;
        if (result!.Canceled)
        {
            return;
        }

        var responseHttp = await Repository.Delete2Async($"api/Soats/{soat.Id}");
        if (responseHttp.Error)
        {
            if (responseHttp.HttpResponseMessage.StatusCode == HttpStatusCode.NotFound)
            {
                NavigationManager.NavigateTo("/Soats");
            }
            else
            {
                var message = await responseHttp.GetErrorMessageAsync();
                Snackbar.Add(message, Severity.Error);
            }
            return;
        }
        await LoadTotalRecordsAsync();
        await table.ReloadServerData();
        Snackbar.Add("Soat borrado con éxito.", Severity.Success);
    }
}