using DatabaseBroker.Repositories.Common;
using Entity.Attributes;
using Entity.Models.Promo;

namespace DatabaseBroker.Repositories.PromoRepositories;
[Injectable]
public class PromoRepository : RepositoryBase<Promo,long>, IPromoRepository
{
    public PromoRepository(DataContext dbContext) : base(dbContext)
    {
    }
}