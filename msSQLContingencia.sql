-- INSTALACION DE IMAGENES Y CONTENEDORES PARA DOCKER

-- Descarga de la imagen con la version correcta de la BBDD de MicrosoftSQL

-- docker pull mcr.microsoft.com/mssql/server:2019-CU21-ubuntu-20.04

-- Creacion del contenedor para la imagen de msSQL

-- docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=<YourStrong@Passw0rd>" -dp 1433:1433 mcr.microsoft.com/mssql/server:2019-CU21-ubuntu-20.04

-- Datos
---- Usuario: sa
---- Contraseña: <YourStrong@Passw0rd>


-- CONSULTAS PARA LA CREACION DE LA BASE DE DATOS

-- Creación de la base de datos
CREATE DATABASE CoworkingDB;

USE CoworkingDB;

CREATE TABLE Roles (
    IdRol INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100),
    Descripcion NVARCHAR(255)
);

CREATE TABLE Usuarios (
    IdUsuario INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100),
    Apellido NVARCHAR(100),
    Email NVARCHAR(255),
    Contrasenia NVARCHAR(255),
    FechaRegistro DATETIME DEFAULT GETDATE(),
    IdRol INT,
    FOREIGN KEY (IdRol) REFERENCES Roles(IdRol)
);

CREATE TABLE TipoSalas (
    IdTipoSala INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100)
);

CREATE TABLE Sala (
    IdSala INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100),
    Capacidad INT,
    IdTipoSala INT,
    FOREIGN KEY (IdTipoSala) REFERENCES TipoSalas(IdTipoSala)
);

CREATE TABLE Mesas (
    IdMesa INT IDENTITY(1,1) PRIMARY KEY,
    NumAsientos INT DEFAULT 4,
    IdSala INT,
    FOREIGN KEY (IdSala) REFERENCES Sala(IdSala)
);

CREATE TABLE Asientos (
    IdAsiento INT IDENTITY(1,1) PRIMARY KEY,
    NumAsiento INT,
    Estado NVARCHAR(50),
    Precio DECIMAL(10,2),
    IdMesa INT,
    FOREIGN KEY (IdMesa) REFERENCES Mesas(IdMesa)
);

CREATE TABLE Lineas (
    IdLinea INT IDENTITY(1,1) PRIMARY KEY,
    IdAsiento INT,
    FOREIGN KEY (IdAsiento) REFERENCES Asientos(IdAsiento)
);

CREATE TABLE Reservas (
    IdReserva INT IDENTITY(1,1) PRIMARY KEY,
    IdUsuario INT,
    IdLinea INT,
    FOREIGN KEY (IdUsuario) REFERENCES Usuarios(IdUsuario),
    FOREIGN KEY (IdLinea) REFERENCES Lineas(IdLinea)
);

CREATE TABLE Facturas (
    IdFactura UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
    IdReserva INT,
    Precio DECIMAL(10,2),
    FOREIGN KEY (IdReserva) REFERENCES Reservas(IdReserva)
);