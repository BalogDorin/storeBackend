using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.DataBase;
using WebApi.DataBase.Model;

namespace WebApi.Resource.Contract
{
    public interface IPozaResource
    {
        void Save(Manager.Model.Poza poza);
        void Update(Manager.Model.Poza poza);
        void Delete(Guid id);
        void DeleteProdus(Guid id);
        Manager.Model.Poza GetById(Guid id);
        bool IsProdusDeleted(Guid id);
        Manager.Model.Poza[] GetByProdusId(Guid id);
        Manager.Model.Poza[] PozaFaraProdus();
    }
    public class PozaResource : IPozaResource
    {
        private DatabaseContext databaseContext;

        public PozaResource(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;

        }
        public void Save(Manager.Model.Poza poza1)
        {
            Poza poza = new Poza();

            if (poza1 != null)
            {
                poza.Id = Guid.NewGuid();
                poza.IdProdus = Guid.Parse(poza1.IdProdus);
                poza.CalePoza = poza1.CalePoza;
                poza.IsProduct = poza1.IsProduct;
                databaseContext.Poza.Add(poza);
                databaseContext.SaveChanges();
            }
            
        }
        public void Update(Manager.Model.Poza poza1)
        {
            Poza poza = new Poza();
            Poza check;
            if (poza1 != null)
            { 
                    poza.Id = Guid.Parse(poza1.Id);
                    poza.IdProdus = Guid.Parse(poza1.IdProdus);
                    poza.CalePoza = poza1.CalePoza;
                    poza.IsProduct = poza1.IsProduct;
                    check = databaseContext.Poza.FirstOrDefault(existingPoza => existingPoza.Id == Guid.Parse(poza1.Id));
                    if (check != null)
                    {
                        databaseContext.Poza.Remove(check);
                        databaseContext.Poza.Add(poza);
                        databaseContext.SaveChanges();
                    }
            }
        }
        
        public void Delete(Guid id)
        {
            Poza poza;
            poza = databaseContext.Poza.FirstOrDefault(existingPoza => existingPoza.Id == id);
            if (poza != null)
            {
                databaseContext.Poza.Remove(poza);
                databaseContext.SaveChanges();
            }            
        }

        public void DeleteProdus(Guid id)
        {
            Poza poza;
            poza = databaseContext.Poza.FirstOrDefault(existingPoza => existingPoza.IdProdus == id);
            List<Poza> poze = databaseContext.Poza.Where(c => c.IdProdus == id).ToList();
            if (poza != null)
            {
                databaseContext.Poza.RemoveRange(poze);
                databaseContext.SaveChanges();
            }           
        }

        public bool IsProdusDeleted(Guid id)
        {
            Poza poza;
            poza = databaseContext.Poza.FirstOrDefault(existingPoza => existingPoza.IdProdus == id);
            List<Poza> poze = databaseContext.Poza.Where(c => c.IdProdus == id).ToList();
            if (poza != null)
            {
                return false;
            }
           else
                return true;

        }

        public Manager.Model.Poza GetById(Guid id)
        {
            Manager.Model.Poza poza = new Manager.Model.Poza();
            Poza poza1;
            poza1 = databaseContext.Poza.FirstOrDefault(existingPoza => existingPoza.Id == id);
            if (poza1 != null)
            {
                poza.Id = poza1.Id.ToString();
                poza.CalePoza = poza1.CalePoza;
                poza.IdProdus = poza1.IdProdus.ToString();
                poza.IsProduct = poza1.IsProduct;
                return poza;
            }
            else
                return null;
        }
        public Manager.Model.Poza[] GetByProdusId(Guid id)
        {
            Poza[] poza1;
            poza1 = databaseContext.Poza.Where(existingPoza => existingPoza.IdProdus == id).ToArray();
            Manager.Model.Poza[] poza = new Manager.Model.Poza[poza1.Length];

            if (poza != null)
            {
                for(int i = 0; i < poza1.Length; i++)
                {
                    poza[i] = new Manager.Model.Poza
                    {
                        Id = poza1[i].Id.ToString(),
                        CalePoza = poza1[i].CalePoza,
                        IdProdus = poza1[i].IdProdus.ToString(),
                        IsProduct = poza1[i].IsProduct
                    };
                }  
                return poza;
            }
            else
                return null;
        }
        public Manager.Model.Poza[] PozaFaraProdus()
        {
            Poza[] poza1;
            poza1 = databaseContext.Poza.Where(existingPoza => existingPoza.IsProduct == false).ToArray();
            Manager.Model.Poza[] poza = new Manager.Model.Poza[poza1.Length];

            
                for (int i = 0; i < poza1.Length; i++)
                {
                poza[i] = new Manager.Model.Poza();
                 
                    poza[i].Id = poza1[i].Id.ToString();
                    poza[i].CalePoza = poza1[i].CalePoza;
                    poza[i].IdProdus = poza1[i].IdProdus.ToString();
                    poza[i].IsProduct = poza1[i].IsProduct;
                }
                return poza;
            
            
        }
    }
}
