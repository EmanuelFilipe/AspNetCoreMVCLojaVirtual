using LojaVirtual.Libraries.Arquivo;
using LojaVirtual.Libraries.Filtro;
using LojaVirtual.Libraries.Lang;
using LojaVirtual.Models;
using LojaVirtual.Models.ProdutoAgregador;
using LojaVirtual.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace LojaVirtual.Areas.Colaborador.Controllers
{
    [Area("Colaborador")]
    //[ColaboradorAutorizacao]
    public class ProdutoController : Controller
    {
        private IProdutoRepository _produtoRepository;
        private ICategoriaRepository _categoriaRepository;
        private IImagemRepository _imagemRepository;

        public ProdutoController(IProdutoRepository produtoRepository, ICategoriaRepository categoriaRepository, IImagemRepository imagemRepository)
        {
            _produtoRepository = produtoRepository;
            _categoriaRepository = categoriaRepository;
            _imagemRepository = imagemRepository;
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

                var listPathTemp = new List<string>(Request.Form["imagem"]);

                List<Imagem> listaImgensDefinitivo = GerenciadorArquivo.MoverImagensProduto(listPathTemp, produto.Id);
                _imagemRepository.CadastrarImagens(listaImgensDefinitivo, produto.Id);

                TempData["MSG_S"] = Mensagem.MSG_S001;

                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewBag.Categorias = _categoriaRepository.GetAllCategorias().Select(c => new SelectListItem(c.Nome, c.Id.ToString()));
                produto.Imagens = new List<string>(Request.Form["imagem"]).Where(im => im.Trim().Length > 0)
                                                                          .Select(i => new Imagem() { Caminho = i }).ToList();

                return View(produto);
            }
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

                var listPathTemp = new List<string>(Request.Form["imagem"]);

                List<Imagem> listaImgensDefinitivo = GerenciadorArquivo.MoverImagensProduto(listPathTemp, produto.Id);
                _imagemRepository.ExcluirImagensDoProduto( produto.Id);
                _imagemRepository.CadastrarImagens(listaImgensDefinitivo, produto.Id);

                TempData["MSG_S"] = Mensagem.MSG_S001;

                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewBag.Categorias = _categoriaRepository.GetAllCategorias().Select(c => new SelectListItem(c.Nome, c.Id.ToString()));
                produto.Imagens = new List<string>(Request.Form["imagem"]).Where(im => im.Trim().Length > 0)
                                                                          .Select(i => new Imagem() { Caminho = i }).ToList();

                return View(produto);
            }
        }


        [HttpPost]
        [ValidateHttpReferer]
        public JsonResult Excluir([FromForm]int id)
        {
            var produto = _produtoRepository.GetProduto(id);
            GerenciadorArquivo.ExcluirImagensProduto(produto.Imagens.ToList());

            _imagemRepository.ExcluirImagensDoProduto(id);

            _produtoRepository.Excluir(id);

            return Json(new { status = true, mensagem = Mensagem.MSG_S002 });
        }

    }
}