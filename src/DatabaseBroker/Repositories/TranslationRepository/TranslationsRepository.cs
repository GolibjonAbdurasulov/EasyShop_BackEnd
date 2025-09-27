using DatabaseBroker.Repositories.Common;
using Entity.Attributes;
using Entity.Models.Translation;

namespace DatabaseBroker.Repositories.TranslationRepository;

[Injectable]
public class TranslationsRepository : RepositoryBase<Translation, long>, ITranslationsRepository
{
    public TranslationsRepository(DataContext dbContext) : base(dbContext)
    {
    }
}