using DatabaseBroker.Repositories.Common;
using Entity.Attributes;
using Entity.Models;
using Microsoft.EntityFrameworkCore;

namespace DatabaseBroker.Repositories.ReferenceToBlogRepository;
[Injectable]
public class ReferenceToBlogRepository : RepositoryBase<ReferenceToBlog,long>, IReferenceToBlogRepository
{
    public ReferenceToBlogRepository(DataContext dbContext) : base(dbContext)
    {
    }
}