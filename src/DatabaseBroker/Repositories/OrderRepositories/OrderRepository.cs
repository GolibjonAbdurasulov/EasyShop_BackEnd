using DatabaseBroker.Repositories.Common;
using Entity.Attributes;
using Entity.Models.Order;

namespace DatabaseBroker.Repositories.OrderRepositories;
[Injectable]
public class OrderRepository : RepositoryBase<Order,long>, IOrderRepository
{
    public OrderRepository(DataContext dbContext) : base(dbContext)
    {
    }
}