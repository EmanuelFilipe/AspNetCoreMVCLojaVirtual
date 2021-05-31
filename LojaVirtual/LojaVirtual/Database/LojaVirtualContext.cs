using LojaVirtual.Models;
using LojaVirtual.Models.ProdutoAgregador;
using Microsoft.EntityFrameworkCore;

namespace LojaVirtual.DataBase
{
    public class LojaVirtualContext : DbContext
    {
        /*
         * EF Core - ORM
         * ORM -> Biblioteca que mapea objetos para banco de dados relacionais
         */

        public LojaVirtualContext(DbContextOptions<LojaVirtualContext> options) : base(options)
        {

        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<NewsLetterEmail> NewsLetterEmails { get; set; }
        public DbSet<Colaborador> Colaboradores { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Imagem> Imagens { get; set; }

    }
}
