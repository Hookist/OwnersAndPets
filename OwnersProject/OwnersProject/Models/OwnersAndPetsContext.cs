using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SQLite;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Configuration;

namespace OwnersProject.Models
{
    public class OwnersAndPetsContext : DbContext
    {
        string connectionString;
        public OwnersAndPetsContext()
        {
            this.connectionString = ConfigurationManager.ConnectionStrings["DBContext"].ConnectionString;
        }
        public OwnersAndPetsContext(string connectionString) 
        {
            this.connectionString = connectionString; 
        }

        public async Task<List<Owner>> SelectOwners()
        {
            List<Owner> owners = new List<Owner>();
            using (var dbConnection = new SQLiteConnection(connectionString))
            {
                dbConnection.Open();

                string sql = "SELECT * FROM Owners";
                SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
                var reader = await command.ExecuteReaderAsync();
                //Thread.Sleep(3000);
                while (reader.Read())
                    owners.Add(new Owner() { Id = Convert.ToInt32(reader["Id"]), Name = reader["Name"].ToString() });

                dbConnection.Close();
            }
            return owners;
        }

        
        public async Task<Owner> SelectOwner(int Id)
        {
            Owner owner = new Owner();
            using (var dbConnection = new SQLiteConnection(connectionString))
            {
                dbConnection.Open();

                string sql = $"SELECT * FROM Owners WHERE Id = {Id} LIMIT 1";
                SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
                var reader = await command.ExecuteReaderAsync();
                while (reader.Read())
                {
                    owner.Id = Convert.ToInt32(reader["Id"]);
                    owner.Name = reader["Name"].ToString();
                }
                dbConnection.Close();
            }

            return owner;
        }

        public async Task InsertOwner(Owner owner)
        {
            using (var dbConnection = new SQLiteConnection(connectionString))
            {
                dbConnection.Open();

                string sql = $"INSERT INTO Owners (Name) VALUES ('{owner.Name}')";
                SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
                await command.ExecuteNonQueryAsync();

                dbConnection.Close();
            }
        }

        public async Task UpdateOwner(int id, Owner owner)
        {
            using (var dbConnection = new SQLiteConnection(connectionString))
            {
                dbConnection.Open();

                string sql = $"UPDATE Owners SET Name = '{owner.Name}' WHERE Id = {id};";
                SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
                await command.ExecuteNonQueryAsync();

                dbConnection.Close();
            }
        }

        public async Task DeleteOwner(int id)
        {
        
            using (var dbConnection = new SQLiteConnection(connectionString))
            {
                dbConnection.Open();

                string sql = $"DELETE FROM Owners WHERE Id = {id};";
                SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
                await command.ExecuteNonQueryAsync();

                string sql2 = $"DELETE FROM Pets WHERE OwnerId = {id};";
                SQLiteCommand command2 = new SQLiteCommand(sql2, dbConnection);
                await command2.ExecuteNonQueryAsync();

                dbConnection.Close();
            }
           
        }


      


        public async Task<List<Pet>> SelectPets()
        {
            List<Pet> pets = new List<Pet>();
            using (var dbConnection = new SQLiteConnection(connectionString))
            {
                dbConnection.Open();

                string sql = "SELECT * FROM Pets";
                SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
                var reader = await command.ExecuteReaderAsync();
                while (reader.Read())
                    pets.Add(new Pet() { Id = Convert.ToInt32(reader["Id"]), Name = reader["Name"].ToString(), OwnerId = Convert.ToInt32(reader["OwnerId"]) });

                dbConnection.Close();
            }
            return pets;
        }

        public async Task<Pet> SelectPet(int Id)
        {
            Pet pet = new Pet();
            using (var dbConnection = new SQLiteConnection(connectionString))
            {
                dbConnection.Open();

                string sql = $"SELECT * FROM Pets WHERE Id = {Id} LIMIT 1";
                SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
                var reader = await command.ExecuteReaderAsync();
                while (reader.Read())
                {
                    pet.Id = Convert.ToInt32(reader["Id"]);
                    pet.Name = reader["Name"].ToString();
                    pet.OwnerId = Convert.ToInt32(reader["OwnerId"]);
                }
                dbConnection.Close();
            }

            return pet;
        }

        public async Task<List<Pet>> SelectByOwnerId(int ownerId)
        {
            List<Pet> pets = new List<Pet>();

            using (var dbConnection = new SQLiteConnection(connectionString))
            {
                dbConnection.Open();

                string sql = $"SELECT * FROM Pets WHERE OwnerId = {ownerId} ORDER BY Id";
                SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
                var reader = await command.ExecuteReaderAsync();
                while (reader.Read())
                    pets.Add(new Pet() { Id = Convert.ToInt32(reader["Id"]), Name = reader["Name"].ToString(), OwnerId = Convert.ToInt32(reader["OwnerId"]) });

                dbConnection.Close();
            }
            return pets;
        }

        public async Task InsertPet(Pet pet)
        {
            using (var dbConnection = new SQLiteConnection(connectionString))
            {
                dbConnection.Open();

                string sql = $"INSERT INTO Pets (Name, OwnerId) VALUES ('{pet.Name}', {Convert.ToInt32(pet.OwnerId)})";
                SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
                await command.ExecuteNonQueryAsync();

                dbConnection.Close();
            }
        }

        public async Task UpdatePet(int id, Pet pet)
        {
            using (var dbConnection = new SQLiteConnection(connectionString))
            {
                dbConnection.Open();

                string sql = $"UPDATE Pets SET Name = '{pet.Name}', OwnerId = '{pet.OwnerId}' WHERE Id = {id};";
                SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
                await command.ExecuteNonQueryAsync();

                dbConnection.Close();
            }
        }

        public async Task DeletePet(int id)
        {
            using (var dbConnection = new SQLiteConnection(connectionString))
            {
                dbConnection.Open();

                string sql = $"DELETE FROM Pets WHERE Id = {id};";
                SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
                await command.ExecuteNonQueryAsync();

                dbConnection.Close();
            }
        }




    }
}