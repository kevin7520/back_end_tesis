-- Crear la base de datos y usarla
DROP DATABASE IF EXISTS BD_TEKA;
CREATE DATABASE BD_TEKA;
USE BD_TEKA;

-- Tabla Rol
CREATE TABLE Rol (
    IdRol INT AUTO_INCREMENT PRIMARY KEY,
    NombreRol VARCHAR(100) NOT NULL
);

-- Tabla EstadoUsuario
CREATE TABLE EstadoUsuario (
    IdEstado INT AUTO_INCREMENT PRIMARY KEY,
    NombreEstado VARCHAR(100) NOT NULL
);

-- Tabla Usuario
CREATE TABLE Usuario (
    IdUsuario INT AUTO_INCREMENT PRIMARY KEY,
    NombreUsuario VARCHAR(100) NOT NULL,
    Contraseña VARCHAR(100) NOT NULL,
    Correo VARCHAR(100) NOT NULL,
    IdRol INT,
    IdEstado INT,
    FOREIGN KEY (IdRol) REFERENCES Rol(IdRol),
    FOREIGN KEY (IdEstado) REFERENCES EstadoUsuario(IdEstado)
);

-- Tabla Ciudad
CREATE TABLE Ciudad (
    IdCiudad INT AUTO_INCREMENT PRIMARY KEY,
    NombreCiudad VARCHAR(100) NOT NULL
);

-- Tabla Cliente
CREATE TABLE Cliente (
    IdCliente INT AUTO_INCREMENT PRIMARY KEY,
    Cedula VARCHAR(100) NOT NULL,
    Nombres VARCHAR(100) NOT NULL,
    Telefono VARCHAR(100),
    Direccion VARCHAR(100),
    Correo VARCHAR(100),
    IdCiudad INT,
    FOREIGN KEY (IdCiudad) REFERENCES Ciudad(IdCiudad)
);

-- Tabla EstadoTecnico
CREATE TABLE EstadoTecnico (
    IdEstado INT AUTO_INCREMENT PRIMARY KEY,
    NombreEstado VARCHAR(100) NOT NULL
);

-- Tabla Tecnico
CREATE TABLE Tecnico (
    IdTecnico INT AUTO_INCREMENT PRIMARY KEY,
    NombreTecnico VARCHAR(100) NOT NULL,
    Cedula VARCHAR(100) NOT NULL,
    TelefonoTecnico VARCHAR(100) NOT NULL,
    IdEstado INT,
    FOREIGN KEY (IdEstado) REFERENCES EstadoTecnico(IdEstado)
);

-- Tabla TipoServicio
CREATE TABLE TipoServicio (
    IdTipoServicio INT AUTO_INCREMENT PRIMARY KEY,
    NombreTipoServicio VARCHAR(100) NOT NULL
);

-- Tabla Servicio
CREATE TABLE Servicio (
    IdServicio INT AUTO_INCREMENT PRIMARY KEY,
    IdCliente INT,
    IdTecnico INT,
    IdTipoServicio INT,
    FechaTentativaAtencion DATETIME NOT NULL,
    Estado VARCHAR(100) NOT NULL,
    FOREIGN KEY (IdCliente) REFERENCES Cliente(IdCliente),
    FOREIGN KEY (IdTecnico) REFERENCES Tecnico(IdTecnico),
    FOREIGN KEY (IdTipoServicio) REFERENCES TipoServicio(IdTipoServicio)
);

-- Tabla Categoria
CREATE TABLE Categoria (
    IdCategoria INT AUTO_INCREMENT PRIMARY KEY,
    NombreCategoria VARCHAR(100) NOT NULL
);

-- Tabla EstadoProducto
CREATE TABLE EstadoProducto (
    IdEstadoProducto INT AUTO_INCREMENT PRIMARY KEY,
    NombreEstadoProducto VARCHAR(100) NOT NULL
);

-- Tabla Producto
CREATE TABLE Producto (
    IdProducto INT AUTO_INCREMENT PRIMARY KEY,
    IdCategoria INT,
    CodigoProducto VARCHAR(50) NOT NULL,
    Modelo VARCHAR(50) NOT NULL,
    IdEstadoProducto INT,
    SerieProducto VARCHAR(50) NOT NULL,
    Precio DECIMAL(18, 2) NOT NULL,
    FOREIGN KEY (IdCategoria) REFERENCES Categoria(IdCategoria),
    FOREIGN KEY (IdEstadoProducto) REFERENCES EstadoProducto(IdEstadoProducto)
);

