using DatabaseBroker.Repositories.Common;
using Entity.Attributes;
using Entity.Models.Product.Tags;
using Microsoft.EntityFrameworkCore;

namespace DatabaseBroker.Repositories.Tags.OilProductTagsRepository;
[Injectable]
public class OilProductTagsRepository : RepositoryBase<OilProductTags, long>, IOilProductTagsRepository
{
    public OilProductTagsRepository(DataContext dbContext) : base(dbContext)
    {
    }
}