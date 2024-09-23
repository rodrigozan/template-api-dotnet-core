using api.Models;
using api.ViewModels;
using System.Security.Claims;

namespace api.Extensions
{
    public static class RoleClaimsExtension
    {

        public static IEnumerable<Claim> GetClaims(this LoginViewModel user)
        {
            var result = new List<Claim> { };

            //Cria Claim personalizado
            //result.Add(new Claim("usuario", user.nome));               
            //result.Add(new Claim("perfil", user.Perfil.descricao));
            
            ////Necessario inserir as roles para o Authorize
            //result.Add(new Claim(ClaimTypes.Role, user.Perfil.descricao));

            ////Adicona uma Lista para o Claim
            //result.AddRange(
            //    user.Roles.Select(RoleClaimsExtension => new Claim(ClaimTypes.Role, role.nivel))
            //);

            return result;

        }
    }
}
