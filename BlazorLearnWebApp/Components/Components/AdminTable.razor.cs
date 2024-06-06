using Microsoft.AspNetCore.Components;
using System.Diagnostics.CodeAnalysis;

namespace BlazorLearnWebApp.Components.Components
{
    [CascadingTypeParameter(nameof(TItem))]
    public partial class AdminTable<TItem>where TItem : class ,new()
    {
        [NotNull]
        [Parameter]
        public RenderFragment<TItem>? TableColumns { get; set; }

    }
}
