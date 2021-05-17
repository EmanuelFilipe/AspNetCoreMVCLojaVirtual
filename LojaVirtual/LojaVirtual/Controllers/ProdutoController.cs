using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LojaVirtual.Models;
using LojaVirtual.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace LojaVirtual.Controllers
{
    public class ProdutoController : Controller
    {
        private ICategoriaRepository _categoriaRepository;
        private IProdutoRepository _produtoRepository;
        private List<Categoria> lista;

        public ProdutoController(ICategoriaRepository categoriaRepository, IProdutoRepository produtoRepository)
        {
            _categoriaRepository = categoriaRepository;
            _produtoRepository = produtoRepository;
            lista = new List<Categoria>();
        }

        [Route("/Produto/Categoria/{slug}")]
        public IActionResult ListagemCategoria(string slug)
        {
            return View(_categoriaRepository.GetCategoria(slug));
        }

        public ActionResult Visualizar(int id)
        {
            return View(_produtoRepository.GetProduto(id));
        }

        private List<Categoria> GetCategorias(List<Categoria> categorias, Categoria categoriaPrincipal)
        {
            if (!lista.Exists(c => c.Id == categoriaPrincipal.Id))
            {
                lista.Add(categoriaPrincipal);
            }

            var listaCategoriaFilho = categorias.Where(c => c.CategoriaPaiId == categoriaPrincipal.Id);

            if (listaCategoriaFilho.Count() > 0)
            {
                lista.AddRange(listaCategoriaFilho.ToList());
                foreach (var categoria in listaCategoriaFilho)
                {
                    GetCategorias(categorias, categoria);
                }
            }

            return lista;
        }
        
    }
}