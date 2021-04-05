using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LojaVirtual.Libraries.Email;
using LojaVirtual.Libraries.Filtro;
using LojaVirtual.Libraries.Keys;
using LojaVirtual.Libraries.Lang;
using LojaVirtual.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace LojaVirtual.Areas.Colaborador.Controllers
{
    [Area("Colaborador")]
    //[ColaboradorAutorizacao("G")]
    public class ColaboradorController : Controller
    {
        private IColaboradorRepository _colaboradorRepository;
        private GerenciarEmail _gerenciarEmail;

        public ColaboradorController(IColaboradorRepository colaboradorRepository, GerenciarEmail gerenciarEmail)
        {
            _colaboradorRepository = colaboradorRepository;
            _gerenciarEmail = gerenciarEmail;
        }

        public IActionResult Index(int? pagina)
        {
            var colaboradores =_colaboradorRepository.GetAllColaboradores(pagina);
            return View(colaboradores);
        }

        public IActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Cadastrar(Models.Colaborador colaborador)
        {
            // remove o campo Senha da validação do formulário.
            ModelState.Remove("Senha");

            if (ModelState.IsValid)
            {
                colaborador.Tipo = "C";
                colaborador.Senha = KeyGenerator.GetUniqueKey(8);

                _colaboradorRepository.Cadastrar(colaborador);
                _gerenciarEmail.EnviarSenhaParaColaboradorPorEmail(colaborador);

                TempData["MSG_S"] = Mensagem.MSG_S001;

                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public IActionResult GerarSenha(int id)
        {
            var colaborador = _colaboradorRepository.GetColaborador(id);
            colaborador.Senha = KeyGenerator.GetUniqueKey(8);

            _colaboradorRepository.AtualizarSenha(colaborador);
            _gerenciarEmail.EnviarSenhaParaColaboradorPorEmail(colaborador);

            TempData["MSG_S"] = Mensagem.MSG_S003;

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Atualizar(int id)
        {
            var colaborador = _colaboradorRepository.GetColaborador(id);
            return View(colaborador);
        }

        [HttpPost]
        public IActionResult Atualizar(Models.Colaborador colaborador)
        {
            // remove o campo Senha da validação do formulário.
            ModelState.Remove("Senha");

            if (ModelState.IsValid)
            {
                _colaboradorRepository.Atualizar(colaborador);
                TempData["MSG_S"] = Mensagem.MSG_S001;

                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        [HttpPost]
        public JsonResult Excluir([FromForm]int id)
        {
            _colaboradorRepository.Excluir(id);

            return Json(new { status = true, mensagem = Mensagem.MSG_S002 });
        }
    }
}