using API.Common;
using DatabaseBroker.Extensions;
using DatabaseBroker.Repositories.TranslationRepository;
using Entity.Models.Common;
using Entity.Models.Translation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Dtos;
using Services.Interfaces;

namespace API.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class TranslationsController : ControllerBase
{
    private ITranslationService _translationService;
    private readonly ITranslationsRepository _translationsRepository;

    public TranslationsController(ITranslationService translationService,
        ITranslationsRepository translationsRepository)
    {
        _translationService = translationService;
        _translationsRepository = translationsRepository;
    }

    // [HttpGet]
    // [Authorize]
    // public async Task<ResponseModelBase> AddTest()
    // {
    //     string s =
    //         "{\"settings\": \"Sozlamalar\",\n  \"id\": \"ID\",\n  \"total\": \"Umumiy\",\n  \"user\": \"Foydalanuvchi\",\n  \"userRoles\": \"Foydalanuvchi rollari\",\n  \"userStatuses\": \"Foydalanuvchi statuslari\",\n  \"references\": \"Yordamchilar\",\n  \"actions\": \"Amallar\",\n  \"name\": \"Nomi\",\n  \"code\": \"Kod\",\n  \"save\": \"Saqlash\",\n  \"sooguList\": \"Soogular\",\n  \"main\": \"Asosiy\",\n  \"yes\": \"Ha\",\n  \"no\": \"Yo'q\",\n  \"users\": \"Foydalanuvchilar\",\n  \"phoneNumber\": \"Mobil raqam\",\n  \"roleName\": \"Roll nomi\",\n  \"statusName\": \"Holat\",\n  \"tin\": \"INN\",\n  \"companyTin\": \"Tashkilot INN raqami\",\n  \"companyName\": \"Tashkilot nomi\",\n  \"userInformationsNotFound\": \"Foydalanuvchi ma'lumotlari topilmadi\",\n  \"formNotCompleted\": \"Ma'lumotlar to'liq taqdim etilmadi\",\n  \"networkError\": \"Tarmoq xatoligi\",\n  \"deals\": \"Shartnomalar\",\n  \"wrongFormatOf\": \"formati noto'g'ri\",\n  \"userNotFound\": \"Foydalanuvchini topib bo'lmadi\",\n  \"areYouSureTo\": \"Tasdiqlaysizmi?\"}";
    //
    //     var d = JsonSerializer.Deserialize<Dictionary<string, string>>(s);
    //
    //     var tr = d.Select(pair => new Translation()
    //     {
    //         Code = pair.Key,
    //         En = pair.Value,
    //         Uz = pair.Value,
    //         Ru = pair.Value
    //     });
    //
    //     await _translationsRepository.AddRangeAsync(tr.ToArray());
    //
    //     return (await _translationService.ExportAsync(), 200);
    // }


    [HttpGet]
    [Authorize]
    public async Task<ResponseModelBase> Export()
    {
        return (await _translationService.ExportAsync(), 200);
    }

    [HttpGet]
    [Authorize]
    [ProducesResponseType(typeof(ResponseModelBase<IEnumerable<Translation>>), 200)]
    public async Task<ResponseModelBase> GetAll([FromQuery] TermModelBase term)
    {
        return await _translationsRepository
            .OrderByDescending(x => x.Id)
            .GetByTermsAsync(term);
    }

    [HttpPost()]
    [Authorize]
    public async Task<ResponseModelBase> CreateAsync(TranslationsDto dto)
    {
        var result = await _translationService.CreateAsync(dto);
        return (result, 200);
    }

    [HttpPut()]
    [Authorize]
    public async ValueTask<ResponseModelBase> UpdateAsync(TranslationsDto dto)
    {
        var result = await _translationService.UpdateAsync(dto);
        return (result, 200);
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async ValueTask<ResponseModelBase> DeleteAsync(long id)
    {
        var result = await _translationService.RemoveAsync(id);
        return (result, 200);
    }
}