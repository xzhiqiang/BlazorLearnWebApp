using BlazorLearnWebApp.Components;
using BlazorLearnWebApp.Service;
using BootstrapBlazor.Components;
using FreeSql;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

IFreeSql fsql = new FreeSql.FreeSqlBuilder()
    .UseConnectionString(FreeSql.DataType.Sqlite, @"Data Source=freedb.db")
    // .UseMonitorCommand(cmd => System.Console.WriteLine($"Sql：{cmd.CommandText}")) //监听SQL语句
    .UseAutoSyncStructure(true) //自动同步实体结构到数据库，FreeSql不会扫描程序集，只有CRUD时才会生成表。
    .Build();

//事务为空
BaseEntity.Initialization(fsql, null);

builder.Services.AddControllers();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// 增加 BootstrapBlazor 服务
builder.Services.AddBootstrapBlazor();

builder.Services.AddScoped(typeof(IDataService<>), typeof(FreesqlDataService<>));
//配置 cookie身份认证
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(
    config => { config.LoginPath = "/login"; }
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();


//启用身份中间件
app.UseAuthentication();
//启用授权中间件
app.UseAuthorization();
app.MapDefaultControllerRoute();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();