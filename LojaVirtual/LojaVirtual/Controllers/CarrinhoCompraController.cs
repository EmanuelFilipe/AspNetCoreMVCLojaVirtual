using LojaVirtual.Libraries.CarrinhoCompra;
using LojaVirtual.Models.ProdutoAgregador;
using LojaVirtual.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace LojaVirtual.Controllers
{
    public class CarrinhoCompraController : Controller
    {
        private CarrinhoCompra _carrinhoCompra;
        private IProdutoRepository _produtoRepository;

        public CarrinhoCompraController(CarrinhoCompra carrinhoCompra, IProdutoRepository produtoRepository)
        {
            _carrinhoCompra = carrinhoCompra;
            _produtoRepository = produtoRepository;
        }

        public IActionResult Index()
        {
            return View();
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
                var item = new ProdutoItem() { Id = id, Quantidade = 1 };
                _carrinhoCompra.Cadastrar(item);

                return RedirectToAction(nameof(Index));
            }
        }

        public IActionResult AlterarQuantidade(int id, int quantidade)
        {
            var item = new ProdutoItem() { Id = id, Quantidade = quantidade };
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