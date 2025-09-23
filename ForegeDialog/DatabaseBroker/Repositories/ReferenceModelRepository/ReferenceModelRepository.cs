using DatabaseBroker.Repositories.Common;
using Entity.Attributes;
using Entity.Models;

namespace DatabaseBroker.Repositories.ReferenceModelRepository;
[Injectable]
public class ReferenceModelRepository : RepositoryBase<ReferenceModel,long> , IReferenceModelRepository
{
    public ReferenceModelRepository(DataContext dbContext) : base(dbContext)
    {
    }
}