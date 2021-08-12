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
    public class CategoriasBLL : StandartBase<Categorias>, ICategoriasService
    {
        private CategoriasDAL categoriasDAL = new CategoriasDAL();

        public Response Delete(int id)
        {
            return categoriasDAL.Delete(id);
        }

        public DataResponse<Categorias> GetAll()
        {
            return categoriasDAL.GetAll();
        }

        public Response Insert(Categorias c)
        {
            Response response = this.Validate(c);
            if (!response.Success)
            {
                return response;
            }

            return categoriasDAL.Insert(c);
        }

        public Response Update(Categorias c)
        {
            Response response = this.Validate(c);
            if (!response.Success)
            {
                return response;
            }

            return categoriasDAL.Update(c);
        }
    }
}
