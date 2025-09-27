using System;
using DatabaseBroker.Repositories.Common;
using Entity.Models.File;

namespace DatabaseBroker.Repositories.FileRepository;

public interface IFileRepository : IRepositoryBase<FileModel,Guid>
{
    
}