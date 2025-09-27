using DatabaseBroker.Repositories.Common;
using Entity.Attributes;
using Entity.Models.Product.Tags;
using Microsoft.EntityFrameworkCore;

namespace DatabaseBroker.Repositories.Tags.FoodProductTagsRepository;
[Injectable]
public class FoodProductTagsRepository : RepositoryBase<FoodProductTags, long>, IFoodProductTagsRepository
{
    public FoodProductTagsRepository(DataContext dbContext) : base(dbContext)
    {
    }
}