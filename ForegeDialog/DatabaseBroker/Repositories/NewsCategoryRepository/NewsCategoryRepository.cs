using DatabaseBroker.Repositories.Common;
using Entity.Attributes;
using Entity.Models;
using Microsoft.EntityFrameworkCore;

namespace DatabaseBroker.Repositories.NewsCategoryRepository;
[Injectable]
public class NewsCategoryRepository(DataContext dbContext)
    : RepositoryBase<NewsCategory, long>(dbContext), INewsCategoryRespository;