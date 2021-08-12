using BussinesLogicalLayer;
using Entites;
using Entities;
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
    public partial class FormCadastroDeClientes : Form
    {

        private StandardValidation standardValidation = new StandardValidation();
        private ClientesBLL clientesBLL = new ClientesBLL();
        private ModalidadesBLL modalidadesBLL = new ModalidadesBLL();

        public FormCadastroDeClientes()
        {
            InitializeComponent();
            BuscarModadlidade();
        }

        private bool VerificaCheckbox()
        {
            foreach (DataGridViewRow row in dgvModalidades.Rows)
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
            listaLabel.Add(lblCPF);
            listaLabel.Add(lblRG);
            listaLabel.Add(lblTelefoneCelular);
            listaLabel.Add(lblTelefoneFixo);
            listaLabel.Add(lblEmail);
            listaLabel.Add(lblDataMatricula);
            listaLabel.Add(lblDataNascimento);
            listaLabel.Add(lblGenero);

            return listaLabel;
        }

        private bool ValidarCampos()
        {
            lblNome.ForeColor = standardValidation.ValidationsLabel(standardValidation.ValidationsNome(txtNome.Text));
            lblCPF.ForeColor = standardValidation.ValidationsLabel(standardValidation.ValidationsCpf(txtCPF.Text));
            lblRG.ForeColor = standardValidation.ValidationsLabel(standardValidation.ValidationsRg(txtRG.Text));
            lblTelefoneCelular.ForeColor = standardValidation.ValidationsLabel(standardValidation.ValidationsTelefone(txtTelefoneCelular.Text));
            lblTelefoneFixo.ForeColor = standardValidation.ValidationsLabel(standardValidation.ValidationsTelefone(txtTelefoneFixo.Text));
            lblEmail.ForeColor = standardValidation.ValidationsLabel(standardValidation.ValidationsEmail(txtEmail.Text));
            lblDataMatricula.ForeColor = standardValidation.ValidationsLabel(standardValidation.ValidationsDataMatricula(dtpDataMatricula.Text));
            lblDataNascimento.ForeColor = standardValidation.ValidationsLabel(standardValidation.ValidationsDataNascimento(dtpDataNascimento.Text));
            lblGenero.ForeColor = standardValidation.ValidationsLabel(standardValidation.ValidationsGenero(cmbGenero.Text));

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
                Clientes cliente = new Clientes();

                cliente.Nome = (txtNome.Text);
                cliente.CPF = (txtCPF.Text);
                cliente.RG = (txtRG.Text);
                cliente.TelefonePrincipal = (txtTelefoneCelular.Text);
                cliente.TelefoneSecundario = (txtTelefoneFixo.Text);
                cliente.Email = txtEmail.Text;
                cliente.DataNascimento = Convert.ToDateTime(dtpDataNascimento.Text);
                cliente.DataMatricula = Convert.ToDateTime(dtpDataMatricula.Text);
                cliente.Ativo = true;
                cliente.Genero = cmbGenero.Text;

                clientesBLL.Insert(cliente);

                foreach(int idModalidade in PegarValorCheckBox())
                {
                    clientesBLL.RelacionarModalidadeCliente(clientesBLL.BuscarPeloCPF(cliente.CPF),
                        idModalidade);
                }

                MessageBox.Show("Cliente cadastrado com sucesso!", "Sucesso!");
            }
        }

        private void BuscarModadlidade()
        {
            DataResponse<Modalidades> response = modalidadesBLL.GetAll();
            List<Modalidades> listaModalidades = new List<Modalidades>();

            foreach(Modalidades modalidade in response.Data)
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

                dgvModalidades.Columns.Insert(0, dataGridViewCheckBoxColumn);

                this.dgvModalidades.DataSource = listaModalidades;
            }
            else
            {
                MessageBox.Show(response.Message);
            }
        }

        private List<int> PegarValorCheckBox()
        {
            List<int> listaIdModalidade = new List<int>();

            foreach (DataGridViewRow row in dgvModalidades.Rows)
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
