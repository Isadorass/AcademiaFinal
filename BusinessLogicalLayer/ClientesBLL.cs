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
    public class ClientesBLL : StandartBase<Clientes>, IClientesService
    {
        private ClientesDAL clientesDAL = new ClientesDAL();


        public Response Delete(int id)
        {
            return clientesDAL.Delete(id);
        }

        public DataResponse<Clientes> GetAll()
        {
            return clientesDAL.GetAll();
        }

        public Response Insert(Clientes c)
        {

            Response response = this.Validate(c);
            if (!response.Success)
            {
                return response;
            }

            return clientesDAL.Insert(c);
        }

        public Response Update(Clientes c)
        {
            Response response = this.Validate(c);
            if (!response.Success)
            {
                return response;
            }

            return clientesDAL.Update(c);
        }
    }
}
