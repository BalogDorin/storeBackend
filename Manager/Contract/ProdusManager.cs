using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Engine.Contract;
using WebApi.Resource.Contract;

namespace WebApi.Manager.Contract
{
    public interface IProdusManager
    {
        Infrastructure.ValidationError[] Save(Model.Produs produs);
        Infrastructure.ValidationError[] Update(Model.Produs produs);
        Infrastructure.ValidationError[] Delete(Guid produs);
        Model.Produs GetById(Guid id);
        Model.Produs[] GetByCategorie(string categorie);
        Model.Produs[] GetDiscount();
    }
    public class ProdusManager:IProdusManager
    {
        IProdusResource produsResource;
        IProdusValidationEngine produsValidationEngine;
        IPozaResource pozaResource=null;
        public ProdusManager(IProdusResource produsResource, IProdusValidationEngine produsValidationEngine, IPozaResource pozaResource)
        {
            this.produsResource = produsResource;
            this.produsValidationEngine = produsValidationEngine;
            this.pozaResource = pozaResource;
        }

      public  Infrastructure.ValidationError[] Save(Model.Produs produs)
        {
            if(produs != null)
            produs.Id = Guid.NewGuid().ToString();
            int number = 0;
            string filePath = "";
            string newFolder = "";

            Model.Poza poza = new Model.Poza();

            

            Infrastructure.ValidationError[] validationErrors = produsValidationEngine.Save(produs);

            if (validationErrors.Any())
                return validationErrors;

            newFolder = produs.Id;
            filePath = @"C:/Users/balog/OneDrive/Desktop/Alina/Proiect/Proiect/src/assets/" + newFolder;
            produs.CaleFolder = "assets/"+ newFolder;

            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            foreach (var file in produs.Files)
            {
                string path = Path.Combine(produs.CaleFolder, number.ToString());
                string allPath = Path.Combine(filePath, number.ToString());
                path = String.Concat(path, ".png");
                allPath = String.Concat(allPath, ".png");
                poza.CalePoza = path;
                poza.IdProdus = produs.Id;
                poza.IsProduct = true;
                pozaResource.Save(poza);
                using (var fileStream = new FileStream(allPath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                number++;
            }

            produsResource.Save(produs);

            return new Infrastructure.ValidationError[0];
        }

      public  Infrastructure.ValidationError[] Update(Model.Produs produs)
        {
            Infrastructure.ValidationError[] validationErrors = produsValidationEngine.Update(produs);

            if (validationErrors.Any())
                return validationErrors;

            produsResource.Update(produs);

            return new Infrastructure.ValidationError[0];

        }
       public Infrastructure.ValidationError[] Delete(Guid produs)
        {
            Infrastructure.ValidationError[] validationErrors = produsValidationEngine.Delete(produs);

            if (validationErrors.Any())
                return validationErrors;

            produsResource.Delete(produs);

            return new Infrastructure.ValidationError[0];

        }

       public Model.Produs GetById(Guid id)
        {
            return produsResource.GetById(id);
        }

       public Model.Produs[] GetByCategorie(string categorie)
        {
            return produsResource.GetByCategorie(categorie);
        }
        public Model.Produs[] GetDiscount()
        {
            return produsResource.GetDiscount();
        }
    }
}
