using DataAccessLayer;
using Entites;
using Entites.Interfaces;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLogicalLayer
{
    public class ProdutosBLL : StandartBase<Produtos>, IProdutosService
    {
        private ProdutosDAL produtosDAL = new ProdutosDAL();

        public Response Delete(int id)
        {
            return produtosDAL.Delete(id);
        }

        public DataResponse<Produtos> GetAll()
        {
            return produtosDAL.GetAll();
        }

        public Response Insert(Produtos p)
        {
            Response response = this.Validate(p);
            if (!response.Success)
            {
                return response;
            }

            return produtosDAL.Insert(p);
        }

        public Response Update(Produtos p)
        {
            Response response = this.Validate(p);
            if (!response.Success)
            {
                return response;
            }

            return produtosDAL.Update(p);
        }
    }
}
