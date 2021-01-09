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
            smtp.Credentials = new NetworkCredential("cloudwalstorn@gmail.com", "senha");
            smtp.EnableSsl = true;

            //MailMessage = constroi a mensagem a ser enviada
            MailMessage mensagem = new MailMessage();

        }
    }
}
