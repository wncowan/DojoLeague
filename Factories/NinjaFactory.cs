using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data;
using MySql.Data.MySqlClient;
using DojoLeague.Models;

namespace DojoLeague.Factory {
    public class NinjaFactory : IFactory<Ninja> {
        private string connectionString;
        public NinjaFactory()
        {
            connectionString = "server=localhost;userid=root;password=root;port=3306;database=dojoleaguedb;SslMode=None";
        }
        internal IDbConnection Connection
        {
            get {
                return new MySqlConnection(connectionString);
            }
        }
        public void Add(Ninja item){
            using (IDbConnection dbConnection = Connection){
                string query = $"INSERT INTO ninjas (Name, Level, Description, DojoId) VALUES (@Name, @Level, @Description, {item.Dojo.Id})";
                dbConnection.Open();
                dbConnection.Execute(query, item);
            }

        }
        public Ninja GetById(int id){
            using (IDbConnection dbConnection = Connection){
                dbConnection.Open();
                var query = $"SELECT * FROM ninjas JOIN dojos ON ninjas.DojoId = dojos.id WHERE ninjas.Id = {id}";
                var myNinja = dbConnection.Query<Ninja, Dojo, Ninja>(query, (ninja, dojo) => {ninja.Dojo = dojo; return ninja; }).FirstOrDefault();
                return myNinja;
            }
        }
        public IEnumerable<Ninja> GetAll(){
            using (IDbConnection dbConnection = Connection){
                var query = "SELECT * FROM ninjas JOIN dojos ON ninjas.DojoId = dojos.Id";
                dbConnection.Open();
                var ninjas = dbConnection.Query<Ninja, Dojo, Ninja>(query, (ninja, dojo) => {ninja.Dojo = dojo; return ninja; });
                return ninjas;
            }
        }
    }
    
}