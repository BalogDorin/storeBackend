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

    public class PozaController : Controller
    {
        IPozaManager pozaManager;
        public PozaController(IPozaManager pozaManager)
        {
            this.pozaManager = pozaManager;
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPost("Save")]
       public Infrastructure.ValidationError[] Save(Poza poza)
        {
            return pozaManager.Save(poza);
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPut("Update")]
        public Infrastructure.ValidationError[] Update(Poza poza)
        {
            poza = null;
            return pozaManager.Update(poza);
        }

        [Authorize(Roles = Role.Admin)]
        [HttpDelete("Delete")]
        public Infrastructure.ValidationError[] Delete(Guid id)
        {
            return pozaManager.Delete(id);
        }

        [Authorize(Roles = Role.Admin)]
        [HttpDelete("DeletePozeProdus")]
        public Infrastructure.ValidationError[] DeleteProdus(Guid id)
        {
            return pozaManager.DeleteProdus(id);
        }

        [AllowAnonymous]
        [HttpGet("GetById")]
        public Poza GetById(Guid id)
        {
            return pozaManager.GetById(id);

        }
        [AllowAnonymous]
        [HttpGet("test")]
        public String test()
        {
            return "it works";

        }

        [AllowAnonymous]
        [HttpGet("GetByProdusId")]
        public Poza[] GetByProdusId(Guid id)
        {
            return pozaManager.GetByProdusId(id);
        }

        [AllowAnonymous]
        [HttpGet("PozaFaraProdus")]
        public Poza[] PozaFaraProdus()
        {
            return pozaManager.PozaFaraProdus();
        }






    }
}