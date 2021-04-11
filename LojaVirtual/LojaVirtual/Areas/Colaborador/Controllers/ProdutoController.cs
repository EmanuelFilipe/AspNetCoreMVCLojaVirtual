using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LojaVirtual.Libraries.Filtro;
using LojaVirtual.Libraries.Lang;
using LojaVirtual.Models;
using LojaVirtual.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LojaVirtual.Areas.Colaborador.Controllers
{
    [Area("Colaborador")]
    public class ProdutoController : Controller
    {
        private IProdutoRepository _produtoRepository;
        private ICategoriaRepository _categoriaRepository;

        public ProdutoController(IProdutoRepository produtoRepository, ICategoriaRepository categoriaRepository)
        {
            _produtoRepository = produtoRepository;
            _categoriaRepository = categoriaRepository;
        }

        public IActionResult Index(int? pagina, string pesquisa)
        {
            var produtos = _produtoRepository.GetAllProdutos(pagina, pesquisa);
            return View(produtos);
        }

        public IActionResult Cadastrar()
        {
            ViewBag.Categorias = _categoriaRepository.GetAllCategorias().Select(c => new SelectListItem(c.Nome, c.Id.ToString()));
            return View();
        }

        [HttpPost]
        [ValidateHttpReferer]
        public IActionResult Cadastrar(Produto produto)
        {
            if (ModelState.IsValid)
            {
                _produtoRepository.Cadastrar(produto);
                TempData["MSG_S"] = Mensagem.MSG_S001;

                return RedirectToAction(nameof(Index));
            }


            ViewBag.Categorias = _categoriaRepository.GetAllCategorias().Select(c => new SelectListItem(c.Nome, c.Id.ToString()));
            return View();
        }

        public IActionResult Atualizar(int id)
        {
            var produto = _produtoRepository.GetProduto(id);
            ViewBag.Categorias = _categoriaRepository.GetAllCategorias().Select(c => new SelectListItem(c.Nome, c.Id.ToString()));

            return View(produto);
        }

        [HttpPost]
        [ValidateHttpReferer]
        public IActionResult Atualizar(Produto produto)
        {
            if (ModelState.IsValid)
            {
                _produtoRepository.Atualizar(produto);
                TempData["MSG_S"] = Mensagem.MSG_S001;

                return RedirectToAction(nameof(Index));
            }


            ViewBag.Categorias = _categoriaRepository.GetAllCategorias().Select(c => new SelectListItem(c.Nome, c.Id.ToString()));
            return View();
        }


        [HttpPost]
        [ValidateHttpReferer]
        public JsonResult Excluir([FromForm]int id)
        {
            _produtoRepository.Excluir(id);

            return Json(new { status = true, mensagem = Mensagem.MSG_S002 });
        }

    }
}