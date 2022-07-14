
use [Nexoz]
go
create table Autor (
IdAutor int primary key identity,
Nombre varchar (100),
Fecha_Nacimiento date,
Ciudad_Procedencia varchar (100),
Correo varchar (100)
);
create table Genero (
IdGenero int primary key identity,
Descipcion varchar (100),
);

create table Libros(
IdLibros int primary key identity,
Titulo varchar (MAX),
Año varchar (MAX),
IdGenero int foreign key references Genero,
NumPaginas varchar (MAX),
IdAutor int foreign key references Autor)
;