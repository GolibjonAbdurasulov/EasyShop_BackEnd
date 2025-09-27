using DatabaseBroker.Repositories.Common;
using Entity.Models.Translation;

namespace DatabaseBroker.Repositories.TranslationRepository;

public interface ITranslationsRepository : IRepositoryBase<Translation,long>
{
    
}