using DAFA.Application.Interfaces;
using DAFA.Application.Validation;
using DAFA.Application.ViewModels;
using DAFA.Domain.Interfaces.Services;
using DAFA.Infra.Data.Context;
using System;
using System.Collections.Generic;

namespace DAFA.Application.AppServices
{
    public class ClientAppService : BaseAppService<DAFAContext>, IClientAppService
    {
        private readonly IClientService clientService;
        private readonly IFieldService fieldService;

        public ClientAppService(IClientService clientService, IFieldService fieldService)
        {
            this.clientService = clientService;
            this.fieldService = fieldService;
        }

        public ValidationAppResult Add(ClientViewModel clientViewModel)
        {
            var clientObj = Mapping.ClientMapper.FromViewModelToDomain(clientViewModel);

            var result = clientService.Add(clientObj);

            Commit();

            return FromDomainToApplicationResult(result);
        }

        public void Dispose()
        {
            clientService.Dispose();
            GC.SuppressFinalize(this);
        }

        public IEnumerable<ClientViewModel> GetActive()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ClientViewModel> GetAll()
        {
            var clients = clientService.GetAll();

            return Mapping.ClientMapper.FromDomainToViewModel(clients);
        }

        public ClientFieldViewModel GetById(Guid id)
        {
            var clientObj = clientService.GetById(id);

            var fields = fieldService.GetAllByClient(id);

            return Mapping.ClientMapper.FromDomainToViewModel(clientObj, fields);
        }

        public void Remove(Guid id)
        {
            throw new NotImplementedException();
        }

        public ValidationAppResult Update(ClientViewModel clientViewModel)
        {
            var clientObj = Mapping.ClientMapper.FromViewModelToDomain(clientViewModel);

            BeginTransaction();

            var result = clientService.Update(clientObj);

            if (result.IsValid)
                Commit();

            return FromDomainToApplicationResult(result);
        }
    }
}
