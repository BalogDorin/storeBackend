using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApi.DataBase;
using WebApi.DataBase.Model;

namespace WebApi.Resource.Contract
{
    public interface IProdusResource
    {
        void Save(Manager.Model.Produs produs);
        void Update(Manager.Model.Produs produs);
        void Delete(Guid produs);
        Manager.Model.Produs GetById(Guid id);
        Manager.Model.Produs[] GetByCategorie(string categorie);
        Manager.Model.Produs[] GetDiscount();
        bool IsUniqueProdus(string categorie, string culoare, string titlu);
    }

    public class ProdusResource : IProdusResource
    {
        private DatabaseContext databaseContext;
        private IPozaResource pozaService;

        public ProdusResource(DatabaseContext databaseContext, IPozaResource pozaService)
        {
            this.databaseContext = databaseContext;
            this.pozaService = pozaService;
        }

        public void Save(Manager.Model.Produs produs1)
        {
            Produs produs = new Produs();

            Produs produsExistent;

            if (produs1 != null)
            {
                produs.Id = Guid.Parse(produs1.Id);
                produs.Marime = produs1.Marime;
                produs.Pret = produs1.Pret;
                produs.PretRedus = produs1.PretRedus;
                produs.Titlu = produs1.Titlu;
                produs.Categorie = produs1.Categorie;
                produs.Culoare = produs1.Culoare;
                produs.Descriere = produs1.Descriere;
                produs.CaleFolder = produs1.CaleFolder;

                produsExistent = databaseContext.Produs.FirstOrDefault(existingProdus => existingProdus.Id == produs.Id);
                if (produsExistent == null)
                {
                    databaseContext.Produs.Add(produs);
                    databaseContext.SaveChanges();
                }
            }
        }

        public void Update(Manager.Model.Produs produs1)
        {
            Produs produsExistent;
            Produs produs = new Produs();
            if (produs1 != null)
            {
                produs.Id = Guid.Parse(produs1.Id);
                produs.Marime = produs1.Marime;
                produs.Pret = produs1.Pret;
                produs.PretRedus = produs1.PretRedus;
                produs.Titlu = produs1.Titlu;
                produs.Categorie = produs1.Categorie;
                produs.Culoare = produs1.Culoare;
                produs.Descriere = produs1.Descriere;

                produsExistent = databaseContext.Produs.FirstOrDefault(existingProdus => existingProdus.Id == produs.Id);
                if (produsExistent != null)
                {
                    produs.CaleFolder = produsExistent.CaleFolder;
                    databaseContext.Produs.Remove(produsExistent);
                    databaseContext.Produs.Add(produs);
                    databaseContext.SaveChanges();
                }
            }
        }

        public void Delete(Guid id)
        {
            Produs produsExistent;

            produsExistent = databaseContext.Produs.FirstOrDefault(existingProdus => existingProdus.Id == id);
           
            if (produsExistent != null)  
            {
                if (produsExistent.CaleFolder != "")
                    if (Directory.Exists(produsExistent.CaleFolder))
                        Directory.Delete(produsExistent.CaleFolder, true);


                pozaService.DeleteProdus(produsExistent.Id);
                databaseContext.Attach(produsExistent);
                databaseContext.Produs.Remove(produsExistent);
                databaseContext.SaveChanges();
            }
        }

        public Manager.Model.Produs GetById(Guid id)
        {
            Produs produs1;
            Manager.Model.Produs produs= new Manager.Model.Produs();
                        produs1 = databaseContext.Produs.FirstOrDefault(existingProdus => existingProdus.Id == id);
            if (produs1 != null)
            {
                produs.Id = produs1.Id.ToString();
                produs.Marime = produs1.Marime;
                produs.Pret = produs1.Pret;
                produs.PretRedus = produs1.PretRedus;
                produs.Titlu = produs1.Titlu;
                produs.Categorie = produs1.Categorie;
                produs.Culoare = produs1.Culoare;
                produs.Descriere = produs1.Descriere;
                produs.CaleFolder = produs1.CaleFolder;
            }
            return produs;
        }

        public Manager.Model.Produs[] GetByCategorie(string categorie)
        {
            Produs[] produs1 = databaseContext.Produs.Where(existingProdus => existingProdus.Categorie == categorie).ToArray();
            Manager.Model.Produs[] produs = new Manager.Model.Produs[produs1.Length];
         
            for (int i = 0; i < produs1.Length; i++)
            {
                produs[i] = new Manager.Model.Produs
                {
                    Id = produs1[i].Id.ToString(),
                    Marime = produs1[i].Marime,
                    Pret = produs1[i].Pret,
                    PretRedus = produs1[i].PretRedus,
                    Titlu = produs1[i].Titlu,
                    Categorie = produs1[i].Categorie,
                    Culoare = produs1[i].Culoare,
                    Descriere = produs1[i].Descriere,
                    CaleFolder = produs1[i].CaleFolder
                };
            }

            return produs;
        }
        public Manager.Model.Produs[] GetDiscount()
        {
            Produs[] produs1 = databaseContext.Produs.Where(existingProdus => existingProdus.Pret > existingProdus.PretRedus).ToArray();
            Manager.Model.Produs[] produs = new Manager.Model.Produs[produs1.Length];

            for (int i = 0; i < produs1.Length; i++)
            {
                produs[i] = new Manager.Model.Produs
                {
                    Id = produs1[i].Id.ToString(),
                    Marime = produs1[i].Marime,
                    Pret = produs1[i].Pret,
                    PretRedus = produs1[i].PretRedus,
                    Titlu = produs1[i].Titlu,
                    Categorie = produs1[i].Categorie,
                    Culoare = produs1[i].Culoare,
                    Descriere = produs1[i].Descriere,
                    CaleFolder = produs1[i].CaleFolder
                };
            }

            return produs;
        }

        public  bool IsUniqueProdus(string categorie, string culoare, string titlu)
        {

            Produs produs;

            produs = databaseContext.Produs.FirstOrDefault(existingProdus => existingProdus.Categorie == categorie && existingProdus.Culoare == culoare && existingProdus.Titlu == titlu);

            if (produs != null)
                return false;
            else
                return true;
        }
    }
}
