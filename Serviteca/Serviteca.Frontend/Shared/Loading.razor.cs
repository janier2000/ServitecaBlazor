using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace Serviteca.Frontend.Shared;

public partial class Loading
{
    [Parameter] public string? Label { get; set; }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        if (string.IsNullOrEmpty(Label))
        {
            Label = "Por favor espera...";
        }
    }
}