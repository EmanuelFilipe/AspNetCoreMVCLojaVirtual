using LojaVirtual.DataBase;
using LojaVirtual.Models;
using LojaVirtual.Repositories.Contracts;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace LojaVirtual.Repositories
{
    public class ColaboradorRepository : IColaboradorRepository
    {
        private LojaVirtualContext _banco;
        private IConfiguration _configuration;

        public ColaboradorRepository(LojaVirtualContext banco, IConfiguration configuration)
        {
            _banco = banco;
            _configuration = configuration;
        }

        public void Atualizar(Colaborador colaborador)
        {
            _banco.Update(colaborador);
            _banco.SaveChanges();
        }

        public void Cadastrar(Colaborador colaborador)
        {
            _banco.Add(colaborador);
            _banco.SaveChanges();
        }

        public void Excluir(int id)
        {
            Colaborador colaborador = GetColaborador(id);

            if (colaborador != null)
            {
                _banco.Remove(colaborador);
                _banco.SaveChanges();
            }
        }

        public IPagedList<Colaborador> GetAllColaboradores(int? pagina)
        {
            int numeroPagina = pagina ?? 1;
            return _banco.Colaboradores.Where(c => c.Tipo != "G").ToPagedList<Colaborador>(numeroPagina, _configuration.GetValue<int>("RegistroPorPagina"));
        }

        public Colaborador GetColaborador(int id)
        {
            return _banco.Colaboradores.Find(id);
        }

        public Colaborador Login(string email, string senha)
        {
            return _banco.Colaboradores.DefaultIfEmpty(new Colaborador())
                                  .Where(x => x.Email == email && x.Senha == senha).FirstOrDefault();
        }
    }
}
