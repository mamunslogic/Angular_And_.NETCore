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
    public class ContactGroupController : ControllerBase
    {
        private readonly ContactGroupRepository _contactGroupRepository;
        private IValidator<ContactGroup> _validator;

        public ContactGroupController(ContactGroupRepository contactGroupRepository, IValidator<ContactGroup> validator)
        {
            _contactGroupRepository = contactGroupRepository;
            _validator = validator;
        }

        [HttpGet]
        public async Task<ActionResult> ContactGroupList()
        {
            var allContacts = await _contactGroupRepository.GetContactGroupsAsync();
            return Ok(allContacts);
        }

        [HttpPost]
        public async Task<ActionResult> SaveContactGroup(ContactGroup viewModel)
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

            await _contactGroupRepository.SaveContactGroup(viewModel);
            return Ok(viewModel);
        }
    }
}
