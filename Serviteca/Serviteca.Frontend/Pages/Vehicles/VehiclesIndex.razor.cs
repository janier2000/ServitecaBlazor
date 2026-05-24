using Microsoft.AspNetCore.Components;
using MudBlazor;
using Serviteca.Frontend.Repositories;
using Serviteca.Frontend.Shared;
using System.Net;
using E = Serviteca.Shared.Entities;

namespace Serviteca.Frontend.Pages.Vehicles;

public partial class VehiclesIndex
{
    private List<E.Vehicle>? lstVehicle { get; set; }
    private MudTable<E.Vehicle> table = new();
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
        var url = $"api/Vehicles/TotalRecordsPaginated";

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

    private async Task<TableData<E.Vehicle>> LoadListAsync(TableState state, CancellationToken cancellationToken)
    {
        int page = state.Page + 1;
        int pageSize = state.PageSize;
        var url = $"api/Vehicles/Paginated/?page={page}&recordsnumber={pageSize}";

        if (!string.IsNullOrWhiteSpace(Filter))
        {
            url += $"&filter={Filter}";
        }

        var responseHttp = await Repository.GetAsync<List<E.Vehicle>>(url);
        if (responseHttp.Error)
        {
            var message = await responseHttp.GetErrorMessageAsync();
            Snackbar.Add(message, Severity.Error);
            return new TableData<E.Vehicle>
            {
                Items = [],
                TotalItems = 0
            };
        }
        if (responseHttp.Response == null)
        {
            return new TableData<E.Vehicle>
            {
                Items = [],
                TotalItems = 0
            };
        }
        return new TableData<E.Vehicle>
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
        IDialogReference? dialog = DialogService.Show<VehiclesCreate>($"Nuevo vehiculo", options);
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
        IDialogReference? dialog = DialogService.Show<VehiclesEdit>($"Editar vehiculo", parameters, options);
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
        IDialogReference? dialog = DialogService.Show<VehiclesDetail>($"Detalle del vehiculo", parameters, options);
        var result = await dialog.Result;
        if (result!.Canceled)
        {
            await LoadTotalRecordsAsync();
            await table.ReloadServerData();
        }
    }

    private async Task DeleteAsync(E.Vehicle Vehicle)
    {
        var parameters = new DialogParameters
        {
            {
                "Message", $"¿Está seguro de borrar el vehiculo: {Vehicle.Plate} {Vehicle.Model}?"
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

        var responseHttp = await Repository.Delete2Async($"api/Vehicles/{Vehicle.Id}");
        if (responseHttp.Error)
        {
            if (responseHttp.HttpResponseMessage.StatusCode == HttpStatusCode.NotFound)
            {
                NavigationManager.NavigateTo("/Vehicles");
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
        Snackbar.Add("Vehiculo borrado con éxito.", Severity.Success);
    }
}