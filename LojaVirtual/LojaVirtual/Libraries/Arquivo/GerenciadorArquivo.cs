using Microsoft.AspNetCore.Http;
using System.IO;

namespace LojaVirtual.Libraries.Arquivo
{
    public class GerenciadorArquivo
    {
        public static string CadastrarImagemProduto(IFormFile file)
        {
            var fileName = Path.GetFileName(file.FileName);
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/temp", fileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return Path.Combine("/uploads/temp", fileName).Replace("\\", "/");
        }

        public static bool ExcluirImagemProduto(string path)
        {
            string newPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", path.TrimStart('/'));

            if (File.Exists(newPath))
            {
                File.Delete(newPath);
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
