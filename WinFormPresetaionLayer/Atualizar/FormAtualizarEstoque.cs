using BussinesLogicalLayer;
using Entites;
using Entities;
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
    public partial class FormAtualizarEstoque : Form
    {
        private StandardValidation standardValidation = new StandardValidation();
        EstoqueBLL estoqueBLL = new EstoqueBLL();
        ProdutosBLL produtosBLL = new ProdutosBLL();
        public FormAtualizarEstoque()
        {
            InitializeComponent();
            BuscarEstoques();
            BuscarProduto();
        }

        private bool VerificaCheckbox()
        {
            foreach (DataGridViewRow row in dgvProduto.Rows)
            {
                if (Convert.ToBoolean(row.Cells["Checkbox"].Value))
                {
                    return true;
                }
            }
            return false;
        }

        private void BuscarProduto()
        {
            DataResponse<Produtos> response = produtosBLL.GetAll();
            List<ProdutoDTO> listaModalidades = new List<ProdutoDTO>();

            if (response.Success)
            {
                foreach (Produtos produto in response.Data)
                {
                    ProdutoDTO produtos = new ProdutoDTO();

                    produtos.ID = produto.ID;
                    produtos.Nome = produto.Nome;
                    produtos.Descricao = produto.Descricao;
                    produtos.Categoria = produto.Categoria.Nome;

                    listaModalidades.Add(produtos);
                }

                var dataGridViewCheckBoxColumn = new DataGridViewCheckBoxColumn();
                dataGridViewCheckBoxColumn.Name = "Checkbox";
                dataGridViewCheckBoxColumn.HeaderText = "Selecionado";
                dataGridViewCheckBoxColumn.FalseValue = false;
                dataGridViewCheckBoxColumn.TrueValue = true;

                //Make the default checked
                dataGridViewCheckBoxColumn.CellTemplate.Value = true;
                dataGridViewCheckBoxColumn.CellTemplate.Style.NullValue = false;

                dgvProduto.Columns.Insert(0, dataGridViewCheckBoxColumn);

                this.dgvProduto.DataSource = listaModalidades;
            }
            else
            {
                MessageBox.Show(response.Message);
            }
        }

        private List<int> PegarValorCheckBox()
        {
            List<int> listaIdModalidade = new List<int>();

            foreach (DataGridViewRow row in dgvProduto.Rows)
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

        private void BuscarEstoques()
        {
            DataResponse<Estoque> dataResponse = estoqueBLL.GetAll();

            dgvEstoque.DataSource = dataResponse.Data;
        }

        private List<Label> CriarListaLabel()
        {
            List<Label> listaLabel = new List<Label>();

            listaLabel.Add(lblDataEntrada);
            listaLabel.Add(lblQuantidade);
            listaLabel.Add(lblValorUnitario);

            return listaLabel;
        }

        private bool ValidarCampos()
        {
            lblDataEntrada.ForeColor = standardValidation.ValidationsLabel(standardValidation.ValidationNumero(txtQuantidade.Text));
            lblQuantidade.ForeColor = standardValidation.ValidationsLabel(standardValidation.ValidationNumero(txtValorUnitario.Text));
            lblValorUnitario.ForeColor = standardValidation.ValidationsLabel(standardValidation.ValidationsDataMatricula(dtpDataEntrada.Text));

            if ((standardValidation.ValidationColor(CriarListaLabel())))
            {
                return true;
            }
            return false;
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (standardValidation.VerificarLinhasDeUmaGrid(dgvEstoque))
            {
                Response response = estoqueBLL.Delete((int)dgvEstoque.SelectedRows[0].Cells["ID"].Value);

                if (response.Success)
                {
                    MessageBox.Show(response.Message, "Sucesso");
                    BuscarEstoques();
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

        private void dgvEstoque_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (standardValidation.VerificarLinhasDeUmaGrid(dgvEstoque))
            {
                txtQuantidade.Text = Convert.ToString(dgvEstoque.SelectedRows[0].Cells["Quantidade"].Value);
                txtValorUnitario.Text = Convert.ToString(dgvEstoque.SelectedRows[0].Cells["ValorUnitario"].Value);
                dtpDataEntrada.Text = Convert.ToString(dgvEstoque.SelectedRows[0].Cells["DataEntrada"].Value);
            }
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            if ((standardValidation.VerificarLinhasDeUmaGrid(dgvEstoque))
                && (ValidarCampos()))
            {
                Estoque estoque = new Estoque();

                estoque.ID = (int) dgvEstoque.SelectedRows[0].Cells["ID"].Value;
                estoque.Quantidade = Convert.ToInt32(txtQuantidade.Text);
                estoque.ValorUnitario = Convert.ToDouble(txtValorUnitario.Text);
                estoque.DataEntrada = Convert.ToDateTime(dtpDataEntrada.Text);

                Response response = estoqueBLL.Update(estoque);

                if (VerificaCheckbox())
                {
                    foreach (int idProduto in PegarValorCheckBox())
                    {
                        estoqueBLL.RelacionarProdutoEstoque((int) dgvEstoque.SelectedRows[0].Cells["ID"].Value,
                            idProduto);
                    }
                }
                
                if (response.Success)
                {
                    MessageBox.Show(response.Message, "Sucesso");
                    BuscarEstoques();
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
