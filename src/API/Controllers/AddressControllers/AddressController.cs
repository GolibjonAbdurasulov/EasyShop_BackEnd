using System.Net;
using API.Common;
using API.Controllers.AddressControllers.Dtos;
using API.Controllers.TagsController.FoodProductTagsController.Dtos;
using DatabaseBroker.Repositories.AddressRepositories;
using DatabaseBroker.Repositories.Tags.FoodProductTagsRepository;
using Entity.Models.Order;
using Entity.Models.Product.Tags;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.AddressControllers;

[ApiController]
[Route("[controller]/[action]")]
public class AddressController : ControllerBase
{
  private IAddressRepository AddressRepository { get; set; }
    public AddressController(IAddressRepository addressRepository)
    {
        AddressRepository = addressRepository;
    }

    [HttpPost]
    [Authorize]
    public async Task<ResponseModelBase> CreateAsync( AddressCreationDto dto)
    {
        var entity = new Address
        {
            FullAddress = dto.FullAddress,
            Latitude = dto.Latitude,
            Longitude = dto.Longitude,
            City = dto.City,
            Region = dto.Region,
            PostalCode = dto.PostalCode,
            ClientId = dto.ClientId
        };
        var resEntity=await AddressRepository.AddAsync(entity);
        
        var resDto = new AddressDto
        {
            Id = resEntity.Id,
            FullAddress = resEntity.FullAddress,
            Latitude = resEntity.Latitude,
            Longitude = resEntity.Longitude,
            City = resEntity.City,
            Region = entity.Region,
            PostalCode = resEntity.PostalCode,
            ClientId = resEntity.ClientId
        };
        return new ResponseModelBase(resDto);
    }


  
    [HttpPut]
    [Authorize]
    public async Task<ResponseModelBase> UpdateAsync( AddressDto dto)
    {
        var res =  await AddressRepository.GetByIdAsync(dto.Id);
        res.FullAddress=dto.FullAddress;
        res.Latitude=dto.Latitude;
        res.Longitude=dto.Longitude;
        res.City=dto.City;
        res.Region=dto.Region;
        res.PostalCode=dto.PostalCode;
        res.ClientId=dto.ClientId;
        
        await AddressRepository.UpdateAsync(res);
        return new ResponseModelBase(dto);
    }
    
    
    [HttpDelete]
    [Authorize]
    public async Task<ResponseModelBase> DeleteAsync(long id)
    {
        var res =  await AddressRepository.GetByIdAsync(id);
        await AddressRepository.RemoveAsync(res);
        return new ResponseModelBase(res);
    }
    
    [HttpGet]
    public async Task<ResponseModelBase> GetByIdAsync(long id)
    {
        var resEntity =  await AddressRepository.GetByIdAsync(id);
        var dto = new AddressDto
        {
            Id = resEntity.Id,
            FullAddress = resEntity.FullAddress,
            Latitude = resEntity.Latitude,
            Longitude = resEntity.Longitude,
            City = resEntity.City,
            Region = resEntity.Region,
            PostalCode = resEntity.PostalCode,
            ClientId = resEntity.ClientId
        };
        return new ResponseModelBase(dto);
    }
    
    [HttpGet]
    public async Task<ResponseModelBase> GetAddressByClientId(long clientId)
    {
        var resEntity =   AddressRepository.GetAllAsQueryable().FirstOrDefault(address => address.ClientId==clientId );
        if (resEntity==null)
            return new ResponseModelBase("Address not found",HttpStatusCode.NotFound);
        
        var dto = new AddressDto
        {
            Id = resEntity.Id,
            FullAddress = resEntity.FullAddress,
            Latitude = resEntity.Latitude,
            Longitude = resEntity.Longitude,
            City = resEntity.City,
            Region = resEntity.Region,
            PostalCode = resEntity.PostalCode,
            ClientId = resEntity.ClientId
        };
        return new ResponseModelBase(dto);
    }
    
    [HttpGet]
    public async Task<ResponseModelBase> GetAllAsync()
    {
        var res =   AddressRepository.GetAllAsQueryable().ToList();
        List<AddressDto> dtos = new List<AddressDto>();
        foreach (Address model in res)
        {
            dtos.Add(new AddressDto
            {
                Id = model.Id,
                FullAddress =model.FullAddress,
                Latitude = model.Latitude,
                Longitude = model.Longitude,
                City = model.City,
                Region = model.Region,
                PostalCode = model.PostalCode,
                ClientId = model.ClientId
            });
        }
        
        return new ResponseModelBase(dtos);
    }  
}