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
    public partial class FormAtualizarCategoria : Form
    {
        private CategoriasBLL categoriasBLL = new CategoriasBLL();
        private StandardValidation standardValidation = new StandardValidation();
        public FormAtualizarCategoria()
        {
            InitializeComponent();
            BuscarCategorias();
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

        private void BuscarCategorias()
        {
            DataResponse<Categorias> dataResponse = categoriasBLL.GetAll();

            dgvCategoria.DataSource = dataResponse.Data;
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (standardValidation.VerificarLinhasDeUmaGrid(dgvCategoria))
            {
                Response response = categoriasBLL.Delete((int)dgvCategoria.SelectedRows[0].Cells["ID"].Value);

                if (response.Success)
                {
                    MessageBox.Show(response.Message, "Sucesso");
                    BuscarCategorias();
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

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            if (standardValidation.VerificarLinhasDeUmaGrid(dgvCategoria)
                 && (ValidarCampos()))
            {
                Categorias categorias = new Categorias();

                categorias.ID = (int) dgvCategoria.SelectedRows[0].Cells["ID"].Value;
                categorias.Nome = txtNome.Text;

                Response response = categoriasBLL.Update(categorias);

                if (response.Success)
                {
                    MessageBox.Show(response.Message, "Sucesso");
                    BuscarCategorias();
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

        private void dgvCategoria_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (standardValidation.VerificarLinhasDeUmaGrid(dgvCategoria))
            {
                txtNome.Text = (string)dgvCategoria.SelectedRows[0].Cells["NOME"].Value;
            }
        }
    }
}
