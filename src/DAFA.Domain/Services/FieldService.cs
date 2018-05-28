using DAFA.Domain.Entities;
using DAFA.Domain.Interfaces.Repository;
using DAFA.Domain.Interfaces.Repository.ReadOnly;
using DAFA.Domain.Interfaces.Services;
using DAFA.Domain.Validation.Field;
using DAFA.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DAFA.Domain.Services
{
    public class FieldService : BaseService<Field>, IFieldService
    {
        private readonly IFieldRepository fieldRepository;
        private readonly IFieldReadOnlyRepository fieldReadOnlyRepository;

        public FieldService(IFieldRepository fieldRepository, IFieldReadOnlyRepository fieldReadOnlyRepository)
            : base(fieldRepository, fieldReadOnlyRepository)
        {
            this.fieldRepository = fieldRepository;
            this.fieldReadOnlyRepository = fieldReadOnlyRepository;
        }

        public IEnumerable<Field> Find(Expression<Func<Field, bool>> predicate)
        {
            return fieldReadOnlyRepository.Find(predicate);
        }

        public IEnumerable<Field> GetActiveByClient(Guid clientId)
        {
            return fieldReadOnlyRepository.GetActiveByClient(clientId);
        }

        public IEnumerable<Field> GetAllByClient(Guid clientId)
        {
            return fieldReadOnlyRepository.GetAllByClient(clientId);
        }

        public new ValidationResult Add(Field field)
        {
            var validationResult = new ValidationResult();

            if (!field.IsValid())
            {
                validationResult.AddError(field.ValidationResult);
                return validationResult;
            }

            var validator = new FieldIsVerifiedForRegistration();
            var validationService = validator.Validate(field);
            if (!validationService.IsValid)
            {
                validationResult.AddError(field.ValidationResult);
                return validationResult;
            }

            fieldRepository.Add(field);

            return validationResult;
        }

        public new ValidationResult Update(Field field)
        {
            var validationResult = new ValidationResult();

            if (!field.IsValid())
            {
                validationResult.AddError(field.ValidationResult);
                return validationResult;
            }

            var validator = new FieldIsVerifiedForRegistration();
            var validationService = validator.Validate(field);
            if (!validationService.IsValid)
            {
                validationResult.AddError(field.ValidationResult);
                return validationResult;
            }

            fieldRepository.Update(field);

            return validationResult;
        }
    }
}
