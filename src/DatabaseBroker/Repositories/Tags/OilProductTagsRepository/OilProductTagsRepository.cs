using DatabaseBroker.Repositories.Common;
using Entity.Models.Product.Tags;
using Microsoft.EntityFrameworkCore;

namespace DatabaseBroker.Repositories.Tags.OilProductTagsRepository;

public class OilProductTagsRepository : RepositoryBase<OilProductTags, long>, IOilProductTagsRepository
{
    public OilProductTagsRepository(DataContext dbContext) : base(dbContext)
    {
    }
}