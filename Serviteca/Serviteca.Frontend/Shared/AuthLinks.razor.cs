using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Localization;
using MudBlazor;
using Serviteca.Frontend.Pages.Auth;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Serviteca.Frontend.Shared;

public partial class AuthLinks
{
    private string? photoUser;

    [Inject] private NavigationManager NavigationManager { get; set; } = null!;
    [Inject] private IDialogService DialogService { get; set; } = null!;
    [CascadingParameter] private Task<AuthenticationState> AuthenticationStateTask { get; set; } = null!;

    protected override async Task OnParametersSetAsync()
    {
        //var authenticationState = await AuthenticationStateTask;
        //var claims = authenticationState.User.Claims.ToList();
        //var photoClaim = claims.FirstOrDefault(x => x.Type == "Photo");
        //var nameClaim = claims.FirstOrDefault(x => x.Type == "UserName");
        //if (photoClaim is not null)
        //{
        //    photoUser = photoClaim.Value;
        //}
    }

    private void EditAction()
    {
        NavigationManager.NavigateTo("/EditUser");
    }

    private void ShowModalLogIn()
    {
        var closeOnEscapeKey = new DialogOptions()
        {
            CloseOnEscapeKey = true
        };
        DialogService.Show<Login>("Iniciar Sesión", closeOnEscapeKey);
    }

    private void ShowModalLogOut()
    {
        var closeOnEscapeKey = new DialogOptions()
        {
            CloseOnEscapeKey = true
        };
        DialogService.Show<Logout>("¿Estás seguro que deseas cerrar sesión ?", closeOnEscapeKey);
    }
}