using FluentValidation;
using Sibintek.BeerMachine.DataContracts;

namespace Sibintek.BeerMachine.Validation
{
    public class LocationArrayValidator : AbstractValidator<Location[]>
    {
        public LocationArrayValidator()
        {
            RuleForEach(x => x).SetValidator(new LocationValidator());
        }
    }
}