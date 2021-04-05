using LojaVirtual.Models;
using Microsoft.Extensions.Configuration;
using System.Net.Mail;

namespace LojaVirtual.Libraries.Email
{
    public class GerenciarEmail
    {
        private SmtpClient _smtp;
        private IConfiguration _configuration;

        public GerenciarEmail(SmtpClient smtp, IConfiguration configuration)
        {
            _smtp = smtp;
            _configuration = configuration;
        }

        public void EnviarContatoPorEmail(Contato contato)
        {
            // serviço que irá enviar a mensagem
            string bodyMsg = string.Format("<h2>Contato - LojaVirtual</h2>" +
                                           "<b>Nome: </b>{0} <br/>" +
                                           "<b>E-mail: </b>{1} <br/>" +
                                           "<b>Texto: </b>{2} <br/>" +
                                           "<br/>E-mail enviado automaticamente <br/>",
                                           contato.Nome, contato.Email, contato.Texto);

            //MailMessage = constrói a mensagem a ser enviada
            MailMessage mensagem = new MailMessage();
            mensagem.From = new MailAddress(_configuration.GetValue<string>("Email:Username"));
            mensagem.To.Add("emanuel_filipe@yahoo.com.br");
            mensagem.Subject = "Contato - LojaVirtual - Email: " + contato.Email;
            mensagem.Body = bodyMsg;
            mensagem.IsBodyHtml = true;

            // Enviar mensagem via SMTP
            _smtp.Send(mensagem);
        }

        public void EnviarSenhaParaColaboradorPorEmail(Colaborador colaborador)
        {
            string bodyMsg = string.Format("<h2>Colaborador - LojaVirtual</h2>" +
                                           "<b>Sua senha é: </b> <h3> {0} </h3> <br/>",
                                           colaborador.Senha);

            //MailMessage = constrói a mensagem a ser enviada
            MailMessage mensagem = new MailMessage();
            mensagem.From = new MailAddress(_configuration.GetValue<string>("Email:Username"));
            mensagem.To.Add(colaborador.Email);
            mensagem.Subject = "Colaborador - LojaVirtual - Senha do colaborador: " + colaborador.Nome;
            mensagem.Body = bodyMsg;
            mensagem.IsBodyHtml = true;

            // Enviar mensagem via SMTP
            _smtp.Send(mensagem);
        }
    }
}
