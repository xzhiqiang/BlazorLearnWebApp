using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;
using System.Diagnostics.CodeAnalysis;

namespace BlazorLearnWebApp.Components.Components
{
    [CascadingTypeParameter(nameof(TItem))]
    public partial class AdminTable<TItem>where TItem : class ,new()
    {
        private Table<TItem> _table;

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

        /// <summary>获得/设置 是否分页 默认为 false</summary>
        [Parameter]
        public bool IsPagination { get; set; } = true;

        /// <summary>
        /// 获得/设置 是否为多选模式 默认为 false
        /// </summary>
        /// <remarks>此参数在 <see cref="IsExcel"/> 模式下为 true</remarks>
        [Parameter]
        public bool IsMultipleSelect { get; set; } = false;
        [Parameter] public RenderFragment? TableToolbarTemplate { get; set; }
        /// <summary>
        /// 获得/设置 表格 Toolbar 按钮模板
        /// <para>表格工具栏右侧按钮模板，模板中内容出现在默认按钮后面</para>
        /// </summary>
        [Parameter]
        public RenderFragment? TableExtensionToolbarTemplate { get; set; }


        public async Task QueryAsync()
        {
            await _table.QueryAsync();
        }
    }
}
