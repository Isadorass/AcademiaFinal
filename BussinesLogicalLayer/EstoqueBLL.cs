using DataAccessLayer;
using Entities;
using Entities.Interfaces;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BussinesLogicalLayer
{
    public class EstoqueBLL : StandartBase<Estoque>, IEstoqueService
    {
        private EstoqueDAL estoqueDAL = new EstoqueDAL();

        public DataResponse<Estoque> PesquisarPorDataEntrada(string dataEntrada)
        {
            return estoqueDAL.PesquisarPorDataEntrada(dataEntrada);
        }
        public int BuscarUltimoEstoque()
        {
            return estoqueDAL.BuscarUltimoEstoque();
        }

        public bool RelacionarProdutoEstoque(int idEstoque, int idProduto)
        {
            return estoqueDAL.RelacionarProdutoEstoque(idEstoque, idProduto);
        }

        public Response Delete(int id)
        {
            return estoqueDAL.Delete(id);
        }

        public DataResponse<Estoque> GetAll()
        {
            return estoqueDAL.GetAll();
        }

        public Response Insert(Estoque e)
        {
            Response response = this.Validate(e);
            if (!response.Success)
            {
                return response;
            }
            return estoqueDAL.Insert(e);
        }

        public Response Update(Estoque e)
        {
            return estoqueDAL.Update(e);
        }
    }
}
