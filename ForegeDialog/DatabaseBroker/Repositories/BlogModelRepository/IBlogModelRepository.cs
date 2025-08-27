using DatabaseBroker.Repositories.Common;
using Entity.Models.Blog;

namespace DatabaseBroker.Repositories.BlogModelRepository;

public interface IBlogModelRepository : IRepositoryBase<BlogModel,long>
{
    
}