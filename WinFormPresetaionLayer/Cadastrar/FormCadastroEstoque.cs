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

namespace WinFormPresetaionLayer
{
    public partial class FormCadastroEstoque : Form
    {
        private EstoqueBLL estoqueBLL = new EstoqueBLL();
        private ProdutosBLL produtosBLL = new ProdutosBLL();
        private StandardValidation standardValidation = new StandardValidation();
        public FormCadastroEstoque()
        {
            InitializeComponent();
            BuscarProduto();
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
            lblDataEntrada.ForeColor = standardValidation.ValidationsLabel(standardValidation.ValidationsDataMatricula(dtpDataEntrada.Text));
            lblQuantidade.ForeColor = standardValidation.ValidationsLabel(standardValidation.ValidationValor(txtQuantidade.Text));
            lblValorUnitario.ForeColor = standardValidation.ValidationsLabel(standardValidation.ValidationValor(txtValorUnitario.Text));

            if ((standardValidation.ValidationColor(CriarListaLabel())))
            {
                return true;
            }
            return false;
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
            MessageBox.Show("Escolha as modalidades para o cliente.", "Atenção");
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

        private void dtpDataEntrada_ValueChanged(object sender, EventArgs e)
        {

        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            if (ValidarCampos() && VerificaCheckbox())
            {
                Estoque estoque = new Estoque();

                estoque.DataEntrada = Convert.ToDateTime(dtpDataEntrada.Text);
                estoque.ValorUnitario = Convert.ToDouble(txtValorUnitario.Text);
                estoque.Quantidade = Convert.ToInt32(txtQuantidade.Text);                

                estoqueBLL.Insert(estoque);

                foreach (int idProduto in PegarValorCheckBox())
                {
                    estoqueBLL.RelacionarProdutoEstoque(estoqueBLL.BuscarUltimoEstoque(),
                        idProduto);
                }
                MessageBox.Show("Cliente cadastrado com sucesso!", "Sucesso!");
            }
        }
    }
}
