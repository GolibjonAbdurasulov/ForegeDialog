using DatabaseBroker.Repositories.Common;
using Entity.Attributes;
using Entity.Models.News;

namespace DatabaseBroker.Repositories.NewsRepository;
[Injectable]
public class NewsRepository(DataContext dbContext) : RepositoryBase<News, long>(dbContext), INewsRepository;