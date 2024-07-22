-- Crear la base de datos y usarla
DROP DATABASE IF EXISTS BD_TEKA;
CREATE DATABASE BD_TEKA;
USE BD_TEKA;

CREATE TABLE Rol (
    IdRol INT PRIMARY KEY AUTO_INCREMENT,
    NombreRol VARCHAR(255) NOT NULL
);

CREATE TABLE EstadoUsuario (
    IdEstado INT PRIMARY KEY AUTO_INCREMENT,
    NombreEstado VARCHAR(255) NOT NULL
);

CREATE TABLE Usuario (
    IdUsuario INT PRIMARY KEY AUTO_INCREMENT,
    NombreUsuario VARCHAR(255) NOT NULL,
    Contraseña VARCHAR(255) NOT NULL,
    Correo VARCHAR(255) NOT NULL,
    IdRol INT NOT NULL,
    IdEstado INT NOT NULL,
    FOREIGN KEY (IdRol) REFERENCES Rol(IdRol),
    FOREIGN KEY (IdEstado) REFERENCES EstadoUsuario(IdEstado)
);

CREATE TABLE Ciudad (
    IdCiudad INT PRIMARY KEY AUTO_INCREMENT,
    NombreCiudad VARCHAR(255) NOT NULL
);

CREATE TABLE Cliente (
    IdCliente INT PRIMARY KEY AUTO_INCREMENT,
    Cedula VARCHAR(255) NOT NULL,
    Nombres VARCHAR(255) NOT NULL,
    Telefono VARCHAR(255),
    Direccion VARCHAR(255),
    Correo VARCHAR(255),
    IdCiudad INT NOT NULL,
    FOREIGN KEY (IdCiudad) REFERENCES Ciudad(IdCiudad)
);

CREATE TABLE EstadoTecnico (
    IdEstado INT PRIMARY KEY AUTO_INCREMENT,
    NombreEstado VARCHAR(255) NOT NULL
);

CREATE TABLE Tecnico (
    IdTecnico INT PRIMARY KEY AUTO_INCREMENT,
    NombreTecnico VARCHAR(255) NOT NULL,
    Cedula VARCHAR(255) NOT NULL,
    TelefonoTecnico VARCHAR(255) NOT NULL,
    IdEstado INT NOT NULL,
    FOREIGN KEY (IdEstado) REFERENCES EstadoTecnico(IdEstado)
);

CREATE TABLE TipoServicio (
    IdTipoServicio INT PRIMARY KEY AUTO_INCREMENT,
    NombreTipoServicio VARCHAR(100) NOT NULL,
    Opciones VARCHAR(100),
    Valor1 VARCHAR(100),
    Valor2 VARCHAR(100),
    Valor3 VARCHAR(100),
    Valor4 VARCHAR(100)
);

CREATE TABLE EstadoServicio (
    IdEstadoServicio INT PRIMARY KEY AUTO_INCREMENT,
    NombreEstadoServicio VARCHAR(100) NOT NULL
);

CREATE TABLE Categoria (
    IdCategoria INT PRIMARY KEY AUTO_INCREMENT,
    NombreCategoria VARCHAR(100) NOT NULL
);

CREATE TABLE EstadoProducto (
    IdEstadoProducto INT PRIMARY KEY AUTO_INCREMENT,
    NombreEstadoProducto VARCHAR(100) NOT NULL
);

CREATE TABLE Producto (
    IdProducto INT PRIMARY KEY AUTO_INCREMENT,
    IdCategoria INT NOT NULL,
    CodigoProducto VARCHAR(50) NOT NULL,
    Modelo VARCHAR(50) NOT NULL,
    IdEstadoProducto INT NOT NULL,
    SerieProducto VARCHAR(50) NOT NULL,
    Precio DECIMAL(18, 2) NOT NULL,
    FOREIGN KEY (IdCategoria) REFERENCES Categoria(IdCategoria),
    FOREIGN KEY (IdEstadoProducto) REFERENCES EstadoProducto(IdEstadoProducto)
);


CREATE TABLE Factura (
    IdFactura INT PRIMARY KEY AUTO_INCREMENT,
    FechaCompra DATE,
    NumeroFactura NVARCHAR(50)
);

CREATE TABLE Almacen (
    IdAlmacen INT PRIMARY KEY AUTO_INCREMENT,
    NombreAlmacen NVARCHAR(100)
);

CREATE TABLE Servicio (
    IdServicio INT PRIMARY KEY AUTO_INCREMENT,
    IdCliente INT NULL,
    IdTecnico INT NULL,
    IdTipoServicio INT NULL,
    IdEstadoServicio INT NULL,
	IdFactura INT NULL,
    IdAlmacen INT NULL,
    FechaTentativaAtencion DATETIME,
    FechaSolicitudServicio DATETIME,
    FOREIGN KEY (IdCliente) REFERENCES Cliente(IdCliente),
    FOREIGN KEY (IdTecnico) REFERENCES Tecnico(IdTecnico),
    FOREIGN KEY (IdTipoServicio) REFERENCES TipoServicio(IdTipoServicio),
    FOREIGN KEY (IdEstadoServicio) REFERENCES EstadoServicio(IdEstadoServicio),
    FOREIGN KEY (IdFactura) REFERENCES Factura(IdFactura),
    FOREIGN KEY (IdAlmacen) REFERENCES Almacen(IdAlmacen)
);

CREATE TABLE ServicioProducto (
    IdServicioProducto INT PRIMARY KEY AUTO_INCREMENT,
    IdServicio INT NOT NULL,
    IdProducto INT NOT NULL,
    Valor FLOAT,
    Serie VARCHAR(255),
    FOREIGN KEY (IdServicio) REFERENCES Servicio(IdServicio),
    FOREIGN KEY (IdProducto) REFERENCES Producto(IdProducto)
);

