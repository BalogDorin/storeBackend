using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using WebApi.Resource.Contract;

namespace WebApi.Engine.Contract
{
    public interface IContactValidationEngine
    {
        Infrastructure.ValidationError[] Save(Manager.Model.Contact contact);
        Infrastructure.ValidationError[] Delete(Guid id);
    }
    public class ContactValidationEngine :IContactValidationEngine
    {
        IContactResource contactResource;
        public ContactValidationEngine(IContactResource contactResource)
        {
            this.contactResource = contactResource;
        }

      public  Infrastructure.ValidationError[] Save(Manager.Model.Contact contact)
        {
            List<Infrastructure.ValidationError> errors = new List<Infrastructure.ValidationError>();
            if (contact != null)
            {
                if (string.IsNullOrWhiteSpace(contact.Comment))
                    errors.Add(new Infrastructure.ValidationError { Message = "Completeaza campul Comment!" });

                if (string.IsNullOrWhiteSpace(contact.Email))
                    errors.Add(new Infrastructure.ValidationError { Message = "Completeaza campul Email!" });
                else 
                try
                {
                    MailAddress mailAddress = new MailAddress(contact.Email);
                }
                catch (Exception)
                {
                    errors.Add(new Infrastructure.ValidationError { Message = "Email invalid!" });

                }

                

                if (string.IsNullOrWhiteSpace(contact.Nume))
                    errors.Add(new Infrastructure.ValidationError { Message = "Completeaza campul Nume!" });

                if (string.IsNullOrWhiteSpace(contact.Prenume))
                    errors.Add(new Infrastructure.ValidationError { Message = "Completeaza campul Prenume!" });
            }
            else errors.Add(new Infrastructure.ValidationError { Message = "Obiectul trimis este null!" });

            return errors.ToArray();
        }

       public Infrastructure.ValidationError[] Delete(Guid id)
        {
            List<Infrastructure.ValidationError> errors = new List<Infrastructure.ValidationError>();

            if(contactResource.IsContactDeleted(id) == true)
                errors.Add(new Infrastructure.ValidationError { Message = "Contactul nu exista in baza de date!" });

            return errors.ToArray();

        }

    }
}
