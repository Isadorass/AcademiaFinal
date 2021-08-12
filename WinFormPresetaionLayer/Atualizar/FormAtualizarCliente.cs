using BussinesLogicalLayer;
using Entites;
using Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormPresetaionLayer.Atualizar
{
    public partial class FormAtualizarCliente : Form
    {
        private ModalidadesBLL modalidadesBLL = new ModalidadesBLL();
        private ClientesBLL clientesBLL = new ClientesBLL();
        private StandardValidation standardValidation = new StandardValidation();

        public FormAtualizarCliente()
        {
            InitializeComponent();
            BuscarModalidades();
            BuscarClientes();
        }

        private bool VerificaCheckbox()
        {
            foreach (DataGridViewRow row in dgvModalidade.Rows)
            {
                if (Convert.ToBoolean(row.Cells["Checkbox"].Value))
                {
                    return true;
                }
            }
            return false;
        }

        private List<Label> CriarListaLabel()
        {
            List<Label> listaLabel = new List<Label>();

            listaLabel.Add(lblNome);
            listaLabel.Add(lblEmail);
            listaLabel.Add(lblTelefoneCelular);
            listaLabel.Add(lblTelefoneFixo);
            listaLabel.Add(lblDataMatricula);
            listaLabel.Add(lblGenero);

            return listaLabel;
        }

        private bool ValidarCampos()
        {
            lblNome.ForeColor = standardValidation.ValidationsLabel(standardValidation.ValidationsNome(txtNome.Text));
            lblEmail.ForeColor = standardValidation.ValidationsLabel(standardValidation.ValidationsEmail(txtEmail.Text));
            lblTelefoneCelular.ForeColor = standardValidation.ValidationsLabel(standardValidation.ValidationsTelefone(txtTelefoneCelular.Text));
            lblTelefoneFixo.ForeColor = standardValidation.ValidationsLabel(standardValidation.ValidationsTelefone(txtTelefoneFixo.Text));
            lblDataMatricula.ForeColor = standardValidation.ValidationsLabel(standardValidation.ValidationsDataMatricula(dtpDataMatricula.Text));
            lblGenero.ForeColor = standardValidation.ValidationsLabel(standardValidation.ValidationsGenero(cmbGenero.Text));

            if ((standardValidation.ValidationColor(CriarListaLabel())))
            {
                return true;
            }
            return false;
        }

        private void BuscarClientes()
        {
            DataResponse<Clientes> dataResponse = clientesBLL.GetAll();

            dgvCliente.DataSource = dataResponse.Data;
        }

        private void BuscarModalidades()
        {
            DataResponse<Modalidades> response = modalidadesBLL.GetAll();
            List<Modalidades> listaModalidades = new List<Modalidades>();


            foreach (Modalidades modalidade in response.Data)
            {
                Modalidades modalidades = new Modalidades();

                modalidades.ID = modalidade.ID;
                modalidades.Descricao = modalidade.Descricao;
                modalidades.Valor = modalidade.Valor;

                listaModalidades.Add(modalidades);
            }
            if (response.Success)
            {
                var dataGridViewCheckBoxColumn = new DataGridViewCheckBoxColumn();
                dataGridViewCheckBoxColumn.Name = "Checkbox";
                dataGridViewCheckBoxColumn.HeaderText = "Selecionado";
                dataGridViewCheckBoxColumn.FalseValue = false;
                dataGridViewCheckBoxColumn.TrueValue = true;

                //Make the default checked
                dataGridViewCheckBoxColumn.CellTemplate.Value = true;
                dataGridViewCheckBoxColumn.CellTemplate.Style.NullValue = false;

                dgvModalidade.Columns.Insert(0, dataGridViewCheckBoxColumn);

                this.dgvModalidade.DataSource = listaModalidades;
            }
            else
            {
                MessageBox.Show(response.Message);
            }
        }

        private List<int> PegarValorCheckBox()
        {
            List<int> listaIdModalidade = new List<int>();

            foreach (DataGridViewRow row in dgvModalidade.Rows)
            {
                if (row.IsNewRow) continue;

                if (Convert.ToBoolean(row.Cells["Checkbox"].FormattedValue))
                {
                    int idProduto = Convert.ToInt32(row.Cells["ID"].Value);
                    listaIdModalidade.Add(idProduto);
                }
            }
            return listaIdModalidade;
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            if ((standardValidation.VerificarLinhasDeUmaGrid(dgvCliente))
                && (ValidarCampos()))
            {
                Clientes clientes = new Clientes();

                clientes.CPF = Convert.ToString(dgvCliente.SelectedRows[0].Cells["CPF"].Value);
                clientes.Nome = txtNome.Text;
                clientes.Email = (txtEmail.Text);
                clientes.TelefonePrincipal = (txtTelefoneCelular.Text);
                clientes.TelefoneSecundario = txtTelefoneFixo.Text;
                clientes.DataMatricula = Convert.ToDateTime(dtpDataMatricula.Text);
                clientes.Genero = cmbGenero.Text;

                Response response = clientesBLL.Update(clientes);

                if (VerificaCheckbox())
                {
                    foreach (int idModalidade in PegarValorCheckBox())
                    {
                        clientesBLL.RelacionarModalidadeCliente((int)dgvCliente.SelectedRows[0].Cells["ID"].Value,
                            idModalidade);
                    }
                }

                if (response.Success)
                {
                    BuscarClientes();
                    MessageBox.Show(response.Message);
                }
                else
                {
                    MessageBox.Show(response.Message);
                }
            }
            else
            {
                MessageBox.Show("Selecione uma linha da tabela." +
                    "\nOu certifique-se que os dados são válidos.", "Atenção!");
            }
        }

        private void btnDesativar_Click(object sender, EventArgs e)
        {

        }

        private void dgvCliente_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (standardValidation.VerificarLinhasDeUmaGrid(dgvCliente))
            {
                txtNome.Text = (string)dgvCliente.SelectedRows[0].Cells["NOME"].Value;
                txtEmail.Text = Convert.ToString(dgvCliente.SelectedRows[0].Cells["EMAIL"].Value);
                txtTelefoneCelular.Text = Convert.ToString(dgvCliente.SelectedRows[0].Cells["TELEFONEPRINCIPAL"].Value);
                txtTelefoneFixo.Text = Convert.ToString(dgvCliente.SelectedRows[0].Cells["TELEFONESECUNDARIO"].Value);
                cmbGenero.Text = Convert.ToString(dgvCliente.SelectedRows[0].Cells["GENERO"].Value);
                dtpDataMatricula.Text = Convert.ToString(dgvCliente.SelectedRows[0].Cells["DATAMATRICULA"].Value);
            }
        }
    }
}
