using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites
{
    public class Produtos
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }        
        public Categorias Categoria { get; set; }

        public Produtos()
        {
            Categorias categorias = new Categorias();
            this.Categoria = categorias;
        }
    }
}