-- Tabla Repuesto
CREATE TABLE Repuesto (
    IdRepuesto INT AUTO_INCREMENT PRIMARY KEY,
    CodigoRepuesto VARCHAR(100) NOT NULL,
    NombreRepuesto VARCHAR(100) NOT NULL,
    Cantidad INT NOT NULL,
    Precio DECIMAL(18, 2) NOT NULL
);

-- Tabla EstadoProforma
CREATE TABLE EstadoProforma (
    IdEstadoProforma INT AUTO_INCREMENT PRIMARY KEY,
    NombreEstadoProforma VARCHAR(50) NOT NULL
);

-- Tabla Proforma
CREATE TABLE Proforma (
    IdProforma INT AUTO_INCREMENT PRIMARY KEY,
    DescripcionProducto VARCHAR(100) NOT NULL,
    Subtotal DECIMAL(18, 2) NOT NULL,
    Iva DECIMAL(18, 2) NOT NULL,
    Total DECIMAL(18, 2) NOT NULL,
    IdCliente INT,
    IdProducto INT,
    IdEstadoProforma INT,
    FOREIGN KEY (IdCliente) REFERENCES Cliente(IdCliente),
    FOREIGN KEY (IdProducto) REFERENCES Producto(IdProducto),
    FOREIGN KEY (IdEstadoProforma) REFERENCES EstadoProforma(IdEstadoProforma)
);

-- Tabla DetalleProforma
CREATE TABLE DetalleProforma (
    IdDetalleProforma INT AUTO_INCREMENT PRIMARY KEY,
    IdProforma INT,
    Cantidad INT NOT NULL,
    DescripcionRepuesto VARCHAR(100) NOT NULL,
    PrecioUnitario DECIMAL(18, 2) NOT NULL,
    PrecioFinal DECIMAL(18, 2) NOT NULL,
    FOREIGN KEY (IdProforma) REFERENCES Proforma(IdProforma)
);

-- Tabla Pedido
CREATE TABLE Pedido (
    IdPedido INT AUTO_INCREMENT PRIMARY KEY,
    IdCliente INT,
    TipoPedido VARCHAR(100) NOT NULL,
    FechaPedido DATETIME NOT NULL,
    IdProducto INT,
    FOREIGN KEY (IdCliente) REFERENCES Cliente(IdCliente),
    FOREIGN KEY (IdProducto) REFERENCES Producto(IdProducto)
);

-- Tabla DetallePedido
CREATE TABLE DetallePedido (
    IdDetallePedido INT AUTO_INCREMENT PRIMARY KEY,
    IdPedido INT,
    Cantidad INT NOT NULL,
    DescripcionRepuesto VARCHAR(100) NOT NULL,
    FOREIGN KEY (IdPedido) REFERENCES Pedido(IdPedido)
);

CREATE TABLE Horario (
    IdHorario INT AUTO_INCREMENT PRIMARY KEY,
    IdTecnico INT NOT NULL,
    Fecha DATE NOT NULL,
    HoraInicio TIME NOT NULL,
    HoraFin TIME NOT NULL,
    FOREIGN KEY (IdTecnico) REFERENCES Tecnico(IdTecnico)
);

-- Tabla HorarioServicio
CREATE TABLE HorarioServicio (
    IdHorarioServicio INT AUTO_INCREMENT PRIMARY KEY,
    IdHorario INT NOT NULL,
    IdServicio INT NOT NULL,
    FOREIGN KEY (IdHorario) REFERENCES Horario(IdHorario),
    FOREIGN KEY (IdServicio) REFERENCES Servicio(IdServicio)
);

CREATE TABLE EstadoServicio (
    IdEstadoServicio INT AUTO_INCREMENT PRIMARY KEY,
    NombreEstadoServicio VARCHAR(100) NOT NULL
);

-- Modificación de la tabla Servicio para incluir la clave foránea a EstadoServicio
ALTER TABLE Servicio ADD COLUMN IdEstadoServicio INT NOT NULL;
ALTER TABLE Servicio ADD FOREIGN KEY (IdEstadoServicio) REFERENCES EstadoServicio(IdEstadoServicio);