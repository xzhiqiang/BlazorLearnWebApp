using System.Security.Claims;
using BlazorLearnWebApp.Entity;
using BlazorLearnWebApp.Vo;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace BlazorLearnWebApp.Controllers;

[ApiController]
[Route("/api/[controller]/[action]")]
public class AccountController : Controller
{
    [HttpPost]
    public object Login([FromBody] LoginVo loginVo)
    {
        var user = UserEntity.Where(x => x.UserName == loginVo.UserName && x.Password == loginVo.PassWord).First();
        if (user == null)
        {
            return new { Code = 5000, Message = "用户名或密码错误" };
        }

        var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
        identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName!));
        identity.AddClaim(new Claim(ClaimTypes.Role,user.RoleId.ToString()));
        
        
        HttpContext.SignInAsync(new ClaimsPrincipal(identity), new AuthenticationProperties()
        {
            IsPersistent = true,
            ExpiresUtc = loginVo.IsKeep ? DateTimeOffset.Now.AddDays(5) : DateTimeOffset.Now.AddMinutes(30)
        });

        return new { Code = 2000, Message = "登录成功" };
    }
}