-- Cria o banco de dados e o usa.
CREATE DATABASE CadastroDeClientes;
GO

-- Criar tabelas
-- Tabela estados
USE CadastroDeClientes;
CREATE TABLE estados
(estadoId INT IDENTITY (1,1) NOT NULL,
nome VARCHAR(MAX) NOT NULL,
sigla CHAR(2) NOT NULL,
CONSTRAINT PK_estadoId PRIMARY KEY CLUSTERED (estadoId));
GO

-- Tabela cidades
USE CadastroDeClientes;
CREATE TABLE cidades
(cidadeId INT IDENTITY (1,1) NOT NULL,
nome VARCHAR(MAX) NOT NULL,
estadoId INT NOT NULL,
CONSTRAINT PK_cidadeId PRIMARY KEY CLUSTERED (cidadeId),
CONSTRAINT FK_estadoId FOREIGN KEY (estadoId) REFERENCES estados(estadoId));
GO

-- Tabela clientes
USE CadastroDeClientes;
CREATE TABLE clientes
(cpf CHAR(11) NOT NULL,
nome VARCHAR(MAX) NOT NULL,
dataNascimento DATE NOT NULL,
sexo CHAR(1) NOT NULL,
endereco VARCHAR(MAX) NOT NULL,
cidadeId INT NOT NULL,

CONSTRAINT PK_cpf PRIMARY KEY CLUSTERED (cpf),
CONSTRAINT FK_cidadeId FOREIGN KEY (cidadeId) REFERENCES cidades(cidadeId));
GO