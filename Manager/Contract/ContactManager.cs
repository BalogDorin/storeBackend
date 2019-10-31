using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Engine.Contract;
using WebApi.Resource.Contract;

namespace WebApi.Manager.Contract
{
    public interface IContactManager
    {
        Infrastructure.ValidationError[] Save(Model.Contact contact);
        Infrastructure.ValidationError[] Delete(Guid id);
        Model.Contact GetById(Guid id);
        Model.Contact[] GetAll();

    }

    public class ContactManager:IContactManager
    {
        IContactValidationEngine contactValidationEngine;
        IContactResource contactResource;
        public ContactManager(IContactResource contactResource, IContactValidationEngine contactValidationEngine)
        {
            this.contactResource = contactResource;
            this.contactValidationEngine = contactValidationEngine;
        }
      public  Infrastructure.ValidationError[] Save(Model.Contact contact)
        {
            Infrastructure.ValidationError[] validationErrors = contactValidationEngine.Save(contact);

            if (validationErrors.Any())
                return validationErrors;

            contactResource.Save(contact);

            return new Infrastructure.ValidationError[0];

        }

       public Infrastructure.ValidationError[] Delete(Guid id)
        {
            Infrastructure.ValidationError[] validationErrors = contactValidationEngine.Delete(id);

            if (validationErrors.Any())
                return validationErrors;

            contactResource.Delete(id);

            return new Infrastructure.ValidationError[0];

        }

       public Model.Contact GetById(Guid id)
        {
            return contactResource.GetById(id);
        }

       public Model.Contact[] GetAll()
        {
            return contactResource.GetAll();
        }




    }
}
