using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites.Interfaces
{
    public interface IModalidadesService
    {
        Response Insert(Modalidades m);
        Response Update(Modalidades m);
        Response Delete(int id);
        DataResponse<Modalidades> GetAll();
    }
}
