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
    public class ProdutosDAL : IProdutosService
    {
        CategoriasDAL categoriasDAL = new CategoriasDAL();

        public DataResponse<Produtos> PesquisarPorNome(string nome)
        {
            string connectionString = SqlUtils.CONNECTION_STRING;
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = connectionString;

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "SELECT * FROM PRODUTO where NOME like @NOME ORDER BY ID";

            command.Parameters.AddWithValue("@NOME", "%"+nome+"%");

            DataResponse<Produtos> resposta = new DataResponse<Produtos>();

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                List<Produtos> produtos = new List<Produtos>();

                while (reader.Read())
                {
                    Produtos produto = new Produtos();
                    produto.ID = Convert.ToInt32(reader["ID"]);
                    produto.Nome = Convert.ToString(reader["NOME"]);
                    produto.Descricao = Convert.ToString(reader["DESCRICAO"]);
                    produto.Categoria = categoriasDAL.BuscarPeloId(Convert.ToInt32(reader["CATEGORIA_ID"]));

                    produtos.Add(produto);
                }

                resposta.Success = true;
                resposta.Message = "Dados selecionados com sucesso!";
                resposta.Data = produtos;
                return resposta;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                resposta.Success = false;
                resposta.Message = "Erro no banco de dados, contate o administrador.";
                resposta.Data = new List<Produtos>();
                return resposta;
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
            command.CommandText = "DELETE FROM PRODUTO WHERE ID = @ID";
            command.Parameters.AddWithValue("@ID", id);

            Response resposta = new Response();
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                resposta.Success = true;
                resposta.Message = "Produto excluído com sucesso!";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Success = false;
                if (ex.Message.Substring(0, 10).Equals("The DELETE"))
                {
                    resposta.Message = "Produto não pode ser excluído," +
                        "pois\n existem atividades vinculadas a ele!";
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

        public DataResponse<Produtos> GetAll()
        {
            string connectionString = SqlUtils.CONNECTION_STRING;
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = connectionString;

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "SELECT * FROM PRODUTO ORDER BY ID";

            DataResponse<Produtos> resposta = new DataResponse<Produtos>();

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                List<Produtos> produtos = new List<Produtos>();

                while (reader.Read())
                {
                    Produtos produto = new Produtos();
                    produto.ID = Convert.ToInt32(reader["ID"]);
                    produto.Nome = Convert.ToString(reader["NOME"]);
                    produto.Descricao = Convert.ToString(reader["DESCRICAO"]);                    
                    produto.Categoria = categoriasDAL.BuscarPeloId(Convert.ToInt32(reader["CATEGORIA_ID"]));

                    produtos.Add(produto);
                }

                resposta.Success = true;
                resposta.Message = "Dados selecionados com sucesso!";
                resposta.Data = produtos;
                return resposta;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                resposta.Success = false;
                resposta.Message = "Erro no banco de dados, contate o administrador.";
                resposta.Data = new List<Produtos>();
                return resposta;
            }
            finally
            {
                connection.Dispose();
            }
        }

        public Response Insert(Produtos p)
        {
            string connectionString = SqlUtils.CONNECTION_STRING;
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = connectionString;

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "INSERT INTO PRODUTO " +
                "(NOME, DESCRICAO, CATEGORIA_ID) VALUES " +
                "(@NOME, @DESCRICAO, @CATEGORIA_ID)";
            command.Parameters.AddWithValue("@NOME", p.Nome);
            command.Parameters.AddWithValue("@DESCRICAO", p.Descricao);
            command.Parameters.AddWithValue("@CATEGORIA_ID", p.Categoria.ID);

            Response resposta = new Response();
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                resposta.Success = true;
                resposta.Message = "Produto cadastrado com sucesso!";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Success = false;

                if (ex.Message.Contains("UQ__PRODUTOS"))
                {
                    resposta.Message = "Produto já cadastrado!";
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

        public Response Update(Produtos p)
        {
            string connectionString = SqlUtils.CONNECTION_STRING;
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = connectionString;

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "UPDATE PRODUTO SET NOME = @NOME, " +
                "DESCRICAO = @DESCRICAO, CATEGORIA_ID = @CATEGORIA_ID " +
                "WHERE ID = @ID";
            command.Parameters.AddWithValue("@NOME", p.Nome);
            command.Parameters.AddWithValue("@DESCRICAO", p.Descricao);
            command.Parameters.AddWithValue("@CATEGORIA_ID", p.Categoria.ID);
            command.Parameters.AddWithValue("@ID", p.ID);

            Response resposta = new Response();
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                resposta.Success = true;
                resposta.Message = "Produto editado com sucesso!";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Success = false;

                if (ex.Message.Contains("UQ__PRODUTOS"))
                {
                    resposta.Message = "Produto já cadastrado!";
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
    }
}
