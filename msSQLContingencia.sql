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


-- Creacion de tabla Salas y Salas

CREATE TABLE Salas (
    idSala INT PRIMARY KEY IDENTITY(1,1),
    nombre VARCHAR(50) NOT NULL,
    capacidad INT NOT NULL
);


INSERT INTO Salas (nombre, capacidad)
VALUES ('Sala 1', 100);

INSERT INTO Salas (nombre, capacidad)
VALUES ('Sala 2', 100);


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
