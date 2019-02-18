using FluentValidation;
using Sibintek.BeerMachine.DataContracts;

namespace Sibintek.BeerMachine.Validation
{
    public class LocationValidator : AbstractValidator<Location>
    {
        public LocationValidator()
        {
            Include(new AccountValidator());
            RuleFor(x => x.Room).IsInEnum();
        }
    }
}