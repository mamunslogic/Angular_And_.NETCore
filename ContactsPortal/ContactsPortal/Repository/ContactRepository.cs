using ContactsPortal.Data;
using ContactsPortal.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactsPortal.Repository
{
    public class ContactRepository
    {
        private readonly AppDbContext _dbContext;

        public ContactRepository(AppDbContext dbContext) => _dbContext = dbContext;

        public async Task<List<Contact>> GetContactsAsync()
        {
            return await _dbContext.Contacts.Include(c => c.ContactGroup).ToListAsync();
        }

        public async Task SaveContact(Contact contact)
        {
            contact.ContactGroup = null;
            _dbContext.Contacts.Attach(contact);
            _dbContext.Entry(contact).State = EntityState.Added;
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateContact(int id, Contact contact)
        {
            //if (contact == null || contact.Id == 0)
            //{
            //    throw new Exception("Contact information not found.");
            //}

            var contactResult = await _dbContext.Contacts.FindAsync(id);
            if (contactResult == null)
            {
                throw new Exception("Contact information is not found.");
            }
            contactResult.Name = contact.Name;
            contactResult.PhoneNumber = contact.PhoneNumber;
            contactResult.ContactType = contact.ContactType;
            contactResult.ContactGroupId = contact.ContactGroupId;

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteContact(int id)
        {
            var contactResult = await _dbContext.Contacts.FindAsync(id);
            if (contactResult == null)
            {
                throw new Exception("Contact information is not found.");
            }

            _dbContext.Contacts.Remove(contactResult);
            await _dbContext.SaveChangesAsync();
        }
    }
}
