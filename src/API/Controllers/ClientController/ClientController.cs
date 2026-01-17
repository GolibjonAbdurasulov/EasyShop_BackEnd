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
            ClientFullName = dto.ClientFullName,
            CompanyName = dto.CompanyName,
            INN = dto.INN,
            PhoneNumber = dto.PhoneNumber,
            Password = dto.Password,
            IsSigned = false
        };
        
        var res= await ClientRepository.AddAsync(client);
        return new ResponseModelBase(res);
    }
    
    [HttpPut]
    [Authorize]
    public async Task<ResponseModelBase> UpdateAsync(ClientDto dto)
    {
        var clientFromDb = await ClientRepository.GetByIdAsync(dto.Id);
        if (clientFromDb == null)
            return new ResponseModelBase(new Exception("Client not found"));

        clientFromDb.ClientFullName = dto.ClientFullName;
        clientFromDb.CompanyName = dto.CompanyName;
        clientFromDb.INN = dto.INN;
        clientFromDb.PhoneNumber = dto.PhoneNumber;
        clientFromDb.IsSigned = dto.IsSigned;


        if (!string.IsNullOrWhiteSpace(dto.Password))
        {
            clientFromDb.Password = dto.Password; 
        }


        var updatedClient = await ClientRepository.UpdateAsync(clientFromDb);

        return new ResponseModelBase(updatedClient);
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