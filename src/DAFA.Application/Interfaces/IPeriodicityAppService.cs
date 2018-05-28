using DAFA.Application.Validation;
using DAFA.Application.ViewModels;
using System;
using System.Collections.Generic;

namespace DAFA.Application.Interfaces
{
    public interface IPeriodicityAppService : IDisposable
    {
        IEnumerable<PeriodicityViewModel> GetActive();

        // TODO: paging should be added to method.
        IEnumerable<PeriodicityViewModel> GetAll();

        PeriodicityViewModel GetById(Guid id);

        ValidationAppResult Add(PeriodicityViewModel periodicityViewModel);

        ValidationAppResult Update(PeriodicityViewModel periodicityViewModel);

        void Remove(Guid id);

        IEnumerable<UnitViewModel> GetUnits();
    }
}
