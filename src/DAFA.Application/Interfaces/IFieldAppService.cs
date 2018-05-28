using DAFA.Application.Validation;
using DAFA.Application.ViewModels;
using System;
using System.Collections.Generic;

namespace DAFA.Application.Interfaces
{
    public interface IFieldAppService
    {
        IEnumerable<FieldViewModel> GetActiveByClient(Guid clientId);

        IEnumerable<FieldViewModel> GetAllByClient(Guid clientId);

        FieldViewModel GetById(Guid id);

        ValidationAppResult Add(FieldViewModel fieldViewModel);

        ValidationAppResult Update(FieldViewModel fieldViewModel);

        void Remove(Guid id);
    }
}
