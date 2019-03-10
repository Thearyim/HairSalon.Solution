using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace HairSalon.Models
{
    public class SpecialtyClass
    {
        private string _description;
        private int _id;

        public SpecialtyClass(string description, int id = 0)
        {
            _description = description;
            _id = id;
        }

        public static void DeleteAll()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;

            // 1) Delete from the join tables first
            // 2) Delete from the source table last
            cmd.CommandText =
                @"DELETE FROM stylists_specialties;
                  DELETE FROM specialty;";

            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static SpecialtyClass Find(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM specialty WHERE id = (@searchId);";
            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterName = "@searchId";
            searchId.Value = id;
            cmd.Parameters.Add(searchId);
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            int specialtyId = 0;
            string specialtyDescription = "";
            SpecialtyClass specialty = null;

            while (rdr.Read())
            {
                specialtyId = rdr.GetInt32(0);
                specialtyDescription = rdr.GetString(1);
                specialty = new SpecialtyClass(specialtyDescription, specialtyId);
            }

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return specialty;
        }

        public static List<SpecialtyClass> GetAll()
        {
            List<SpecialtyClass> allSpecialties = new List<SpecialtyClass> { };
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM specialty;";
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while (rdr.Read())
            {
                int specialtyId = rdr.GetInt32(0);
                string specialtyDescription = rdr.GetString(1);
                SpecialtyClass newSpecialty = new SpecialtyClass(specialtyDescription, specialtyId);
                allSpecialties.Add(newSpecialty);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allSpecialties;
        }

        public void Create()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO specialty (description) VALUES (@description);";
            MySqlParameter description = new MySqlParameter();
            description.ParameterName = "@description";
            description.Value = _description;
            cmd.Parameters.Add(description);
            _id = (int)cmd.LastInsertedId;

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
                @"DELETE FROM stylists_specialties WHERE specialty_id = @thisId;
                  DELETE FROM specialty WHERE id = @thisId;";

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

        public string GetDescription()
        {
            return _description;
        }

        public override int GetHashCode()
        {
            return GetId().GetHashCode();
        }

        public int GetId()
        {
            return _id;
        }

        public void SetDescription(string newDescription)
        {
            _description = newDescription;
        }
    }
}