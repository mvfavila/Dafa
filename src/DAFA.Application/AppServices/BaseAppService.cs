using DAFA.Application.Interfaces;
using DAFA.Application.Validation;
using DAFA.Domain.ValueObjects;
using DAFA.Infra.Data.Context;
using DAFA.Infra.Data.Interfaces;
using DAFA.Infra.Data.UoW;

namespace DAFA.Application.AppServices
{
    public class BaseAppService<TContext> : IBaseAppService<TContext> where TContext : IDbContext, new()
    {
        private readonly IUnitOfWork<TContext> unitOfWork = new UnitOfWork<TContext>(new ContextManager());

        //public BaseAppService(IUnitOfWork<TContext> unitOfWork)
        //{
        //    this.unitOfWork = unitOfWork;
        //}

        public void BeginTransaction()
        {
            unitOfWork.BeginTransaction();
        }

        public virtual void Commit()
        {
            unitOfWork.SaveChanges();
        }

        protected static ValidationAppResult FromDomainToApplicationResult(ValidationResult result)
        {
            var validationAppResult = new ValidationAppResult();

            foreach (var validationError in result.Errors)
            {
                validationAppResult.Errors.Add(new ValidationAppError(validationError.Message));
            }
            validationAppResult.IsValid = result.IsValid;

            return validationAppResult;
        }
    }
}
