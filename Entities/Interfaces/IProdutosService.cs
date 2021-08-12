using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites.Interfaces
{
    public interface IProdutosService
    {
        Response Insert(Produtos p);
        Response Update (Produtos p);
        Response Delete(int id);
        DataResponse<Produtos> GetAll();
    }
}
