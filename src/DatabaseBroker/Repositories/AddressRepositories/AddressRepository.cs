using DatabaseBroker.Repositories.Common;
using Entity.Attributes;
using Entity.Models.Order;
using Microsoft.EntityFrameworkCore;

namespace DatabaseBroker.Repositories.AddressRepositories;
[Injectable]
public class AddressRepository : RepositoryBase<Address,long>, IAddressRepository
{
    public AddressRepository(DataContext dbContext) : base(dbContext)
    {
    }
}