using FluentValidation;
using Sibintek.BeerMachine.DataContracts;

namespace Sibintek.BeerMachine.Validation
{
    public class PersonValidator : AbstractValidator<Person>
    {
        public PersonValidator()
        {
            RuleFor(x => x.FullName).NotEmpty();
        }
    }
}