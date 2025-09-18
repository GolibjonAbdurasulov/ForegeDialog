using DatabaseBroker.Repositories.Common;
using Entity.Attributes;
using Entity.Models;
using Microsoft.EntityFrameworkCore;

namespace DatabaseBroker.PicturesModelRepository;
[Injectable]
public class PicturesModelRepository : RepositoryBase<PicturesModel,long>, IPicturesModelRepository
{
    public PicturesModelRepository(DataContext dbContext) : base(dbContext)
    {
    }
}