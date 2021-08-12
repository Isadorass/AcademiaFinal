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
    public class FormaPagamentoDAL : IFormaPagamentoService
    {

        public DataResponse<FormaPagamento> PesquisarPorNome(string nome)
        {
            string connectionString = SqlUtils.CONNECTION_STRING;
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = connectionString;

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "SELECT * FROM FORMA_PAGAMENTO where NOME like @NOME ORDER BY ID";

            command.Parameters.AddWithValue("@NOME", "%"+nome+"%");

            DataResponse<FormaPagamento> resposta = new DataResponse<FormaPagamento>();

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                List<FormaPagamento> clientes = new List<FormaPagamento>();

                while (reader.Read())
                {
                    FormaPagamento formaPagamento = new FormaPagamento();

                    formaPagamento.ID = Convert.ToInt32(reader["ID"]);
                    formaPagamento.Nome = Convert.ToString(reader["NOME"]);

                    clientes.Add(formaPagamento);
                }

                resposta.Success = true;
                resposta.Message = "Dados selecionados com sucesso!";
                resposta.Data = clientes;
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Success = false;
                resposta.Message = "Erro no banco de dados, contate o administrador.";
                resposta.Data = new List<FormaPagamento>();
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
            command.CommandText = "DELETE FROM FORMA_PAGAMENTO WHERE ID = @ID";
            command.Parameters.AddWithValue("@ID", id);

            Response resposta = new Response();
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                resposta.Success = true;
                resposta.Message = "Forma de pagamento excluída com sucesso!";
                return resposta;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("FK__VENDA__FORMA_PAGAMENTO"))
                {
                    resposta.Message = "Forma de pagamento não pode ser" +
                        "excluída, pois existem vendas vinculadas a ela!";
                    return resposta;
                }
                resposta.Success = false;
                resposta.Message = "Erro no banco de dados, contate o administrador.";
                return resposta;
            }
            finally
            {
                connection.Dispose();
            }
        }

        public DataResponse<FormaPagamento> GetAll()
        {
            string connectionString = SqlUtils.CONNECTION_STRING;
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = connectionString;

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "SELECT * FROM FORMA_PAGAMENTO ORDER BY ID";

            DataResponse<FormaPagamento> resposta = new DataResponse<FormaPagamento>();

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                List<FormaPagamento> clientes = new List<FormaPagamento>();

                while (reader.Read())
                {
                    FormaPagamento formaPagamento = new FormaPagamento();

                    formaPagamento.ID = Convert.ToInt32(reader["ID"]);
                    formaPagamento.Nome = Convert.ToString(reader["NOME"]);                   

                    clientes.Add(formaPagamento);
                }

                resposta.Success = true;
                resposta.Message = "Dados selecionados com sucesso!";
                resposta.Data = clientes;
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Success = false;
                resposta.Message = "Erro no banco de dados, contate o administrador.";
                resposta.Data = new List<FormaPagamento>();
                return resposta;
            }
            finally
            {
                connection.Dispose();
            }
        }

        public Response Insert(FormaPagamento f)
        {
            string connectionString = SqlUtils.CONNECTION_STRING;
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = connectionString;

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "INSERT INTO FORMA_PAGAMENTO (NOME) VALUES " +
                "(@NOME)";
            command.Parameters.AddWithValue("@NOME", f.Nome);            

            Response resposta = new Response();
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                resposta.Success = true;
                resposta.Message = "Forma de pagamento cadastrada com sucesso!";
                return resposta;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                resposta.Success = false;

                if (ex.Message.Contains("UQ__FORMA_PAGAMENTO"))
                {
                    resposta.Message = "Forma de pagamento já cadastrado!";
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

        public Response Update(FormaPagamento f)
        {
            string connectionString = SqlUtils.CONNECTION_STRING;
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = connectionString;

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "UPDATE FORMA_PAGAMENTO SET NOME = @NOME" +
                " where ID = @ID;";

            command.Parameters.AddWithValue("@NOME", f.Nome);
            command.Parameters.AddWithValue("@ID", f.ID);            

            Response resposta = new Response();
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                resposta.Success = true;
                resposta.Message = "Forma de pagamento editada com sucesso!";
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
