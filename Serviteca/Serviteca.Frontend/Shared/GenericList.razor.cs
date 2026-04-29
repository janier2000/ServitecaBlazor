using Microsoft.AspNetCore.Components;

namespace Serviteca.Frontend.Shared
{
    public partial class GenericList<Titem>
    {
        [Parameter] public RenderFragment? Loading { get; set; }
        [Parameter] public RenderFragment? NoRecords { get; set; }
        [EditorRequired, Parameter] public RenderFragment Body { get; set; } = null!; // este significa que es obligatorio
        [EditorRequired, Parameter] public List<Titem> MyList { get; set; } = null!;
    }
}