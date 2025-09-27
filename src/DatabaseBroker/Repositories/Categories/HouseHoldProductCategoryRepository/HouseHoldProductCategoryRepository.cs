using DatabaseBroker.Repositories.Common;
using Entity.Attributes;
using Entity.Models.Product.Categories;
using Microsoft.EntityFrameworkCore;

namespace DatabaseBroker.Repositories.Categories.HouseHoldProductCategoryRepository;
[Injectable]
public class HouseHoldProductCategoryRepository : RepositoryBase<HouseholdProductCategory,long>
{
    public HouseHoldProductCategoryRepository(DataContext dbContext) : base(dbContext)
    {
    }
}