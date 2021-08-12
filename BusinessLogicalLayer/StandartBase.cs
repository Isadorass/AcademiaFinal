using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLogicalLayer
{
    public class StandartBase<T>
    {
        private StringBuilder erros = new StringBuilder();

        protected void AddError(string error)
        {
            if (!string.IsNullOrWhiteSpace(error))
            {
                this.erros.AppendLine(error);
            }
        }

        public virtual Response Validate(T item)
        {
            Response response = new Response();
            if (this.erros.Length != 0)
            {
                response.Success = false;
                response.Message = this.erros.ToString();
                this.erros.Clear();
                return response;
            }

            response.Success = true;
            response.Message = "Validação realizada com sucesso!";
            return response;
        }
    }
}
