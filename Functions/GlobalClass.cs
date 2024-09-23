using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System.DirectoryServices.Protocols;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;

namespace api.Functions
{
    public class GlobalClass
    {

        public string Criptografar(string sTexto)
        {
            try
            {
                string sLetra;
                string sTextoCriptografado = "";
                int i;

                sTexto = sTexto.Trim();

                for (i = 1; i <= sTexto.Length; i++)
                {
                    sLetra = Strings.Asc(Strings.Mid(sTexto, i, 1)).ToString();
                    sLetra = Conversions.ToString(Strings.Chr(Conversions.ToInteger(Strings.Trim(Conversion.Str((long)Math.Round(Conversion.Val(sLetra)) ^ i * 2)))));
                    sTextoCriptografado = sTextoCriptografado + sLetra.ToUpper().Replace("|", @"\").Replace("{", "[").Replace("}", "]");
                }

                return sTextoCriptografado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic formatToDBNull(dynamic param)
        {
            if (param is null || param == -1 || param == 0)
            {
                return DBNull.Value;

            }

            return param;
        }

        public dynamic formatTo1(dynamic param)
        {
            if (param is null || param == -1 || param == 0)
            {
                return 1;

            }

            return param;
        }

        public dynamic formatToNegative1(dynamic param)
        {
            if (param is null || param == -1 || param == 0)
            {
                return -1;

            }

            return param;
        }

        public string formatDateToYYYYMMDD(string data)
        {
            var dia = data.Split("/")[0];
            var mes = data.Split("/")[1];
            var ano = data.Split("/")[2];

            return ano + '-' + mes + '-' + dia;
        }

        public string FormatDbToFront(string txt)
        {
            if(txt.Contains("_"))
            {
                string[] txtSplit = txt.Split("_");
                txt = txtSplit[0];

                for (int i = 1; txtSplit.Length > i; i++)
                {
                    txt += txtSplit[i].Substring(0, 1).ToUpper() + txtSplit[i].Substring(1);
                }
            }

            return txt;
        }
        public string GetBase64(string path, string description, string extension)
        {
            string fileName = description.Replace(" ", "_");
            string pathFile = Path.Combine(path, string.Concat(fileName, ".", extension));

            try
            {
                byte[] fileBytes = File.ReadAllBytes(pathFile);

                return Convert.ToBase64String(fileBytes);
            }
            catch (Exception ex) 
            {
                return ex.Message;
            }
        }

        public bool checkLoginAD(string username,
                                 string password)
        {
            try
            {
                LdapConnection oLDAPConnection = new LdapConnection(new LdapDirectoryIdentifier("jnjdir.jnj.com"));
                oLDAPConnection.Credential = new System.Net.NetworkCredential(userName: String.Concat("la\\", username), password);
                oLDAPConnection.AuthType = AuthType.Basic;
                oLDAPConnection.Bind();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

    }
}
