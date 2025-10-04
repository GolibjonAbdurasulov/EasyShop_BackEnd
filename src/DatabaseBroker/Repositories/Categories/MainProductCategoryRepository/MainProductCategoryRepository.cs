using DatabaseBroker.Repositories.Common;
using Entity.Attributes;
using Entity.Models.Product.Categories;

namespace DatabaseBroker.Repositories.Categories.MainProductCategoryRepository;
[Injectable]
public class MainProductCategoryRepository : RepositoryBase<MainProductCategories, long>,IMainProductCategoryRepository
{
    public MainProductCategoryRepository(DataContext dbContext) : base(dbContext)
    {
    }
}