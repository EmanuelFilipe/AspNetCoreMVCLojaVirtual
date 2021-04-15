using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LojaVirtual.Libraries.Arquivo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LojaVirtual.Areas.Colaborador.Controllers
{
    [Area("colaborador")]
    public class ImagemController : Controller
    {
        [HttpPost]
        public IActionResult Armazenar(IFormFile file)
        {
            var path = GerenciadorArquivo.CadastrarImagemProduto(file);

            if(path.Length > 0)
            {
                return Ok(new { path = path });
            }
            else
            {
                return new StatusCodeResult(500);
            }
        }

        public IActionResult Excluir(string path)
        {
            if (GerenciadorArquivo.ExcluirImagemProduto(path))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}