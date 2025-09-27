using DatabaseBroker.Repositories.Common;
using Entity.Models.Product.Products;

namespace DatabaseBroker.Repositories.Products.FoodProductRepository;

public interface IFoodProductRepository : IRepositoryBase<FoodProducts,long>
{
    
}