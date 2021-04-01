﻿using LojaVirtual.Models;
using System.Collections.Generic;
using X.PagedList;

namespace LojaVirtual.Repositories.Contracts
{
    public interface IColaboradorRepository
    {
        Colaborador Login(string email, string senha);

        void Cadastrar(Colaborador colaborador);

        void Atualizar(Colaborador colaborador);

        void Excluir(int id);

        Colaborador GetColaborador(int id);

        IPagedList<Colaborador> GetAllColaboradores(int? pagina);
    }
}
