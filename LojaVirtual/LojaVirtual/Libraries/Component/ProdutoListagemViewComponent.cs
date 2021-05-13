using LojaVirtual.Models;
using LojaVirtual.Models.ViewModels;
using LojaVirtual.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Libraries.Component
{
    public class ProdutoListagemViewComponent : ViewComponent
    {
        private IProdutoRepository _produtoRepository;
        private ICategoriaRepository _categoriaRepository;

        public ProdutoListagemViewComponent(IProdutoRepository produtoRepository, ICategoriaRepository categoriaRepository)
        {
            _produtoRepository = produtoRepository;
            _categoriaRepository = categoriaRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            int? pagina = 1;
            string pesquisa = "";
            string ordenacao = "A";
            IEnumerable<Categoria> categorias = null; 

            if (HttpContext.Request.Query.ContainsKey("pagina"))
                pagina = int.Parse(HttpContext.Request.Query["pagina"]);

            if (HttpContext.Request.Query.ContainsKey("pesquisa"))
                pesquisa = HttpContext.Request.Query["pesquisa"];

            if (HttpContext.Request.Query.ContainsKey("ordenacao"))
                ordenacao = HttpContext.Request.Query["pagina"];

            if (ViewContext.RouteData.Values.ContainsKey("slug"))
            {
                string slug = ViewContext.RouteData.Values["slug"].ToString();
                Categoria categoriaPrincipal = _categoriaRepository.GetCategoria(slug);
                categorias = _categoriaRepository.GetCategoriasRecursivas(categoriaPrincipal);
            }

            var viewModel = new ProdutoListagemViewModel { lista = _produtoRepository.GetAllProdutos(pagina, pesquisa, ordenacao, categorias) };

            return View(viewModel);
        }
    }
}
