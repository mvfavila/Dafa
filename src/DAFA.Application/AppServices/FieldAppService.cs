using DAFA.Application.Interfaces;
using DAFA.Application.Validation;
using DAFA.Application.ViewModels;
using DAFA.Domain.Interfaces.Services;
using DAFA.Infra.Data.Context;
using System;
using System.Collections.Generic;

namespace DAFA.Application.AppServices
{
    public class FieldAppService : BaseAppService<DAFAContext>, IFieldAppService
    {
        private readonly IFieldService fieldService;

        public FieldAppService(IFieldService fieldService)
        {
            this.fieldService = fieldService;
        }

        public ValidationAppResult Add(FieldViewModel fieldViewModel)
        {
            var fieldObj = Mapping.FieldMapper.FromViewModelToDomain(fieldViewModel);

            var result = fieldService.Add(fieldObj);

            Commit();

            return FromDomainToApplicationResult(result);
        }

        public void Dispose()
        {
            fieldService.Dispose();
            GC.SuppressFinalize(this);
        }

        public IEnumerable<FieldViewModel> GetActiveByClient(Guid clientId)
        {
            var fields = fieldService.GetActiveByClient(clientId);

            return Mapping.FieldMapper.FromDomainToViewModel(fields);
        }

        public IEnumerable<FieldViewModel> GetAllByClient(Guid clientId)
        {
            var fields = fieldService.GetAllByClient(clientId);

            return Mapping.FieldMapper.FromDomainToViewModel(fields);
        }

        public FieldViewModel GetById(Guid id)
        {
            var fieldObj = fieldService.GetById(id);

            return Mapping.FieldMapper.FromDomainToViewModel(fieldObj);
        }

        public void Remove(Guid id)
        {
            throw new NotImplementedException();
        }

        public ValidationAppResult Update(FieldViewModel fieldViewModel)
        {
            var fieldObj = Mapping.FieldMapper.FromViewModelToDomain(fieldViewModel);

            BeginTransaction();

            var result = fieldService.Update(fieldObj);

            if (result.IsValid)
                Commit();

            return FromDomainToApplicationResult(result);
        }
    }
}
