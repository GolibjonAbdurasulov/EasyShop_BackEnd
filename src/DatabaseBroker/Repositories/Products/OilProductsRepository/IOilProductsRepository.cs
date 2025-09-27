using DatabaseBroker.Repositories.Common;
using Entity.Models.Product.Products;

namespace DatabaseBroker.Repositories.Products.OilProductsRepository;

public interface IOilProductsRepository : IRepositoryBase<OilProducts,long>
{
    
}