using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Models
{
    public class Categoria
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        /*
         * Nome: Telefone sem fio
         * Slug: telefone-sem-fio
         * URL normal: www.lojavirtual.com.br/categoria/5
         * URL Amigavel e com Slug: www.lojavirtual.com.br/categoria/telefone-sem-fio
         */
        public string Slug { get; set; }


        /*
         * Auto-Relacionamento || P de pai
         * 1-Informática - P: null
         *   2-Mouse: P:1
         *     3-Mouse sem fio P:2
         *     4-Mouse Gamer   P:2
         */
        public int? CategoriaPaiId { get; set; }

        [ForeignKey("CategoriaPaiId")]
        public virtual Categoria CategoriaPai { get; set; }
    }
}
