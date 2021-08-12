using Entities;
using Entities.Interfaces;
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
    public class EstoqueDAL : IEstoqueService
    {

        public DataResponse<Estoque> PesquisarPorDataEntrada(string dataEntrada)
        {
            string connectionString = SqlUtils.CONNECTION_STRING;
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = connectionString;

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "SELECT * FROM ESTOQUE where DATA_ENTRADA = @DATAENTRADA ORDER BY ID";

            command.Parameters.AddWithValue("@DATAENTRADA", dataEntrada);

            DataResponse<Estoque> resposta = new DataResponse<Estoque>();

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                List<Estoque> estoques = new List<Estoque>();

                while (reader.Read())
                {
                    Estoque estoque = new Estoque();

                    estoque.ID = Convert.ToInt32(reader["ID"]);
                    estoque.DataEntrada = Convert.ToDateTime(reader["DATA_ENTRADA"]);
                    estoque.ValorUnitario = Convert.ToDouble(reader["VALOR_UNITARIO"]);
                    estoque.Quantidade = Convert.ToInt32(reader["QUANTIDADE"]);

                    estoques.Add(estoque);
                }

                resposta.Success = true;
                resposta.Message = "Dados selecionados com sucesso!";
                resposta.Data = estoques;
                return resposta;

            }
            catch (Exception ex)
            {
                resposta.Success = false;
                resposta.Message = "Erro no banco de dados, contate o administrador.";
                resposta.Data = new List<Estoque>();
                return resposta;
            }
            finally
            {
                connection.Dispose();
            }
        }

        public bool VerificaEstoqueProduto(int idEstoque, int idProduto)
        {
            string connectionString = SqlUtils.CONNECTION_STRING;
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = connectionString;

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "select * from PRODUTO_ESTOQUE " +
                "where PRODUTO_ID = @PRODUTO_ID " +
                "AND ESTOQUE_ID = @ESTOQUE_ID";

            command.Parameters.AddWithValue("@PRODUTO_ID", idProduto);
            command.Parameters.AddWithValue("@ESTOQUE_ID", idEstoque);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                connection.Dispose();
            }
        }

        public int BuscarUltimoEstoque()
        {
            string connectionString = SqlUtils.CONNECTION_STRING;
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = connectionString;

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "SELECT * FROM ESTOQUE order by ID asc";

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                List<Estoque> clientes = new List<Estoque>();

                while (reader.Read())
                {
                    return Convert.ToInt32(reader["ID"]);
                }

                return 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }
            finally
            {
                connection.Dispose();
            }
        }

        public bool RelacionarProdutoEstoque(int idEstoque, int idProduto)
        {
            string connectionString = SqlUtils.CONNECTION_STRING;
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = connectionString;

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "INSERT INTO PRODUTO_ESTOQUE (PRODUTO_ID, ESTOQUE_ID) " +
                "VALUES (@PRODUTO_ID, @ESTOQUE_ID)";

            command.Parameters.AddWithValue("@ESTOQUE_ID", idEstoque);
            command.Parameters.AddWithValue("@PRODUTO_ID", idProduto);

            try
            {
                if (!VerificaEstoqueProduto(idEstoque, idProduto))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
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
            command.CommandText = "DELETE FROM ESTOQUE WHERE ID = @ID";
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
                    resposta.Message = "Estoque não pode ser excluído, pois" +
                        "\nexistem atividades vinculadas a ele!";
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

        public DataResponse<Estoque> GetAll()
        {
            string connectionString = SqlUtils.CONNECTION_STRING;
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = connectionString;

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "SELECT * FROM ESTOQUE ORDER BY ID";

            DataResponse<Estoque> resposta = new DataResponse<Estoque>();

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                List<Estoque> estoques = new List<Estoque>();

                while (reader.Read())
                {
                    Estoque estoque = new Estoque();

                    estoque.ID = Convert.ToInt32(reader["ID"]);
                    estoque.DataEntrada = Convert.ToDateTime(reader["DATA_ENTRADA"]);
                    estoque.ValorUnitario = Convert.ToDouble(reader["VALOR_UNITARIO"]);
                    estoque.Quantidade = Convert.ToInt32(reader["QUANTIDADE"]);

                    estoques.Add(estoque);
                }

                resposta.Success = true;
                resposta.Message = "Dados selecionados com sucesso!";
                resposta.Data = estoques;
                return resposta;

            }
            catch (Exception ex)
            {
                resposta.Success = false;
                resposta.Message = "Erro no banco de dados, contate o administrador.";
                resposta.Data = new List<Estoque>();
                return resposta;
            }
            finally
            {
                connection.Dispose();
            }
        }

        public Response Insert(Estoque e)
        {
            string connectionString = SqlUtils.CONNECTION_STRING;
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = connectionString;

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "INSERT INTO ESTOQUE (DATA_ENTRADA, VALOR_UNITARIO, QUANTIDADE) " +
                "VALUES " +
                "(@DATA_ENTRADA, @VALOR_UNITARIO, @QUANTIDADE)";

            command.Parameters.AddWithValue("@DATA_ENTRADA", e.DataEntrada);
            command.Parameters.AddWithValue("@VALOR_UNITARIO", e.ValorUnitario);
            command.Parameters.AddWithValue("@QUANTIDADE", e.Quantidade);

            Response resposta = new Response();
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                resposta.Success = true;
                resposta.Message = "Estoque cadastrada com sucesso!";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Success = false;

                if (ex.Message.Contains("UQ__ESTOQUE"))
                {
                    resposta.Message = "Estoque já cadastrado!";
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

        public Response Update(Estoque e)
        {
            string connectionString = SqlUtils.CONNECTION_STRING;
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = connectionString;

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "UPDATE ESTOQUE " +
                "SET DATA_ENTRADA = @DATA_ENTRADA," +
                " VALOR_UNITARIO = @VALOR_UNITARIO," +
                " QUANTIDADE = @QUANTIDADE " +
                "WHERE ID = @ID";
            command.Parameters.AddWithValue("@DATA_ENTRADA", e.DataEntrada);
            command.Parameters.AddWithValue("@VALOR_UNITARIO", e.ValorUnitario);
            command.Parameters.AddWithValue("@QUANTIDADE", e.Quantidade);
            command.Parameters.AddWithValue("@ID", e.ID);


            Response resposta = new Response();
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                resposta.Success = true;
                resposta.Message = "Estoque editada com sucesso!";
                return resposta;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
