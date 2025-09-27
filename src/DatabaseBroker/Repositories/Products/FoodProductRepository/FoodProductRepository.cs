using DatabaseBroker.Repositories.Common;
using Entity.Attributes;
using Entity.Models.Product.Products;
using Microsoft.EntityFrameworkCore;

namespace DatabaseBroker.Repositories.Products.FoodProductRepository;
[Injectable]
public class FoodProductRepository : RepositoryBase<FoodProducts,long>, IFoodProductRepository
{
    public FoodProductRepository(DataContext dbContext) : base(dbContext)
    {
    }
}