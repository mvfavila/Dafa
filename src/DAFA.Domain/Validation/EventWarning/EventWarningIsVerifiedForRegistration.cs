using DAFA.Domain.Specification.EventWarning;
using DAFA.Domain.Validation.Base;

namespace DAFA.Domain.Validation.EventWarning
{
    public class EventWarningIsVerifiedForRegistration : BaseSupervisor<Entities.EventWarning>
    {
        public EventWarningIsVerifiedForRegistration()
        {
            var isKeyNotNull = new EventWarningtIsKeyNotNull();
            var isValidSolvedState = new EventWarningIsValidSolvedState();
            var isSolvedDateValid = new EventWarningIsSolvedDateValid();
            var isEventValid = new EventWarningIsEventValid();

            base.AddRule("IsKeyNotNull", new Rule<Entities.EventWarning>(isKeyNotNull,
                $"{nameof(Entities.EventWarning.EventWarningId)} is required"));

            base.AddRule("IsValidSolvedState", new Rule<Entities.EventWarning>(isValidSolvedState,
                $@"The {nameof(Entities.EventWarning)} has to have either {nameof(Entities.EventWarning.Solved)} equals
                true and {nameof(Entities.EventWarning.SolvedDate)} not null or
                {nameof(Entities.EventWarning.Solved)} equals false and
                {nameof(Entities.EventWarning.SolvedDate)} null "));

            base.AddRule("IsSolvedDateValid", new Rule<Entities.EventWarning>(isSolvedDateValid,
                $@"{nameof(Entities.EventWarning.SolvedDate)} has to be equals or higher than
                {nameof(Entities.EventWarning.Date)}"));

            base.AddRule("IsEventValid", new Rule<Entities.EventWarning>(isEventValid,
                $"{nameof(Entities.EventWarning.Event)} is required"));
        }
    }
}
