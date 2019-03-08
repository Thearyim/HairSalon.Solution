using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;
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

    public static void ClearAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM specialties;";
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
      cmd.CommandText = @"SELECT * FROM specialties WHERE id = (@searchId);";
      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = id;
      cmd.Parameters.Add(searchId);
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int specialtyId = 0;
      string specialtyDescription = "";

      while (rdr.Read())
      {
        specialtyId = rdr.GetInt32(0);
        specialtyDescription = rdr.GetString(1);
      }
      SpecialtyClass newSpecialty = new SpecialtyClass(specialtyDescription, specialtyId);
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return newSpecialty;
    }

        public static List<SpecialtyClass> GetAll()
        {
            List<SpecialtyClass> allSpecialties = new List<SpecialtyClass> {};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM specialties;";
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

        public void AddStylist(Stylist newStylist)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO stylists_specialties (stylist_id, specialty_id) VALUES (@StylistId, @SpecialtyId);";
            MySqlParameter category_id = new MySqlParameter();
            stylist_id.ParameterName = "@StylistId";
            stylist_id.Value = newStylist.GetId();
            cmd.Parameters.Add(stylist_id);
            MySqlParameter specialty_id = new MySqlParameter();
            specialty_id.ParameterName = "@SpecialtyId";
            specialty_id.Value = _id;
            cmd.Parameters.Add(specialty_id);
            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public void Delete()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM specialties WHERE id = @SpecialtyId; DELETE FROM stylists_specialties WHERE specialty_id = @SpecialtyId;";
            MySqlParameter specialtyIdParameter = new MySqlParameter();
            specialtyIdParameter.ParameterName = "@SpecialtyId";
            specialtyIdParameter.Value = this.GetId();
            cmd.Parameters.Add(specialtyIdParameter);
            cmd.ExecuteNonQuery();
            if (conn != null)
            {
                conn.Close();
            }
        }

        public void Edit(string newDescription)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"UPDATE specialties SET description = @newDescription;";
            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterName = "@searchId";
            searchId.Value = _id;
            cmd.Parameters.Add(searchId);
            MySqlParameter description = new MySqlParameter();
            description.ParameterName = "@newDescription";
            description.Value = newDescription;
            cmd.Parameters.Add(description);
            cmd.ExecuteNonQuery();
            _description = newDescription;
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public override bool Equals(System.Object otherItem)
        {
            if (!(otherSpecialty is Specialty))
            {
                return false;
            }
            else
            {
                SpecialtyClass newSpecialty = (SpecialtyClass)otherSpecialty;
                bool idEquality = (this.GetId() == newSpecialty.GetId());
                bool descriptionEquality = (this.GetDescription() == newSpecialty.GetDescription());
                return (idEquality && descriptionEquality);
            }
        }

        public List<StylistClass> GetStylists()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT stylists.* FROM specialties
                JOIN stylists_specialties ON (specialties.id = stylists_specialties.specialty_id)
                JOIN stylists ON (stylists_specialties.stylist_id = stylists.id)
                WHERE specialties.id = @SpecialtyId;";
            MySqlParameter specialtyIdParameter = new MySqlParameter();
            specialtyIdParameter.ParameterName = "@specialtyId";
            specialtyIdParameter.Value = _id;
            cmd.Parameters.Add(specialtyIdParameter);
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            List<StylistClass> stylists = new List<StylistClass> {};
            while (rdr.Read())
            {
                int thisStylistId = rdr.GetInt32(0);
                string stylistName = rdr.GetString(1);
                StylistClass foundStylist = new StylistClass(stylistName, thisStylistId);
                stylists.Add(foundStylist);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return stylists;
        }


        public string GetDescription()
        {
          return _description;
        }

        public override int GetHashCode()
        {
            return this.GetId().GetHashCode();
        }

        public int GetId()
        {
            return _id;
        }

      public void Save()
      {
          MySqlConnection conn = DB.Connection();
          conn.Open();
          var cmd = conn.CreateCommand() as MySqlCommand;
          cmd.CommandText = @"INSERT INTO specialties (description) VALUES (@description);";

          MySqlParameter description = new MySqlParameter();
          description.ParameterName = "@description";
          description.Value = this._description;
          cmd.Parameters.Add(description);

          cmd.ExecuteNonQuery();
          _id = (int)cmd.LastInsertedId;
          conn.Close();
          if (conn != null)
          {
              conn.Dispose();
          }
      }

      public void SetDescription(string newDescription)
      {
        _description = newDescription;
      }
  }
}
