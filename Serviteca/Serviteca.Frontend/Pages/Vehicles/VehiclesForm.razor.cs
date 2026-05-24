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
    private string? isActiveMessage;
    private EditContext editContext = null!;

    private List<VehicleBrand>? LstVehicleBrand;
    private VehicleBrand selectedBrand = new();

    private List<VehicleUse>? LstVehicleUse;
    private VehicleUse selectedUse = new();

    private List<VehicleType>? LstVehicleType;
    private VehicleType selectedType = new();

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
        LoadData();
        await LoadBrandAsync();
        await LoadTypeAsync();
        await LoadUseAsync();
        await LoadCustomerAsync();
        LoadReturnDateAsync();
    }

    private void LoadData()
    {
        if (VehicleDtoENT.Id == 0)
        {
            accion = "Crear";
        }
        else
        {
            accion = "Editar";
        }
    }

    private async Task LoadBrandAsync()
    {
        var responseHttp = await Repository.GetAsync<List<VehicleBrand>>("/api/VehicleBrands/combo");
        if (responseHttp.Error)
        {
            var message = await responseHttp.GetErrorMessageAsync();
            await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
            return;
        }
        LstVehicleBrand = responseHttp.Response;
        if (VehicleDtoENT.VehicleBrandId != 0)
        {
            selectedBrand = LstVehicleBrand!.FirstOrDefault(x => x.Id == VehicleDtoENT.VehicleBrandId)!;
        }
    }

    private async Task LoadTypeAsync()
    {
        var responseHttp = await Repository.GetAsync<List<VehicleType>>("/api/VehicleTypes/combo");
        if (responseHttp.Error)
        {
            var message = await responseHttp.GetErrorMessageAsync();
            await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
            return;
        }
        LstVehicleType = responseHttp.Response;
        if (VehicleDtoENT.VehicleTypeId != 0)
        {
            selectedType = LstVehicleType!.FirstOrDefault(x => x.Id == VehicleDtoENT.VehicleTypeId)!;
        }
    }

    private async Task LoadUseAsync()
    {
        var responseHttp = await Repository.GetAsync<List<VehicleUse>>("/api/VehicleUses/combo");
        if (responseHttp.Error)
        {
            var message = await responseHttp.GetErrorMessageAsync();
            await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
            return;
        }
        LstVehicleUse = responseHttp.Response;
        if (VehicleDtoENT.VehicleUseId != 0)
        {
            selectedUse = LstVehicleUse!.FirstOrDefault(x => x.Id == VehicleDtoENT.VehicleUseId)!;
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

    private void ChangedBrand(VehicleBrand vehicleBrandEnt)
    {
        selectedBrand = vehicleBrandEnt;
        VehicleDtoENT.VehicleBrandId = vehicleBrandEnt.Id;
    }

    private void ChangedUse(VehicleUse vehicleUseEnt)
    {
        selectedUse = vehicleUseEnt;
        VehicleDtoENT.VehicleUseId = vehicleUseEnt.Id;
    }

    private void ChangedType(VehicleType vehicleTypeEnt)
    {
        selectedType = vehicleTypeEnt;
        VehicleDtoENT.VehicleTypeId = vehicleTypeEnt.Id;
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

    private async Task<IEnumerable<VehicleBrand>> SearchBrand(string searchText, CancellationToken cancellationToken)
    {
        await Task.Delay(5);
        if (string.IsNullOrWhiteSpace(searchText))
        {
            return LstVehicleBrand!;
        }

        return LstVehicleBrand!.Where(x => x.Name.Contains(searchText, StringComparison.InvariantCultureIgnoreCase)).ToList();
    }

    private async Task<IEnumerable<VehicleUse>> SearchUse(string searchText, CancellationToken cancellationToken)
    {
        await Task.Delay(5);
        if (string.IsNullOrWhiteSpace(searchText))
        {
            return LstVehicleUse!;
        }
        return LstVehicleUse!.Where(x => x.Name.Contains(searchText, StringComparison.InvariantCultureIgnoreCase)).ToList();
    }

    private async Task<IEnumerable<VehicleType>> SearchType(string searchText, CancellationToken cancellationToken)
    {
        await Task.Delay(5);
        if (string.IsNullOrWhiteSpace(searchText))
        {
            return LstVehicleType!;
        }
        return LstVehicleType!.Where(x => x.Name.Contains(searchText, StringComparison.InvariantCultureIgnoreCase)).ToList();
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