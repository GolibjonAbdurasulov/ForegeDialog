using System.Threading.Tasks;
using Services.Dtos;

namespace Services.Interfaces;

public interface IAuthService
{
    public Task<UserDto> Login(UserLoginDto dto);
    public Task<ClientDto> ClientLogin(ClientLoginDto dto);
    public  Task<bool> LogOut(long id);
    public  Task<bool> ClientLogOut(long id);
    public Task<UserDto> GetUser(long id);
}