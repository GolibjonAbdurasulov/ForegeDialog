using DatabaseBroker.Repositories.Common;
using Entity.Attributes;
using Entity.Models;

namespace DatabaseBroker.Repositories.OurPartnersRepository;
[Injectable]
public class OurPartnersRepository(DataContext dbContext)
    : RepositoryBase<OurPartners, long>(dbContext), IOurPartnersRepository;