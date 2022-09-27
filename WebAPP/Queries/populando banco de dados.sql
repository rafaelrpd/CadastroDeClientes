USE CadastroDeClientes
GO

INSERT INTO estados
VALUES
('S�o Paulo', 'SP'),
('Rio de Janeiro', 'RJ'),
('Paran�', 'PR');
GO

INSERT INTO cidades
VALUES
('S�o Paulo', 1),
('Guarulhos', 1),
('Mairipor�', 1),
('Rio de Janeiro', 2),
('Niter�i', 2),
('Maca�', 2),
('Curitiba', 3),
('Londrina', 3),
('Maring�', 3);
GO

INSERT INTO clientes
VALUES
('11345678900', 'Teste da Silva', '1990-01-10', 'M', 'Rua 123 de junho', 1),
('22345678900', 'Testando', '1980-05-20', 'M', 'Rua 456 de mar�o', 4),
('33345678900', 'Testada', '1970-09-30', 'F', 'Rua 789 de dezembro', 7);
GO
