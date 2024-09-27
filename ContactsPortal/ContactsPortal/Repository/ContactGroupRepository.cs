using ContactsPortal.Data;
using ContactsPortal.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactsPortal.Repository
{
    public class ContactGroupRepository
    {
        private AppDbContext _dbContext;

        public ContactGroupRepository(AppDbContext dbContext) => _dbContext = dbContext;

        public async Task<List<ContactGroup>> GetContactGroupsAsync()
        {
            return await _dbContext.ContactGroups.ToListAsync();
        }

        public async Task SaveContactGroup(ContactGroup contactGroup)
        {
            _dbContext.ContactGroups.Attach(contactGroup);
            _dbContext.Entry(contactGroup).State = EntityState.Added;
            await _dbContext.SaveChangesAsync();
        }
    }
}
