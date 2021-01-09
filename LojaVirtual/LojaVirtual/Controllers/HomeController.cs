using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LojaVirtual.Models;
using Microsoft.AspNetCore.Mvc;

namespace LojaVirtual.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Contato()
        {
            return View();
        }

        public IActionResult ContatoAcao()
        {
            Contato contato = new Contato
            {
                Nome = HttpContext.Request.Form["nome"],
                Email = HttpContext.Request.Form["email"],
                Texto = HttpContext.Request.Form["texto"]
            };

            return new ContentResult() { Content = $"Dados recebidos com sucesso!<br /> Nome: {contato.Nome} <br />Email: {contato.Email} <br />Texto: {contato.Texto}", ContentType = "text/html" };
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