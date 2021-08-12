using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Estoque
    {
        public int ID { get; set; }
        public DateTime DataEntrada { get; set; }
        public double ValorUnitario { get; set; }
        public int Quantidade { get; set; }
    }
}
