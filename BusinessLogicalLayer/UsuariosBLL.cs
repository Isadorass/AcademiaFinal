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
    class UsuariosBLL : StandartBase<Usuarios>, IUsuariosService
    {
        private UsuariosDAL usuariosDAL = new UsuariosDAL();

        public Response Delete(int id)
        {
            return usuariosDAL.Delete(id);
        }

        public DataResponse<Usuarios> GetAll()
        {
            return usuariosDAL.GetAll();
        }

        public Response Insert(Usuarios u)
        {
            Response response = this.Validate(u);
            if (!response.Success)
            {
                return response;
            }

            return usuariosDAL.Insert(u);
        }

        public Response Update(Usuarios u)
        {
            Response response = this.Validate(u);
            if (!response.Success)
            {
                return response;
            }

            return usuariosDAL.Update(u);
        }
    }
}
