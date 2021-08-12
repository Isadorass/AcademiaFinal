alter table funcionario
drop column ENDERECO;

alter table funcionario
add CIDADE varchar(40) not null, 
BAIRRO varchar(50) not null,
RUA varchar(50) not null,
NUMERO varchar(6) not null,
COMPLEMENTO varchar(50) not null;

select * from funcionario;