CREATE TABLE Repuesto (
    IdRepuesto INT PRIMARY KEY AUTO_INCREMENT,
    CodigoRepuesto VARCHAR(255) NOT NULL,
    NombreRepuesto VARCHAR(255) NOT NULL,
    Cantidad INT NOT NULL,
    Precio DECIMAL(18, 2) NOT NULL
);

CREATE TABLE EstadoProforma (
    IdEstadoProforma INT PRIMARY KEY AUTO_INCREMENT,
    NombreEstadoProforma VARCHAR(50) NOT NULL
);

CREATE TABLE Proforma (
    IdProforma INT PRIMARY KEY AUTO_INCREMENT,
    DescripcionProducto VARCHAR(255) NOT NULL,
    Subtotal DECIMAL(18, 2) NOT NULL,
    Iva DECIMAL(18, 2) NOT NULL,
    Total DECIMAL(18, 2) NOT NULL,
    IdCliente INT,
--    IdProducto INT,
    IdEstadoProforma INT,
    FOREIGN KEY (IdCliente) REFERENCES Cliente(IdCliente),
    FOREIGN KEY (IdEstadoProforma) REFERENCES EstadoProforma(IdEstadoProforma)
  --  FOREIGN KEY (IdProducto) REFERENCES Producto(IdProducto)
);

CREATE TABLE DetalleProforma (
    IdDetalleProforma INT PRIMARY KEY AUTO_INCREMENT,
    IdProforma INT NOT NULL,
    IdRepuesto INT NOT NULL,
    Cantidad INT NOT NULL,
    DescripcionRepuesto VARCHAR(255) NOT NULL,
    PrecioUnitario DECIMAL(18, 2) NOT NULL,
    PrecioFinal DECIMAL(18, 2) NOT NULL,
    FOREIGN KEY (IdProforma) REFERENCES Proforma(IdProforma),
	FOREIGN KEY (IdRepuesto) REFERENCES Repuesto(IdRepuesto)
);

CREATE TABLE EstadoPedido (
    IdEstadoPedido INT PRIMARY KEY AUTO_INCREMENT,
    NombreEstadoPedido VARCHAR(50) NOT NULL
);

CREATE TABLE Pedido (
    IdPedido INT PRIMARY KEY AUTO_INCREMENT,
    DescripcionProducto VARCHAR(255) NOT NULL,
    Subtotal DECIMAL(18, 2) NOT NULL,
    Iva DECIMAL(18, 2) NOT NULL,
    Total DECIMAL(18, 2) NOT NULL,
    IdCliente INT,
    IdEstadoPedido INT,
    FOREIGN KEY (IdCliente) REFERENCES Cliente(IdCliente),
    FOREIGN KEY (IdEstadoPedido) REFERENCES EstadoPedido(IdEstadoPedido)
  --  FOREIGN KEY (IdProducto) REFERENCES Producto(IdProducto)
);

CREATE TABLE DetallePedido (
    IdDetallePedido INT PRIMARY KEY AUTO_INCREMENT,
    IdPedido INT NOT NULL,
    IdRepuesto INT NOT NULL,
    Cantidad INT NOT NULL,
    DescripcionRepuesto VARCHAR(255) NOT NULL,
    PrecioUnitario DECIMAL(18, 2) NOT NULL,
    PrecioFinal DECIMAL(18, 2) NOT NULL,
    FOREIGN KEY (IdPedido) REFERENCES Pedido(IdPedido),
	FOREIGN KEY (IdRepuesto) REFERENCES Repuesto(IdRepuesto)
);

CREATE TABLE Horario (
    IdHorario INT PRIMARY KEY AUTO_INCREMENT,
    IdTecnico INT NOT NULL,
    Fecha DATETIME NOT NULL,
    HoraInicio TIME NOT NULL,
    HoraFin TIME NOT NULL,
    FOREIGN KEY (IdTecnico) REFERENCES Tecnico(IdTecnico)
);

CREATE TABLE HorarioServicio (
    IdHorarioServicio INT PRIMARY KEY AUTO_INCREMENT,
    IdHorario INT NOT NULL,
    IdServicio INT NOT NULL,
    FOREIGN KEY (IdHorario) REFERENCES Horario(IdHorario),
    FOREIGN KEY (IdServicio) REFERENCES Servicio(IdServicio)
);

CREATE TABLE ServicioRepuesto (
    IdServicioRepuesto INT PRIMARY KEY AUTO_INCREMENT,
    IdServicio INT NOT NULL,
    IdRepuesto INT NOT NULL,
    CONSTRAINT FK_ServicioRepuesto_Servicio FOREIGN KEY (IdServicio) REFERENCES Servicio(IdServicio),
    CONSTRAINT FK_ServicioRepuesto_Repuesto FOREIGN KEY (IdRepuesto) REFERENCES Repuesto(IdRepuesto)
);

/*CREATE TABLE ProformaRepuesto (
	IdProformaRepuesto INT PRIMARY KEY AUTO_INCREMENT,
    IdProforma INT NOT NULL,
    IdRepuesto INT NOT NULL,
    CONSTRAINT FK_ProformaRepuesto_Proforma FOREIGN KEY (IdProforma) REFERENCES Proforma(IdProforma),
    CONSTRAINT FK_ProformaRepuesto_Repuesto FOREIGN KEY (IdRepuesto) REFERENCES Repuesto(IdRepuesto)
);
*/

ALTER TABLE Servicio
ADD COLUMN Valor DECIMAL(18, 2) NULL;