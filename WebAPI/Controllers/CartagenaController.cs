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
    public class CartagenaController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public CartagenaController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]   // Metodo API para tomar detalles de la tabla que se encuentra en la base de datos
        public JsonResult Get()
        {
            string query = @"
                    select 
                    convert(varchar(10),Fecha,120) as Fecha,
                    IDInfo, Titulo, Autor, Descripcion
                    from dbo.noticias
                    ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("InfoCartagena");
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

            return new JsonResult(table);
        }


        [HttpPost]
        public JsonResult Post(Cartagena emp)
        {
            string query = @"
                    insert into dbo.noticias
                    (Fecha,Titulo,Autor,Descripcion)
                    values 
                    (
                    '" + emp.Fecha + @"'
                    ,'" + emp.Titulo + @"'
                    ,'" + emp.Autor + @"'
                    ,'" + emp.Descripcion + @"'

                    )
                    ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("InfoCartagena");
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


        [HttpPut]
        public JsonResult Put(Cartagena emp)
        {
            string query = @"
                    update dbo.noticias set 
                    Fecha = '" + emp.Fecha + @"'
                    ,Noticias = '" + emp.Titulo + @"'
                    ,Clima = '" + emp.Autor + @"'
                    ,Clima = '" + emp.Descripcion + @"'
                    where IDInfo = " + emp.IDInfo + @" 
                    ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("InfoCartagena");
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

            return new JsonResult("Actualización Exitosa");
        }


        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"
                    delete from dbo.noticias
                    where IDInfo = " + id + @" 
                    ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("InfoCartagena");
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

            return new JsonResult("Borrado Exitoso");
        }



        [Route("GetNoticias")]
        public JsonResult GetNoticias()
        {
            string query = @"
                    select Fecha,Titulo,Autor,Descripcion from dbo.noticias
                    ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("InfoCartagena");
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

            return new JsonResult(table);
        }

        [Route("GetClima")]

        public JsonResult GetClima()
        {
            string query = @"
                    select Temperatura,Descripcion from dbo.clima
                    ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("InfoCartagena");
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

            return new JsonResult(table);
        }
    }
}

