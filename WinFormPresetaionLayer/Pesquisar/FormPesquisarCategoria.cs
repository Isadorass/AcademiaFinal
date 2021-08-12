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

namespace WinFormPresetaionLayer.Pesquisar
{
    public partial class FormPesquisarCategoria : Form
    {
        CategoriasBLL categoriasBLL = new CategoriasBLL();
        StandardValidation standardValidation = new StandardValidation();
        public FormPesquisarCategoria()
        {
            InitializeComponent();
            BuscarCategorias();
        }

        private void BuscarCategorias()
        {
            DataResponse<Categorias> dataResponse = categoriasBLL.GetAll();

            dgvCategoria.DataSource = dataResponse.Data;
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            if (standardValidation.ValidationNullOrWhiteSpace(txtNome.Text))
            {
                DataResponse<Categorias> dataResponse = categoriasBLL.PesquisarPorNome(txtNome.Text);

                dgvCategoria.DataSource = dataResponse.Data;

                BuscarCategorias();
            }
            else 
            {
                BuscarCategorias();
            }

        }
    }
}
