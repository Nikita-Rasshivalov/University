using Dapper;
using PersonalAPI.Models;
using System.Data;
using System.Data.SqlClient;

namespace PersonalAPI.Services
{
    public class PersonalService : IPersonalService
    {
        private readonly string _connectionString;

        public PersonalService(PersonalServiceOptions options)
        {
            _connectionString = options.ConnectionString;
        }

        public async Task<List<Personal>> GetAsync()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return (await db.QueryAsync<Personal>("SELECT * FROM Personal")).ToList();
            }
        }

        public async Task<Personal?> GetAsync(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return (await db.QueryAsync<Personal>("SELECT * FROM Personal WHERE PersonId = @id", new { id })).FirstOrDefault();
            }
        }

        public async Task<Personal> CreateAsync(Personal person)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sqlQuery = """INSERT INTO Personal (PersonName, PersonSurname, DepartmentNumber, Speciality) VALUES(@PersonName, @PersonSurname, @DepartmentNumber, @Speciality); SELECT CAST(SCOPE_IDENTITY() as int)""";
                int? personId = (await db.QueryAsync<int>(sqlQuery, person)).FirstOrDefault();
                person.PersonId = personId.Value;
            }

            return person; 
        }

        public async Task UpdateAsync(Personal person)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sqlQuery = """UPDATE Personal SET  PersonName = @PersonName, PersonSurname = @PersonSurname, DepartmentNumber = @DepartmentNumber, Speciality = @Speciality WHERE PersonId = @PersonId""";
                await db.ExecuteAsync(sqlQuery, person);
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sqlQuery = "DELETE FROM Personal WHERE PersonId = @id";
                await db.ExecuteAsync(sqlQuery, new { id });
            }
        }
    }
}
