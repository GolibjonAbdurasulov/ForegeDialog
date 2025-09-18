using DatabaseBroker.Repositories.Common;
using Entity.Attributes;
using Entity.Models;

namespace DatabaseBroker.Repositories.ImageModelRepository;
[Injectable]
public class ImageModelRepository : RepositoryBase<ImageModel,long>, IImageModelRepository
{
    public ImageModelRepository(DataContext dbContext) : base(dbContext)
    {
    }
}