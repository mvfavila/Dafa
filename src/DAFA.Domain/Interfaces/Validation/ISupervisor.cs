using DAFA.Domain.ValueObjects;

namespace DAFA.Domain.Interfaces.Validation
{
    public interface ISupervisor<in TEntity>
    {
        ValidationResult Validate(TEntity entity);
    }
}
