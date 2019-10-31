using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Engine.Model;
using WebApi.Entities;
using WebApi.Manager.Contract;

namespace WebApi.Controllers
{
    [Authorize]
    [Route("[controller]")]
    
    public class ProdusController : ControllerBase
    { IProdusManager produsManager;
       public ProdusController(IProdusManager produsManager)
        {
            this.produsManager = produsManager;
        }

        [Authorize(Roles = Role.Admin)]
        [HttpDelete("Delete")]
        public Infrastructure.ValidationError[] Delete(string produs)
        {
            Guid guid = new Guid();
            Guid.TryParse(produs, out guid);
            if (guid != Guid.Empty)
                return produsManager.Delete(Guid.Parse(produs));
            else
                return null;
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPut("Update")]
        public Infrastructure.ValidationError[] Update(Manager.Model.Produs produs)
        {
            return produsManager.Update(produs);
        }

[AllowAnonymous]
        [HttpPost("Save")]
        public Infrastructure.ValidationError[] Save(Manager.Model.Produs produs)
        {
            return produsManager.Save(produs);
        }
        [AllowAnonymous]
        [HttpPost("Save1")]
        public void Save1(Manager.Model.Produs produs)
        {
        }
        [AllowAnonymous]
        [HttpGet("GetById")]
        public Manager.Model.Produs GetById(string id)
        {
            Guid guid = new Guid();
            Guid.TryParse(id, out guid);
            if (guid != Guid.Empty)
                return produsManager.GetById(Guid.Parse(id));
            else
                return null;
        }
        [AllowAnonymous]
        [HttpGet("GetByCategorie")]
       public Manager.Model.Produs[] GetByCategorie(string id)
        {

           return produsManager.GetByCategorie(id);
        }
        [AllowAnonymous]
        [HttpGet("GetDiscount")]
        public Manager.Model.Produs[] GetDiscount()
        {

            return produsManager.GetDiscount();
        }
        [AllowAnonymous]
        [HttpGet("test")]
        public void Test(string id)
        {

           
        }




    }
}