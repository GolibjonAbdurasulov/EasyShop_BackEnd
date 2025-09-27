using DatabaseBroker.Repositories.Common;
using Entity.Attributes;
using Entity.Models.Product.Categories;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace DatabaseBroker.Repositories.Categories.FoodProductCategoryRepository;
[Injectable]
public class FoodProductCategoryRepository : RepositoryBase<FoodProductCategory,long>, IFoodProductCategoryRepository
{
    public FoodProductCategoryRepository(DataContext dbContext) : base(dbContext)
    {
    }
}