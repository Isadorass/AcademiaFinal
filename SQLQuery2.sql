--ADICONAR UNIQUE NESSAS PORRA

create table CLIENTE (
	ID integer identity primary key,
	NOME varchar(70) not null,
	CPF varchar(11) unique not null,
	RG varchar(7) unique not null,
	TELEFONE_1 varchar(13) not null,
	TELEFONE_2 varchar(13) not null,
	GENERO varchar(10) not null,
	EMAIL varchar(70) unique not null,
	DATA_NASCIMENTO DATE not null,
	DATA_MATRICULA DATE not null,
	ATIVO BIT not null
)

create table FUNCIONARIO (
	ID integer identity primary key,
	NOME varchar(70) not null,
	CPF varchar(11) unique not null,
	RG varchar(7) unique not null,
	ENDERECO varchar(200) not null,
	SALARIO float not null,
	COMISSAO float not null,
	EMAIL varchar(70) unique not null,
	SENHA varchar(100) not null,
	ATIVO BIT not null,
	PAPEL varchar(15) not null
)

create table MODALIDADE (
	ID integer identity primary key,
	DESCRICAO varchar(100) unique not null,
	VALOR float not null
)

create table FORMA_PAGAMENTO(
	ID integer identity primary key,
	NOME varchar(50) unique not null
)

create table CATEGORIA(
	ID integer identity primary key,
	NOME varchar(50) unique not null
)

create table PRODUTO(
	ID integer identity primary key,
	NOME varchar(50) not null,
	DESCRICAO varchar(100) not null,
	CATEGORIA_ID integer not null,
	FOREIGN KEY (CATEGORIA_ID) REFERENCES [dbo].[CATEGORIA] ([ID])
)

create table CLIENTE_MODALIDADE(
	CLIENTE_ID integer not null,
	MODALIDADE_ID integer not null,
	FOREIGN KEY (CLIENTE_ID) REFERENCES [dbo].[CLIENTE] ([ID]),
	FOREIGN KEY (MODALIDADE_ID) REFERENCES [dbo].[MODALIDADE] ([ID])
)

create table FUNCIONARIO_MODALIDADE(
	FUNCIONARIO_ID integer not null,
	MODALIDADE_ID integer not null,
	FOREIGN KEY (FUNCIONARIO_ID) REFERENCES [dbo].[FUNCIONARIO] ([ID]),
	FOREIGN KEY (MODALIDADE_ID) REFERENCES [dbo].[MODALIDADE] ([ID])
)

create table ESTOQUE(
	ID integer identity primary key,
	DATA_ENTRADA date not null,
	VALOR_UNITARIO float not null,
	QUANTIDADE integer not null,
)

create table PRODUTO_ESTOQUE(
	PRODUTO_ID integer not null,
	ESTOQUE_ID integer not null,
	FOREIGN KEY (PRODUTO_ID) REFERENCES [dbo].[PRODUTO] ([ID]),
	FOREIGN KEY (ESTOQUE_ID) REFERENCES [dbo].[ESTOQUE] ([ID])
)

create table VENDA(
	ID integer identity primary key,
	DATA_VENDA date not null,
	QUANTIDADE integer not null,
	CLIENTE_ID integer not null,
	FUNCIONARIO_ID integer not null,
	FORMA_PAGAMENTO_ID integer not null,
	FOREIGN KEY (CLIENTE_ID) REFERENCES [dbo].[CLIENTE] ([ID]),
	FOREIGN KEY (FUNCIONARIO_ID) REFERENCES [dbo].[FUNCIONARIO] ([ID]),
	FOREIGN KEY (FORMA_PAGAMENTO_ID) REFERENCES [dbo].[FORMA_PAGAMENTO] ([ID])
)

create table PRODUTO_VENDA(
	PRODUTO_ID integer not null,
	VENDA_ID integer not null,
	FOREIGN KEY (PRODUTO_ID) REFERENCES [dbo].[PRODUTO] ([ID]),
	FOREIGN KEY (VENDA_ID) REFERENCES [dbo].[VENDA] ([ID])
)
