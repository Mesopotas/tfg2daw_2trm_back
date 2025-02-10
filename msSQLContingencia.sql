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

-- Tabla roles Usuarios
CREATE TABLE Roles (
    IdRol INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(30),
    Descripcion NVARCHAR(255)
);

-- Tabla de Usuarios
CREATE TABLE Usuarios (
    IdUsuario INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100),
    Apellidos NVARCHAR(200),
    DNI NVARCHAR(16),
    Email NVARCHAR(255),
    Contrasenia NVARCHAR(255),
    Telefono NVARCHAR(15),
    FechaRegistro DATETIME DEFAULT GETDATE(),
    IdRol INT,
    FOREIGN KEY (IdRol) REFERENCES Roles(IdRol)
);

/* PROVISIONAL, NO IMPLEMENTADO DE MOMENTO
CREATE TABLE Mesas (
    IdMesa INT IDENTITY(1,1) PRIMARY KEY,
    NumAsientos INT -- numero de asientos que hay por cada mesa
);
*/
-- Tabla TipoSalas
CREATE TABLE TipoSalas (
    IdTipoSala INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100)
);

-- Tabla de Salas
CREATE TABLE Salas (
    IdSala INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100),
    Tipo NVARCHAR(50) DEFAULT 'Privada',
    Capacidad INT,
    PrecioPorHora DECIMAL(10,2),
    IdTipoSala INT,
    FOREIGN KEY (IdTipoSala) REFERENCES TipoSalas(IdTipoSala)
);


-- Tabla de Asientos
CREATE TABLE Asientos (
    IdAsiento INT IDENTITY(1,1) PRIMARY KEY,
    NumAsiento INT,
    Estado CHAR DEFAULT '0', -- 0 será disponible, 1 será ocupado, y 2 será bloqueado por administrador
    IdSala INT,
    FOREIGN KEY (IdSala) REFERENCES Salas(IdSala)
);

-- Tabla de Reservas
CREATE TABLE Reservas (
    IdReserva INT IDENTITY(1,1) PRIMARY KEY,
    UsuarioID INT,
    SalaID INT,
    Fecha DATE,
    HoraInicio DATETIME,
    HorasReservadas INT,
    PrecioReserva DECIMAL(10,2),
    FOREIGN KEY (UsuarioID) REFERENCES Usuarios(IdUsuario),
    FOREIGN KEY (SalaID) REFERENCES Salas(IdSala)
);

-- Tabla de Reservas de Asientos
CREATE TABLE ReservasAsientos (
    IdReservaAsiento INT IDENTITY(1,1) PRIMARY KEY,
    ReservaID INT,
    AsientoID INT,
    FOREIGN KEY (ReservaID) REFERENCES Reservas(IdReserva),
    FOREIGN KEY (AsientoID) REFERENCES Asientos(IdAsiento)
);

CREATE TABLE Facturas (
    IdFactura INT IDENTITY(1,1) PRIMARY KEY,
    IdReservaAsiento INT,
    FOREIGN KEY(IdReservaAsiento) REFERENCES ReservasAsientos(IdReservaAsiento)
)


/* insert rol de ejemplo */
INSERT INTO Roles(Nombre, Descripcion)
VALUES('Cliente','Usuario normal, sin privilegios')


;

/* insert usuario de ejemplo, requiere al menos 1 rol previo */
INSERT INTO Usuarios(Nombre, Apellidos, DNI, Email, Contrasenia, Telefono, IdRol)
VALUES('Juan','Hernandez Gimenez', '123456789M','a@a.com','password123','123456789',1);
