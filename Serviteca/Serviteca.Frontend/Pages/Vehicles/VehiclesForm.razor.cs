using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using MudBlazor;
using Serviteca.Frontend.Repositories;
using Serviteca.Shared.DTOs;
using Serviteca.Shared.Entities;
using Serviteca.Shared.Enums;

namespace Serviteca.Frontend.Pages.Vehicles;

public partial class VehiclesForm
{
    private EditContext editContext = null!;

    private List<Brand>? LstBrand;
    private Brand selectedBrand = new();

    private List<Use>? LstUse;
    private Use selectedUse = new();

    private List<TypeV>? LstType;
    private TypeV selectedType = new();

    private List<Customer>? LstCustomer;
    private Customer selectedCustomer = new();

    [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
    [Inject] private IRepository Repository { get; set; } = null!;
    [Inject] private ISnackbar Snackbar { get; set; } = null!;
    [EditorRequired, Parameter] public VehicleDTO VehicleDtoENT { get; set; } = null!;
    [EditorRequired, Parameter] public EventCallback OnValidSubmit { get; set; }
    [EditorRequired, Parameter] public EventCallback ReturnAction { get; set; }
    private DateTime? selectedReturnDate { get; set; } = DateTime.Now.Date;
    private string? accion { get; set; }
    public bool FormPostedSuccessfully { get; set; } = false;

    protected override async Task OnInitializedAsync()
    {
        editContext = new(VehicleDtoENT);
        await LoadBrandAsync();
        await LoadTypeAsync();
        await LoadUseAsync();
        await LoadCustomerAsync();
        LoadReturnDateAsync();
        LoadData();
    }

    private void LoadData()
    {
        accion = VehicleDtoENT.Id == 0 ? "Crear" : "Editar";
    }
   
    private async Task LoadBrandAsync()
    {
        var responseHttp = await Repository.GetAsync<List<Brand>>("/api/Brands/combo");
        if (responseHttp.Error)
        {
            var message = await responseHttp.GetErrorMessageAsync();
            await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
            return;
        }
        LstBrand = responseHttp.Response;
        if (VehicleDtoENT.BrandId != 0)
        {
            selectedBrand = LstBrand!.FirstOrDefault(x => x.Id == VehicleDtoENT.BrandId)!;
        }
    }

    private async Task LoadTypeAsync()
    {
        var responseHttp = await Repository.GetAsync<List<TypeV>>("/api/Types/combo");
        if (responseHttp.Error)
        {
            var message = await responseHttp.GetErrorMessageAsync();
            await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
            return;
        }
        LstType = responseHttp.Response;
        if (VehicleDtoENT.TypeVId != 0)
        {
            selectedType = LstType!.FirstOrDefault(x => x.Id == VehicleDtoENT.TypeVId)!;
        }
    }

    private async Task LoadUseAsync()
    {
        var responseHttp = await Repository.GetAsync<List<Use>>("/api/Uses/combo");
        if (responseHttp.Error)
        {
            var message = await responseHttp.GetErrorMessageAsync();
            await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
            return;
        }
        LstUse = responseHttp.Response;
        if (VehicleDtoENT.UseId != 0)
        {
            selectedUse = LstUse!.FirstOrDefault(x => x.Id == VehicleDtoENT.UseId)!;
        }
    }

    private async Task LoadCustomerAsync()
    {
        var responseHttp = await Repository.GetAsync<List<Customer>>("/api/Customers/combo");
        if (responseHttp.Error)
        {
            var message = await responseHttp.GetErrorMessageAsync();
            await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
            return;
        }
        LstCustomer = responseHttp.Response;
        if (VehicleDtoENT.CustomerId != 0)
        {
            selectedCustomer = LstCustomer!.FirstOrDefault(x => x.Id == VehicleDtoENT.CustomerId)!;
        }
    }

    private void LoadReturnDateAsync()
    {
        if (VehicleDtoENT.ReturnDate != default)
        {
            selectedReturnDate = VehicleDtoENT.ReturnDate;
        }
        else
        {
            selectedReturnDate = DateTime.Now;
        }
    }

    protected override void OnParametersSet()
    {
        //base.OnParametersSet();
        //if (!string.IsNullOrEmpty(CustomerDTOENT.Image))
        //{
        //    CustomerDTOENT.Image = null;
        //}
        //isActiveMessage = CustomerDTOENT.IsActive ? $"{Localizer["Group"]} {Localizer["Active"]}" : $"{Localizer["Group"]} {Localizer["Inactive"]}";
    }

    private void ChangedBrand(Brand brandEnt)
    {
        selectedBrand = brandEnt;
        VehicleDtoENT.BrandId = brandEnt.Id;
    }

    private void ChangedUse(Use useEnt)
    {
        selectedUse = useEnt;
        VehicleDtoENT.UseId = useEnt.Id;
    }

    private void ChangedType(TypeV typeEnt)
    {
        selectedType = typeEnt;
        VehicleDtoENT.TypeVId = typeEnt.Id;
    }

    private void ChangedCustomer(Customer customerEnt)
    {
        selectedCustomer = customerEnt;
        VehicleDtoENT.CustomerId = customerEnt.Id;
    }

    private void ChangedReturnDate(DateTime? newDate)
    {
        selectedReturnDate = newDate;
        VehicleDtoENT.ReturnDate = selectedReturnDate.HasValue ? selectedReturnDate.Value : default;
    }

    private async Task<IEnumerable<Brand>> SearchBrand(string searchText, CancellationToken cancellationToken)
    {
        await Task.Delay(5);
        if (string.IsNullOrWhiteSpace(searchText))
        {
            return LstBrand!;
        }

        return LstBrand!.Where(x => x.Name.Contains(searchText, StringComparison.InvariantCultureIgnoreCase)).ToList();
    }

    private async Task<IEnumerable<Use>> SearchUse(string searchText, CancellationToken cancellationToken)
    {
        await Task.Delay(5);
        if (string.IsNullOrWhiteSpace(searchText))
        {
            return LstUse!;
        }
        return LstUse!.Where(x => x.Name.Contains(searchText, StringComparison.InvariantCultureIgnoreCase)).ToList();
    }

    private async Task<IEnumerable<TypeV>> SearchType(string searchText, CancellationToken cancellationToken)
    {
        await Task.Delay(5);
        if (string.IsNullOrWhiteSpace(searchText))
        {
            return LstType!;
        }
        return LstType!.Where(x => x.Name.Contains(searchText, StringComparison.InvariantCultureIgnoreCase)).ToList();
    }

    private async Task<IEnumerable<Customer>> SearchCustomer(string searchText, CancellationToken cancellationToken)
    {
        await Task.Delay(5);
        if (string.IsNullOrWhiteSpace(searchText))
        {
            return LstCustomer!;
        }
        return LstCustomer!.Where(x => x.FirstName.Contains(searchText, StringComparison.InvariantCultureIgnoreCase)).ToList();
    }

    private async Task OnBeforeInternalNavigation(LocationChangingContext context)
    {
        var formWasEdited = editContext.IsModified();
        if (!formWasEdited || FormPostedSuccessfully)
        {
            return;
        }

        var result = await SweetAlertService.FireAsync(new SweetAlertOptions
        {
            Title = "Confirmación",
            Text = "¿Deseas abandonar la página y perder los cambios?",
            Icon = SweetAlertIcon.Warning,
            ShowCancelButton = true,
            CancelButtonText = "Cancelar",
        });

        var confirm = !string.IsNullOrEmpty(result.Value);
        if (confirm)
        {
            return;
        }
        context.PreventNavigation();
    }

    private void OnToggledChanged(bool toggled)
    {
        //CustomerDTOENT.IsActive = toggled;
        //isActiveMessage = CustomerDTOENT.IsActive ? $"{Localizer["Group"]} {Localizer["Active"]}" : $"{Localizer["Group"]} {Localizer["Inactive"]}";
    }

    private void OnInvalidSubmit(EditContext editContext)
    {
        var messages = editContext.GetValidationMessages();

        foreach (var message in messages)
        {
            Snackbar.Add(message, Severity.Error);
        }
    }
}