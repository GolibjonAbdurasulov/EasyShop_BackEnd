using System.Threading.Tasks;
using Entity.Models.Translation;
using Services.Dtos;

namespace Services.Interfaces;

public interface ITranslationService
{
    public ValueTask<object> ExportAsync();
    public ValueTask<Translation> CreateAsync(TranslationsDto translationsDto);
    public ValueTask<Translation> GetByIdAsyc(long id);
    public ValueTask<Translation> UpdateAsync(TranslationsDto translationsDto);
    public ValueTask<Translation> RemoveAsync(long id);
}