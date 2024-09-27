using ContactsPortal.Models;
using FluentValidation;

namespace ContactsPortal.Validator
{
    public class ContactGroupValidator : AbstractValidator<ContactGroup>
    {
        public ContactGroupValidator()
        {
            RuleFor(contact=>contact.ContactGroupName)
                .NotEmpty().WithMessage("Contact group name is required.")
                   .NotNull()
                .Length(2, 50).WithMessage("Contact group name must be between 2 and 50 characters.");
        }
    }
}
