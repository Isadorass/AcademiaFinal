using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Interfaces
{
    public interface IFormaPagamentoService
    {
        Response Insert(FormaPagamento f);
        Response Update(FormaPagamento f);
        Response Delete(int id);
        DataResponse<FormaPagamento> GetAll();
    }
}
