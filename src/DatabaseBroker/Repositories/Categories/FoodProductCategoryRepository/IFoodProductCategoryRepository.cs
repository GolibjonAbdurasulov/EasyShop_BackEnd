using DatabaseBroker.Repositories.Common;
using Entity.Models.Product.Categories;

namespace DatabaseBroker.Repositories.Categories.FoodProductCategoryRepository;

public interface IFoodProductCategoryRepository : IRepositoryBase<FoodProductCategory,long>
{
    
}