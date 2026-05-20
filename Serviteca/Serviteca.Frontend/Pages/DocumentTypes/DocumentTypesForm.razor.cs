using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.Extensions.Localization;
using System.Diagnostics.Metrics;
using e = Serviteca.Shared.Entities;

namespace Serviteca.Frontend.Pages.DocumentTypes;

public partial class DocumentTypesForm
{
    private EditContext editContext = null!;

    protected override void OnInitialized()
    {
        editContext = new(DocumentTypeENT);
    }

    [EditorRequired, Parameter] public e.DocumentType DocumentTypeENT { get; set; } = null!;
    [EditorRequired, Parameter] public EventCallback OnValidSubmit { get; set; }
    [EditorRequired, Parameter] public EventCallback ReturnAction { get; set; }
    public bool FormPostedSuccessfully { get; set; } = false;
    [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;

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
            ShowCancelButton = true
        });

        var confirm = !string.IsNullOrEmpty(result.Value);
        if (confirm)
        {
            return;
        }

        context.PreventNavigation();
    }
}