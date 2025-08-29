using DatabaseBroker.Repositories.Common;
using Entity.Attributes;
using Entity.Models;
using Microsoft.EntityFrameworkCore;

namespace DatabaseBroker.Repositories.ResourceRepository;
[Injectable]
public class ResourceRepository(DataContext dbContext) : 
    RepositoryBase<Resource, long>(dbContext), IResourceRepository;