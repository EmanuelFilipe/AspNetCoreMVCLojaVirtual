using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LojaVirtual.Libraries.Filtro;
using LojaVirtual.Models;
using LojaVirtual.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using X.PagedList;

namespace LojaVirtual.Areas.Colaborador.Controllers
{
    [Area("Colaborador")]
    //[ColaboradorAutorizacao]
    public class CategoriaController : Controller
    {
        private ICategoriaRepository _categoriaRepository;

        public CategoriaController(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        public IActionResult Index(int? pagina)
        {
            var categorias = _categoriaRepository.GetAllCategorias(pagina);
            return View(categorias);
        }

        public IActionResult Cadastrar()
        {
            ViewBag.Categorias = _categoriaRepository.GetAllCategorias()
                                                     .Select(c => new SelectListItem(c.Nome, c.Id.ToString()));
            return View();
        }

        [HttpPost]
        public IActionResult Cadastrar(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                _categoriaRepository.Cadastrar(categoria);
                TempData["MSG_S"] = "Registro salvo com sucesso";

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Categorias = _categoriaRepository.GetAllCategorias()
                                                     .Select(c => new SelectListItem(c.Nome, c.Id.ToString()));
            return View();
        }

        public IActionResult Atualizar(int id)
        {
            ViewBag.Categorias = _categoriaRepository.GetAllCategorias()
                                                     .Where(c => c.Id != id)
                                                     .Select(c => new SelectListItem(c.Nome, c.Id.ToString()));

            Categoria categoria = _categoriaRepository.GetCategoria(id);

            return View(categoria);
        }

        [HttpPost]
        public IActionResult Atualizar(Categoria categoria, int id)
        {
            if (ModelState.IsValid)
            {
                _categoriaRepository.Atualizar(categoria);
                TempData["MSG_S"] = "Registro salvo com sucesso";

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Categorias = _categoriaRepository.GetAllCategorias()
                                                     .Where(c => c.Id != id)
                                                     .Select(c => new SelectListItem(c.Nome, c.Id.ToString()));

            return View();
        }

        public IActionResult Excluir(int id)
        {
            _categoriaRepository.Excluir(id);
            TempData["MSG_S"] = "Registro excluído com sucesso";

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public JsonResult ExcluirAjax([FromForm]int id)
        {
            _categoriaRepository.Excluir(id);

            return Json(new { status = true, mensagem = "Registro excluído com sucesso" });
        }

    }
}