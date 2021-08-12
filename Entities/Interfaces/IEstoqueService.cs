using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Interfaces
{
    public interface IEstoqueService
    {
        Response Insert(Estoque e);
        Response Update(Estoque e);
        Response Delete(int id);

        DataResponse<Estoque> GetAll();
    }
}
