using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data;
using MySql.Data.MySqlClient;
using DojoLeague.Models;

namespace DojoLeague.Factory {
    public class DojoFactory : IFactory<Ninja> {
        private string connectionString;
        public DojoFactory()
        {
            connectionString = "server=localhost;userid=root;password=root;port=3306;database=dojoleaguedb;SslMode=None";
        }
        internal IDbConnection Connection
        {
            get {
                return new MySqlConnection(connectionString);
            }
        }


        public void Add(Dojo item) {
            using (IDbConnection dbConnection = Connection){
                string query = "INSERT INTO dojos (Name, Location, Description) VALUES (@Name, @Location, @Description)";
                dbConnection.Open();
                dbConnection.Execute(query, item);
            }
        }
        public IEnumerable<Dojo> GetAll() {
            using (IDbConnection dbConnection = Connection){
                dbConnection.Open();
                return dbConnection.Query<Dojo>("SELECT * FROM dojos");
            }
        }
        
        public Dojo GetById(int id){
            using (IDbConnection dbConnection = Connection){
                string query = "SELECT * FROM dojos WHERE(Id=@Id)";
                dbConnection.Open();
                return dbConnection.Query<Dojo>(query, new{Id = id}).FirstOrDefault();
            }
        }

        public Dojo GetNinjasById(int dojo_id){
            using (IDbConnection dbConnection = Connection){
                var query = 
                @"
                SELECT * FROM dojos WHERE Id=@Id;
                SELECT * FROM ninjas WHERE DojoId = @Id;
                ";

                using (var multi = dbConnection.QueryMultiple(query, new {Id = dojo_id})){
                    var dojo = multi.Read<Dojo>().Single();
                    dojo.ninjas = multi.Read<Ninja>().ToList();
                    return dojo;
                }
            }
        }
        public IEnumerable<Ninja> GetRogueNinjas(){
            using (IDbConnection dbConnection = Connection){
                var query = "SELECT * FROM ninjas WHERE (DojoId = 1)";
                dbConnection.Open();
                return dbConnection.Query<Ninja>(query);
            }
        }
        public void BanishNinja(int id){
            using (IDbConnection dbConnection = Connection){
                var query = $"UPDATE ninjas SET DojoId = 1 WHERE ninjas.Id = {id}";
                dbConnection.Open();
                dbConnection.Execute(query);
            }
        }
        public void RecruitNinja(int dojo_id, int ninja_id){
            using (IDbConnection dbConnection = Connection){
                var query = $"UPDATE ninjas SET DojoId = {dojo_id} WHERE ninjas.Id = {ninja_id}";
                dbConnection.Open();
                dbConnection.Execute(query);
            }
        }

    }
}