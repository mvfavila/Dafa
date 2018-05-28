using DAFA.Domain.Interfaces.Specification;
using DAFA.Domain.Interfaces.Validation;

namespace DAFA.Domain.Validation
{
    public class Rule<TEntity> : IRule<TEntity>
    {
        private readonly ISpecification<TEntity> specificationRule;
        public string ErrorMessage { get; private set; }

        public Rule(ISpecification<TEntity> rule, string errorMessage)
        {
            specificationRule = rule;
            ErrorMessage = errorMessage;
        }

        public bool Validate(TEntity entity)
        {
            return specificationRule.IsSatisfiedBy(entity);
        }
    }
}
