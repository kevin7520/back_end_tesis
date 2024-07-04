-- Crear la base de datos y usarla
DROP DATABASE IF EXISTS BD_TEKA;
CREATE DATABASE BD_TEKA;
USE BD_TEKA;

-- Crear la tabla Rol
CREATE TABLE Rol (
    IdRol INT AUTO_INCREMENT PRIMARY KEY,
    NombreRol VARCHAR(50) NOT NULL
);

-- Crear la tabla EstadoUsuario
CREATE TABLE EstadoUsuario (
    IdEstado INT AUTO_INCREMENT PRIMARY KEY,
    NombreEstado VARCHAR(50) NOT NULL
);

-- Crear la tabla Usuario
CREATE TABLE Usuario (
    IdUsuario INT AUTO_INCREMENT PRIMARY KEY,
    NombreUsuario VARCHAR(50) NOT NULL,
    Contraseña VARCHAR(50) NOT NULL,
    Correo VARCHAR(50) NOT NULL,
    IdRol INT,
    IdEstado INT,
    FOREIGN KEY (IdRol) REFERENCES Rol(IdRol),
    FOREIGN KEY (IdEstado) REFERENCES EstadoUsuario(IdEstado)
);

-- Crear la tabla Ciudad
CREATE TABLE Ciudad (
    IdCiudad INT AUTO_INCREMENT PRIMARY KEY,
    NombreCiudad VARCHAR(50) NOT NULL
);

-- Crear la tabla Cliente
CREATE TABLE Cliente (
    IdCliente INT AUTO_INCREMENT PRIMARY KEY,
    Cedula VARCHAR(50) NOT NULL,
    Nombres VARCHAR(50) NOT NULL,
    Telefono VARCHAR(20),
    Direccion VARCHAR(50),
    Correo VARCHAR(50),
    IdCiudad INT,
    FOREIGN KEY (IdCiudad) REFERENCES Ciudad(IdCiudad)
);

-- Crear la tabla EstadoTecnico
CREATE TABLE EstadoTecnico (
    IdEstado INT AUTO_INCREMENT PRIMARY KEY,
    NombreEstado VARCHAR(50) NOT NULL
);

-- Crear la tabla Tecnico
CREATE TABLE Tecnico (
    IdTecnico INT AUTO_INCREMENT PRIMARY KEY,
    NombreTecnico VARCHAR(50) NOT NULL,
    Cedula VARCHAR(50) NOT NULL,
    TelefonoTecnico VARCHAR(20) NOT NULL,
    IdEstado INT,
    FOREIGN KEY (IdEstado) REFERENCES EstadoTecnico(IdEstado)
);

-- Crear la tabla Categoria
CREATE TABLE Categoria (
    IdCategoria INT AUTO_INCREMENT PRIMARY KEY,
    NombreCategoria VARCHAR(100) NOT NULL
);

-- Crear la tabla EstadoProducto
CREATE TABLE EstadoProducto (
    IdEstadoProducto INT AUTO_INCREMENT PRIMARY KEY,
    NombreEstadoProducto VARCHAR(100) NOT NULL
);

-- Crear la tabla Producto
CREATE TABLE Producto (
    IdProducto INT AUTO_INCREMENT PRIMARY KEY,
    IdCategoria INT,
    CodigoProducto VARCHAR(50) NOT NULL,
    Modelo VARCHAR(50) NOT NULL,
    IdEstadoProducto INT,
    SerieProducto VARCHAR(50) NOT NULL,
    Precio DECIMAL(10, 2) NOT NULL,
    FOREIGN KEY (IdCategoria) REFERENCES Categoria(IdCategoria),
    FOREIGN KEY (IdEstadoProducto) REFERENCES EstadoProducto(IdEstadoProducto)
);

-- Crear la tabla Repuesto
CREATE TABLE Repuesto (
    IdRepuesto INT AUTO_INCREMENT PRIMARY KEY,
    CodigoRepuesto VARCHAR(50) NOT NULL,
    NombreRepuesto VARCHAR(50) NOT NULL,
    Cantidad INT NOT NULL,
    Precio DECIMAL(10, 2) NOT NULL
);

-- Crear la tabla EstadoProforma
CREATE TABLE EstadoProforma (
    IdEstadoProforma INT AUTO_INCREMENT PRIMARY KEY,
    NombreEstadoProforma VARCHAR(50) NOT NULL
);

-- Crear la tabla Proforma
CREATE TABLE Proforma (
    IdProforma INT AUTO_INCREMENT PRIMARY KEY,
    DescripcionProducto VARCHAR(100) NOT NULL,
    Subtotal DECIMAL(10, 2) NOT NULL,
    Iva DECIMAL(10, 2) NOT NULL,
    Total DECIMAL(10, 2) NOT NULL,
    IdCliente INT,
    IdProducto INT,
    IdEstadoProforma INT,
    FOREIGN KEY (IdCliente) REFERENCES Cliente(IdCliente),
    FOREIGN KEY (IdEstadoProforma) REFERENCES EstadoProforma(IdEstadoProforma),
    FOREIGN KEY (IdProducto) REFERENCES Producto(IdProducto)
);

-- Crear la tabla DetalleProforma
CREATE TABLE DetalleProforma (
    IdDetalleProforma INT AUTO_INCREMENT PRIMARY KEY,
    IdProforma INT,
    Cantidad INT NOT NULL,
    DescripcionRepuesto VARCHAR(100) NOT NULL,
    PrecioUnitario DECIMAL(10, 2) NOT NULL,
    PrecioFinal DECIMAL(10, 2) NOT NULL,
    FOREIGN KEY (IdProforma) REFERENCES Proforma(IdProforma)
);

-- Crear la tabla Pedido
CREATE TABLE Pedido (
    IdPedido INT AUTO_INCREMENT PRIMARY KEY,
    IdCliente INT,
    TipoPedido VARCHAR(50) NOT NULL,
    FechaPedido DATE NOT NULL,
    IdProducto INT,
    FOREIGN KEY (IdCliente) REFERENCES Cliente(IdCliente),
    FOREIGN KEY (IdProducto) REFERENCES Producto(IdProducto)
);

-- Crear la tabla DetallePedido
CREATE TABLE DetallePedido (
    IdDetallePedido INT AUTO_INCREMENT PRIMARY KEY,
    IdPedido INT,
    Cantidad INT NOT NULL,
    DescripcionRepuesto VARCHAR(100) NOT NULL,
    FOREIGN KEY (IdPedido) REFERENCES Pedido(IdPedido)
);
