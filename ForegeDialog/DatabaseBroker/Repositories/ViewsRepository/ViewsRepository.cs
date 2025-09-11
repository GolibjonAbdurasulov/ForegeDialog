using DatabaseBroker.Repositories.Common;
using Entity.Attributes;
using Entity.Models;

namespace DatabaseBroker.Repositories.ViewsRepository;
[Injectable]
public class ViewsRepository(DataContext dbContext) : RepositoryBase<Views, long>(dbContext), IViewsRepository;