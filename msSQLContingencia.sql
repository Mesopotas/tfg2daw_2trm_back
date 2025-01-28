-- INSTALACION DE IMAGENES Y CONTENEDORES PARA DOCKER

-- Descarga de la imagen con la version correcta de la BBDD de MicrosoftSQL

-- docker pull mcr.microsoft.com/mssql/server:2019-CU21-ubuntu-20.04

-- Creacion del contenedor para la imagen de msSQL

-- docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=<YourStrong@Passw0rd>" -dp 1433:1433 mcr.microsoft.com/mssql/server:2019-CU21-ubuntu-20.04

-- Datos
---- Usuario: sa
---- Contraseña: <YourStrong@Passw0rd>





-- CONSULTAS PARA LA CREACION DE LA BASE DE DATOS

CREATE DATABASE cinema;

USE cinema;


-- Creacion de tabla Usuarios y Usuarios


CREATE TABLE Usuarios (
    idUsuario INT PRIMARY KEY IDENTITY(1,1),
    nombre VARCHAR(50) NOT NULL,
    email VARCHAR(50) NOT NULL UNIQUE,
    password VARCHAR(50) NOT NULL,
    fechaRegistro DATETIME NOT NULL DEFAULT GETDATE()
);

INSERT INTO Usuarios (nombre, email, password, fechaRegistro)
VALUES ('Carlos Pérez', 'carlos.perez@example.com', 'password123', GETDATE());

INSERT INTO Usuarios (nombre, email, password, fechaRegistro)
VALUES ('María López', 'maria.lopez@example.com', 'securePass456', GETDATE());

INSERT INTO Usuarios (nombre, email, password, fechaRegistro)
VALUES ('Luis Gómez', 'luis.gomez@example.com', 'mySecret789', GETDATE());


-- Creacion de tabla Salas


CREATE TABLE Salas (
    idSala INT PRIMARY KEY IDENTITY(1,1),
    nombre VARCHAR(50) NOT NULL,
    capacidad INT NOT NULL
);


-- Creacion de tabla Peliculas y Peliculas


CREATE TABLE Peliculas (
    idPelicula INT PRIMARY KEY IDENTITY(1,1),
    titulo VARCHAR(50) NOT NULL,
    sinopsis VARCHAR(50),
    duracion DECIMAL(4,3),
    categoria VARCHAR(50),
    director VARCHAR(50),
    anio DATETIME,
    imagenURL VARCHAR(50),
    puntuacion INT
);


INSERT INTO Peliculas (titulo, sinopsis, duracion, categoria, director, anio, imagenURL, puntuacion)
VALUES 
('Inception', 'Un ladrón roba secretos a través de sueños.', 2.148, 'Ciencia ficción', 'Christopher Nolan', '2010-07-16', 'https://imagen.com/inception.jpg', 9);

INSERT INTO Peliculas (titulo, sinopsis, duracion, categoria, director, anio, imagenURL, puntuacion)
VALUES 
('The Godfather', 'La historia de una familia mafiosa.', 2.555, 'Drama', 'Francis Ford Coppola', '1972-03-24', 'https://imagen.com/godfather.jpg', 10);

INSERT INTO Peliculas (titulo, sinopsis, duracion, categoria, director, anio, imagenURL, puntuacion)
VALUES 
('Toy Story', 'Un juguete lidera una aventura para volver a casa.', 1.400, 'Animación', 'John Lasseter', '1995-11-22', 'https://imagen.com/toystory.jpg', 8);


-- Creacion de tabla Asientos


CREATE TABLE Asientos (
    idAsiento INT PRIMARY KEY IDENTITY(1,1),
    idSala INT NOT NULL,      numAsiento INT NOT NULL,         
    precio DECIMAL(10, 2) NOT NULL DEFAULT 7.50,     
    estado BIT NOT NULL DEFAULT 1,     
    FOREIGN KEY (idSala) REFERENCES Salas(idSala) );
