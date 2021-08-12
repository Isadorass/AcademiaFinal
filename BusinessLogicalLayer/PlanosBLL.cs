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
    class PlanosBLL : StandartBase<Planos>, IPlanosService
    {
        private PlanosDAL planosDAL = new PlanosDAL();

        public Response Delete(int id)
        {
            return planosDAL.Delete(id);
        }

        public DataResponse<Planos> GetAll()
        {
            return planosDAL.GetAll();
        }

        public Response Insert(Planos p)
        {
            Response response = this.Validate(p);
            if (!response.Success)
            {
                return response;
            }

            return planosDAL.Insert(p);
        }

        public Response Update(Planos p)
        {
            Response response = this.Validate(p);
            if (!response.Success)
            {
                return response;
            }

            return planosDAL.Update(p);
        }
    }
}

