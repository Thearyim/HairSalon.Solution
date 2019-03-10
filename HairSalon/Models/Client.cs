using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace HairSalon.Models
{
    public class ClientClass
    {
        private int _id;
        private string _name;

        public ClientClass(string name)
        {
            _name = name;
        }

        public ClientClass(int id, string name)
        {
            _id = id;
            _name = name;
        }

        public int GetId()
        {
            return _id;
        }

        public string GetName()
        {
            return _name;
        }

        public void SetName(string name)
        {
            _name = name;
        }

        public static void DeleteAll()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;

            // 1) Delete from the join tables first
            // 2) Delete from the source table last
            cmd.CommandText =
                @"DELETE FROM stylists_clients;
                  DELETE FROM client;";

            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public void Delete(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText =
                @"DELETE FROM stylists_clients WHERE client_id = @thisId;
                  DELETE FROM client WHERE id = @thisId;";

            MySqlParameter thisId = new MySqlParameter();
            thisId.ParameterName = "@thisId";
            thisId.Value = id;
            cmd.Parameters.Add(thisId);

            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static ClientClass Find(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM client WHERE id = (@searchId);";
            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterName = "@searchId";
            searchId.Value = id;
            cmd.Parameters.Add(searchId);
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            int clientId = 0;
            string clientName = "";
            ClientClass client = null;

            while (rdr.Read())
            {
                clientId = rdr.GetInt32(0);
                clientName = rdr.GetString(1);
                client = new ClientClass(clientId, clientName);
            }

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return client;
        }

        public static List<ClientClass> GetAll()
        {
            List<ClientClass> allClients = new List<ClientClass>();
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM client;";
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while (rdr.Read())
            {
                int clientId = rdr.GetInt32(0);
                string clientName = rdr.GetString(1);
                ClientClass newClient = new ClientClass(clientId, clientName);
                allClients.Add(newClient);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allClients;
        }

        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            if (_id > 0)
            {
                cmd.CommandText = @"update client set name = @name where id = @searchId;";
                MySqlParameter name = new MySqlParameter();
                name.ParameterName = "@name";
                name.Value = _name;
                cmd.Parameters.Add(name);

                MySqlParameter clientId = new MySqlParameter();
                clientId.ParameterName = "@searchId";
                clientId.Value = _id;
                cmd.Parameters.Add(clientId);
            }
            else
            {
                cmd.CommandText = @"INSERT INTO client (name) VALUES (@name);";
                MySqlParameter name = new MySqlParameter();
                name.ParameterName = "@name";
                name.Value = _name;
                cmd.Parameters.Add(name);
                _id = (int)cmd.LastInsertedId;
            }

            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
    }
}