using BussinesLogicalLayer;
using DataAccessLayer;
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

namespace WinFormsPresentationLayer
{
    public partial class FormCadastroDeFuncionario : Form
    {
        private StandardValidation standardValidation = new StandardValidation();
        private FuncionarioBLL funcionarioBLL = new FuncionarioBLL();
        private ModalidadesBLL modalidadesBLL = new ModalidadesBLL();

        public FormCadastroDeFuncionario()
        {
            InitializeComponent();
            BuscarModalidades();
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
                MessageBox.Show("Escolha as modalidades para o cliente.", "Atenção");
            return false;
        }
        private List<Label> CriarListaLabel()
        {
            List<Label> listaLabel = new List<Label>();

            listaLabel.Add(lblNome);
            listaLabel.Add(lblEmail);
            listaLabel.Add(lblTelefone);
            listaLabel.Add(lblCPF);
            listaLabel.Add(lblRG);
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
            lblCPF.ForeColor = standardValidation.ValidationsLabel(standardValidation.ValidationsCpf(txtCPF.Text));
            lblRG.ForeColor = standardValidation.ValidationsLabel(standardValidation.ValidationsRua(txtRG.Text));
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

        private void btnCadastrar_Click(object sender, EventArgs e)
        {

            if (ValidarCampos() && VerificaCheckbox())
            {
                Funcionario funcionario = new Funcionario();

                funcionario.Nome = txtNome.Text;
                funcionario.Email = (txtEmail.Text);
                funcionario.Telefone = (txtTelefone.Text);
                funcionario.Cidade = txtCidade.Text;
                funcionario.Bairro = txtBairro.Text;
                funcionario.Rua = txtRua.Text;
                funcionario.Numero = txtNumero.Text;
                funcionario.Complemento = txtComplemento.Text;
                funcionario.CPF = (txtCPF.Text);
                funcionario.RG = (txtRG.Text);
                funcionario.Salario = Convert.ToDouble(txtSalario.Text);
                funcionario.Ativo = true;
                funcionario.Comissao = 0;
                funcionario.Senha = txtSenha.Text;
                funcionario.Papeis = cmbPapel.Text;

                funcionarioBLL.Insert(funcionario);

                foreach (int idModalidade in PegarValorCheckBox())
                {
                    funcionarioBLL.RelacionarModalidadeFuncionario(funcionarioBLL.BuscarPeloCPF(funcionario.CPF),
                        idModalidade);
                }

                MessageBox.Show("Cliente cadastrado com sucesso!", "Sucesso!");
            }
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
                    int idModalidae = Convert.ToInt32(row.Cells["ID"].Value);
                    listaIdModalidade.Add(idModalidae);
                }
            }
            return listaIdModalidade;
        }
    }
}
