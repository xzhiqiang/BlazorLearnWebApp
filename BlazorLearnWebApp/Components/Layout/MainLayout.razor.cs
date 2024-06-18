using System.Security.Claims;
using BlazorLearnWebApp.Entity;
using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;

namespace BlazorLearnWebApp.Components.Layout;

public partial class MainLayout
{
    private bool IsOpen { get; set; }

    private string? Theme { get; set; }

    private ClaimsPrincipal? _user { get; set; }

    private string? LayoutClassString => CssBuilder.Default("layout-demo")
        .AddClass(Theme)
        .AddClass("is-fixed-tab", IsFixedTab)
        .Build();

    private IEnumerable<MenuItem>? Menus { get; set; }

    /// <summary>
    /// 获得/设置 是否固定 TabHeader
    /// </summary>
    [Parameter]
    public bool IsFixedTab { get; set; }

    /// <summary>
    /// 获得/设置 是否固定页头
    /// </summary>
    [Parameter]
    public bool IsFixedHeader { get; set; } = true;

    /// <summary>
    /// 获得/设置 是否固定页脚
    /// </summary>
    [Parameter]
    public bool IsFixedFooter { get; set; } = true;

    /// <summary>
    /// 获得/设置 侧边栏是否外置
    /// </summary>
    [Parameter]
    public bool IsFullSide { get; set; } = true;

    /// <summary>
    /// 获得/设置 是否显示页脚
    /// </summary>
    [Parameter]
    public bool ShowFooter { get; set; } = true;

    /// <summary>
    /// 获得/设置 是否开启多标签模式
    /// </summary>
    [Parameter]
    public bool UseTabSet { get; set; } = true;

    /// <summary>
    /// OnInitializedAsync 方法
    /// </summary>
    /// <returns></returns>
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        _user = (await Authentication.GetAuthenticationStateAsync()).User;

        if (_user == null)
        {
            return;
        }

        var roleId = _user.FindFirst(ClaimTypes.Role)?.Value;
        if (roleId == null)
        {
            return;
        }

        var role = await RoleEntity.Where(x => x.Id == int.Parse(roleId)).IncludeMany(x => x.Menus).FirstAsync();
        if (role == null || role.Menus == null)
        {
            return;
        }

        Menus = CascadingMenu(role.Menus, 0);
    }

    /// <summary>
    /// 二级菜单树
    /// </summary>
    /// <param name="menuEntities"></param>
    /// <param name="parentId"></param>
    /// <returns></returns>
    private List<MenuItem> CascadingMenu(List<MenuEntity> menuEntities, int parentId) => menuEntities
        .Where(x => x.ParentId == parentId)
        .Select(x => new MenuItem
            { Text = x.MenuName, Items = CascadingMenu(menuEntities, x.Id), Icon = x.Icon, Url = x.Url })
        .ToList();


    /// <summary>
    /// 更新组件方法
    /// </summary>
    public void Update() => StateHasChanged();

    private void ToggleDrawer()
    {
        IsOpen = !IsOpen;
    }
}