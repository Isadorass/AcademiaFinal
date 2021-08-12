using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites.Interfaces
{
    public interface ICategoriasService
    {
        Response Insert(Categorias c);
        Response Update(Categorias c);
        Response Delete(int id);

        DataResponse<Categorias> GetAll();
    }
}
