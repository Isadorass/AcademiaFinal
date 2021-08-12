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

namespace WinFormsPresentationLayer
{
    public partial class FormCadastroDeProdutos : Form
    {
        private StandardValidation standardValidation = new StandardValidation();
        private ProdutosBLL produtosBLL = new ProdutosBLL();
        private CategoriasBLL categoriasBLL = new CategoriasBLL();

        public FormCadastroDeProdutos()
        {
            InitializeComponent();
            BuscarCategoria();
        }        

        private void BuscarCategoria()
        {
            DataResponse<Categorias> dataResponse = categoriasBLL.GetAll();

            if (dataResponse.Success)
            {
                cmbCategoria.DataSource = dataResponse.Data;
                cmbCategoria.ValueMember = "ID";
                cmbCategoria.DisplayMember = "Nome";
            }            
        }

        private List<Label> CriarListaLabel()
        {
            List<Label> listaLabel = new List<Label>();

            listaLabel.Add(lblCategoria);
            listaLabel.Add(lblDescricaoProduto);
            listaLabel.Add(lblNomeProduto);

            return listaLabel;
        }
        
        private bool ValidarCampos()
        {
            lblCategoria.ForeColor = standardValidation.ValidationsLabel(standardValidation.ValidationNullOrWhiteSpace(cmbCategoria.Text));
            lblDescricaoProduto.ForeColor = standardValidation.ValidationsLabel(standardValidation.ValidationsCaracteres(txtDescricaoProduto.Text));
            lblNomeProduto.ForeColor = standardValidation.ValidationsLabel(standardValidation.ValidationsNome(txtNomeProduto.Text));

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
                Produtos p = new Produtos();
                Categorias categorias = new Categorias();

                categorias.ID = (int)cmbCategoria.SelectedValue;
                MessageBox.Show(Convert.ToString(categorias.ID));

                p.Nome = txtNomeProduto.Text;
                p.Categoria.ID = categorias.ID;
                p.Descricao = txtDescricaoProduto.Text;

                Response response = produtosBLL.Insert(p);

                MessageBox.Show(response.Message);
            }
        }
    }
}
