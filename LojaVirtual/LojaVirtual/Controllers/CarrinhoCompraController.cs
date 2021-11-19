using LojaVirtual.Libraries.CarrinhoCompra;
using LojaVirtual.Models.ProdutoAgregador;
using LojaVirtual.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using AutoMapper;

namespace LojaVirtual.Controllers
{
    public class CarrinhoCompraController : Controller
    {
        private CarrinhoCompra _carrinhoCompra;
        private IProdutoRepository _produtoRepository;
        private IMapper _mapper;

        public CarrinhoCompraController(CarrinhoCompra carrinhoCompra, IProdutoRepository produtoRepository, IMapper mapper)
        {
            _carrinhoCompra = carrinhoCompra;
            _produtoRepository = produtoRepository;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            List<ProdutoItem> produtoItemNoCarrinho = _carrinhoCompra.Consultar();
            List<ProdutoItem> produtoItemCompleto = new List<ProdutoItem>();

            foreach (var item in produtoItemNoCarrinho)
            {
                Produto produto = _produtoRepository.GetProduto(item.Id);
                ProdutoItem produtoItem = _mapper.Map<ProdutoItem>(produto);

                //ProdutoItem produtoItem = new ProdutoItem()
                //{
                //    Id = produto.Id,
                //    Nome = produto.Nome,
                //    Imagens = produto.Imagens,
                //    Valor = produto.Valor,
                //    QuantidadeProdutoCarrinho = item.QuantidadeProdutoCarrinho
                //};

                produtoItem.QuantidadeProdutoCarrinho = item.QuantidadeProdutoCarrinho;
                produtoItemCompleto.Add(produtoItem);
            }

            return View(produtoItemCompleto);
        }

        public IActionResult AdicionarItem(int id)
        {
            var produto = _produtoRepository.GetProduto(id);

            if (produto == null)
            {
                return View("NaoExisteItem");
            }
            else
            {
                var item = new ProdutoItem() { Id = id, QuantidadeProdutoCarrinho = 1 };
                _carrinhoCompra.Cadastrar(item);

                return RedirectToAction(nameof(Index));
            }
        }

        public IActionResult AlterarQuantidade(int id, int quantidade)
        {
            var item = new ProdutoItem() { Id = id, QuantidadeProdutoCarrinho = quantidade };
            _carrinhoCompra.Atualizar(item);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult RemoverItem(int id)
        {
            _carrinhoCompra.Remover(new ProdutoItem { Id = id });
            return RedirectToAction(nameof(Index));
        }
    }
}