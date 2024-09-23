using api.Models;
using api.Repositories;
using api.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost("users")]
        public IActionResult GetUserList([FromBody] FilterUserModel filter, [FromQuery] string username)
        {
            try
            {
                List<UserViewModel> result = new UserRepository().GetList(filter, username);

                return Ok(new ResultViewModel<List<UserViewModel>>("", result));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<List<dynamic>>("Erro ao realizar busca", new ()));
            }
        }
        
        [HttpGet("user")]
        public IActionResult GetUser([FromQuery] string user, [FromQuery] string username)
        {
            try
            {
                UserViewModel? model = new UserRepository().GetUser(user, username);

                if (model == null || model.username == null)
                    return Ok(new ResultViewModel<UserViewModel?>("Usuário não encontrado", model));


                return Ok(new ResultViewModel<dynamic>("", model));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<object>("Erro ao realizar busca", new { }));
            }
        }

        [HttpPost("user")]
        public IActionResult InsertUser([FromBody] NewUserModel body, [FromQuery] string username)
        {
            try
            {
                UserRepository repo = new();
                string? result = repo.Add(body, username);

                if (result == null)
                    return Ok(new ResultViewModel<NewUserModel>("Registro não inserido", body));

                return Ok(new ResultViewModel<NewUserModel>(result, body));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResultViewModel<dynamic>(ex.Message, new { }));
            }
        }

        [HttpPut("user")]
        public IActionResult UpdateUser([FromBody] UpdateUserModel body, [FromQuery] string username)
        {
            try
            {
                UserRepository repo = new ();
                bool result = repo.Update(body, username);

                if (!result)
                    return Ok(new ResultViewModel<UpdateUserModel>("Registro não atualizado", body));

                return Ok(new ResultViewModel<UpdateUserModel>("", body));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResultViewModel<dynamic>(ex.Message, new { }));
            }
        }

    }
}
