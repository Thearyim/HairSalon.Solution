using System;
using System.Collections.Generic;
using System.Linq;
using MySql.Data.MySqlClient;

namespace HairSalon.Models
{
    public class StylistClass
    {
        private int _id;
        private string _name;
        private List<SpecialtyClass> _specialties;

        public StylistClass(string name, List<SpecialtyClass> specialties = null)
        {
            _name = name;
            _specialties = specialties;
        }

        public StylistClass(int id, string name, List<SpecialtyClass> specialties = null)
        {
            _id = id;
            _name = name;
            _specialties = specialties;
        }

        public void AddSpecialties(params SpecialtyClass[] specialties)
        {
            if (_specialties == null)
            {
                _specialties = new List<SpecialtyClass>();
            }

            _specialties.AddRange(specialties);
        }

        public int GetId()
        {
            return _id;
        }

        public string GetName()
        {
            return _name;
        }

        public List<SpecialtyClass> GetSpecialties()
        {
            return _specialties ?? new List<SpecialtyClass>();
        }

        public void AddClient(int clientId)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO stylists_clients (stylist_id, client_id) VALUES (@stylist_id, @client_id);";

            MySqlParameter stylistId = new MySqlParameter();
            stylistId.ParameterName = "@stylist_id";
            stylistId.Value = this._id;
            cmd.Parameters.Add(stylistId);

            MySqlParameter client = new MySqlParameter();
            client.ParameterName = "@client_id";
            client.Value = clientId;
            cmd.Parameters.Add(client);

            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public void AddSpecialty(int specialtyId)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO stylists_specialties (stylist_id, specialty_id) VALUES (@stylist_id, @specialty_id);";

            MySqlParameter stylistId = new MySqlParameter();
            stylistId.ParameterName = "@stylist_id";
            stylistId.Value = this._id;
            cmd.Parameters.Add(stylistId);

            MySqlParameter specialty = new MySqlParameter();
            specialty.ParameterName = "@specialty_id";
            specialty.Value = specialtyId;
            cmd.Parameters.Add(specialty);

            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
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
                  DELETE FROM stylists_specialties;
                  DELETE FROM stylist;";

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

            // 1) Delete from the join tables first
            // 2) Delete from the source table last
            cmd.CommandText =
                @"DELETE FROM stylists_clients WHERE stylist_id = @thisId;
                  DELETE FROM stylists_specialties WHERE stylist_id = @thisId;
                  DELETE FROM stylist WHERE id = @thisId;";

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

        public static StylistClass Find(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;

            // Example Dataset:
            // id         name             id      description
            // ---------- ---------------- ------- ---------------------
            // 1          Sophie           1        Cut
            // 1          Sophie           2        Color
            cmd.CommandText =
                @"SELECT stylist.id, stylist.name, specialty.id, specialty.description
                  FROM stylist 
                  JOIN stylists_specialties ON (stylist.id = stylists_specialties.stylist_id)
                  JOIN specialty ON (specialty.id = stylists_specialties.specialty_id)
                  WHERE stylist.id = (@searchId);";

            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterName = "@searchId";
            searchId.Value = id;
            cmd.Parameters.Add(searchId);

            var rdr = cmd.ExecuteReader() as MySqlDataReader;

            int stylistId = 0;
            string stylistName = "";
            int specialtyId = 0;
            string specialtyDescription = "";
            StylistClass stylist = null;
            List<SpecialtyClass> stylistSpecialties = new List<SpecialtyClass>();

            while (rdr.Read())
            {
                if (stylistId <= 0)
                {
                    stylistId = rdr.GetInt32(0);
                    stylistName = rdr.GetString(1);
                }

                specialtyId = rdr.GetInt32(2);
                specialtyDescription = rdr.GetString(3);
                stylistSpecialties.Add(new SpecialtyClass(specialtyDescription, specialtyId));
            }

            if (rdr.HasRows)
            {
                stylist = new StylistClass(stylistId, stylistName, stylistSpecialties);
            }

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }

            return stylist;
        }

        public static List<StylistClass> GetAll()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;

            // Example Dataset:
            // id         name             id      description
            // ---------- ---------------- ------- ---------------------
            // 1          Sophie           1        Cut
            // 1          Sophie           2        Color
            // 2          Jacob            1        Cut
            // 2          Jacod            2        Color
            cmd.CommandText =
               @"SELECT stylist.id, stylist.name, specialty.id, specialty.description
                 FROM stylist 
                 LEFT JOIN stylists_specialties ON (stylist.id = stylists_specialties.stylist_id)
                 LEFT JOIN specialty ON (specialty.id = stylists_specialties.specialty_id)
                 ORDER BY stylist.id ASC;";
            
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            int stylistId = 0;
            string stylistName = "";
            List<StylistClass> stylists = new List<StylistClass>(); 

            while (rdr.Read())
            {
                stylistId = rdr.GetInt32(0);
                stylistName = rdr.GetString(1);

                StylistClass matchingStylist = stylists.FirstOrDefault(stylist => stylist._id == stylistId);
                if (matchingStylist == null)
                {
                    matchingStylist = new StylistClass(stylistId, stylistName);
                    stylists.Add(matchingStylist);
                }

                object specialtyId = rdr.GetValue(2);
                object specialtyDescription = rdr.GetValue(3);

                if (specialtyId != DBNull.Value && specialtyDescription != DBNull.Value)
                {
                    matchingStylist.AddSpecialties(new SpecialtyClass(specialtyDescription.ToString(), (int)specialtyId));
                }
            }

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }

            return stylists;
        }

        public List<ClientClass> GetClients()
        {
            List<ClientClass> allStylistClients = new List<ClientClass> { };
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText =
                @"SELECT client.id, client.name 
                  FROM client
                  JOIN stylists_clients ON(client.id = stylists_clients.client_id)
                  JOIN stylist ON(stylist.id = stylists_clients.stylist_id)
                  WHERE stylist_id = (@stylist_id)";

            MySqlParameter stylistId = new MySqlParameter();
            stylistId.ParameterName = "@stylist_id";
            stylistId.Value = this._id;
            cmd.Parameters.Add(stylistId);
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while (rdr.Read())
            {
                int clientId = rdr.GetInt32(0);
                string clientName = rdr.GetString(1);
                int clientStylistId = rdr.GetInt32(2);
                ClientClass newClient = new ClientClass(clientId, clientName);
                allStylistClients.Add(newClient);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allStylistClients;
        }

        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            if (_id > 0)
            {
                cmd.CommandText = @"UPDATE stylist SET name = @name WHERE id = @searchId;";
                MySqlParameter name = new MySqlParameter();
                name.ParameterName = "@name";
                name.Value = this._name;
                cmd.Parameters.Add(name);

                MySqlParameter stylistId = new MySqlParameter();
                stylistId.ParameterName = "@searchId";
                stylistId.Value = this._id;
                cmd.Parameters.Add(stylistId);
            }
            else
            {
                cmd.CommandText = @"INSERT INTO stylist (name) VALUES (@name);";

                MySqlParameter name = new MySqlParameter();
                name.ParameterName = "@name";
                name.Value = this._name;
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