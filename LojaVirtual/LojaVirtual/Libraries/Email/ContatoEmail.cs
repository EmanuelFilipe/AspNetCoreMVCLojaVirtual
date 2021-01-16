using LojaVirtual.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace LojaVirtual.Libraries.Email
{
    public class ContatoEmail
    {
        public static void EnviarContatoPorEmail(Contato contato)
        {
            // servidor que vai enviar a mensagem
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("cloudwalstorn@gmail.com", "");
            smtp.EnableSsl = true;

            string bodyMsg = string.Format("<h2>Contato - LojaVirtual</h2>" +
                                           "<b>Nome: </b>{0} <br/>" +
                                           "<b>E-mail: </b>{1} <br/>" +
                                           "<b>Texto: </b>{2} <br/>" +
                                           "<br/>E-mail enviado automaticamente <br/>",
                                           contato.Nome, contato.Email, contato.Texto);

            //MailMessage = constrói a mensagem a ser enviada
            MailMessage mensagem = new MailMessage();
            mensagem.From = new MailAddress("cloudwalstorn@gmail.com");
            mensagem.To.Add("cloudwalstorn@gmail.com");
            mensagem.Subject = "Contato - LojaVirtual - Email: " + contato.Email;
            mensagem.Body = bodyMsg;
            mensagem.IsBodyHtml = true;

            // Enviar mensagem via SMTP
            smtp.Send(mensagem);
        }
    }
}
