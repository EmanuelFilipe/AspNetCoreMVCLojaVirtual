﻿using LojaVirtual.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace LojaVirtual.Repositories.Contracts
{
    public interface ICategoriaRepository
    {
        void Cadastrar(Categoria categoria);

        void Atualizar(Categoria categoria);

        void Excluir(int id);

        Categoria GetCategoria(int id);

        Categoria GetCategoria(string slug);

        IEnumerable<Categoria> GetAllCategorias();

        IEnumerable<Categoria> GetCategoriasRecursivas(Categoria categoriaPai);

        IPagedList<Categoria> GetAllCategorias(int? pagina);
    }
}
