-- INSTALACION DE IMAGENES Y CONTENEDORES PARA DOCKER

-- Descarga de la imagen con la version correcta de la BBDD de MicrosoftSQL

-- docker pull mcr.microsoft.com/mssql/server:2019-CU21-ubuntu-20.04

-- Creacion del contenedor para la imagen de msSQL

-- docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=<YourStrong@Passw0rd>" -dp 1433:1433 mcr.microsoft.com/mssql/server:2019-CU21-ubuntu-20.04

-- Datos
---- Usuario: sa
---- Contraseña: <YourStrong@Passw0rd>


-- CONSULTAS PARA LA CREACION DE LA BASE DE DATOS

/* PARA VER LAS TABLAS DE LA BBDD
USE CoWorkingDB;
SELECT * FROM information_schema.tables
WHERE table_type = 'BASE TABLE';
*/

-- Creación de la base de datos
CREATE DATABASE CoworkingDB;
USE CoworkingDB;

CREATE TABLE Sedes ( -- ubicacion fisica de la oficina
    IdSede INT IDENTITY(1,1) PRIMARY KEY,
    Pais VARCHAR(100),
    Ciudad VARCHAR(100),
    Direccion VARCHAR(250),
    CodigoPostal VARCHAR(5),
    Planta VARCHAR(100),
    URL_Imagen VARCHAR(250), -- imagen de la localizacion
    Observaciones VARCHAR(100)
);

CREATE TABLE TiposSalas ( -- privada o comun
    IdTipoSala INT IDENTITY(1,1) PRIMARY KEY,
    EsPrivada BIT DEFAULT 0, -- DEFAULT SERÁ FALSO, OSEA, PUBLICA
    Descripcion VARCHAR(100)
);

    -- INSERT para los 2 tipos de sala iniciales
    INSERT INTO TiposSalas (EsPrivada, Descripcion)
    VALUES (0, 'Sala Publica'); -- EsPrivada = 0 osea False, tendrá ID = 1

    INSERT INTO TiposSalas (EsPrivada, Descripcion)
    VALUES (1, 'Sala Privada'); -- EsPrivada = 1 osea True, tendrá ID = 2


CREATE TABLE Salas ( -- salas dentro de cada sede
    IdSala INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(50),
    URL_Imagen VARCHAR(250), --  imagen de la sala por dentro
    Capacidad INT,
    IdTipoSala INT,
    IdSede INT,
    Precio DECIMAL(10,2), -- precio sala privada o compartida
    Bloqueado BIT DEFAULT 0, -- para el rol del admin de bloquear puestos de trabajo
    FOREIGN KEY (IdSede) REFERENCES Sedes(IdSede),
    FOREIGN KEY (IdTipoSala) REFERENCES TiposSalas(IdTipoSala)
);

CREATE TABLE ZonasTrabajo ( -- aforo dentro de cada sala y sus detalles
    IdZonaTrabajo INT IDENTITY(1,1) PRIMARY KEY,
    NumPuestosTrabajo INT,
    Descripcion VARCHAR(250),
    IdSala INT,
    FOREIGN KEY (IdSala) REFERENCES Salas(IdSala)
);

CREATE TABLE TiposPuestosTrabajo ( -- define los tipos de puestos como silla, mesa, etc y su precio base a aplicar
    IdTipoPuestoTrabajo INT IDENTITY(1,1) PRIMARY KEY,
    Descripcion VARCHAR(200),
    Precio DECIMAL(10,2)
);

CREATE TABLE PuestosTrabajo ( -- puestos de trabajo dentro de cada zona de trabajo, en principio de base solo para sillas a escoger, ampliandolo en un futuro a poder elegir lotes de mesas
    IdPuestoTrabajo INT IDENTITY(1,1) PRIMARY KEY,
    URL_Imagen VARCHAR(250), -- la imagen del componente, como mesas, sillas, etc para el fetch
    Codigo INT,
    Estado INT,
    Capacidad INT,
    TipoPuesto INT,
    IdZonaTrabajo INT,
    IdTipoPuestoTrabajo INT,
    Bloqueado BIT DEFAULT 0, -- para el rol del admin de bloquear puestos de trabajo
    FOREIGN KEY (IdZonaTrabajo) REFERENCES ZonasTrabajo(IdZonaTrabajo),
    FOREIGN KEY (IdTipoPuestoTrabajo) REFERENCES TiposPuestosTrabajo(IdTipoPuestoTrabajo)

);

