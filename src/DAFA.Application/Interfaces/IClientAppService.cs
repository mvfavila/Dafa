using DAFA.Application.Validation;
using DAFA.Application.ViewModels;
using System;
using System.Collections.Generic;

namespace DAFA.Application.Interfaces
{
    public interface IClientAppService : IDisposable
    {
        IEnumerable<ClientViewModel> GetActive();

        // TODO: paging should be added to method.
        IEnumerable<ClientViewModel> GetAll();

        ClientFieldViewModel GetById(Guid id);

        ValidationAppResult Add(ClientViewModel clientViewModel);

        ValidationAppResult Update(ClientViewModel clientViewModel);

        void Remove(Guid id);
    }
}
