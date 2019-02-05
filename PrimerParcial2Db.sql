CREATE DATABASE PrimerParcial2Db

go
Use PrimerParcial2Db
go
create table Cuentas 
(
	CuentaId int primary key identity(1,1),
	Fecha Date,
	Nombre varchar(50),
	Balance Decimal


);

go
create table Depositos
(
	DepositoId int primary key identity(1,1),
	Fecha Date,
	CuentaId int references Cuentas(CuentaId),
	Concepto varchar(50),
	Monto Decimal,

);


select * from Cuentas;
select * from Depositos;
