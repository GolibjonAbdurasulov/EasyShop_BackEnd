using System.Net;
using API.Common;
using API.Controllers.ProductsControllers.OilProductsController.Dtos;
using DatabaseBroker.Repositories.UserRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[ApiController]
[Route("[controller]/[action]")]
public class AboutMeController(IUserRepository userRepository) : ControllerBase
{
    private IUserRepository UserRepository { get; set; } = userRepository;

    [HttpGet]
    [Authorize]
    public async Task<ResponseModelBase> GetData(long userId )
    {
      var user = await UserRepository.GetByIdAsync(userId);

      if (user == null)
          return new ResponseModelBase("User not found");


      return new ResponseModelBase(user, HttpStatusCode.OK);
      
    }
    
    
}