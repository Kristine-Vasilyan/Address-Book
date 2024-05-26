using System.Data;
using System.Data.SqlClient;

namespace WebAddressBook.DAL
{
    public static class Contacts
    {
        public static async Task<List<Contact>> GetAsyncList()
        {
            using SqlConnection con = CreateConnection();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[dbo].[GetContact]";
            using SqlDataReader reader = await cmd.ExecuteReaderAsync();
            List<Contact> contacts = [];
            while (await reader.ReadAsync())
            {
                Contact contact = new()
                {
                    Id = reader.GetInt32(reader.GetOrdinal("contact_id")),
                    FullName = reader.GetString(reader.GetOrdinal("full_name")),
                    EmailAddress = reader.GetString(reader.GetOrdinal("email_address")),
                    PhoneNumber = reader.GetInt32(reader.GetOrdinal("phone_number")),
                    PhysicalAddress = reader.GetString(reader.GetOrdinal("physical_address"))
                };
                contacts.Add(contact);
            }
            return contacts;
        }
        public static async Task<Contact?> GetAsync(int id)
        {
            Contact? result = null;
            using SqlConnection con = CreateConnection();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[dbo].[GetContactSingle]";
            cmd.Parameters.Add("@ID", SqlDbType.Int).Value = id;
            using SqlDataReader reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                result = new Contact
                {
                    Id = reader.GetInt32(reader.GetOrdinal("contact_id")),
                    FullName = reader.GetString(reader.GetOrdinal("full_name")),
                    EmailAddress = reader.GetString(reader.GetOrdinal("email_address")),
                    PhoneNumber = reader.GetInt32(reader.GetOrdinal("phone_number")),
                    PhysicalAddress = reader.GetString(reader.GetOrdinal("physical_address"))
                };
            }
            return result;
        }
        public static async Task<int> InsertAsync(Contact contact)
        {
            using SqlConnection con = CreateConnection();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[dbo].[InsertContact]";
            cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = contact.FullName;
            cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = contact.EmailAddress;
            cmd.Parameters.Add("@phone", SqlDbType.Int).Value = contact.PhoneNumber;
            cmd.Parameters.Add("@address", SqlDbType.VarChar).Value = contact.PhysicalAddress;
            cmd.Parameters.Add("@id", SqlDbType.Int).Direction = ParameterDirection.Output;
            await cmd.ExecuteNonQueryAsync();
            return (int)cmd.Parameters["@id"].Value;
        }
        public static async Task DeleteAsync(int id)
        {
            using SqlConnection con = CreateConnection();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[dbo].[DaleteContact]";
            cmd.Parameters.Add("@ID", SqlDbType.Int).Value = id;
            await cmd.ExecuteNonQueryAsync();
        }
        public static async Task UpdateAsync(Contact contact)
        {
            using SqlConnection con = CreateConnection();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[dbo].[UpdateContact]";
            cmd.Parameters.Add("@ID", SqlDbType.Int).Value = contact.Id;
            cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = contact.FullName;
            cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = contact.EmailAddress;
            cmd.Parameters.Add("@phone", SqlDbType.Int).Value = contact.PhoneNumber;
            cmd.Parameters.Add("@address", SqlDbType.VarChar).Value = contact.PhysicalAddress;
            await cmd.ExecuteNonQueryAsync();
        }
        private static SqlConnection CreateConnection()
        {
            SqlConnection conn = new("Data Source=SQL8003.site4now.net;Initial Catalog=db_aa25a9_kristinev;User Id=db_aa25a9_kristinev_admin;Password=TwZ_kWgLaHfU85n");
            conn.Open();
            return conn;
        }
    }
}
