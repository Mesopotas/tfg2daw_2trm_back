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
