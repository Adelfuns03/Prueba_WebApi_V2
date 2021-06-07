using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistorialController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public HistorialController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]   // Metodo API para tomar detalles de la tabla que se encuentra en la base de datos
        public JsonResult Get()
        {
            string query = @"
                    select 
                    id, Ciudad, Info
                    from dbo.historia
                    ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("Historial"); // Se define una variable para almacenar la cadena de conexión de la base de datos
            SqlDataReader myReader;
            using (SqlConnection myCon2 = new SqlConnection(sqlDataSource)) // Se ejecuta la consulta y completa los resultados en un data table
            {
                myCon2.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon2))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); ;

                    myReader.Close();
                    myCon2.Close();
                }
            }

            return new JsonResult(table);
        }


        [HttpPost]
        public JsonResult Post(Historial emp)
        {
            string query = @"
                    insert into dbo.historia
                    (Ciudad,Info)
                    values 
                    (
                    '" + emp.Ciudad + @"'
                    ,'" + emp.Info + @"'

                    )
                    ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("Historial");
            SqlDataReader myReader;
            using (SqlConnection myCon2 = new SqlConnection(sqlDataSource))
            {
                myCon2.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon2))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); ;

                    myReader.Close();
                    myCon2.Close();
                }
            }

            return new JsonResult("Adición Exitosa");
        }


    }
}
