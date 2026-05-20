using Microsoft.AspNetCore.Components;
using MudBlazor;
using Serviteca.Frontend.Repositories;
using Serviteca.Frontend.Shared;
using System.Diagnostics.Metrics;
using System.Net;
using e = Serviteca.Shared.Entities;

namespace Serviteca.Frontend.Pages.DocumentTypes;

public partial class DocumentTypesIndex
{
    private List<e.DocumentType>? lstDocumentType { get; set; }
    private MudTable<e.DocumentType> table = new();
    private readonly int[] pageSizeOptions = { 10, 25, 50, int.MaxValue };
    private int totalRecords = 0;
    private bool loading;
    private const string baseUrl = "api/DocumentTypes";
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
        var url = $"{baseUrl}/totalRecordsPaginated";

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

    private async Task<TableData<e.DocumentType>> LoadListAsync(TableState state, CancellationToken cancellationToken)
    {
        int page = state.Page + 1;
        int pageSize = state.PageSize;
        var url = $"{baseUrl}/paginated/?page={page}&recordsnumber={pageSize}";

        if (!string.IsNullOrWhiteSpace(Filter))
        {
            url += $"&filter={Filter}";
        }

        var responseHttp = await Repository.GetAsync<List<e.DocumentType>>(url);
        if (responseHttp.Error)
        {
            var message = await responseHttp.GetErrorMessageAsync();
            Snackbar.Add(message, Severity.Error);
            return new TableData<e.DocumentType>
            {
                Items = [],
                TotalItems = 0
            };
        }
        if (responseHttp.Response == null)
        {
            return new TableData<e.DocumentType>
            {
                Items = [],
                TotalItems = 0
            };
        }
        return new TableData<e.DocumentType>
        {
            Items = responseHttp.Response,
            TotalItems = totalRecords
        };
    }

    private async Task ShowModalCreateAsync()
    {
        //var options = new DialogOptions()
        //{
        //    CloseOnEscapeKey = true,
        //    CloseButton = true
        //};
        //IDialogReference? dialog = DialogService
        //                           .Show<CountryCreate>($"{Localizer["New"]} {Localizer["Country"]}", options);

        //var result = await dialog.Result;
        //if (result!.Canceled)
        //{
        //    await LoadTotalRecordsAsync();
        //    await table.ReloadServerData();
        //}
    }

    private async Task DeleteAsync(e.DocumentType documentType)
    {
        var parameters = new DialogParameters
        {
            {
                "Message", $"¿Está seguro de borrar el tipo documento: {documentType.Name}?"
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

        var responseHttp = await Repository.Delete2Async($"{baseUrl}/{documentType.Id}");
        if (responseHttp.Error)
        {
            if (responseHttp.HttpResponseMessage.StatusCode == HttpStatusCode.NotFound)
            {
                NavigationManager.NavigateTo("/countries");
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
        Snackbar.Add("Registro borrado con éxito.", Severity.Success);
    }

    private async Task ShowModalEditAsync(int Id)
    {
    }

    private async Task SetFilterValue(string value)
    {
        Filter = value;
        await LoadTotalRecordsAsync();
        await table.ReloadServerData();
    }
}