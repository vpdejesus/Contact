using System.Collections.Generic;
using ContactBackend.DAL;

namespace ContactBackend
{
    public class ContactService : IContactService
    {
        private ContactDB _contact;

        public ContactService()
        {
            _contact = new ContactDB();
        }

        public List<Contact> GetContacts()
        {
            return _contact.GetContacts();
        }

        public Contact GetContactById(int id)
        {
            return _contact.GetContactById(id);
        }

        public void InsertContact(Contact c)
        {
            _contact.InsertContact(c);
        }

        public int UpdateContact(Contact c)
        {
            return _contact.UpdateContact(c);
        }

        public int DeleteContact(int id)
        {
            return _contact.DeleteContact(id);
        }
    }
}
