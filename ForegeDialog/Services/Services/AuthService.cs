using System;
using System.Threading.Tasks;
using DatabaseBroker.Repositories.ClientRepository;
using Microsoft.Extensions.Configuration;
using DatabaseBroker.Repositories.UserRepository;
using Entity.Attributes;
using Entity.Models;
using Entity.Models.Users;
using Microsoft.EntityFrameworkCore;
using Services.Dtos;
using Services.Interfaces;

namespace Services.Services;
[Injectable]
public class AuthService : IAuthService
{
    private  IUserRepository UserRepository { get; set; }
    private IClientRepository  ClientRepository { get; set; }
    private readonly ITokenService _tokenService;
    public AuthService(IUserRepository repository, ITokenService tokenService, IClientRepository clientRepository)
    {
        UserRepository = repository;
        _tokenService = tokenService;
        ClientRepository = clientRepository;
    }


    public async Task<UserDto> Login(UserLoginDto dto)
    {
        var user = await UserRepository.FirstOrDefaultAsync(user => user.Email == dto.Email && user.Password == dto.Password);
        if (user is  null)
            throw new NullReferenceException();
        string token =  _tokenService.GetToken();
        var resUser = new UserDto()
        {
            Id=user.Id,
            UserName = user.UserName,
            Email = user.Email,
            Password = user.Password,
            Role = user.Role.ToString(),
            IsSigned = true,
            Token = token
        };

        user.IsSigned = true;
        await UserRepository.UpdateAsync(user);
        return resUser;
    }
    
    
    public async Task<ClientDto> ClientLogin(ClientLoginDto dto)
    {
        var client = await ClientRepository.FirstOrDefaultAsync(user => user.Email == dto.Email && user.Password == dto.Password);
        if (client is  null)
            throw new NullReferenceException();
        string token =  _tokenService.GetToken();
        var resUser = new ClientDto()
        {
            Id=client.Id,
            FullName = client.FullName,
            Email = client.Email,
            Password = client.Password,
            IsSigned = true,
            Token = token
        };

        client.IsSigned = true;
        await ClientRepository.UpdateAsync(client);
        return resUser;
    }
   
    public async Task<bool> LogOut(long id)
    {
        var user =await UserRepository.GetByIdAsync(id);
        if (user is  null)
            throw new NullReferenceException();
        
        user.IsSigned = false;
        await UserRepository.UpdateAsync(user);
        return true;
    } public async Task<bool> ClientLogOut(long id)
    {
        var client =await ClientRepository.GetByIdAsync(id);
        if (client is  null)
            throw new NullReferenceException();
        
        client.IsSigned = false;
        await ClientRepository.UpdateAsync(client);
        return true;
    }
    
    public async Task<UserDto> GetUser(long id)
    {
        var user=await UserRepository.GetByIdAsync(id);
        return new UserDto
        {
            UserName=user.UserName,
            Email = user.Email,
            Password = user.Password,
            Role = user.Role.ToString()
        };
    }
}