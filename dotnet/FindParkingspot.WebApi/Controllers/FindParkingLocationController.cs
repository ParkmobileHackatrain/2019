using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace FindParkingspot.WebApi.Controllers
{
    [Route("api/foo")]
    [ApiController]
    public class FindParkingLocationController : ControllerBase
    {
        // GET api/values
        [HttpGet("{latitude}/{longitude}/{arrivalTime}")]
        public ActionResult<IEnumerable<string>> Get(string latitude, string longitude, string arrivalTime)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from ParkingHistory", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                      Console.WriteLine(reader["Id"]);
                    }
                }
            }

            return Ok("all good");

        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection("Server=hackatrain.cmwqmmnrgu5l.eu-west-2.rds.amazonaws.com;Database=robotaxi;Uid=root;Pwd=Letmein123!;");
        }
    }
}
