using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Resource.Contract;

namespace WebApi.Engine.Contract
{
    public interface IProdusValidationEngine
    {
        Infrastructure.ValidationError[] Save(Manager.Model.Produs produs);
        Infrastructure.ValidationError[] Update(Manager.Model.Produs produs);
        Infrastructure.ValidationError[] Delete(Guid produs);
    }

    public class ProdusValidationEngine :IProdusValidationEngine
    {
        IProdusResource produsResource;
        public ProdusValidationEngine(IProdusResource produsResource)
        {
            this.produsResource = produsResource;
        }
       public Infrastructure.ValidationError[] Save(Manager.Model.Produs produs)
        {
            List<Infrastructure.ValidationError> errors = new List<Infrastructure.ValidationError>();
            if (produs != null)
            {
                if (string.IsNullOrWhiteSpace(produs.Culoare))
                    errors.Add(new Infrastructure.ValidationError { Message = "Completeaza campul Pret!" });

                if (string.IsNullOrWhiteSpace(produs.Titlu))
                    errors.Add(new Infrastructure.ValidationError { Message = "Completeaza campul Titlu!" });

                if (string.IsNullOrWhiteSpace(produs.Marime))
                    errors.Add(new Infrastructure.ValidationError { Message = "Completeaza campul Marime!" });

                if (string.IsNullOrWhiteSpace(produs.Descriere))
                    errors.Add(new Infrastructure.ValidationError { Message = "Completeaza campul Descriere!" });

                if (produs.Pret == 0)
                    errors.Add(new Infrastructure.ValidationError { Message = "Completeaza campul Culoare!" });

                if (produs.Files == null)
                    errors.Add(new Infrastructure.ValidationError { Message = "Completeaza campul cu poze!" });

                if (produsResource.IsUniqueProdus(produs.Categorie, produs.Culoare, produs.Titlu) == false)
                    errors.Add(new Infrastructure.ValidationError { Message = "Produsul exista in baza de date!" });
            }
            else errors.Add(new Infrastructure.ValidationError { Message = "Produsul este null!" });

            return errors.ToArray();
        }

       public Infrastructure.ValidationError[] Update(Manager.Model.Produs produs)
        {
            Guid guid = new Guid();

            List<Infrastructure.ValidationError> errors = new List<Infrastructure.ValidationError>();
            if (produs != null)
            {
                if (string.IsNullOrWhiteSpace(produs.Culoare))
                    errors.Add(new Infrastructure.ValidationError { Message = "Completeaza campul Culoare!" });

                if (string.IsNullOrWhiteSpace(produs.Titlu))
                    errors.Add(new Infrastructure.ValidationError { Message = "Completeaza campul Titlu!" });

                if (string.IsNullOrWhiteSpace(produs.Marime))
                    errors.Add(new Infrastructure.ValidationError { Message = "Completeaza campul Marime!" });

                if (string.IsNullOrWhiteSpace(produs.Id))
                    errors.Add(new Infrastructure.ValidationError { Message = "Completeaza campul Id!" });

                if (string.IsNullOrWhiteSpace(produs.Categorie))
                    errors.Add(new Infrastructure.ValidationError { Message = "Completeaza campul Categorie!" });

                if (produs.Pret == 0)
                    errors.Add(new Infrastructure.ValidationError { Message = "Completeaza campul Pret!" });

                Guid.TryParse(produs.Id, out guid);
                if (guid != Guid.Empty)
                    if (produsResource.GetById(guid) == null)
                        errors.Add(new Infrastructure.ValidationError { Message = "Produsul nu exista in baza de date!" });
            }
            else
                 errors.Add(new Infrastructure.ValidationError { Message = "Obiectul este null!" });

            return errors.ToArray();
        }

      public Infrastructure.ValidationError[] Delete(Guid produs)
        {
            List<Infrastructure.ValidationError> errors = new List<Infrastructure.ValidationError>();

            if(produsResource.GetById(produs) == null)
                errors.Add(new Infrastructure.ValidationError { Message = "Produsul nu exista in baza de date!" });

            return errors.ToArray();

        }
    }
}
