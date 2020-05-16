using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace Disks
{
    class DBConnection
    {

        private static DBConnection db;
        private string connStr = "Server = localhost; Database = disks; Uid = root; Pwd = ;";

        private DBConnection() { }

        public static DBConnection GetInstance()
        {
            if (db == null)
            {
                db = new DBConnection();
            }
            return db;
        }
        public List<Disk> GetAllDisks()
        {
            string commandString = "SELECT * FROM collection;";
            MySqlCommand command = new MySqlCommand();
            MySqlConnection conn = new MySqlConnection(connStr);
            command.CommandText = commandString;
            command.Connection = conn;
            MySqlDataReader reader;
            command.Connection.Open();
            reader = command.ExecuteReader();
            int i = 0;
            List<Disk> disks = new List<Disk>();
            while (reader.Read())
            {
                disks.Add(new Disk((int)reader["id"], (string)reader["code"], (string)reader["title"],
                    (string)reader["company"], (int)reader["release_year"], (string)reader["type"]));
                i += 1;
            }
            reader.Close();
            return disks;
        }
        public int GetMaxId()
        {
            MySqlCommand command = new MySqlCommand();
            MySqlConnection conn = new MySqlConnection(connStr);
            string commandString = "SELECT MAX(ID) ID FROM collection;";
            command.CommandText = commandString;
            command.Connection = conn;
            MySqlDataReader reader;
            command.Connection.Open();
            reader = command.ExecuteReader();
            reader.Read();
            return (int)reader["id"];
        }

        public void Add(Disk disk)
        {
            MySqlCommand command = new MySqlCommand();
            MySqlConnection conn = new MySqlConnection(connStr);
            command = new MySqlCommand("INSERT INTO collection (`id`, `code`, `title`, `company`, `release_year`, `type`) VALUES (?, ?, ?, ?, ?, ?)", conn);
            command.Parameters.Add("@id", MySqlDbType.Int16, 11).Value = disk.id;
            command.Parameters.Add("@code", MySqlDbType.VarChar, 20).Value = disk.code;
            command.Parameters.Add("@title", MySqlDbType.VarChar, 20).Value = disk.title;
            command.Parameters.Add("@company", MySqlDbType.VarChar, 20).Value = disk.company;
            command.Parameters.Add("@release_year", MySqlDbType.Int16, 4).Value = disk.releaseYear;
            command.Parameters.Add("@type", MySqlDbType.VarChar, 20).Value = disk.type;
            conn.Open();
            command.ExecuteNonQuery();
        }
        public void Update(Disk disk)
        {
            MySqlCommand command = new MySqlCommand();
            MySqlConnection conn = new MySqlConnection(connStr);
            command = new MySqlCommand("UPDATE collection SET code = ?, title = ?, company = ?, release_year = ?, type = ? WHERE id = ?", conn);
            command.Parameters.Add("@code", MySqlDbType.VarChar, 20).Value = disk.code;
            command.Parameters.Add("@title", MySqlDbType.VarChar, 20).Value = disk.title;
            command.Parameters.Add("@company", MySqlDbType.VarChar, 20).Value = disk.company;
            command.Parameters.Add("@release_year", MySqlDbType.Int16, 4).Value = disk.releaseYear;
            command.Parameters.Add("@type", MySqlDbType.VarChar, 20).Value = disk.type;
            command.Parameters.Add("@id", MySqlDbType.Int16, 11).Value = disk.id;
            conn.Open();
            command.ExecuteNonQuery();
        }
        public List<string> SelectAllCompanies()
        {
            List<string> company = new List<string>();
            string commandString = "SELECT DISTINCT company FROM collection";
            MySqlCommand command = new MySqlCommand();
            MySqlConnection conn = new MySqlConnection(connStr);
            command.CommandText = commandString;
            command.Connection = conn;
            MySqlDataReader reader;
            command.Connection.Open();
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                company.Add((string)reader["company"]);
            }
            reader.Close();
            return company;
        }
        public List<string> SelectAllTypes()
        {
            List<string> types = new List<string>();
            string commandString = "SELECT DISTINCT type FROM collection";
            MySqlCommand command = new MySqlCommand();
            MySqlConnection conn = new MySqlConnection(connStr);
            command.CommandText = commandString;
            command.Connection = conn;
            MySqlDataReader reader;
            command.Connection.Open();
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                types.Add((string)reader["type"]);
            }
            reader.Close();
            return types;
        }
    }
}
