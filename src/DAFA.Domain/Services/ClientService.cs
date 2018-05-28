using DAFA.Domain.Entities;
using DAFA.Domain.Interfaces.Repository;
using DAFA.Domain.Interfaces.Repository.ReadOnly;
using DAFA.Domain.Interfaces.Services;
using DAFA.Domain.Validation.Client;
using DAFA.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DAFA.Domain.Services
{
    public class ClientService : BaseService<Client>, IClientService
    {
        private readonly IClientRepository clientRepository;
        private readonly IClientReadOnlyRepository clientReadOnlyRepository;

        public ClientService(
            IClientRepository clientRepository,
            IClientReadOnlyRepository clientReadOnlyRepository)
            : base(clientRepository, clientReadOnlyRepository)
        {
            this.clientRepository = clientRepository;
            this.clientReadOnlyRepository = clientReadOnlyRepository;
        }

        public IEnumerable<Client> Find(Expression<Func<Client, bool>> predicate)
        {
            return clientReadOnlyRepository.Find(predicate);
        }

        public IEnumerable<Client> GetActive()
        {
            return clientReadOnlyRepository.GetActive();
        }

        public new IEnumerable<Client> GetAll()
        {
            return clientReadOnlyRepository.GetAll();
        }

        public new ValidationResult Add(Client client)
        {
            var validationResult = new ValidationResult();

            if (!client.IsValid())
            {
                validationResult.AddError(client.ValidationResult);
                return validationResult;
            }

            var validator = new ClientIsVerifiedForRegistration();
            var validationService = validator.Validate(client);
            if (!validationService.IsValid)
            {
                validationResult.AddError(client.ValidationResult);
                return validationResult;
            }

            clientRepository.Add(client);

            return validationResult;
        }

        public new ValidationResult Update(Client client)
        {
            var validationResult = new ValidationResult();

            if (!client.IsValid())
            {
                validationResult.AddError(client.ValidationResult);
                return validationResult;
            }

            var validator = new ClientIsVerifiedForRegistration();
            var validationService = validator.Validate(client);
            if (!validationService.IsValid)
            {
                validationResult.AddError(client.ValidationResult);
                return validationResult;
            }

            clientRepository.Update(client);

            return validationResult;
        }
    }
}
