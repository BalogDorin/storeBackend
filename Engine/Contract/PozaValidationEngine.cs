using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Resource.Contract;

namespace WebApi.Engine.Contract
{
    public interface IPozaValidationEngine
    {
        Infrastructure.ValidationError[] Save(Manager.Model.Poza poza);
        Infrastructure.ValidationError[] Update(Manager.Model.Poza poza);
        Infrastructure.ValidationError[] DeleteProdus(Guid id);
        Infrastructure.ValidationError[] DeletePoza(Guid id);

    }

    public class PozaValidationEngine :IPozaValidationEngine
    {
        IPozaResource pozaResource;

        public PozaValidationEngine(IPozaResource pozaResource)
        {
            this.pozaResource = pozaResource;

        }

      public  Infrastructure.ValidationError[] Save(Manager.Model.Poza poza)
        {
            List<Infrastructure.ValidationError> errors = new List<Infrastructure.ValidationError>();
            if (poza != null)
            {
                if (poza.Files == null)
                    errors.Add(new Infrastructure.ValidationError { Message = "Adauga pozele!" });
            }
            else
                errors.Add(new Infrastructure.ValidationError { Message = "Obiectul este null!" });

            return errors.ToArray();
        }

      public  Infrastructure.ValidationError[] Update(Manager.Model.Poza poza)
        {
            List<Infrastructure.ValidationError> errors = new List<Infrastructure.ValidationError>();
            if (poza != null)
            {
                if (string.IsNullOrWhiteSpace(poza.IdProdus))
                    errors.Add(new Infrastructure.ValidationError { Message = "Completeaza campul IdProdus!" });

                if (string.IsNullOrWhiteSpace(poza.Id))
                    errors.Add(new Infrastructure.ValidationError { Message = "Completeaza campul Id!" });

                if (string.IsNullOrWhiteSpace(poza.CalePoza))
                    errors.Add(new Infrastructure.ValidationError { Message = "Completeaza campul CalePoza!" });

                if (string.IsNullOrWhiteSpace(poza.CalePoza))
                    errors.Add(new Infrastructure.ValidationError { Message = "Completeaza campul CalePoza!" });
            }
            else
                errors.Add(new Infrastructure.ValidationError { Message = "Obiectul trimis este null!" });

            return errors.ToArray();
        }

      public  Infrastructure.ValidationError[] DeleteProdus(Guid id)
        {
            List<Infrastructure.ValidationError> errors = new List<Infrastructure.ValidationError>();

            if (pozaResource.IsProdusDeleted(id) == true)
                errors.Add(new Infrastructure.ValidationError { Message = "Poza nu exista in baza de date!" });

            return errors.ToArray();
        }

       public Infrastructure.ValidationError[] DeletePoza(Guid id)
        {
            List<Infrastructure.ValidationError> errors = new List<Infrastructure.ValidationError>();

            if (pozaResource.IsProdusDeleted(id) == true)
                errors.Add(new Infrastructure.ValidationError { Message = "Poza nu exista in baza de date!" });

            return errors.ToArray();
        }


    }
}
