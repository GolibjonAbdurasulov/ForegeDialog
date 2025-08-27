using DatabaseBroker.Repositories.Common;
using Entity.Attributes;
using Entity.Models;
using Microsoft.EntityFrameworkCore;

namespace DatabaseBroker.Repositories.OurCategoriesRepository;
[Injectable]
public class OurCategoriesRepository(DataContext dbContext) : 
    RepositoryBase<OurCategories, long>(dbContext),IOurCategoriesRepository;