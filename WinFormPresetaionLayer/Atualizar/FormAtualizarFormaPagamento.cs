using BussinesLogicalLayer;
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

namespace WinFormPresetaionLayer.Atualizar
{
    public partial class FormAtualizarFormaPagamento : Form
    {
        private StandardValidation standardValidation = new StandardValidation();
        private FormaPagamentoBLL formaPagamentoBLL = new FormaPagamentoBLL();
        public FormAtualizarFormaPagamento()
        {
            InitializeComponent();
            BuscarFormaPagamento();
        }

        private void BuscarFormaPagamento()
        {
            DataResponse<FormaPagamento> dataResponse = formaPagamentoBLL.GetAll();

            dgvFormaPagamento.DataSource = dataResponse.Data;
        }

        private List<Label> CriarListaLabel()
        {
            List<Label> listaLabel = new List<Label>();

            listaLabel.Add(lblNome);

            return listaLabel;
        }

        private bool ValidarCampos()
        {
            lblNome.ForeColor = standardValidation.ValidationsLabel(standardValidation.ValidationsNome(txtNome.Text));

            if ((standardValidation.ValidationColor(CriarListaLabel())))
            {
                return true;
            }
            return false;
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (standardValidation.VerificarLinhasDeUmaGrid(dgvFormaPagamento))
            {
                Response response = formaPagamentoBLL.Delete((int) dgvFormaPagamento.SelectedRows[0].Cells["ID"].Value);

                if (response.Success)
                {
                    MessageBox.Show(response.Message, "Sucesso");
                    BuscarFormaPagamento();
                }
                else
                {
                    MessageBox.Show(response.Message, "Erro");
                }
            }
            else
            {
                MessageBox.Show("Selecione uma linha da tabela." +
                    "\nOu certifique-se que os dados são válidos.", "Atenção!");
            }
        }

        private void dgvFormaPagamento_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (standardValidation.VerificarLinhasDeUmaGrid(dgvFormaPagamento))
            {
                txtNome.Text = (string) dgvFormaPagamento.SelectedRows[0].Cells["Nome"].Value;
            }
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            if ((standardValidation.VerificarLinhasDeUmaGrid(dgvFormaPagamento))
                && (ValidarCampos()))
            {
                FormaPagamento formaPagamento = new FormaPagamento();

                formaPagamento.ID = (int) dgvFormaPagamento.SelectedRows[0].Cells["ID"].Value;                
                formaPagamento.Nome = txtNome.Text;

                Response response = formaPagamentoBLL.Update(formaPagamento);

                if (response.Success)
                {
                    MessageBox.Show(response.Message, "Sucesso");
                    BuscarFormaPagamento();
                }
                else
                {
                    MessageBox.Show(response.Message, "Erro");
                }
            }
            else
            {
                MessageBox.Show("Selecione uma linha da tabela." +
                    "\nOu certifique-se que os dados são válidos.", "Atenção!");
            }
        }
    }
}
