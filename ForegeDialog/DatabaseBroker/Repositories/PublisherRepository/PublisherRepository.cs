using DatabaseBroker.Repositories.Common;
using Entity.Attributes;
using Entity.Models.News;
using Microsoft.EntityFrameworkCore;

namespace DatabaseBroker.Repositories.PublisherRepository;
[Injectable]
public class PublisherRepository(DataContext dbContext)
    : RepositoryBase<Publisher, long>(dbContext), IPublisherRepository;