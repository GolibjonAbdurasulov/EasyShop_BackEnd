using DatabaseBroker.Repositories.Common;
using Entity.Attributes;
using Entity.Models.Product.Tags;
using Microsoft.EntityFrameworkCore;

namespace DatabaseBroker.Repositories.Tags.WaterAndDrinkTagsRepository;
[Injectable]
public class WaterAndDrinkTagsRepository : RepositoryBase<WaterAndDrinksTags, long>, IWaterAndDrinkTagsRepository
{
    public WaterAndDrinkTagsRepository(DataContext dbContext) : base(dbContext)
    {
    }
}