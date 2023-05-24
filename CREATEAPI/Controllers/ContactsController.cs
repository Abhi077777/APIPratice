using CREATEAPI.Data;
using CREATEAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CREATEAPI.Controllers
{
    [ApiController]
    [Route("api/Contacts")]
    public class ContactsController : Controller
    {
        private readonly ContactsAPIDbcontext dbcontext;



        public ContactsController(ContactsAPIDbcontext dbcontext)
        {
            this.dbcontext = dbcontext;
        }




        [HttpGet]
        public async Task<IActionResult> GetContacts()
        {
            return Ok(await dbcontext.Contacts.ToListAsync());

        }

        [HttpGet]
        [Route("{id}")]
        public async Task <IActionResult> GetContact([FromRoute] Guid id)
        {
            var Contact = await dbcontext.Contacts.FindAsync(id);

            if(Contact == null)
            {
                return NotFound();
            }
            return Ok(Contact);
        }

        [HttpPost]
        public async Task<IActionResult> AddContact(AddContactRequest addContactRequest)
        {
            var Contact = new Contact()
            {
                ID = Guid.NewGuid(),
                Address = addContactRequest.Address,
                Email = addContactRequest.Email,
                FullName = addContactRequest.FullName,
                Phone = addContactRequest.Phone,
            };

            await dbcontext.Contacts.AddAsync(Contact);
            await dbcontext.SaveChangesAsync();

            return Ok(Contact);
        }

        [HttpPut] 
        [Route("{id}")]
        public async Task<IActionResult> UpdateContact([FromRoute] Guid id, UpdateContactRequet updateContactRequet )
        {
            var Contact = await dbcontext.Contacts.FindAsync(id);

            if(Contact != null)
            {
                Contact.FullName = updateContactRequet.FullName;
                Contact.Address = updateContactRequet.Address;
                Contact.Phone = updateContactRequet.Phone;
                Contact.Email = updateContactRequet.Email;

                await dbcontext.SaveChangesAsync();

                return Ok(Contact);
            }
            return NotFound();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteContact([FromRoute] Guid id)
        {
           var contact = await dbcontext.Contacts.FindAsync(id);

            if(contact != null)
            {
                dbcontext.Remove(contact);
                await dbcontext.SaveChangesAsync();
                return Ok(contact);
            }
            return NotFound();
        }
    }
}
