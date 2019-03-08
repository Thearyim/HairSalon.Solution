using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;
using HairSalon.Models;

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

        public void SetDescription(string newDescription)
        {
            _description = newDescription;
        }
    }
}
