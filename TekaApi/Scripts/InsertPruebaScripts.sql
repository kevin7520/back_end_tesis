-- Inserción de roles
INSERT INTO Rol (NombreRol) VALUES ('Admin');
INSERT INTO Rol (NombreRol) VALUES ('Estandar');

-- Inserción de estados de usuario
INSERT INTO EstadoUsuario (NombreEstado) VALUES ('Activo');
INSERT INTO EstadoUsuario (NombreEstado) VALUES ('Inactivo');

-- Inserción de ciudades
INSERT INTO Ciudad (NombreCiudad) VALUES ('Ciudad1');
INSERT INTO Ciudad (NombreCiudad) VALUES ('Ciudad2');

-- Inserción de clientes
INSERT INTO Cliente (Cedula, Nombres, Telefono, Direccion, Correo, IdCiudad)
VALUES ('1234567890', 'Cliente 1', '0987654321', 'Direccion 1', 'cliente1@example.com', 1);

INSERT INTO Cliente (Cedula, Nombres, Telefono, Direccion, Correo, IdCiudad)
VALUES ('0987654321', 'Cliente 2', '1234567890', 'Direccion 2', 'cliente2@example.com', 2);

-- Inserción de técnicos
INSERT INTO Tecnico (NombreTecnico, Cedula, TelefonoTecnico, EstadoTecnico)
VALUES ('Tecnico 1', '1234567890', '0987654321', 'Disponible');

INSERT INTO Tecnico (NombreTecnico, Cedula, TelefonoTecnico, EstadoTecnico)
VALUES ('Tecnico 2', '0987654321', '1234567890', 'Disponible');

-- Inserción de repuestos
INSERT INTO Repuesto (CodigoRepuesto, NombreRepuesto, Cantidad, Precio)
VALUES ('REP001', 'Repuesto 1', 10, 20.00);

INSERT INTO Repuesto (CodigoRepuesto, NombreRepuesto, Cantidad, Precio)
VALUES ('REP002', 'Repuesto 2', 5, 30.00);

-- Inserción de proformas
INSERT INTO Proforma (IdCliente, FechaCompra, NumeroFactura, NombreAlmacen)
VALUES (1, '2023-01-01', 'FAC001', 'Almacen 1');

INSERT INTO Proforma (IdCliente, FechaCompra, NumeroFactura, NombreAlmacen)
VALUES (2, '2023-01-02', 'FAC002', 'Almacen 2');

-- Inserción de detalles de proforma
INSERT INTO DetalleProforma (IdProforma, Cantidad, DescripcionRepuesto, PrecioUnitario, PrecioFinal)
VALUES (1, 2, 'Detalle Repuesto 1', 20.00, 40.00);

INSERT INTO DetalleProforma (IdProforma, Cantidad, DescripcionRepuesto, PrecioUnitario, PrecioFinal)
VALUES (2, 3, 'Detalle Repuesto 2', 30.00, 90.00);

-- Inserción de pedidos
INSERT INTO Pedido (IdCliente, TipoPedido, FechaPedido)
VALUES (1, 'Tipo 1', '2023-01-03');

INSERT INTO Pedido (IdCliente, TipoPedido, FechaPedido)
VALUES (2, 'Tipo 2', '2023-01-04');

-- Inserción de detalles de pedido
INSERT INTO DetallePedido (IdPedido, Cantidad, DescripcionRepuesto)
VALUES (1, 1, 'Detalle Pedido 1');

INSERT INTO DetallePedido (IdPedido, Cantidad, DescripcionRepuesto)
VALUES (2, 2, 'Detalle Pedido 2');
