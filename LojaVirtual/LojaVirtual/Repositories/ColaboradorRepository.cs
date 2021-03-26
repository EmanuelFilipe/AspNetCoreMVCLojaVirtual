using LojaVirtual.DataBase;
using LojaVirtual.Models;
using LojaVirtual.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Repositories
{
    public class ColaboradorRepository : IColaboradorRepository
    {

        private LojaVirtualContext _banco;

        public ColaboradorRepository(LojaVirtualContext banco)
        {
            _banco = banco;
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

        public IEnumerable<Colaborador> GetAllColaboradores()
        {
            return _banco.Colaboradores.ToList();
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
