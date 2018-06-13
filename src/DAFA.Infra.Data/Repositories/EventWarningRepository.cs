using DAFA.Domain.Entities;
using DAFA.Domain.Interfaces.Repository;
using DAFA.Infra.Data.Context;

namespace DAFA.Infra.Data.Repositories
{
    public class EventWarningRepository : BaseRepository<EventWarning, DAFAContext>, IEventWarningRepository
    {
    }
}
