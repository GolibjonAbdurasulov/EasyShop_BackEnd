using System.Net;
using API.Common;
using Microsoft.AspNetCore.Mvc;
using Services.Dtos.PromoDtos;
using Services.Interfaces;

namespace API.Controllers.PromoControllers;
[ApiController]
[Route("{controller}/[action]")]
public class PromoController : ControllerBase
{
    private IPromoService _promoService;

    public PromoController(IPromoService promoService)
    {
        this._promoService = promoService;
    }
    [HttpPost]
    public async Task<ResponseModelBase> CreatePromo(PromoCreationDto dto)
    {
        var res=await _promoService.CreatePromoAsync(dto);
        return new ResponseModelBase(res,HttpStatusCode.OK);
    }

    public async Task<ResponseModelBase> UpdatePromo(PromoUpdateDto dto)
    {
        var res = await _promoService.UpdatePromoAsync(dto);
        return new ResponseModelBase(res,HttpStatusCode.OK);
    }

    public async Task<ResponseModelBase> DeletePromo(long id)
    {
        var res =await _promoService.DeletePromoAsync(id);
        return new ResponseModelBase(res,HttpStatusCode.OK);
    }

    public async Task<ResponseModelBase> GetPromo(long id)
    {
        var res = await _promoService.GetPromoByIdAsync(id);
        return new ResponseModelBase(res,HttpStatusCode.OK);
    }

    public async Task<ResponseModelBase> GetAllPromos()
    {
        var res = await _promoService.GetAllPromoAsync();
        return new ResponseModelBase(res,HttpStatusCode.OK);
    }
}