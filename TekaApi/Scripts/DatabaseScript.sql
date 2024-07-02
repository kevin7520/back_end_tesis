DROP DATABASE IF EXISTS BD_MODULOS;
CREATE DATABASE BD_MODULOS;
USE BD_MODULOS;

-- Tabla para los roles
CREATE TABLE Rol (
    IdRol INT AUTO_INCREMENT PRIMARY KEY,
    NombreRol VARCHAR(50) NOT NULL
);

-- Tabla para los estados de usuario
CREATE TABLE EstadoUsuario (
    IdEstado INT AUTO_INCREMENT PRIMARY KEY,
    NombreEstado VARCHAR(50) NOT NULL
);

-- Tabla para los usuarios
CREATE TABLE Usuario (
    IdUsuario INT AUTO_INCREMENT PRIMARY KEY,
    NombreUsuario VARCHAR(50) NOT NULL,
    Contraseña VARCHAR(60) NOT NULL,
    Correo VARCHAR(50) NOT NULL,
    IdRol INT,
    IdEstado INT,
    FOREIGN KEY (IdRol) REFERENCES Rol(IdRol),
    FOREIGN KEY (IdEstado) REFERENCES EstadoUsuario(IdEstado)
);

-- Tabla para las ciudades
CREATE TABLE Ciudad (
    IdCiudad INT AUTO_INCREMENT PRIMARY KEY,
    NombreCiudad VARCHAR(50) NOT NULL
);

-- Tabla para los clientes
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

-- Tabla para los técnicos
CREATE TABLE Tecnico (
    IdTecnico INT AUTO_INCREMENT PRIMARY KEY,
    NombreTecnico VARCHAR(50) NOT NULL,
    Cedula VARCHAR(50) NOT NULL,
    TelefonoTecnico VARCHAR(20) NOT NULL,
    EstadoTecnico VARCHAR(50) NOT NULL
);

-- Tabla para los servicios
CREATE TABLE Servicio (
    IdServicio INT AUTO_INCREMENT PRIMARY KEY,
    IdCliente INT,
    IdTecnico INT,
    TipoServicio VARCHAR(50) NOT NULL,
    FechaTentativaAtencion DATE NOT NULL,
    Estado VARCHAR(50) NOT NULL,
    FOREIGN KEY (IdCliente) REFERENCES Cliente(IdCliente),
    FOREIGN KEY (IdTecnico) REFERENCES Tecnico(IdTecnico)
);

CREATE TABLE Categoria (
    IdCategoria INT AUTO_INCREMENT PRIMARY KEY,
    NombreCategoria VARCHAR(100) NOT NULL UNIQUE
);

CREATE TABLE EstadoProducto (
    IdEstadoProducto INT AUTO_INCREMENT PRIMARY KEY,
    NombreEstadoProducto VARCHAR(100) NOT NULL UNIQUE
);

-- Tabla para los productos
CREATE TABLE Producto (
    IdProducto INT AUTO_INCREMENT PRIMARY KEY,
	IdCategoria INT,
    CodigoProducto VARCHAR(50) NOT NULL UNIQUE,
    Modelo VARCHAR(50) NOT NULL,
    IdEstadoProducto INT,
    SerieProducto VARCHAR(50) NOT NULL UNIQUE,
    Precio DECIMAL(10, 2) NOT NULL,
    FOREIGN KEY (IdCategoria) REFERENCES Categoria(IdCategoria),
    FOREIGN KEY (IdEstadoProducto) REFERENCES EstadoProducto(IdEstadoProducto)
);

-- Tabla para los repuestos
CREATE TABLE Repuesto (
    IdRepuesto INT AUTO_INCREMENT PRIMARY KEY,
    CodigoRepuesto VARCHAR(50) NOT NULL,
    NombreRepuesto VARCHAR(50) NOT NULL,
    Cantidad INT NOT NULL,
    Precio DECIMAL(10, 2) NOT NULL
);

-- Tabla para las proformas
CREATE TABLE Proforma (
    IdProforma INT AUTO_INCREMENT PRIMARY KEY,
    IdCliente INT,
    FechaCompra DATE NOT NULL,
    NumeroFactura VARCHAR(50) NOT NULL,
    NombreAlmacen VARCHAR(50) NOT NULL,
    FOREIGN KEY (IdCliente) REFERENCES Cliente(IdCliente)
);

-- Tabla para los detalles de proforma
CREATE TABLE DetalleProforma (
    IdDetalleProforma INT AUTO_INCREMENT PRIMARY KEY,
    IdProforma INT,
    Cantidad INT NOT NULL,
    DescripcionRepuesto VARCHAR(100) NOT NULL,
    PrecioUnitario DECIMAL(10, 2) NOT NULL,
    PrecioFinal DECIMAL(10, 2) NOT NULL,
    FOREIGN KEY (IdProforma) REFERENCES Proforma(IdProforma)
);

-- Tabla para los pedidos
CREATE TABLE Pedido (
    IdPedido INT AUTO_INCREMENT PRIMARY KEY,
    IdCliente INT,
    TipoPedido VARCHAR(50) NOT NULL,
    FechaPedido DATE NOT NULL,
    FOREIGN KEY (IdCliente) REFERENCES Cliente(IdCliente)
);

-- Tabla para los detalles de pedido
CREATE TABLE DetallePedido (
    IdDetallePedido INT AUTO_INCREMENT PRIMARY KEY,
    IdPedido INT,
    Cantidad INT NOT NULL,
    DescripcionRepuesto VARCHAR(100) NOT NULL,
    FOREIGN KEY (IdPedido) REFERENCES Pedido(IdPedido)
);
