using FreeSql;
using FreeSql.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BlazorLearnWebApp.Entity
{
    [Description("菜单表")]
    public class MenuEntity : BaseEntity<MenuEntity, int>
    {
        [Description("菜单名")]
        [Required(ErrorMessage ="菜单名不能为空")]
        public string? MenuName { get; set; }
        [Description("URL")]
        [Required(ErrorMessage = "URL不能为空")]
        public string? Url { get; set;}
        [Description("图标")]
        [Required(ErrorMessage = "图标不能为空")]
        public string? Icon { get; set;}
        [Description("父菜单")]
        public int ParentId { get; set;}

        [Navigate(nameof(ParentId))]
        public MenuEntity? Parent { get; set;}
        [Navigate(nameof(ParentId))]
        public List<MenuEntity>? Children { get; set; }


        [Navigate(ManyToMany = typeof(RoleMenuEntity))]
        public List<RoleMenuEntity>? Roles { get; set; }
    }
}
