using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites.Interfaces
{
    public interface IFuncionarioService
    {
        Response Insert(Funcionario p);
        Response Update(Funcionario p);
        Response Delete(string cpf);
        DataResponse<Funcionario> GetAll();
    }
}
