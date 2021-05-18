using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BogotaController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public BogotaController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                    select 
                    convert(varchar(10),Fecha,120) as Fecha,
                    IDInfo, Noticias, Clima
                    from dbo.bogota
                    ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("InfoCiudad");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); ;

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }


        [HttpPost]
        public JsonResult Post(Bogota emp)
        {
            string query = @"
                    insert into dbo.bogota
                    (Fecha,Noticias,Clima)
                    values 
                    (
                    '" + emp.Fecha + @"'
                    ,'" + emp.Noticias + @"'
                    ,'" + emp.Clima + @"'
                    
                    )
                    ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("InfoCiudad");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); ;

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Adición Exitosa");
        }


        [HttpPut]
        public JsonResult Put(Bogota emp)
        {
            string query = @"
                    update dbo.newyork set 
                    Fecha = '" + emp.Fecha + @"'
                    ,Noticias = '" + emp.Noticias + @"'
                    ,Clima = '" + emp.Clima + @"'
                    where IDInfo = " + emp.IDInfo + @" 
                    ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("InfoCiudad");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); ;

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Actualización Exitosa");
        }


        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"
                    delete from dbo.newyork
                    where IDInfo = " + id + @" 
                    ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("InfoCiudad");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); ;

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Borrado Exitoso");
        }



        [Route("GetAllinfo")]
        public JsonResult GetNombresdelasAreas()
        {
            string query = @"
                    select Fecha,Noticias,Clima from dbo.bogota
                    ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("InfoCiudad");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); ;

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }


    }
}
