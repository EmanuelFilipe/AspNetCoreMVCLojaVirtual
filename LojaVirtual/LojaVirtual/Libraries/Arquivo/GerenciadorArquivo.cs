using LojaVirtual.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
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

        public static List<Imagem> MoverImagensProduto(List<string> listPathTemp, int produtoId)
        {
            var listaImagensDefinitivo = new List<Imagem>();
            var pastaDefinitivaProduto = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", produtoId.ToString());

            if (!Directory.Exists(pastaDefinitivaProduto))
                Directory.CreateDirectory(pastaDefinitivaProduto);

            foreach (var diretorioTemp in listPathTemp)
            {
                if (!string.IsNullOrEmpty(diretorioTemp))
                {
                    var fileName = Path.GetFileName(diretorioTemp);
                    var diretorioDef = Path.Combine("/uploads", produtoId.ToString(), fileName).Replace("\\", "/");

                    if (diretorioDef != diretorioTemp)
                    {
                        var diretorioAbsolutoTemp = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/temp", fileName);
                        var diretorioAbsolutoDefinitivo = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", produtoId.ToString(), fileName);

                        if (File.Exists(diretorioAbsolutoTemp))
                        {
                            //deleta arquivo no caminho definitivo
                            if (File.Exists(diretorioAbsolutoDefinitivo))
                                File.Delete(diretorioAbsolutoDefinitivo);

                            // copia arquivo na pasta temporaria para definitivo
                            File.Copy(diretorioAbsolutoTemp, diretorioAbsolutoDefinitivo);

                            //deleta arquivo na pasta temporária
                            if (File.Exists(diretorioAbsolutoDefinitivo))
                                File.Delete(diretorioAbsolutoTemp);

                            listaImagensDefinitivo.Add(new Imagem()
                            {
                                Caminho = Path.Combine("/uploads", produtoId.ToString(), fileName).Replace("\\", "/"),
                                ProdutoId = produtoId
                            });
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else
                    {
                        listaImagensDefinitivo.Add(new Imagem()
                        {
                            Caminho = Path.Combine("/uploads", produtoId.ToString(), fileName).Replace("\\", "/"),
                            ProdutoId = produtoId
                        });
                    }
                }
            }

            return listaImagensDefinitivo;
        }

        public static void ExcluirImagensProduto(List<Imagem> listaImagens)
        {
            int produtoId = 0;

            foreach (Imagem imagem in listaImagens)
            {
                ExcluirImagemProduto(imagem.Caminho);
                produtoId = imagem.ProdutoId;
            }

            var diretorioProduto = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", produtoId.ToString());

            if (Directory.Exists(diretorioProduto))
                Directory.Delete(diretorioProduto);


        }
    }
}