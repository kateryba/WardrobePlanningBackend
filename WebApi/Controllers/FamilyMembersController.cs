using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using WebApi.Dtos;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("family")]
    public class FamilyMembersController : Controller
    {

        private readonly ILogger<FamilyMembersController> _logger;
        //private static Random _rng = new Random();

        private static List<FamilyMember> Members;

        public FamilyMembersController(ILogger<FamilyMembersController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<FamilyMember> Get()
        {
            var members = new List<FamilyMember>();
            using (SqlConnection conn = new SqlConnection(DBSettings.ConnectionString))
            {
                SqlCommand command = new SqlCommand("select ID, [Name], Age, Size from FamilyMember", conn);
                conn.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var idColumn = reader.GetOrdinal("ID");
                    var id = reader.GetInt32(idColumn);
                    var nameColumn = reader.GetOrdinal("Name");
                    var name = reader.GetString(nameColumn);
                    var ageColumn = reader.GetOrdinal("Age");
                    var age = reader.GetInt32(ageColumn);
                    var sizeColumn = reader.GetOrdinal("Size");
                    var size = reader.GetInt32(sizeColumn);
                    var member = new FamilyMember
                    {
                        ID = id,
                        Name = name,
                        Age = age,
                        Size = size
                    };
                    members.Add(member);
                }
                reader.Close();
            }
            return members;
        }

        [HttpGet("{id}")]
        public FamilyMember Get(int id)
        {
            FamilyMember member = null;
            using (SqlConnection conn = new SqlConnection(DBSettings.ConnectionString))
            {
                SqlCommand command = new SqlCommand($"select [Name], Age, Size from FamilyMember where ID = {id}; ", conn);
                conn.Open();
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    var nameColumn = reader.GetOrdinal("Name");
                    var name = reader.GetString(nameColumn);
                    var ageColumn = reader.GetOrdinal("Age");
                    var age = reader.GetInt32(ageColumn);
                    var sizeColumn = reader.GetOrdinal("Size");
                    var size = reader.GetInt32(sizeColumn);
                    member = new FamilyMember
                    {
                        ID = id,
                        Name = name,
                        Age = age,
                        Size = size
                    };
                }
                reader.Close();
            }
            return member;
        }

        [HttpPost("add")]
        public int Add([FromBody] FamilyMember familyMember)
        {
            using (SqlConnection conn = new SqlConnection(DBSettings.ConnectionString))
            {
                conn.Open();
                if (familyMember.ID == -1)
                {
                    // sanity checks for the input data
                    var sql = $"insert into FamilyMember([Name], age, size, Inserted, LastUpdated) output INSERTED.ID values ('{familyMember.Name}', {familyMember.Age}, {familyMember.Size}, GETUTCDATE(), GETUTCDATE());";
                    SqlCommand command = new SqlCommand(sql, conn);
                    var ID = (int)command.ExecuteScalar();
                    return (ID);
                }
                return -1;
            }
        }

        [HttpPut("edit/{id}")]
        public void Edit(int id, [FromBody] FamilyMember familyMember)
        {
            using (SqlConnection conn = new SqlConnection(DBSettings.ConnectionString))
            {
                //include sanitize checks for data
                SqlCommand command = new SqlCommand($"update FamilyMember set age = {familyMember.Age}, [Name] = '{familyMember.Name}', size = '{familyMember.Size}', LastUpdated = GETUTCDATE() where id = " +id, conn);
                conn.Open();
                var reader = command.ExecuteNonQuery();
            }
        }

        [HttpDelete("delete/{id}")]
        public int Delete(int id)
        {
            using (SqlConnection conn = new SqlConnection(DBSettings.ConnectionString))
            {
                SqlCommand command = new SqlCommand("delete from FamilyMember where ID =" + id, conn);
                conn.Open();
                var ID = command.ExecuteNonQuery();
                return id;
            }
        }
    }
}