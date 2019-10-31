using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Engine.Contract;
using WebApi.Resource.Contract;

namespace WebApi.Manager.Contract
{
    public interface IPozaManager
    {
        Infrastructure.ValidationError[] Save(Model.Poza poza);
        Infrastructure.ValidationError[] Update(Model.Poza poza);
        Infrastructure.ValidationError[] Delete(Guid id);
        Infrastructure.ValidationError[] DeleteProdus(Guid id);
        Model.Poza GetById(Guid id);
        Model.Poza[] GetByProdusId(Guid id);
        Model.Poza[] PozaFaraProdus();
    }
    public class PozaManager:IPozaManager
    {
        IPozaResource pozaResource;
        IPozaValidationEngine pozaValidation;
        public PozaManager(IPozaResource pozaResource, IPozaValidationEngine pozaValidation)
        {
            this.pozaResource = pozaResource;
            this.pozaValidation = pozaValidation;

        }
      public  Infrastructure.ValidationError[] Save(Model.Poza poza)
        {
            string filePath = "";
            string newFolder = "";
            Guid number;
            Model.Poza poza1 = new Model.Poza();

            Infrastructure.ValidationError[] validationErrors = pozaValidation.Save(poza);

            if (validationErrors.Any())
                return validationErrors;

            newFolder = "PozaSlide";
            filePath = @"C:/Users/balog/OneDrive/Desktop/Alina/Proiect/Proiect/src/assets/" + newFolder;
            poza.CalePoza = "assets/" + newFolder;

            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            foreach (var file in poza.Files)
            {
                number = Guid.NewGuid();
                string allPath = Path.Combine(filePath, number.ToString());
                string path = Path.Combine(poza.CalePoza, number.ToString());

                allPath = string.Concat(allPath, ".png");
                path = string.Concat(path, ".png");

                poza1.Id = Guid.NewGuid().ToString();
                poza1.CalePoza = path;
                poza1.IdProdus = poza1.Id;
                poza1.IsProduct = false;
                pozaResource.Save(poza1);
                using (var fileStream = new FileStream(allPath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                
            }

            return new Infrastructure.ValidationError[0];
        }

       public Infrastructure.ValidationError[] Update(Model.Poza poza)
        {
            Infrastructure.ValidationError[] validationErrors = pozaValidation.Update(poza);

            if (validationErrors.Any())
                return validationErrors;

            pozaResource.Update(poza);

            return new Infrastructure.ValidationError[0];

        }

       public Infrastructure.ValidationError[] DeleteProdus(Guid id)
        {
            Infrastructure.ValidationError[] validationErrors = pozaValidation.DeleteProdus(id);

            if (validationErrors.Any())
                return validationErrors;

            pozaResource.DeleteProdus(id);

            return new Infrastructure.ValidationError[0];

        }

       public Infrastructure.ValidationError[] Delete(Guid id)
        {
            Infrastructure.ValidationError[] validationErrors = pozaValidation.DeletePoza(id);

            if (validationErrors.Any())
                return validationErrors;

            pozaResource.Delete(id);

            return new Infrastructure.ValidationError[0];

        }

       public Model.Poza GetById(Guid id)
        {
            return pozaResource.GetById(id);
        }

       public Model.Poza[] GetByProdusId(Guid id)
        {
            return pozaResource.GetByProdusId(id);
        }
       public Model.Poza[] PozaFaraProdus()
        {
            return pozaResource.PozaFaraProdus();
        }
    }
}
