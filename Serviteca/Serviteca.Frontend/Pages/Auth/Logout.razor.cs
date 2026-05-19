using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using MudBlazor;
using Serviteca.Frontend.Services;

namespace Serviteca.Frontend.Pages.Auth;

public partial class Logout
{
    [Inject] private NavigationManager NavigationManager { get; set; } = null!;
    [Inject] private ILoginService LoginService { get; set; } = null!;
    [CascadingParameter] private MudDialogInstance MudDialog { get; set; } = null!;

    private async Task LogoutActionAsync()
    {
        await LoginService.LogoutAsync();
        NavigationManager.NavigateTo("/");
        CancelAction();
    }

    private void CancelAction()
    {
        MudDialog.Cancel();
    }
}