using DatabaseBroker.Repositories.Common;
using Entity.Attributes;
using Entity.Models;
using Microsoft.EntityFrameworkCore;

namespace DatabaseBroker.Repositories.StatisticsRepository;
[Injectable]
public class StatisticsRepository(DataContext dbContext)
    : RepositoryBase<Statistics, long>(dbContext), IStatisticsRepository;