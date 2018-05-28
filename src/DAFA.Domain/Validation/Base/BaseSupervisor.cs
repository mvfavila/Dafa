using System.Collections.Generic;
using DAFA.Domain.Interfaces.Validation;
using DAFA.Domain.ValueObjects;

namespace DAFA.Domain.Validation.Base
{
    public abstract class BaseSupervisor<TEntity> : ISupervisor<TEntity> where TEntity : class
    {
        private readonly Dictionary<string, IRule<TEntity>> validations = new Dictionary<string, IRule<TEntity>>();

        protected virtual void AddRule(string ruleName, IRule<TEntity> rule)
        {
            validations.Add(ruleName, rule);
        }

        protected virtual void RemoveRule(string ruleName)
        {
            validations.Remove(ruleName);
        }

        public virtual ValidationResult Validate(TEntity entity)
        {
            var result = new ValidationResult();
            foreach (var x in validations.Keys)
            {
                var rule = validations[x];
                if (!rule.Validate(entity))
                    result.AddError(new ValidationError(rule.ErrorMessage));
            }

            return result;
        }

        protected IRule<TEntity> GetRule(string ruleName)
        {
            validations.TryGetValue(ruleName, out IRule<TEntity> rule);
            return rule;
        }
    }
}
