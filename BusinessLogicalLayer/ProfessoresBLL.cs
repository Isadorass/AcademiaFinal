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
    class ProfessoresBLL : StandartBase<Professores>, IProfessoresService
    {
        private ProfessoresDAL professoresDAL = new ProfessoresDAL();


        public Response Delete(int id)
        {
            return professoresDAL.Delete(id);
        }

        public DataResponse<Professores> GetAll()
        {
            return professoresDAL.GetAll();
        }

        public Response Insert(Professores p)
        {
            Response response = this.Validate(p);
            if (!response.Success)
            {
                return response;
            }

            return professoresDAL.Insert(p);
        }

        public Response Update(Professores p)
        {
            Response response = this.Validate(p);
            if (!response.Success)
            {
                return response;
            }

            return professoresDAL.Update(p);
        }
    }
}
