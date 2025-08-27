using DatabaseBroker.Repositories.Common;
using Entity.Attributes;
using Entity.Models.Blog;
using Microsoft.EntityFrameworkCore;

namespace DatabaseBroker.Repositories.BlogModelRepository;
[Injectable]
public class BlogModelRepository(DataContext dbContext)
    : RepositoryBase<BlogModel, long>(dbContext), IBlogModelRepository;