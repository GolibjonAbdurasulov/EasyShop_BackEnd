using DatabaseBroker.Repositories.Common;
using Entity.Models.Users;

namespace DatabaseBroker.Repositories.UserRepository;

public interface IUserRepository : IRepositoryBase<User,long>
{
    
}