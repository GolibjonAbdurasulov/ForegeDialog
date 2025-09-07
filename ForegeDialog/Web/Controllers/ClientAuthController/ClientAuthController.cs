using Microsoft.AspNetCore.Mvc;
using Services.Dtos;
using Services.Interfaces;
using Web.Common;

namespace Web.Controllers.ClientAuthController;

[ApiController]
[Route("[controller]/[action]")]
public class ClientAuthController(IAuthService authService) : ControllerBase
{
    private IAuthService _authService = authService;
    
    [HttpPost]
    public async Task<ResponseModelBase> Login([FromBody]ClientLoginDto dto)
    {
        var res = await _authService.ClientLogin(dto);
        return new ResponseModelBase(res);
    }

    [HttpPost]
    public async Task<ResponseModelBase> LogOut(long id)
    {
        var res = await _authService.ClientLogOut(id);
        
        return new ResponseModelBase(res);
    }
    
        
    [HttpPost]
    public async Task<ResponseModelBase> Registration(RegisterDto dto)
    {
        var res=await _authService.Registration(dto);
        
        return new ResponseModelBase(res);
    }
    
    [HttpPost]
    public async Task<ResponseModelBase> ResendOtp(ClientDto dto)
    {
        var res=await _authService.ResendOtp(dto);
        
        return new ResponseModelBase(res);
    }
    
    [HttpPost]
    public async Task<ResponseModelBase> VerifyOtp(string email, string otp)
    {
        var res=await _authService.ConfirmEmail(email, otp);
        
        return new ResponseModelBase(res);
    }

}