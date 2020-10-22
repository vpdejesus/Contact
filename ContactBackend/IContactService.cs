using System.ServiceModel;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ContactBackend
{
    [ServiceContract]
    public interface IContactService
    {
        [OperationContract] 
        List<Contact> GetContacts();
        [OperationContract]
        Contact GetContactById(int id);
        [OperationContract]
        void InsertContact(Contact c);
        [OperationContract]
        int UpdateContact(Contact c);
        [OperationContract]
        int DeleteContact(int id);
    }

    [DataContract]
    public class Contact
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string Mobile { get; set; } 
    }
}