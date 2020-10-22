using System.Configuration;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Collections.Generic;

namespace ContactBackend.DAL
{
    [DataObject(true)]
    public class ContactDB
    {
        private string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["ContactContext"].ConnectionString;
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<Contact> GetContacts()
        {
            List<Contact> contactList = new List<Contact>();
            var sql = "SELECT Id, Name, Address, Email, Mobile "
                         + "FROM Contacts ORDER BY Name";

            using (var con = new SqlConnection(GetConnectionString()))
            {
                using (var cmd = new SqlCommand(sql, con))
                {
                    con.Open();
                    var dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        var contact = new Contact
                        {
                            Id = (int)dr["Id"],
                            Name = dr["Name"].ToString(),
                            Address = dr["Address"].ToString(),
                            Email = dr["Email"].ToString(),
                            Mobile = dr["Mobile"].ToString()
                        };
                        contactList.Add(contact);
                    }
                    dr.Close();
                }
            }

            return contactList;
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public Contact GetContactById(int id)
        {
            var contact = new Contact();
            var sql = "SELECT * FROM Contacts WHERE Id = @Id ORDER BY Name";

            using (var con = new SqlConnection(GetConnectionString()))
            {
                using (var cmd = new SqlCommand(sql, con))
                {
                    con.Open();
                    cmd.Parameters.AddWithValue("Id", id);

                    var dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        contact.Id = (int)dr["Id"];
                        contact.Name = dr["Name"].ToString();
                        contact.Address = dr["Address"].ToString();
                        contact.Email = dr["Email"].ToString();
                        contact.Mobile = dr["Mobile"].ToString();
                    }

                    dr.Close();
                }
            }

            return contact;
        }

        [DataObjectMethod(DataObjectMethodType.Insert)]
        public void InsertContact(Contact contact)
        {
            var sql = "INSERT INTO Contacts (Name, Address, Email, Mobile) "
                         + "VALUES (@Name, @Address, @Email, @Mobile)";

            using (var con = new SqlConnection(GetConnectionString()))
            {
                using (var cmd = new SqlCommand(sql, con))
                {
                    con.Open();
                    cmd.Parameters.AddWithValue("Name", contact.Name);
                    cmd.Parameters.AddWithValue("Address", contact.Address);
                    cmd.Parameters.AddWithValue("Email", contact.Email);
                    cmd.Parameters.AddWithValue("Mobile", contact.Mobile);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        [DataObjectMethod(DataObjectMethodType.Delete)]
        public int DeleteContact(int id)
        {
            int deleteCount = 0;
            var sql = "DELETE FROM Contacts WHERE Id = @Id";

            using (var con = new SqlConnection(GetConnectionString()))
            {
                using (var cmd = new SqlCommand(sql, con))
                {
                    con.Open();
                    cmd.Parameters.AddWithValue("Id", id);
                    deleteCount = cmd.ExecuteNonQuery();
                }
            }

            return deleteCount;
        }

        public int UpdateContact(Contact contact)
        {
            int updateCount = 0;
            var sql = "UPDATE Contacts SET Name = @Name, Address = @Address, "
                + "Email = @Email, Mobile = @Mobile WHERE Id = @Id";

            using (var con = new SqlConnection(GetConnectionString()))
            {
                using (var cmd = new SqlCommand(sql, con))
                {
                    con.Open();
                    cmd.Parameters.AddWithValue("Id", contact.Id);
                    cmd.Parameters.AddWithValue("Name", contact.Name);
                    cmd.Parameters.AddWithValue("Address", contact.Address);
                    cmd.Parameters.AddWithValue("Email", contact.Email);
                    cmd.Parameters.AddWithValue("Mobile", contact.Mobile);
                    updateCount = cmd.ExecuteNonQuery();
                }
            }

            return updateCount;
        }
    }
}