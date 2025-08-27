using DatabaseBroker.Repositories.Common;
using Entity.Attributes;
using Entity.Models;
using Microsoft.EntityFrameworkCore;

namespace DatabaseBroker.Repositories.OurTeamRepository;

[Injectable]
public class OurTeamRepository(DataContext dbContext) : 
    RepositoryBase<OurTeam, long>(dbContext), IOurTeamRepository;