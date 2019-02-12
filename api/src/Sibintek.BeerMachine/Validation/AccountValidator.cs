using FluentValidation;
using Sibintek.BeerMachine.DataContracts;

namespace Sibintek.BeerMachine.Validation
{
    public class AccountValidator : AbstractValidator<Account>
    {
        public AccountValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}