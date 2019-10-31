using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.DataBase;
using WebApi.DataBase.Model;

namespace WebApi.Resource.Contract
{
    public interface IContactResource
    {
        void Save(Manager.Model.Contact contact);
        void Delete(Guid id);
        Manager.Model.Contact GetById(Guid id);
        bool IsContactDeleted(Guid id);
        Manager.Model.Contact[] GetAll();
    }

    public class ContactResource : IContactResource
    {
        private DatabaseContext databaseContext;

        public ContactResource(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public void Save(Manager.Model.Contact contact1)
        {
            Contact contact = new Contact();
            if (contact1 != null)
            {
                contact.Comment = contact1.Comment;
                contact.Email = contact1.Email;
                contact.Id = Guid.NewGuid();
                contact.Nume = contact1.Nume;
                contact.Prenume = contact1.Prenume;

                databaseContext.Contact.Add(contact);

                databaseContext.SaveChanges();
            }
        }
        public void Delete(Guid id)
        {
            Contact contact;
            contact = databaseContext.Contact.FirstOrDefault(existingContact => existingContact.Id == id);
            if (contact != null)
            {
                databaseContext.Contact.Remove(contact);
                databaseContext.SaveChanges();
            }
        }
        public bool IsContactDeleted(Guid id)
        {
            Contact contact;
            contact = databaseContext.Contact.FirstOrDefault(existingContact => existingContact.Id == id);
            if (contact != null)
            {
                return false;
            }
            else
                return true;
        }

        public Manager.Model.Contact GetById(Guid id)
        {
            Manager.Model.Contact contact = new Manager.Model.Contact();

            Contact contact1;
            contact1 = databaseContext.Contact.FirstOrDefault(existingContact => existingContact.Id == id);
            if (contact1 != null)
            {
                contact.Comment = contact1.Comment;
                contact.Email = contact1.Email;
                contact.Id = Guid.NewGuid().ToString();
                contact.Nume = contact1.Nume;
                contact.Prenume = contact1.Prenume;
                return contact;
            }
            return null;
        }

        public Manager.Model.Contact[] GetAll()
        {
            Contact[] contact1;
            contact1 = databaseContext.Contact.Where(existingContact => existingContact.Comment != "").ToArray();
            Manager.Model.Contact[] contact = new Manager.Model.Contact[contact1.Length];
            for(int i = 0; i < contact1.Length; i++)
            {
                contact[i] = new Manager.Model.Contact
                {
                    Comment = contact1[i].Comment,
                    Nume = contact1[i].Nume,
                    Prenume = contact1[i].Prenume,
                    Id = contact1[i].Id.ToString(),
                    Email = contact1[i].Email
                };
            }
            return contact;

        }

    }
}
