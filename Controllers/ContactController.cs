using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Entities;
using WebApi.Manager.Contract;
using WebApi.Manager.Model;
namespace WebApi.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class ContactController : ControllerBase
    {
        IContactManager contactManager;
        public ContactController(IContactManager contactManager)
        {
            this.contactManager = contactManager;
        }
        [AllowAnonymous]
        [HttpPost("Save")]
       public Infrastructure.ValidationError[] Save(Contact contact)
        {
            return contactManager.Save(contact);
        }
        [AllowAnonymous]
        [HttpPost("SaveApp")]
       public Infrastructure.ValidationError[] SaveApp([FromBody]Contact contact)
        {
            return contactManager.Save(contact);
        }

        [Authorize(Roles = Role.Admin)]
        [HttpDelete("Delete")]
        public Infrastructure.ValidationError[] Delete(string id)
        {
            Guid guid = new Guid();
            Guid.TryParse(id, out guid);
            if (guid != Guid.Empty)
                return contactManager.Delete(Guid.Parse(id));           
            else
                return null;
        }

        [Authorize(Roles = Role.Admin)]
        [HttpGet("GetById")]
        public Contact GetById(string id)
        {
            Guid guid = new Guid();
            Guid.TryParse(id, out guid);
            if (guid != Guid.Empty)
                return contactManager.GetById(Guid.Parse(id));
            else
                return null;
        }

        [Authorize(Roles = Role.Admin)]
        [HttpGet("GetAll")]
        public Contact[] GetAll()
        {
            return contactManager.GetAll();
        }
    }
}