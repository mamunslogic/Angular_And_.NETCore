using ContactsPortal.Models;
using FluentValidation;

namespace ContactsPortal.Validator
{
    public class ContactValidator : AbstractValidator<Contact>
    {
        public ContactValidator()
        {
            RuleFor(contact => contact.Name)
                .NotEmpty().WithMessage("Name is required.")
                .NotNull()
                .Length(2, 50).WithMessage("Name must be between 2 and 50 characters.");

            RuleFor(contact => contact.PhoneNumber)
               .NotEmpty().WithMessage("Phone number is required.")
               .NotNull()
               .Length(11).WithMessage("Length of phone number is 11 required.");

            RuleFor(contact => contact.ContactType)
                .NotEmpty().WithMessage("Contact type is required.")
                .NotNull()
                .Length(2, 50).WithMessage("Contact type must be between 2 and 50 characters.");
        }
    }
}
