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
    public partial class FormAtualizarModalidade : Form
    {
        private StandardValidation standardValidation = new StandardValidation();
        private ModalidadesBLL modalidadesBLL = new ModalidadesBLL();
        public FormAtualizarModalidade()
        {
            InitializeComponent();
            BuscarModalidades();
        }

        private List<Label> CriarListaLabel()
        {
            List<Label> listaLabel = new List<Label>();

            listaLabel.Add(lblDescricao);
            listaLabel.Add(lblValor);

            return listaLabel;
        }

        private bool ValidarCampos()
        {
            lblDescricao.ForeColor = standardValidation.ValidationsLabel(standardValidation.ValidationNullOrWhiteSpace(txtDescricao.Text));
            lblValor.ForeColor = standardValidation.ValidationsLabel(standardValidation.ValidationNumero(txtValor.Text));

            if ((standardValidation.ValidationColor(CriarListaLabel())))
            {
                return true;
            }
            return false;
        }

        private void BuscarModalidades()
        {
            DataResponse<Modalidades> dataResponse = modalidadesBLL.GetAll();

            dgvModalidade.DataSource = dataResponse.Data;
        }

        private void dgvModalidade_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (standardValidation.VerificarLinhasDeUmaGrid(dgvModalidade))
            {
                txtDescricao.Text = (string) dgvModalidade.SelectedRows[0].Cells["Descricao"].Value;
                txtValor.Text = Convert.ToString(dgvModalidade.SelectedRows[0].Cells["Valor"].Value);
            }
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            if ((standardValidation.VerificarLinhasDeUmaGrid(dgvModalidade))
                && (ValidarCampos()))
            {
                Modalidades modalidades = new Modalidades();

                modalidades.ID = (int) dgvModalidade.SelectedRows[0].Cells["ID"].Value;
                modalidades.Descricao = txtDescricao.Text;
                modalidades.Valor = Convert.ToDouble(txtValor.Text);

                Response response = modalidadesBLL.Update(modalidades);

                if (response.Success)
                {
                    MessageBox.Show(response.Message, "Sucesso");
                    BuscarModalidades();
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

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (standardValidation.VerificarLinhasDeUmaGrid(dgvModalidade))
            {
                Response response = modalidadesBLL.Delete((int) dgvModalidade.SelectedRows[0].Cells["ID"].Value);

                if (response.Success)
                {
                    MessageBox.Show(response.Message, "Sucesso");
                    BuscarModalidades();
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
