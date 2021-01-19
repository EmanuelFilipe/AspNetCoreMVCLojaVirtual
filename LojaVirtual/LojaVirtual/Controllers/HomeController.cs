using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LojaVirtual.DataBase;
using LojaVirtual.Libraries.Email;
using LojaVirtual.Models;
using Microsoft.AspNetCore.Mvc;

namespace LojaVirtual.Controllers
{
    public class HomeController : Controller
    {
        private LojaVirtualContext _banco;

        public HomeController(LojaVirtualContext banco)
        {
            _banco = banco;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(NewsLetterEmail model)
        {
            if (ModelState.IsValid)
            {
                _banco.NewsLetterEmails.Add(new NewsLetterEmail { Email = model.Email });
                _banco.SaveChanges();

                TempData["msg_s"] = "E-mail cadastrado! Agora você irá receber promoções especiais no seu e-mail";

                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        public IActionResult Contato()
        {
            return View();
        }

        public IActionResult ContatoAcao()
        {
            try
            {
                Contato contato = new Contato
                {
                    Nome = HttpContext.Request.Form["nome"],
                    Email = HttpContext.Request.Form["email"],
                    Texto = HttpContext.Request.Form["texto"]
                };

                var listsaMensagem = new List<ValidationResult>();
                var contexto = new ValidationContext(contato);
                bool isValid = Validator.TryValidateObject(contato, contexto, listsaMensagem, true);

                if (isValid)
                {
                    ContatoEmail.EnviarContatoPorEmail(contato);
                    ViewData["msg_s"] = "Mensagem de contato enviado com sucesso!";
                }
                else
                {
                    StringBuilder sb = new StringBuilder();

                    foreach (var texto in listsaMensagem)
                    {
                        sb.Append(texto.ErrorMessage + " <br />");
                    }

                    ViewData["msg_e"] = sb.ToString();
                    ViewData["contato"] = contato;
                }
            }
            catch (Exception e)
            {
                ViewData["msg_e"] = e.Message.ToString();

                //TODO - Implementar log
            }

            return View("Contato");
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult CadastroCliente()
        {
            return View();
        }

        public IActionResult CarrinhoCompras()
        {
            return View();
        }
    }
}