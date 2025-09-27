using DatabaseBroker.Repositories.Common;
using Entity.Attributes;
using Entity.Models.Product.Products;
using Microsoft.EntityFrameworkCore;

namespace DatabaseBroker.Repositories.Products.HouseHoldProductsRepository;
[Injectable]
public class HouseHoldProductsRepository : RepositoryBase<HouseholdProducts,long>, IHouseHoldProductsRepository
{
    public HouseHoldProductsRepository(DataContext dbContext) : base(dbContext)
    {
    }
}