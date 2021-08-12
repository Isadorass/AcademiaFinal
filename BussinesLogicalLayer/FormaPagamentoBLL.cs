using DataAccessLayer;
using Entities;
using Entities.Interfaces;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLogicalLayer
{
    public class FormaPagamentoBLL : StandartBase<FormaPagamento>, IFormaPagamentoService
    {
        private FormaPagamentoDAL formaPagamentoDAL = new FormaPagamentoDAL();

        public DataResponse<FormaPagamento> PesquisarPorNome(string nome)
        {
            return formaPagamentoDAL.PesquisarPorNome(nome);
        }

        public Response Delete(int id)
        {
            return formaPagamentoDAL.Delete(id);
        }

        public DataResponse<FormaPagamento> GetAll()
        {
            return formaPagamentoDAL.GetAll();
        }

        public Response Insert(FormaPagamento f)
        {

            Response response = this.Validate(f);
            if (!response.Success)
            {
                return response;
            }

            return formaPagamentoDAL.Insert(f);
        }

        public Response Update(FormaPagamento f)
        {
            Response response = this.Validate(f);
            if (!response.Success)
            {
                return response;
            }

            return formaPagamentoDAL.Update(f);
        }
    }
}
