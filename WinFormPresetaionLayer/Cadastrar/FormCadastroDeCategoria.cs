using BussinesLogicalLayer;
using Entites;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormPresetaionLayer
{
    public partial class FormCadastroDeCategoria : Form
    {

        private StandardValidation standardValidation = new StandardValidation();
        private CategoriasBLL categoriaBLL = new CategoriasBLL();

        public FormCadastroDeCategoria()
        {
            InitializeComponent();
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

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            if (ValidarCampos())
            {
                Categorias categorias = new Categorias();

                categorias.Nome = txtNome.Text;

                categoriaBLL.Insert(categorias);
            }
        }
    }
}
