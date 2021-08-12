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
    public class ModalidadesDAL : IModalidadesService
    {
        public DataResponse<Modalidades> PesquiasarPorDescricao(string descricao)
        {
            string connectionString = SqlUtils.CONNECTION_STRING;
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = connectionString;

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "SELECT * FROM MODALIDADE where DESCRICAO like @DESCRICAO ORDER BY ID";

            command.Parameters.AddWithValue("@DESCRICAO", descricao);

            DataResponse<Modalidades> resposta = new DataResponse<Modalidades>();

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                List<Modalidades> modalidades = new List<Modalidades>();

                while (reader.Read())
                {
                    Modalidades modalidade = new Modalidades();
                    modalidade.ID = Convert.ToInt32(reader["ID"]);
                    modalidade.Descricao = Convert.ToString(reader["DESCRICAO"]);
                    modalidade.Valor = Convert.ToDouble(reader["VALOR"]);

                    modalidades.Add(modalidade);
                }

                resposta.Success = true;
                resposta.Message = "Dados selecionados com sucesso!";
                resposta.Data = modalidades;
                return resposta;

            }
            catch (Exception ex)
            {
                resposta.Success = false;
                resposta.Message = "Erro no banco de dados, contate o administrador.";
                resposta.Data = new List<Modalidades>();
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
            command.CommandText = "DELETE FROM MODALIDADE WHERE ID = @ID";
            command.Parameters.AddWithValue("@ID", id);

            Response resposta = new Response();
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                resposta.Success = true;
                resposta.Message = "Modalidade excluída com sucesso!";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Success = false;
                if (ex.Message.Substring(0, 10).Equals("The DELETE"))
                {
                    resposta.Message = "Modalidade não pode ser excluída," +
                        "pois\n existem atividades vinculadas a ela!";
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

        public DataResponse<Modalidades> GetAll()
        {
            string connectionString = SqlUtils.CONNECTION_STRING;
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = connectionString;

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "SELECT * FROM MODALIDADE ORDER BY ID";

            DataResponse<Modalidades> resposta = new DataResponse<Modalidades>();

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                List<Modalidades> modalidades = new List<Modalidades>();

                while (reader.Read())
                {
                    Modalidades modalidade = new Modalidades();
                    modalidade.ID = Convert.ToInt32(reader["ID"]);
                    modalidade.Descricao = Convert.ToString(reader["DESCRICAO"]);
                    modalidade.Valor = Convert.ToDouble(reader["VALOR"]);

                    modalidades.Add(modalidade);
                }

                resposta.Success = true;
                resposta.Message = "Dados selecionados com sucesso!";
                resposta.Data = modalidades;
                return resposta;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                resposta.Success = false;
                resposta.Message = "Erro no banco de dados, contate o administrador.";
                resposta.Data = new List<Modalidades>();
                return resposta;
            }
            finally
            {
                connection.Dispose();
            }
        }

        public Response Insert(Modalidades m)
        {
            string connectionString = SqlUtils.CONNECTION_STRING;
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = connectionString;

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "INSERT INTO MODALIDADE" +
                "(DESCRICAO, VALOR) VALUES (@DESCRICAO, @VALOR)";
            command.Parameters.AddWithValue("@DESCRICAO", m.Descricao);
            command.Parameters.AddWithValue("@VALOR", m.Valor);


            Response resposta = new Response();
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                resposta.Success = true;
                resposta.Message = "Modalidade cadastrada com sucesso!";
                return resposta;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                resposta.Success = false;

                if (ex.Message.Contains("UQ__MODALIDADE"))
                {
                    resposta.Message = "Modalidade já cadastrada!";
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

        public Response Update(Modalidades m)
        {
            string connectionString = SqlUtils.CONNECTION_STRING;
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = connectionString;

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "UPDATE MODALIDADE " +
                "SET DESCRICAO = @DESCRICAO, VALOR = @VALOR " +
                "WHERE ID = @ID";
            command.Parameters.AddWithValue("@DESCRICAO", m.Descricao);
            command.Parameters.AddWithValue("@VALOR", m.Valor);
            command.Parameters.AddWithValue("@ID", m.ID);


            Response resposta = new Response();
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                resposta.Success = true;
                resposta.Message = "Modalidade editada com sucesso!";
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
