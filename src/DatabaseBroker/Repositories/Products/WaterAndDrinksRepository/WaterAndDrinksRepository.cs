using DatabaseBroker.Repositories.Common;
using Entity.Attributes;
using Entity.Models.Product.Products;
using Microsoft.EntityFrameworkCore;

namespace DatabaseBroker.Repositories.Products.WaterAndDrinksRepository;
[Injectable]
public class WaterAndDrinksRepository : RepositoryBase<WaterAndDrinks, long>, IWaterAndDrinksRepository
{
    public WaterAndDrinksRepository(DataContext dbContext) : base(dbContext)
    {
    }
}