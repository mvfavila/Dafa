using System;
using System.Collections.Generic;
using DAFA.Domain.Entities;
using DAFA.Domain.Interfaces.Repository;
using DAFA.Domain.Interfaces.Repository.ReadOnly;
using DAFA.Domain.Interfaces.Services;

namespace DAFA.Domain.Services
{
    public class EventWarningService : BaseService<EventWarning>, IEventWarningService
    {
        private readonly IEventWarningRepository eventWarningRepository;
        private readonly IEventWarningReadOnlyRepository eventWarningReadOnlyRepository;

        public EventWarningService(
            IEventWarningRepository eventWarningRepository,
            IEventWarningReadOnlyRepository eventWarningReadOnlyRepository)
            : base(eventWarningRepository, eventWarningReadOnlyRepository)
        {
            this.eventWarningRepository = eventWarningRepository;
            this.eventWarningReadOnlyRepository = eventWarningReadOnlyRepository;
        }

        public IEnumerable<EventWarning> GetUnsolved()
        {
            return eventWarningReadOnlyRepository.GetUnsolved();
        }

        public IEnumerable<EventWarning> GetUnsolvedByClient(Guid id)
        {
            return eventWarningReadOnlyRepository.GetUnsolvedByClient(id);
        }
    }
}
