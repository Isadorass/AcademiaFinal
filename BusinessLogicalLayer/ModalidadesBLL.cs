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
    public class ModalidadesBLL : StandartBase<Modalidades>, IModalidadesService
    {
        private ModalidadesDAL modalidadesDAL = new ModalidadesDAL();

        public Response Delete(int id)
        {
            return modalidadesDAL.Delete(id);
        }

        public DataResponse<Modalidades> GetAll()
        {
            return modalidadesDAL.GetAll();
        }

        public Response Insert(Modalidades m)
        {
            Response response = this.Validate(m);
            if (!response.Success)
            {
                return response;
            }

            return modalidadesDAL.Insert(m);
        }

        public Response Update(Modalidades m)
        {
            Response response = this.Validate(m);
            if (!response.Success)
            {
                return response;
            }

            return modalidadesDAL.Update(m);
        }
    }
}
