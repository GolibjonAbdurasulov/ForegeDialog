using DatabaseBroker.Repositories.Common;
using Entity.Attributes;
using Entity.Models;

namespace DatabaseBroker.Repositories.ResourceCategoryRepository;
[Injectable]
public class ResourceCategoryRepository(DataContext dbContext)
    : RepositoryBase<ResourceCategory, long>(dbContext), IResourceCategoryRepository;