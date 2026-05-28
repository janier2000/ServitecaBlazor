using Microsoft.AspNetCore.Components;
using MudBlazor;
using Serviteca.Frontend.Repositories;
using Serviteca.Frontend.Shared;
using Serviteca.Shared.Entities;
using System.Net;

namespace Serviteca.Frontend.Pages.Uses;

public partial class UsesIndex
{
    private List<Use>? lstUse { get; set; }
    private MudTable<Use> table = new();
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
        var url = $"api/Uses/totalRecordsPaginated";

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

    private async Task<TableData<Use>> LoadListAsync(TableState state, CancellationToken cancellationToken)
    {
        int page = state.Page + 1;
        int pageSize = state.PageSize;
        var url = $"api/Uses/paginated/?page={page}&recordsnumber={pageSize}";

        if (!string.IsNullOrWhiteSpace(Filter))
        {
            url += $"&filter={Filter}";
        }

        var responseHttp = await Repository.GetAsync<List<Use>>(url);
        if (responseHttp.Error)
        {
            var message = await responseHttp.GetErrorMessageAsync();
            Snackbar.Add(message, Severity.Error);
            return new TableData<Use>
            {
                Items = [],
                TotalItems = 0
            };
        }
        if (responseHttp.Response == null)
        {
            return new TableData<Use>
            {
                Items = [],
                TotalItems = 0
            };
        }
        return new TableData<Use>
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
        IDialogReference? dialog = DialogService.Show<UsesCreate>($"Nuevo uso de vehículo ", options);
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
        IDialogReference? dialog = DialogService.Show<UsesEdit>($"Editar uso de vehículo ", parameters, options);
        var result = await dialog.Result;
        if (result!.Canceled)
        {
            await LoadTotalRecordsAsync();
            await table.ReloadServerData();
        }
    }

    private async Task DeleteAsync(Use vehicleType)
    {
        var parameters = new DialogParameters
        {
            {
                "Message", $"¿Está seguro de borrar el uso de vehículo: {vehicleType.Name}?"
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

        var responseHttp = await Repository.Delete2Async($"api/Uses/{vehicleType.Id}");
        if (responseHttp.Error)
        {
            if (responseHttp.HttpResponseMessage.StatusCode == HttpStatusCode.NotFound)
            {
                NavigationManager.NavigateTo("/Uses");
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
        Snackbar.Add("Uso vehiculo borrado con éxito.", Severity.Success);
    }
}