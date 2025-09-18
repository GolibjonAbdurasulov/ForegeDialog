using DatabaseBroker.Repositories.Common;
using Entity.Attributes;
using Entity.Models;
using Microsoft.EntityFrameworkCore;

namespace DatabaseBroker.Repositories.ImageCategoryRepository;
[Injectable]
public class ImageCategoryRepository : RepositoryBase<ImageCategory, long>, IImageCategoryRepository
{
    public ImageCategoryRepository(DataContext dbContext) : base(dbContext)
    {
    }
}