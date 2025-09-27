using DatabaseBroker.Repositories.Common;
using Entity.Attributes;
using Entity.Models.Product.Tags;
using Microsoft.EntityFrameworkCore;

namespace DatabaseBroker.Repositories.Tags.HouseHoldProductTagsRepository;
[Injectable]
public class HouseHoldProductTagsRepository : RepositoryBase<HouseholdProductTags, long>, IHouseHoldProductTagsRepository
{
    public HouseHoldProductTagsRepository(DataContext dbContext) : base(dbContext)
    {
    }
}