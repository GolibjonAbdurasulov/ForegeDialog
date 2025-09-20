using DatabaseBroker.Repositories.Common;
using Entity.Attributes;
using Entity.Models;

namespace DatabaseBroker.Repositories.PicturesModelRepository;
[Injectable]
public class PicturesModelRepository : RepositoryBase<PicturesModel,long>, IPicturesModelRepository
{
    public PicturesModelRepository(DataContext dbContext) : base(dbContext)
    {
    }
}