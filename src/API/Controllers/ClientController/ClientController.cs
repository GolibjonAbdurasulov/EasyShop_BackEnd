using API.Common;
using API.Controllers.ClientController.Dtos;
using DatabaseBroker.Repositories.ClientRepository;
using Entity.Models.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Dtos;
using Services.Interfaces;
using ClientDto = API.Controllers.ClientController.Dtos.ClientDto;

namespace API.Controllers.ClientController;

[ApiController]
[Route("[controller]/[action]")]
public class ClientController : ControllerBase
{
   
    private IClientRepository ClientRepository { get; set; }
    
    public ClientController(IClientRepository clientRepository)
    {
        ClientRepository = clientRepository;
    }
    
    [HttpPost]
    public async Task<ResponseModelBase> CreateAsync( ClientCreationDto dto)
    {
        var client = new Client
        {
            FullName = dto.FullName,
            Email = dto.PhoneNumber,
            Password = dto.Password,
            IsSigned = false
        };
        
        var res= await ClientRepository.AddAsync(client);
        return new ResponseModelBase(res);
    }
    
    [HttpPut]
    [Authorize]
    public async Task<ResponseModelBase> UpdateAsync( ClientDto dto)
    {
        var client =await ClientRepository.UpdateAsync(new Client
        {
            Id = dto.Id,
            FullName = dto.FullName,
            Email = dto.PhoneNumber,
            Password = dto.Password,
            IsSigned = dto.IsSigned
        });
        
        return new ResponseModelBase(client);
    }
    
    
    [HttpDelete]
    [Authorize]
    public async Task<ResponseModelBase> DeleteAsync(long id)
    {
        var client =await ClientRepository.GetByIdAsync(id);
        var res =await ClientRepository.RemoveAsync(client);
        return new ResponseModelBase(res);
    }
   
    [HttpGet]
    public async Task<ResponseModelBase> GetAsync(long id)
    {
        var res =await ClientRepository.GetByIdAsync(id);
        return new ResponseModelBase(res);
    }
    
    
    [HttpGet]
    public async Task<ResponseModelBase> GetAllAsync()
    {
        var res = ClientRepository.GetAllAsQueryable().ToList();
        return new ResponseModelBase(res);
    }
}