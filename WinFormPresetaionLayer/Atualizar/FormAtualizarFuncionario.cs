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
    public partial class FormAtualizarFuncionario : Form
    {

        private ModalidadesBLL modalidadesBLL = new ModalidadesBLL();
        private FuncionarioBLL funcionarioBLL = new FuncionarioBLL();
        private StandardValidation standardValidation = new StandardValidation();

        public FormAtualizarFuncionario()
        {
            InitializeComponent();
            BuscarModalidades();
            BuscarFuncionarios();
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
            listaLabel.Add(lblTelefone);
            listaLabel.Add(lblSalario);
            listaLabel.Add(lblRua);
            listaLabel.Add(lblNumero);
            listaLabel.Add(lblBairro);
            listaLabel.Add(lblCidade);
            listaLabel.Add(lblComplemento);

            return listaLabel;
        }

        private bool ValidarCampos()
        {
            lblNome.ForeColor = standardValidation.ValidationsLabel(standardValidation.ValidationsNome(txtNome.Text));
            lblEmail.ForeColor = standardValidation.ValidationsLabel(standardValidation.ValidationsEmail(txtEmail.Text));
            lblTelefone.ForeColor = standardValidation.ValidationsLabel(standardValidation.ValidationsTelefone(txtTelefone.Text));
            lblSalario.ForeColor = standardValidation.ValidationsLabel(standardValidation.ValidationsSalario(txtSalario.Text));
            lblRua.ForeColor = standardValidation.ValidationsLabel(standardValidation.ValidationsRua(txtRua.Text));
            lblNumero.ForeColor = standardValidation.ValidationsLabel(standardValidation.ValidationNumero(txtNumero.Text));
            lblBairro.ForeColor = standardValidation.ValidationsLabel(standardValidation.ValidationsBairro(txtBairro.Text));
            lblCidade.ForeColor = standardValidation.ValidationsLabel(standardValidation.ValidationsCidade(txtCidade.Text));
            lblComplemento.ForeColor = standardValidation.ValidationsLabel(standardValidation.ValidationNullOrWhiteSpace(txtComplemento.Text));

            if ((standardValidation.ValidationColor(CriarListaLabel())))
            {
                return true;
            }
            return false;
        }

        private void BuscarFuncionarios()
        {
            DataResponse<Funcionario> dataResponse = funcionarioBLL.GetAll();

            dgvFuncionario.DataSource = dataResponse.Data;
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
            if ((standardValidation.VerificarLinhasDeUmaGrid(dgvFuncionario))
                && (ValidarCampos()))
            {
                Funcionario funcionario = new Funcionario();

                funcionario.CPF = Convert.ToString(dgvFuncionario.SelectedRows[0].Cells["CPF"].Value);
                funcionario.Nome = txtNome.Text;
                funcionario.Email = (txtEmail.Text);
                funcionario.Telefone = (txtTelefone.Text);
                funcionario.Cidade = txtCidade.Text;
                funcionario.Bairro = txtBairro.Text;
                funcionario.Rua = txtRua.Text;
                funcionario.Numero = txtNumero.Text;
                funcionario.Complemento = txtComplemento.Text;
                funcionario.Salario = Convert.ToDouble(txtSalario.Text);
                funcionario.Ativo = true;
                funcionario.Comissao = 0;
                funcionario.Senha = txtSenha.Text;
                funcionario.Papeis = cmbPapel.Text;

                Response response = funcionarioBLL.Update(funcionario);

                if (VerificaCheckbox())
                {
                    foreach (int idModalidade in PegarValorCheckBox())
                    {
                        funcionarioBLL.RelacionarModalidadeFuncionario((int)dgvFuncionario.SelectedRows[0].Cells["ID"].Value,
                            idModalidade);
                    }
                }

                if (response.Success)
                {
                    BuscarFuncionarios();
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

        private void dgvFuncionario_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (standardValidation.VerificarLinhasDeUmaGrid(dgvFuncionario))
            {
                txtNome.Text = (string)dgvFuncionario.SelectedRows[0].Cells["NOME"].Value;
                txtSalario.Text = Convert.ToString(dgvFuncionario.SelectedRows[0].Cells["SALARIO"].Value);
                txtEmail.Text = Convert.ToString(dgvFuncionario.SelectedRows[0].Cells["EMAIL"].Value);
                txtSenha.Text = Convert.ToString(dgvFuncionario.SelectedRows[0].Cells["SENHA"].Value);
                txtTelefone.Text = Convert.ToString(dgvFuncionario.SelectedRows[0].Cells["TELEFONE"].Value);
                txtRua.Text = Convert.ToString(dgvFuncionario.SelectedRows[0].Cells["RUA"].Value);
                txtBairro.Text = Convert.ToString(dgvFuncionario.SelectedRows[0].Cells["BAIRRO"].Value);
                txtCidade.Text = Convert.ToString(dgvFuncionario.SelectedRows[0].Cells["CIDADE"].Value);
                txtNumero.Text = Convert.ToString(dgvFuncionario.SelectedRows[0].Cells["NUMERO"].Value);
                txtComplemento.Text = Convert.ToString(dgvFuncionario.SelectedRows[0].Cells["COMPLEMENTO"].Value);                
                cmbPapel.Text = Convert.ToString(dgvFuncionario.SelectedRows[0].Cells["PAPEIS"].Value);
            }
        }

        private void btnDesativar_Click(object sender, EventArgs e)
        {
            if ((standardValidation.VerificarLinhasDeUmaGrid(dgvFuncionario)))
            {
                Funcionario funcionario = new Funcionario();

                Response response = funcionarioBLL.Delete(Convert.ToString(dgvFuncionario.SelectedRows[0].Cells["CPF"].Value));

                if (response.Success)
                {
                    BuscarFuncionarios();
                    MessageBox.Show(response.Message);
                }
                else
                {
                    MessageBox.Show(response.Message);
                }
            }
            else
            {
                MessageBox.Show("Selecione uma linha da tabela.");
            }
        }
    }
}