CREATE TABLE TramosHorarios ( -- intervalos de tiempo en los que hay disponibilidad
    IdTramoHorario INT IDENTITY(1,1) PRIMARY KEY,
    HoraInicio VARCHAR(5),
    HoraFin VARCHAR(5),
    DiaSemanal INT
);

CREATE TABLE Disponibilidades ( -- disponibilidad de puestos de trabajo o salas en una hora espcifica
    IdDisponibilidad INT IDENTITY(1,1) PRIMARY KEY,
    Fecha DATETIME,
    Estado BIT DEFAULT 1, -- por defecto estará disponible
    IdTramoHorario INT,
    IdSala INT,
    IdPuestoTrabajo INT,
    FOREIGN KEY (IdTramoHorario) REFERENCES TramosHorarios(IdTramoHorario),
    FOREIGN KEY (IdSala) REFERENCES Salas(IdSala),
    FOREIGN KEY (IdPuestoTrabajo) REFERENCES PuestosTrabajo(IdPuestoTrabajo)
);

CREATE TABLE DetallesReservas ( -- reserva puestos de trabajo
    IdDetalleReserva INT IDENTITY(1,1) PRIMARY KEY,
    Descripcion VARCHAR(250),
    IdPuestoTrabajo INT,
    IdDisponibilidad INT,
    FOREIGN KEY (IdPuestoTrabajo) REFERENCES PuestosTrabajo(IdPuestoTrabajo),
    FOREIGN KEY (IdDisponibilidad) REFERENCES Disponibilidades(IdDisponibilidad)
);

CREATE TABLE Roles ( -- roles de usuario (admin y cliente de base)
    IdRol INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(100),
    Descripcion VARCHAR(255)
);

-- INSERT 2 PRIMEROS ROLES INICIALES
INSERT INTO Roles (Nombre, Descripcion)
VALUES ('Admin', 'Rol con privilegios avanzados para gestión');

INSERT INTO Roles (Nombre, Descripcion)
VALUES ('Cliente', 'Rol limitado para consumidores');

CREATE TABLE Usuarios ( -- personas registradas en la plataforma
    IdUsuario INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100),
    Apellidos NVARCHAR(255),
    Email NVARCHAR(255),
    Contrasenia NVARCHAR(255),
    FechaRegistro DATETIME DEFAULT GETDATE(),
    IdRol INT DEFAULT 2, -- rol de id 2 por defecto será Usuario normal (cliente)
    FOREIGN KEY (IdRol) REFERENCES Roles(IdRol)
);

CREATE TABLE Reservas ( -- reservas realizadas por los usuarios
    IdReserva INT IDENTITY(1,1) PRIMARY KEY,
    IdUsuario INT,
    Fecha DATETIME,
    Descripcion VARCHAR(250),
    DescuentoAplicado DECIMAL(10,2) DEFAULT 0,
    FOREIGN KEY (IdUsuario) REFERENCES Usuarios(IdUsuario)
);

CREATE TABLE Lineas ( -- reserva relacionada con sus detalles especificos, aquello que veria el usuario a modo factura
    IdLinea UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
    IdReserva INT,
    IdDetalleReserva INT,
    FOREIGN KEY (IdReserva) REFERENCES Reservas(IdReserva),
    FOREIGN KEY (IdDetalleReserva) REFERENCES DetallesReservas(IdDetalleReserva)
);

/* AÑADIR PROXIMAMENTE CONFORME TODO ESTE HECHO 
CREATE TABLE Descuentos ( 
    IdDescuento INT IDENTITY(1,1) PRIMARY KEY,
    Descripcion VARCHAR(200),
    Porcentaje DECIMAL(5,2),
    MinHoras INT,
    MinAsientos INT
);
*/


/* BORRAR TODAS LAS TABLAS
DROP TABLE Lineas;
DROP TABLE DetallesReservas;
DROP TABLE Reservas;
DROP TABLE Disponibilidades;
DROP TABLE ZonasTrabajo;
DROP TABLE PuestosTrabajo;
DROP TABLE TiposPuestosTrabajo;
DROP TABLE Salas;
DROP TABLE TiposSalas;
DROP TABLE Sedes;
DROP TABLE Usuarios;
DROP TABLE Roles;
DROP TABLE TramosHorarios;


*/