using DAFA.Application.Interfaces;
using DAFA.Domain.Interfaces.Services;
using DAFA.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAFA.Application.Validation;
using DAFA.Application.ViewModels;
using DAFA.Domain.ValueObjects;

namespace DAFA.Application.AppServices
{
    public class PeriodicityAppService : BaseAppService<DAFAContext>, IPeriodicityAppService
    {
        private readonly IPeriodicityService periodicityService;

        public PeriodicityAppService(IPeriodicityService periodicityService)
        {
            this.periodicityService = periodicityService;
        }

        public ValidationAppResult Add(PeriodicityViewModel periodicityViewModel)
        {
            var periodicity = Mapping.PeriodicityMapper.FromViewModelToDomain(periodicityViewModel);

            var result = periodicityService.Add(periodicity);

            Commit();

            return FromDomainToApplicationResult(result);
        }

        public void Dispose()
        {
            periodicityService.Dispose();
            GC.SuppressFinalize(this);
        }

        public IEnumerable<PeriodicityViewModel> GetActive()
        {
            var periodicities = periodicityService.GetActive();

            return Mapping.PeriodicityMapper.FromDomainToViewModel(periodicities);
        }

        public IEnumerable<PeriodicityViewModel> GetAll()
        {
            var periodicities = periodicityService.GetAll();

            return Mapping.PeriodicityMapper.FromDomainToViewModel(periodicities);
        }

        public PeriodicityViewModel GetById(Guid id)
        {
            var periodicity = periodicityService.GetById(id);

            return Mapping.PeriodicityMapper.FromDomainToViewModel(periodicity);
        }

        public IEnumerable<UnitViewModel> GetUnits()
        {
            var units = new List<UnitViewModel>();
            foreach (var item in Enum.GetValues(typeof(Unit)).Cast<Unit>())
            {
                var unit = Mapping.PeriodicityMapper.FromDomainToViewModel(item);
                units.Add(unit);
            }
            return units;
        }

        public void Remove(Guid id)
        {
            throw new NotImplementedException();
        }

        public ValidationAppResult Update(PeriodicityViewModel periodicityViewModel)
        {
            var periodicity = Mapping.PeriodicityMapper.FromViewModelToDomain(periodicityViewModel);

            BeginTransaction();

            var result = periodicityService.Update(periodicity);

            if (result.IsValid)
                Commit();

            return FromDomainToApplicationResult(result);
        }
    }
}
