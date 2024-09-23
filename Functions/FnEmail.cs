using System.Net.Mail;

namespace api.Functions
{
    public class FnEmail
    {
        public void Send(string sTo,
                      string sCc,
                      string sBCc,
                      string sSubject,
                      string sBody,
                      string sAnexo,
                      bool bIsHTML,
                      string sServidor = "",
                      int iPorta = 0,
                      string sSenha = "",
                      string sUsuario = "",
                      bool bSSL = false)
        {
            try
            {
                // Configura Servidor de E-mail
                if (string.IsNullOrEmpty(sServidor))
                {
                    sServidor = "smtp.na.jnj.com";
                    sUsuario = "syspack@its.jnj.com";
                    iPorta = 25;
                    bSSL = false;
                }

                // Cria uma instância do objeto MailMessage
                MailMessage mMailMessage = new ();

                // Configura Dados do E-mail
                mMailMessage.From = new MailAddress("syspack@its.jnj.com");

                if (!string.IsNullOrEmpty(sTo))
                {
                    if (sTo.Contains(";"))
                    {
                        string[] sPara = sTo.Split(';');
                        foreach (string email in sPara)
                        {
                            mMailMessage.To.Add(new MailAddress(email));
                        }
                    }
                    else
                    {
                        mMailMessage.To.Add(new MailAddress(sTo));
                    }
                }
                if (!string.IsNullOrEmpty(sCc))
                {
                    if (sCc.Contains(";"))
                    {
                        string[] sCopia = sCc.Split(';');
                        foreach (string email in sCopia)
                        {
                            mMailMessage.CC.Add(new MailAddress(email));
                        }
                    }
                    else
                    {
                        mMailMessage.CC.Add(new MailAddress(sCc));
                    }
                }
                if (!string.IsNullOrEmpty(sBCc))
                {
                    if (sBCc.Contains(";"))
                    {
                        string[] sCopiaOculta = sBCc.Split(';');
                        foreach (string email in sCopiaOculta)
                        {
                            mMailMessage.Bcc.Add(new MailAddress(email));
                        }
                    }
                    else
                    {
                        mMailMessage.Bcc.Add(new MailAddress(sBCc));
                    }
                }
                if (!string.IsNullOrEmpty(sAnexo))
                {
                    if (sAnexo.Contains(";"))
                    {
                        string[] sArquivoAnexo = sAnexo.Split(';');
                        foreach (string arquivo in sArquivoAnexo)
                        {
                            mMailMessage.Attachments.Add(new Attachment(arquivo));
                        }
                    }
                    else
                    {
                        mMailMessage.Attachments.Add(new Attachment(sAnexo));
                    }
                }
                mMailMessage.Subject = sSubject;
                mMailMessage.Priority = MailPriority.High;
                mMailMessage.IsBodyHtml = bIsHTML;

                // Define o corpo da mensagem
                mMailMessage.Body = sBody;

                // Cria uma instância de SmtpClient
                SmtpClient mSmtpClient = new ();

                // Configuração do Servidor
                mSmtpClient.Host = sServidor;
                mSmtpClient.Port = iPorta;
                mSmtpClient.EnableSsl = bSSL;
                if (!string.IsNullOrEmpty(sSenha))
                {
                    mSmtpClient.Credentials = new System.Net.NetworkCredential(sUsuario, sSenha);
                    mSmtpClient.EnableSsl = bSSL;
                }

                // Envia o e-mail
                mSmtpClient.Send(mMailMessage);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
