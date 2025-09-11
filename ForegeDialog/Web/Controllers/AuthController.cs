using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Services.Dtos;
using Services.Interfaces;
using Services.Services;
using Web.Common;

namespace Web.Controllers;
[ApiController]
[Route("[controller]/[action]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<ResponseModelBase> Login([FromBody]UserLoginDto dto)
    {
        var res = await _authService.Login(dto);
        return new ResponseModelBase(res);
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<ResponseModelBase> LogOut(long id)
    {
        var res =await _authService.LogOut(id);
        
        return new ResponseModelBase(res);
    }
}
