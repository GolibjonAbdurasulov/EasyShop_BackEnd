using System;
using System.Linq;
using System.Threading.Tasks;
using DatabaseBroker.Repositories.TranslationRepository;
using Entity.Attributes;
using Entity.Exceptions;
using Entity.Models.Translation;
using Microsoft.EntityFrameworkCore;
using Services.Dtos;
using Services.Interfaces;

namespace Services.Services;

[Injectable]
public class TranslationService : ITranslationService
{
    private ITranslationsRepository _repository;

    public TranslationService(ITranslationsRepository repository)
    {
        _repository = repository;
    }

    public async ValueTask<object> ExportAsync()
    {
        var tr = await _repository.ToListAsync();

        var en = tr.ToDictionary(x => x.Code, x => x.En);
        var uz = tr.ToDictionary(x => x.Code, x => x.Uz);
        var ru = tr.ToDictionary(x => x.Code, x => x.Ru);
        var ger = tr.ToDictionary(x => x.Code, x => x.Ger);

        return new
        {
            en,
            ru,
            uz,
            ger
        };
    }

    public async ValueTask<Translation> CreateAsync(TranslationsDto translationsDto)
    {
        var resourceCode = new Translation
        {
            Code = translationsDto.Code,
            Uz = translationsDto.Uz,
            Ru = translationsDto.Ru,
            En = translationsDto.En,
            Ger = translationsDto.Ger
        };
        return await _repository.AddAsync(resourceCode);
    }

    public async ValueTask<Translation> GetByIdAsyc(long id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async ValueTask<Translation> UpdateAsync(TranslationsDto translationsDto)
    {
        var resourceCode = _repository.GetAllAsQueryable().ToList()
            .FirstOrDefault(code => code.Code == translationsDto.Code);

        NotFoundException.ThrowIfNull(resourceCode);

        resourceCode!.En = translationsDto.En;
        resourceCode.Uz = translationsDto.Uz;
        resourceCode.Ru = translationsDto.Ru;
        resourceCode.Ger = translationsDto.Ger;

        return await _repository.UpdateAsync(resourceCode);
    }

    public async ValueTask<Translation> RemoveAsync(long id)
    {
        var resourceCode = await GetByIdAsyc(id);
        return await _repository.RemoveAsync(resourceCode);
    }
}