using DatabaseBroker.Repositories.Common;
using Entity.Attributes;
using Entity.Models;
using Entity.Models.Client;
using Microsoft.EntityFrameworkCore;

namespace DatabaseBroker.Repositories.ClientRepository;
[Injectable]
public class ClientRepository(DataContext dbContext) : RepositoryBase<Client, long>(dbContext), IClientRepository;