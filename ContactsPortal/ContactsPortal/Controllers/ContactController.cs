using ContactsPortal.Models;
using ContactsPortal.Repository;
using ContactsPortal.ResponseModel;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ContactsPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly ContactRepository _contactRepository;
        private IValidator<Contact> _validator;

        public ContactController(ContactRepository contactRepository, IValidator<Contact> validator)
        {
            _contactRepository = contactRepository;
            _validator = validator;
        }

        [HttpGet]
        public async Task<ActionResult> ContactList()
        {
            var allContacts = await _contactRepository.GetContactsAsync();
            return Ok(allContacts);
        }

        [HttpPost]
        public async Task<ActionResult> SaveContact(Contact viewModel)
        {
            var result = await _validator.ValidateAsync(viewModel);
            string errorMessage = "";

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    errorMessage += $"{error.ErrorMessage} ";
                }

                var errors = result.Errors.Select(x => new { x.ErrorCode, x.ErrorMessage }).ToArray();
                return BadRequest(new CustomResponseModel(HttpStatusCode.BadRequest, errorMessage, errors));
            }

            await _contactRepository.SaveContact(viewModel);
            return Ok(viewModel);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateContact(int id, [FromBody] Contact viewModel)
        {
            var result = await _validator.ValidateAsync(viewModel);
            string errorMessage = "";

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    errorMessage += $"{error.ErrorMessage} ";
                }

                var errors = result.Errors.Select(x => new { x.ErrorCode, x.ErrorMessage }).ToArray();
                return BadRequest(new CustomResponseModel(HttpStatusCode.BadRequest, errorMessage, errors));
            }

            await _contactRepository.UpdateContact(id, viewModel);
            return Ok(viewModel);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteContact(int id)
        {
            await _contactRepository.DeleteContact(id);
            return Ok();
        }
    }
}
