using DatabaseBroker.Repositories.Common;
using Entity.Models.News;

namespace DatabaseBroker.Repositories.NewsRepository;

public interface INewsRepository : IRepositoryBase<News,long>
{
    
}