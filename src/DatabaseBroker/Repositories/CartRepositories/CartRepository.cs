using DatabaseBroker.Repositories.Common;
using Entity.Attributes;
using Entity.Models.Order;

namespace DatabaseBroker.Repositories.CartRepositories;
[Injectable]
public class CartRepository : RepositoryBase<Cart,long>, ICartRepository
{
    public CartRepository(DataContext dbContext) : base(dbContext)
    {
    }
}