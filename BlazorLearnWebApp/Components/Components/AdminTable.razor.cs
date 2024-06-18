using BootstrapBlazor.Components;
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
        [Parameter]
        public Func<TItem, ItemChangedType, Task<bool>>?  OnSaveAsync { get; set; }
        /// <summary>
        /// 获得/设置 删除按钮异步回调方法
        /// </summary>
        [Parameter]
        public Func<IEnumerable<TItem>, Task<bool>>? OnDeleteAsync { get; set; }
        [Parameter]
        public bool IsTree { get; set; }
        //
        // 摘要:
        //     获得/设置 生成树状结构回调方法
        [Parameter]
        public Func<IEnumerable<TItem>, Task<IEnumerable<TableTreeNode<TItem>>>>? TreeNodeConverter { get; set; }
        [Parameter] 
        public Func<TItem,bool>? ShowExtendEditButtonCallback { get; set; }
        [Parameter] 
        public Func<TItem,bool>? ShowExtendDeleteButtonCallback { get; set; }
        [Parameter]
        public RenderFragment<TItem>? BeforeRowButtonTemplate { get; set; }
        [Parameter]
        public RenderFragment<TItem>? RowButtonTemplate { get; set; }
    }
}
