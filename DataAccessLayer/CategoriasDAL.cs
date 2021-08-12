using Entites;
using Entites.Interfaces;
using Shared;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataAccessLayer
{
    public class CategoriasDAL : ICategoriasService
    {

        public DataResponse<Categorias> PesquisarPeloNome(string nome)
        {
            string connectionString = SqlUtils.CONNECTION_STRING;
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = connectionString;

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "SELECT * FROM CATEGORIA where NOME like @NOME order by NOME";

            command.Parameters.AddWithValue("@NOME", "%"+nome+"%");

            DataResponse<Categorias> resposta = new DataResponse<Categorias>();

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                List<Categorias> categorias = new List<Categorias>();

                while (reader.Read())
                {
                    Categorias categoria = new Categorias();
                    categoria.ID = Convert.ToInt32(reader["ID"]);
                    categoria.Nome = Convert.ToString(reader["NOME"]);
                    categorias.Add(categoria);
                }

                resposta.Success = true;
                resposta.Message = "Dados selecionados com sucesso!";
                resposta.Data = categorias;
                return resposta;

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                resposta.Success = false;
                resposta.Message = "Erro no banco de dados, contate o administrador.";
                resposta.Data = new List<Categorias>();
                return resposta;
            }
            finally
            {
                connection.Dispose();
            }
        }

        public Categorias BuscarPeloId(int id)
        {
            string connectionString = SqlUtils.CONNECTION_STRING;
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = connectionString;

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "SELECT * FROM CATEGORIA where " +
                "ID = @ID";

            command.Parameters.AddWithValue("@ID", id);

            DataResponse<Categorias> resposta = new DataResponse<Categorias>();

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                Categorias categoria = new Categorias();

                while (reader.Read())
                {
                    categoria.ID = Convert.ToInt32(reader["ID"]);
                    categoria.Nome = Convert.ToString(reader["NOME"]);

                    return categoria;
                }

                return categoria;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Categorias categorias = new Categorias();
                return categorias;
            }
            finally
            {
                connection.Dispose();
            }
        }

        public Response Delete(int id)
        {
            string connectionString = SqlUtils.CONNECTION_STRING;
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = connectionString;

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "DELETE FROM CATEGORIA WHERE ID = @ID";
            command.Parameters.AddWithValue("@ID", id);

            Response resposta = new Response();
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                resposta.Success = true;
                resposta.Message = "Categoria excluída com sucesso!";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Success = false;
                
                if (ex.Message.Substring(0, 10).Equals("The DELETE"))
                {
                    resposta.Message = "Categoria não pode ser excluída, " +
                        "pois\n existem produtos vinculadas a ela!";
                    return resposta;
                }
                resposta.Message = "Erro no banco de dados, contate o administrador.";
                return resposta;
            }
            finally
            {
                connection.Dispose();
            }
        }

        public DataResponse<Categorias> GetAll()
        {
            string connectionString = SqlUtils.CONNECTION_STRING;
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = connectionString;

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "SELECT * FROM CATEGORIA ORDER BY ID";

            DataResponse<Categorias> resposta = new DataResponse<Categorias>();

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                List<Categorias> categorias = new List<Categorias>();

                while (reader.Read())
                {
                    Categorias categoria = new Categorias();
                    categoria.ID = Convert.ToInt32(reader["ID"]);
                    categoria.Nome = Convert.ToString(reader["NOME"]);
                    categorias.Add(categoria);
                }

                resposta.Success = true;
                resposta.Message = "Dados selecionados com sucesso!";
                resposta.Data = categorias;
                return resposta;

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                resposta.Success = false;
                resposta.Message = "Erro no banco de dados, contate o administrador.";
                resposta.Data = new List<Categorias>();
                return resposta;
            }
            finally
            {
                connection.Dispose();
            }
        }

        public Response Insert(Categorias c)
        {
            string connectionString = SqlUtils.CONNECTION_STRING;
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = connectionString;

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "INSERT INTO CATEGORIA (NOME) VALUES (@NOME)";
            command.Parameters.AddWithValue("@NOME", c.Nome);

            Response resposta = new Response();
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                resposta.Success = true;
                resposta.Message = "Categorias cadastrada com sucesso!";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Success = false;

                if (ex.Message.Contains("UQ__CATEGORIA"))
                {
                    resposta.Message = "Categoria já cadastrada!";
                    return resposta;
                }

                resposta.Message = "Erro no banco de dados, contate o administrador.";
                return resposta;
            }
            finally
            {
                connection.Dispose();
            }
        }

        public Response Update(Categorias c)
        {
            string connectionString = SqlUtils.CONNECTION_STRING;
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = connectionString;

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "UPDATE CATEGORIA SET NOME = @NOME WHERE ID = @ID";
            command.Parameters.AddWithValue("@NOME", c.Nome);
            command.Parameters.AddWithValue("@ID", c.ID);


            Response resposta = new Response();
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                resposta.Success = true;
                resposta.Message = "Categoria editada com sucesso!";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Success = false;
                resposta.Message = "Erro no banco de dados, contate o administrador.";
                return resposta;
            }
            finally
            {
                connection.Dispose();
            }
        }
    }
}
