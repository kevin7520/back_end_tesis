USE BD_TEKA;

-- Insertar roles
INSERT INTO Rol (NombreRol) VALUES ('Administrador');
INSERT INTO Rol (NombreRol) VALUES ('Usuario');

-- Insertar estados de usuario
INSERT INTO EstadoUsuario (NombreEstado) VALUES ('Activo');
INSERT INTO EstadoUsuario (NombreEstado) VALUES ('Inactivo');

-- Insertar estados de técnico
INSERT INTO EstadoTecnico (NombreEstado) VALUES ('Disponible');
INSERT INTO EstadoTecnico (NombreEstado) VALUES ('No Disponible');

-- Insertar estados de producto
INSERT INTO EstadoProducto (NombreEstadoProducto) VALUES ('Nuevo');
INSERT INTO EstadoProducto (NombreEstadoProducto) VALUES ('Usado');

-- Insertar estados de proforma
INSERT INTO EstadoProforma (NombreEstadoProforma) VALUES ('Pendiente');
INSERT INTO EstadoProforma (NombreEstadoProforma) VALUES ('Completada');

-- Insertar tipos de servicio
INSERT INTO TipoServicio (NombreTipoServicio) VALUES ('SERVICIOS DE INSTALACION');
INSERT INTO TipoServicio (NombreTipoServicio) VALUES ('SERVICIO DE REPARACION');
INSERT INTO TipoServicio (NombreTipoServicio) VALUES ('SERVICIO DE GARANTIA');

-- Insertar ciudades
INSERT INTO Ciudad (NombreCiudad) VALUES ('Quito');
INSERT INTO Ciudad (NombreCiudad) VALUES ('Guayaquil');
INSERT INTO Ciudad (NombreCiudad) VALUES ('Cuenca');

-- Insertar categorías
INSERT INTO Categoria (NombreCategoria) VALUES ('Electrodomésticos');
INSERT INTO Categoria (NombreCategoria) VALUES ('Muebles');
INSERT INTO Categoria (NombreCategoria) VALUES ('Tecnología');
