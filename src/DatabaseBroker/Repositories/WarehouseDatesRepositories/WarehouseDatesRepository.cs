using DatabaseBroker.Repositories.Common;
using Entity.Attributes;
using Entity.Models.Product;

namespace DatabaseBroker.Repositories.WarehouseDatesRepositories;
[Injectable]
public class WarehouseDatesRepository : RepositoryBase<WarehouseDates,long>, IWarehouseDatesRepository
{
    public WarehouseDatesRepository(DataContext dbContext) : base(dbContext)
    {
    }
}