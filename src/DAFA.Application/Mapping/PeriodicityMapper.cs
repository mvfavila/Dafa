using System;
using System.Collections.Generic;
using DAFA.Application.ViewModels;
using DAFA.Domain.Entities;
using DAFA.Domain.ValueObjects;

namespace DAFA.Application.Mapping
{
    public static class PeriodicityMapper
    {
        // PeriodicityViewModel

        internal static Periodicity FromViewModelToDomain(PeriodicityViewModel periodicityViewModel)
        {
            return Periodicity.FactoryMap(
                periodicityViewModel.PeriodicityId,
                periodicityViewModel.Description,
                periodicityViewModel.Quantity,
                (Unit)periodicityViewModel.Unit,
                periodicityViewModel.Active);
        }

        internal static PeriodicityViewModel FromDomainToViewModel(Periodicity periodicity)
        {
            return new PeriodicityViewModel
            {
                PeriodicityId = periodicity.PeriodicityId,
                Description = periodicity.Description,
                Quantity = periodicity.Quantity,
                Unit = (byte)periodicity.Unit,
                Active = periodicity.Active
            };
        }

        internal static IEnumerable<PeriodicityViewModel> FromDomainToViewModel(IEnumerable<Periodicity> periodicities)
        {
            var result = new List<PeriodicityViewModel>();
            foreach (var periodicity in periodicities)
            {
                result.Add(FromDomainToViewModel(periodicity));
            }
            return result;
        }

        // UnitViewModel

        internal static UnitViewModel FromDomainToViewModel(Unit unit)
        {
            return new UnitViewModel
            {
                Description = unit.ToString(),
                Value = (byte)unit
            };
        }
    }
}
