using DatabaseBroker.Repositories.Common;
using Entity.Models.Order;

namespace DatabaseBroker.Repositories.OrderRepositories;

public interface IOrderRepository : IRepositoryBase<Order,long>
{
    
}