using api.Models;
using api.ViewModels;
using api.Services;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using api.Functions;
using api.Repositories;
using Newtonsoft.Json;
using System.Data;
using System;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ITfoxtec.Identity.Saml2;
using Microsoft.Extensions.Options;
using System.Text;
using System;
using System.IO;


namespace api.Controllers
{
    [ApiController]
    public class LoginController : ControllerBase
    {

        private readonly byte[] Key;
        private readonly byte[] IV;

        public LoginController()
        {
            Key = Encoding.UTF8.GetBytes("0123456789abcdef");
            IV = Encoding.UTF8.GetBytes("abcdef0123456789");
        }

        /* Login JNJ */

        [HttpGet("login")]
        public IActionResult Login([FromQuery] string username, [FromQuery] string password)
        {
            try
            {
                LoginViewModel login = new();
                GlobalClass g = new();
                SqlConnection connection = new(Configuration.ConnectionString);

                string procedureName = "sp_yrd_validate_user";

                SqlCommand command = new(procedureName, connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                command.Parameters.Add("username", System.Data.SqlDbType.VarChar).Value = username;

                connection.Open();

                var result = command.ExecuteReader();

                connection.Close();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { authenticated = false, message = ex.Message.ToString() });
            }
        }

       [HttpGet("checkEmail")]
       public IActionResult CheckEmail([FromQuery] string email)
        {
            try
            {
                LoginViewModel login = new();
                GlobalClass g = new();

                using (var connection = new SqlConnection(Configuration.ConnectionString))
                {
                    using (var command = new SqlCommand("sp_yrd_select_user_by_email", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("userEmail", SqlDbType.VarChar).Value = email;

                        connection.Open();

                        using (var result = command.ExecuteReader())
                        {
                            while (result.Read())
                            {
                                Console.WriteLine(result.ToString());
                                if ((string)result["message"] == "")
                                {

                                    var mail = (string)result["email"];
                                    var pass = (string)result["email"];

                                    login.authenticated = true;
                                    login.name = (string)result["name"];
                                    login.email = (string)result["email"];
                                    login.profile = (string)result["profile"];
                                    login.language = (string)result["language_id"];
                                    Console.WriteLine("confirmando antes do token");
                                    if(login.expireToken != null)
                                    {
                                        login.token = new TokenService().GenerateToken(login.expireToken);
                                    }

                                    Console.WriteLine("E continua na confirmação");

                                }
                                else
                                {
                                    login.authenticated = false;
                                    login.message = (string)result["message"];
                                }
                            }

                            Console.WriteLine("login: " + login);

                            connection.Close();

                            return Ok(login);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { authenticated = false, message = ex.Message.ToString() });
            }
        }
       
    }
}