using DatabaseBroker.Repositories.Common;
using Entity.Attributes;
using Entity.Models.Product.Products;
using Microsoft.EntityFrameworkCore;

namespace DatabaseBroker.Repositories.Products.OilProductsRepository;
[Injectable]
public class OilProductsRepository : RepositoryBase<OilProducts, long>, IOilProductsRepository
{
    public OilProductsRepository(DataContext dbContext) : base(dbContext)
    {
    }
}