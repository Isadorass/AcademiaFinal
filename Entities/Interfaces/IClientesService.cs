using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites.Interfaces
{
    public interface IClientesService
    {
        Response Insert(Clientes c);
        Response Update(Clientes c);
        Response Delete(string id);
        DataResponse<Clientes> GetAll();
    }
}
