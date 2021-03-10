using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Data;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using WebApi.Dtos;


namespace WebApi.Controllers
{
    [ApiController]
    [Route("cloth")]
    public class ClothesController : Controller
    {

        private readonly ILogger<ClothesController> _logger;
        //private static Random _rng = new Random();

        private static List<Cloth> Items;

        public ClothesController(ILogger<ClothesController> logger)
        {
            _logger = logger;
        }

        // GET: Clothes
        [HttpGet]
        public IEnumerable<Cloth> Get()
        {
            var items = new List<Cloth>();
            using (SqlConnection conn = new SqlConnection(DBSettings.ConnectionString))
            {
                SqlCommand command = new SqlCommand("select Cloth.ID, Cloth.Size, ClothType.[Name] as [Type], FamilyMember.[Name] as [Owner], Color, ClothSeason.[Name] as [Season]from Cloth inner join ClothSeason on Season = ClothSeason.ID inner join ClothType on Cloth.[Type] = ClothType.ID inner join FamilyMember on Cloth.[Owner] = FamilyMember.ID", conn);
                conn.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var idColumn = reader.GetOrdinal("ID");
                    var id = reader.GetInt32(idColumn);      
                    var sizeColumn = reader.GetOrdinal("Size");
                    var size = reader.GetInt32(sizeColumn);
                    var typeColumn = reader.GetOrdinal("Type");
                    var type = reader.GetString(typeColumn);
                    var ownerColumn = reader.GetOrdinal("Owner");
                    var owner = reader.GetString(ownerColumn);
                    var colorColumn = reader.GetOrdinal("Color");
                    var color = reader.GetString(colorColumn);
                    var seasonColumn = reader.GetOrdinal("Season");
                    var season = reader.GetString(seasonColumn);
                    var item = new Cloth
                    {
                        ID = id,
                        Size = size,
                        Type = type,
                        Owner = owner,
                        Color = color,
                        Season = season
                    };
                    items.Add(item);
                }
                reader.Close();
            }
            return items;
        }

        [HttpGet("{id}")]
        // GET: Cloth/5
        public Cloth Get(int ID)
        {
            Cloth item = null;
            using (SqlConnection conn = new SqlConnection(DBSettings.ConnectionString))
            {
                SqlCommand command = new SqlCommand($"select Cloth.ID, Cloth.Size, ClothType.[Name] as [Type], FamilyMember.[Name] as [Owner], Color, ClothSeason.[Name] as [Season]from Cloth inner join ClothSeason on Season = ClothSeason.ID inner join ClothType on Cloth.[Type] = ClothType.ID inner join FamilyMember on Cloth.[Owner] = FamilyMember.ID where Cloth.ID = {ID};", conn);
                conn.Open();
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    var idColumn = reader.GetOrdinal("ID");
                    var id = reader.GetInt32(idColumn);
                    var sizeColumn = reader.GetOrdinal("Size");
                    var size = reader.GetInt32(sizeColumn);
                    var typeColumn = reader.GetOrdinal("Type");
                    var type = reader.GetString(typeColumn);
                    var ownerColumn = reader.GetOrdinal("Owner");
                    var owner = reader.GetString(ownerColumn);
                    var colorColumn = reader.GetOrdinal("Color");
                    var color = reader.GetString(colorColumn);
                    var seasonColumn = reader.GetOrdinal("Season");
                    var season = reader.GetString(seasonColumn);
                    item = new Cloth
                    {
                        ID = id,
                        Size = size,
                        Type = type,
                        Owner = owner,
                        Color = color,
                        Season = season
                    };
                }
                reader.Close();
            }
            return item;
        }

        [HttpPost("add")]
        // POST: Clothes
        public int Add([FromBody] Cloth clothItem)
        {
            using (SqlConnection conn = new SqlConnection(DBSettings.ConnectionString))
            {
                conn.Open();
                if (clothItem.ID == -1)
                {
                    // sanity checks for the input data
                    var sql = $"insert into Cloth(Cloth.Size, Cloth.[Type], Cloth.[Owner], Color, Cloth.Season, Inserted, LastUpdated) output INSERTED.ID values ({clothItem.Size}, (select ID from ClothType where ClothType.[Name] = '{clothItem.Type}'), (select ID from FamilyMember where FamilyMember.[Name] = '{clothItem.Owner}'), '{clothItem.Color}', (select ID from ClothSeason where ClothSeason.[Name] = '{clothItem.Season}'), GETUTCDATE(), GETUTCDATE());";
                    SqlCommand command = new SqlCommand(sql, conn);
                    var ID = (int)command.ExecuteScalar();
                    return (ID);
                }
                return -1;
            }

        }

        [HttpPut("edit/{id}")]
        // PUT: Clothes/5
        public void Edit(int id, Cloth clothItem)
        {
            using (SqlConnection conn = new SqlConnection(DBSettings.ConnectionString))
            {
                //include sanitize checks for data
                SqlCommand command = new SqlCommand($"update Cloth set size = {clothItem.Size}, Cloth.[Type] = (select ClothType.ID from ClothType where ClothType.[Name] = '{clothItem.Type}'), Cloth.[Owner] = (select FamilyMember.ID from FamilyMember where FamilyMember.[Name] = '{clothItem.Owner}'), color = '{clothItem.Color}', season = (select ClothSeason.ID from ClothSeason where ClothSeason.[Name] = '{clothItem.Season}'), LastUpdated = GETUTCDATE() where id = " + id, conn);
                conn.Open();
                var reader = command.ExecuteNonQuery();
            }
            // Items[id] = clothItem; 
        }

        [HttpDelete("delete/{id}")]
        public int Delete(int id)
        {
            using (SqlConnection conn = new SqlConnection(DBSettings.ConnectionString))
            {
                SqlCommand command = new SqlCommand("delete from Cloth where Cloth.ID =" + id, conn);
                conn.Open();
                var ID = command.ExecuteNonQuery();
                return id;
            }
        }

    }
}