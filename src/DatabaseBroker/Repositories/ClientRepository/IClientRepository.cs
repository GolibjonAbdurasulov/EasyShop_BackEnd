using DatabaseBroker.Repositories.Common;
using Entity.Models;
using Entity.Models.Client;

namespace DatabaseBroker.Repositories.ClientRepository;

public interface IClientRepository : IRepositoryBase<Client,long>
{
    
}