using CREATEAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace CREATEAPI.Data
{
    public class ContactsAPIDbcontext : DbContext
    {
        public ContactsAPIDbcontext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Contact> Contacts { get; set; }
    }
}
