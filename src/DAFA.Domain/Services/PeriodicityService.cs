using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using DAFA.Domain.Entities;
using DAFA.Domain.Interfaces.Repository;
using DAFA.Domain.Interfaces.Repository.ReadOnly;
using DAFA.Domain.Interfaces.Services;
using DAFA.Domain.ValueObjects;
using DAFA.Domain.Validation.Periodicity;

namespace DAFA.Domain.Services
{
    public class PeriodicityService : BaseService<Periodicity>, IPeriodicityService
    {
        private readonly IPeriodicityRepository periodicityRepository;
        private readonly IPeriodicityReadOnlyRepository periodicityReadOnlyRepository;

        public PeriodicityService(
            IPeriodicityRepository periodicityRepository,
            IPeriodicityReadOnlyRepository periodicityReadOnlyRepository)
            : base(periodicityRepository, periodicityReadOnlyRepository)
        {
            this.periodicityRepository = periodicityRepository;
            this.periodicityReadOnlyRepository = periodicityReadOnlyRepository;
        }

        public IEnumerable<Periodicity> Find(Expression<Func<Periodicity, bool>> predicate)
        {
            return periodicityReadOnlyRepository.Find(predicate);
        }

        public IEnumerable<Periodicity> GetActive()
        {
            return periodicityReadOnlyRepository.GetActive();
        }

        ValidationResult IPeriodicityService.Add(Periodicity periodicity)
        {
            var validationResult = new ValidationResult();

            if (!periodicity.IsValid())
            {
                validationResult.AddError(periodicity.ValidationResult);
                return validationResult;
            }

            var validator = new PeriodicityIsVerifiedForRegistration();
            var validationService = validator.Validate(periodicity);
            if (!validationService.IsValid)
            {
                validationResult.AddError(periodicity.ValidationResult);
                return validationResult;
            }

            periodicityRepository.Add(periodicity);

            return validationResult;
        }

        ValidationResult IPeriodicityService.Update(Periodicity periodicity)
        {
            var validationResult = new ValidationResult();

            if (!periodicity.IsValid())
            {
                validationResult.AddError(periodicity.ValidationResult);
                return validationResult;
            }

            var validator = new PeriodicityIsVerifiedForRegistration();
            var validationService = validator.Validate(periodicity);
            if (!validationService.IsValid)
            {
                validationResult.AddError(periodicity.ValidationResult);
                return validationResult;
            }

            periodicityRepository.Update(periodicity);

            return validationResult;
        }
    }
}
