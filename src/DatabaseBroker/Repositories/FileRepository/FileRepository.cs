using DatabaseBroker.Repositories.Common;
using Entity.Attributes;
using Entity.Models.File;
namespace DatabaseBroker.Repositories.FileRepository;
[Injectable]
public class FileRepository : RepositoryBase<FileModel,Guid>, IFileRepository
{
    public FileRepository(DataContext dbContext) : base(dbContext)
    {
    }
}