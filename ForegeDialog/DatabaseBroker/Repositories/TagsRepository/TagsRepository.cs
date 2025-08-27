using DatabaseBroker.Repositories.Common;
using Entity.Attributes;
using Entity.Models;
using Microsoft.EntityFrameworkCore;

namespace DatabaseBroker.Repositories.TagsRepository;
[Injectable]
public class TagsRepository(DataContext dbContext) : RepositoryBase<Tags, long>(dbContext), ITagsRepository;