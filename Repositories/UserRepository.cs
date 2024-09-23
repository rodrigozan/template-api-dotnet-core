using api.Functions;
using api.Models;
using api.Services;
using api.ViewModels;
using System.Data.SqlClient;

namespace api.Repositories
{
    public class UserRepository
    {
        public List<UserViewModel> GetList(FilterUserModel filter, string username)
        {
            try
            {
                List<UserViewModel> users = new();
                SqlConnection connection = new(Configuration.ConnectionString);
                string procedureName = "sp_yrd_select_users";
                SqlCommand command = new(procedureName, connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                command.Parameters.Add("name", System.Data.SqlDbType.VarChar).Value = filter.name;
                command.Parameters.Add("email", System.Data.SqlDbType.VarChar).Value = filter.email;
                command.Parameters.Add("profile_id", System.Data.SqlDbType.Int).Value = filter.profileId;
                command.Parameters.Add("language_id", System.Data.SqlDbType.VarChar).Value = filter.language;
                command.Parameters.Add("active", System.Data.SqlDbType.Bit).Value = filter.active;

                connection.Open();
                var result = command.ExecuteReader();
                
                connection.Close();

                return users;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                return new List<UserViewModel>();
            }
        }


        public UserViewModel? GetUser(string user, string username)
        {
            try
            {
                UserViewModel resultUser = new ();
                SqlConnection connection = new(Configuration.ConnectionString);
                string procedureName = "sp_yrd_select_user";
                SqlCommand command = new(procedureName, connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                command.Parameters.Add("user", System.Data.SqlDbType.VarChar).Value = user;

                connection.Open();
                var result = command.ExecuteReader();

                connection.Close();

                return resultUser;
            }
            catch
            {
                return null;
            }
        }

        public string? Add(NewUserModel user, string username_input)
        {
            try
            {
                string message = "";
                GlobalClass g = new ();
                SqlConnection connection = new(Configuration.ConnectionString);
                string procedureName = "sp_yrd_insert_user";
                SqlCommand command = new(procedureName, connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                command.Parameters.Add("name", System.Data.SqlDbType.VarChar).Value = user.name;
                command.Parameters.Add("email", System.Data.SqlDbType.VarChar).Value = user.email;
                command.Parameters.Add("password", System.Data.SqlDbType.VarChar).Value = g.Criptografar(user.password is null ? "" : user.password);
                command.Parameters.Add("profile_id", System.Data.SqlDbType.Int).Value = user.profileId;
                command.Parameters.Add("language_id", System.Data.SqlDbType.VarChar).Value = user.language;
                command.Parameters.Add("login_ad", System.Data.SqlDbType.Bit).Value = user.loginAd;
                command.Parameters.Add("active", System.Data.SqlDbType.Bit).Value = user.active;

                connection.Open();
                var result = command.ExecuteReader();

                while (result.Read())
                {
                    message = (string)result["message"];
                }

                connection.Close();

                return message;
            }
            catch { return null; }
        }

        public bool Update(UpdateUserModel user, string username_update)
        {
            try
            {
                GlobalClass g = new();
                SqlConnection connection = new(Configuration.ConnectionString);
                connection.Open();

                foreach (var branch in user.branchs)
                {

                    string procedureName = "sp_yrd_update_user";
                    SqlCommand command = new(procedureName, connection)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure
                    };

                    command.Parameters.Add("name", System.Data.SqlDbType.VarChar).Value = user.name;
                    command.Parameters.Add("email", System.Data.SqlDbType.VarChar).Value = user.email;
                    command.Parameters.Add("password", System.Data.SqlDbType.VarChar).Value = user.password is null ? "" : g.Criptografar(user.password);
                    command.Parameters.Add("profile_id", System.Data.SqlDbType.Int).Value = user.profileId;
                    command.Parameters.Add("language_id", System.Data.SqlDbType.VarChar).Value = user.language;
                    command.Parameters.Add("login_ad", System.Data.SqlDbType.Bit).Value = user.loginAd;
                    command.Parameters.Add("active", System.Data.SqlDbType.Bit).Value = user.active;
                    
                    var result = command.ExecuteNonQuery();

                }

                connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocorreu um erro durante o update: " + ex.Message);
                return false;
            }
        }

    }
}
