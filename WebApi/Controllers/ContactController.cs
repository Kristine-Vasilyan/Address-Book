using Microsoft.AspNetCore.Mvc;
using WebAddressBook.DAL;

namespace WebAddressBook.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : Controller
    {
        [HttpGet]
        public Task<List<Contact>> GetAsync()
        {
            return Contacts.GetAsyncList();
        }

        [HttpGet("{id}")]
        public Task<Contact?> GetAsync(int id)
        {
            return Contacts.GetAsync(id);
        }

        [HttpPost]
        public Task<int> PostAsync([FromBody] Contact contact)
        {
            return Contacts.InsertAsync(contact);
        }
        [HttpDelete("{id}")]
        public Task DeleteAsync(int id)
        {
            return Contacts.DeleteAsync(id);
        }
        [HttpPut]
        public Task PutAsync([FromBody] Contact obj)
        {
            return Contacts.UpdateAsync(obj);
        }
    }
}
