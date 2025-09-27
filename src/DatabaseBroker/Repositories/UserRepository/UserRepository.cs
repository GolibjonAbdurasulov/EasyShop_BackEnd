using DatabaseBroker.Repositories.Common;
using Entity.Attributes;
using Entity.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace DatabaseBroker.Repositories.UserRepository;
[Injectable]
public class UserRepository : RepositoryBase<User,long> ,IUserRepository
{
    public UserRepository(DataContext dbContext) : base(dbContext)
    {
    }
}