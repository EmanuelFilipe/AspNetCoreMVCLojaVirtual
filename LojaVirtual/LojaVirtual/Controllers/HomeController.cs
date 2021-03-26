using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LojaVirtual.DataBase;
using LojaVirtual.Libraries.Email;
using LojaVirtual.Libraries.Filtro;
using LojaVirtual.Libraries.Login;
using LojaVirtual.Models;
using LojaVirtual.Repositories.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LojaVirtual.Controllers
{
    public class HomeController : Controller
    {
        private IClienteRepository _repositoryCliente;
        private INewsLetterRepository _newsLetterRepository;
        private LoginCliente _loginCliente;

        public HomeController(IClienteRepository repositoryCliente,
                              INewsLetterRepository newsLetterRepository,
                              LoginCliente loginCliente)
        {
            _repositoryCliente = repositoryCliente;
            _newsLetterRepository = newsLetterRepository;
            _loginCliente = loginCliente;
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
                _newsLetterRepository.Cadastrar(new NewsLetterEmail { Email = model.Email });

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

        [HttpPost]
        public IActionResult Login(Cliente cliente)
        {
            Cliente clienteDB = _repositoryCliente.Login(cliente.Email, cliente.Senha);

            if (clienteDB != null)
            {
                _loginCliente.Login(clienteDB);
                return RedirectToAction(nameof(Painel));
            }
            else
            {
                ViewData["msg_e"] = "Usuário não encontrado, verifique o e-mail e senha digitado!";
                return View();
            }
        }

        [ClienteAutorizacao]
        public IActionResult Painel()
        {
            return View();
        }

        public IActionResult CadastroCliente()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CadastroCliente(Cliente model)
        {
            // valida os dataannotation definidas na model: Ex: Required
            if (ModelState.IsValid)
            {
                _repositoryCliente.Cadastrar(model);
                TempData["msg_s"] = "Cadastro realizado com sucesso!";

                //TODO - Implementar redirecionamentos diferentes (painel, carrinho de compras, etc)
                return RedirectToAction(nameof(CadastroCliente));
            }

            return View();
        }

        public IActionResult CarrinhoCompras()
        {
            return View();
        }
    }
}