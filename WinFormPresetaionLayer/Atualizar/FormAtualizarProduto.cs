using BussinesLogicalLayer;
using Entites;
using Entities.DTO;
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
    public partial class FormAtualizarProduto : Form
    {
        private ProdutosBLL produtosBLL = new ProdutosBLL();
        private StandardValidation standardValidation = new StandardValidation();
        CategoriasBLL categoriasBLL = new CategoriasBLL();
        public FormAtualizarProduto()
        {
            InitializeComponent();
            BuscarCategoria();
            BuscarProdutos();
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

            listaLabel.Add(lblNomeProduto);
            listaLabel.Add(lblDescricaoProduto);
            listaLabel.Add(lblCategoria);

            return listaLabel;
        }

        private bool ValidarCampos()
        {
            lblNomeProduto.ForeColor = standardValidation.ValidationsLabel(standardValidation.ValidationsNome(txtNome.Text));
            lblDescricaoProduto.ForeColor = standardValidation.ValidationsLabel(standardValidation.ValidationNullOrWhiteSpace(txtDescricao.Text));
            lblCategoria.ForeColor = standardValidation.ValidationsLabel(standardValidation.ValidationNullOrWhiteSpace(cmbCategoria.Text));
            
            if ((standardValidation.ValidationColor(CriarListaLabel())))
            {
                return true;
            }
            return false;
        }

        private void BuscarProdutos()
        {
            DataResponse<Produtos> dataResponse = produtosBLL.GetAll();
            List<ProdutoDTO> listaProdutoDTO = new List<ProdutoDTO>();

            foreach (Produtos produtos in dataResponse.Data)
            {
                ProdutoDTO produtoDTO = new ProdutoDTO();

                produtoDTO.ID = produtos.ID;
                produtoDTO.Nome = produtos.Nome;
                produtoDTO.Descricao = produtos.Descricao;
                produtoDTO.Categoria = produtos.Categoria.Nome;

                listaProdutoDTO.Add(produtoDTO);
            }

            dgvProduto.DataSource = listaProdutoDTO;
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (standardValidation.VerificarLinhasDeUmaGrid(dgvProduto) && ValidarCampos())
            {
                Response response = produtosBLL.Delete((int) dgvProduto.SelectedRows[0].Cells["ID"].Value);

                if (response.Success)
                {
                    MessageBox.Show(response.Message, "Sucesso");
                    BuscarProdutos();
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
            if ((standardValidation.VerificarLinhasDeUmaGrid(dgvProduto)) && ValidarCampos())
            {
                Produtos produto = new Produtos();

                produto.ID = (int) dgvProduto.SelectedRows[0].Cells["ID"].Value;
                produto.Descricao = txtDescricao.Text;
                produto.Nome = txtNome.Text;
                MessageBox.Show(Convert.ToString(cmbCategoria.SelectedValue));
                produto.Categoria.ID = (int) cmbCategoria.SelectedValue;

                Response response = produtosBLL.Update(produto);

                if (response.Success)
                {
                    MessageBox.Show(response.Message, "Sucesso");
                    BuscarProdutos();
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

        private void dgwProduto_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (standardValidation.VerificarLinhasDeUmaGrid(dgvProduto))
            {
                txtDescricao.Text = (string) dgvProduto.SelectedRows[0].Cells["Descricao"].Value;
                txtNome.Text = (string) dgvProduto.SelectedRows[0].Cells["Nome"].Value;
                cmbCategoria.Text = (string)dgvProduto.SelectedRows[0].Cells["Categoria"].Value;
            }
        }

        private void cmbCategoria_Click(object sender, EventArgs e)
        {
            BuscarCategoria();
        }
    }
}
