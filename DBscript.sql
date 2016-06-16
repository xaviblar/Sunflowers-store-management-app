--use master
--drop database Sunflowers

create database Sunflowers
use Sunflowers
go

/***********************************************************************************************
Valores por defecto
***********************************************************************************************/

CREATE DEFAULT DSexo AS 'F'
GO
CREATE DEFAULT DDia AS 'L'
GO
CREATE DEFAULT DTipoVenta AS 'CONTADO'
GO

/***********************************************************************************************
Tipos de datos definidos por el usuario
***********************************************************************************************/

CREATE RULE RCedula		AS	@cedula		like	'[0-9]-[0-9][0-9][0-9][0-9]-[0-9][0-9][0-9][0-9]' 
GO
CREATE RULE RTelefono	AS	@telefono	like	'[0-9][0-9][0-9][0-9]-[0-9][0-9][0-9][0-9]'
GO
CREATE RULE REmail		AS	@email		like	'[a-Z]%@[a-Z]%.[a-Z]%'
GO
CREATE RULE RSexo		AS	@sexo		in		('F','M')
GO
CREATE RULE RDia		AS	@dia		in		('L','K','M','J','V','S','D')
GO
CREATE RULE RTIPOVENTA	AS	@tipoVenta		in		('CONTADO', 'AMBAS')
GO
 
/***********************************************************************************************
Tipos de datos definidos por el usuario
***********************************************************************************************/

EXEC sp_addtype		TCedula,	'char(11)', 'not null'
GO
EXEC sp_addtype		TEmail,		'char(30)', 'null'
GO
EXEC sp_addtype		TTelefono,	'char(9)', 'not null'
GO
EXEC sp_addtype		TSexo,		'char(1)', 'not null' 
GO
EXEC sp_addtype		TDia,		'char(1)', 'not null' 
GO
EXEC sp_addtype		TTipoVenta,		'varchar(7)', 'not null' 
GO

--Vincula Reglas con tipos de datos
EXEC sp_bindrule 'RCedula', 'TCedula'
EXEC sp_bindrule 'RTelefono', 'TTelefono'
EXEC sp_bindrule 'RSexo', 'TSexo'
EXEC sp_bindrule 'REmail', 'TEmail'
EXEC sp_bindrule 'RDia', 'TDia'
EXEC sp_bindrule 'RTipoVenta', 'TTipoVenta'

--Vincula valores por defecto con tipos de datos
EXEC sp_bindefault 'DSexo', 'TSexo'
EXEC sp_bindefault 'DDia', 'TDia'
EXEC sp_bindefault 'DTipoVenta', 'TTipoVenta'

/***********************************************************************************************
CREAR TABLAS. Crea todas las entidades definidas en el modelo relacional
***********************************************************************************************/

--drop table provincias 
CREATE TABLE provincias 
(
 	idProvincia		TINYINT,
	nombre			VARCHAR(30),
	CONSTRAINT		PK_idProvincia_provincia		PRIMARY KEY	(idProvincia),
	CONSTRAINT		CHK_idProvincia_provincia		CHECK (idProvincia between 1 and 9) 
)

--drop table cantones 
create table cantones 
(
	idCanton	tinyint		identity (1,1),
	nombre		varchar(30)	not null,
	idProvincia tinyint		not null,
	CONSTRAINT	PK_idCanton_canton		primary key (IdCanton),
	CONSTRAINT	FK_IdProvincia_canton	foreign key (IdProvincia) references Provincias 
	on delete cascade on update cascade
)

--drop table distritos 
create table distritos 
(
	idDistrito	smallint		identity(1,1),
	nombre		varchar(50)		not null,
	idCanton	tinyint			not null,
	CONSTRAINT	PK_idDistrito_distrito	primary key	(idDistrito),
	CONSTRAINT	FK_idCanton_distrito	foreign key	(idCanton)	references cantones
	on delete cascade on update cascade
)

--drop table personas
create table personas
(
  cedula			Tcedula,
  nombre			varchar (30)		not null,
  apellido1			varchar (30)		not null,
  apellido2			varchar (30)		not null,
  sexo				TSexo,
  idDistrito		smallint			not null,
  direccionExacta	varchar (200)		not null,
  fechaNacimiento DATE,
  CONSTRAINT		PK_cedula_persona		primary Key (cedula),
  CONSTRAINT		FK_idDistrito_persona	foreign	key (idDistrito)	references distritos
	on delete cascade on update cascade
)

--drop table telefonosPersonas 
create table telefonosPersonas 
(
	cedula		char(11), 
	telefono	TTelefono,
	CONSTRAINT	PK_telefono_telefonosPersonas	primary key (cedula,telefono), 
	CONSTRAINT	FK_cedula_telefonosPersonas		foreign key (cedula)			references personas 
	on delete cascade on update cascade
)

--drop table emailPersona
create table emailPersona
(
	cedula		char(11), 
	email		TEmail,
	CONSTRAINT	PK_cedula_email_emailPersona	primary key	(cedula,email),
	CONSTRAINT	FK_cedula_emailPersona			foreign key (cedula)		references personas
	on delete cascade on update cascade
)

--drop table Sucursales
create table Sucursales
(
	Nombre		varchar(50),	
	idDistrito		smallint			not null,
	direccionExacta	varchar (200)		not null,
	CONSTRAINT	PK_Nombre_Sucursales		primary key	(Nombre),
	CONSTRAINT	FK_idDistrito_Sucursales	foreign	key (idDistrito)	references distritos
	on delete cascade on update cascade
)

--drop table telefonosSucursales 
create table telefonosSucursales 
(
	NombreSucursales		varchar(50),
	telefono	TTelefono,
	CONSTRAINT	PK_telefono_telefonosPSucursales	primary key (NombreSucursales,telefono), 
	CONSTRAINT	FK_cedula_telefonosPSucursales		foreign key (NombreSucursales)		references Sucursales
	on delete cascade on update cascade
)

--drop table emailSucursales
create table emailSucursales 
(
	NombreSucursales		varchar(50),
	email		TEmail,
	CONSTRAINT	PK_cedula_email_emailSucursales	primary key	(NombreSucursales,email),
	CONSTRAINT	FK_cedula_emailSucursales			foreign key (NombreSucursales)	references Sucursales
	on delete cascade on update cascade
)

--drop table empleados
create table Empleados
(
	cedula		char(11),
	fechaIngreso DATE,
	Salario		int,		
	jornada		tinyint,	
	NombreSucursales varchar(50),
	CONSTRAINT	PK_cedula_empleados		primary key (cedula),
	CONSTRAINT	FK_cedula_empleados		foreign key (cedula) references personas
	on delete cascade on update cascade,
	CONSTRAINT	FK_nombreSucursales_empleados		foreign key (NombreSucursales) references Sucursales,
	CONSTRAINT	CHK_jornada_empleados	CHECK		(jornada > 0)
)

--drop table Horarios
create table Horarios
(
	idHorario		int		identity (0,1),
	cedula		char(11),
	Dia				TDia,
	HEntrada		time	not null,
	HSalida			time	not null,

	CONSTRAINT	PK_idHorario_horarios		primary key (idHorario),	
	CONSTRAINT	FK_cedula_horarios		foreign key (cedula) references Empleados,
	CONSTRAINT	CHK_HEntrada_HSalida_horarios	CHECK	(HEntrada<HSalida),
	CONSTRAINT	UNQ_HEntrada_HSalida_horarios	UNIQUE	(cedula, Dia, HEntrada, HSalida)
)



--drop table Funciones
create table Funciones
(	
	idFuncion		int		identity (0,1), 
	cedulaEmpleado		TCedula,
	nombre		varchar(50) not null,
	descripcion		varchar(200) not null,
	CONSTRAINT	PK_idFuncion_CedulaEmpleado_Funciones			primary key (idFuncion, cedulaEmpleado),	
	CONSTRAINT	FK_CedulaEmpleado_Funciones			foreign key (cedulaEmpleado)		references Empleados,
	CONSTRAINT	UNQ_CedulaEmpleado_NombreFuncion_Funciones			unique (cedulaEmpleado, Nombre)
)

--drop table Proveedores
create table Proveedores
(
	idProveedor		int		identity (0,1), 
	Nombre			varChar(50),
	Direccion		varchar(200),
	CONSTRAINT	PK_idProveedor_Proveedores			primary key (iDProveedor)
)

--drop table telefonosProveedores
create table telefonosProveedores 
(
	idProveedor		int not null,
	telefono	TTelefono,
	CONSTRAINT	PK_telefono_telefonosProveedores 	primary key (idProveedor,telefono), 
	CONSTRAINT	FK_idProveedor_telefonosPProveedores 		foreign key (idProveedor)		references Proveedores
	on delete cascade on update cascade
)

--drop table emailProveedores
create table emailProveedores
(
	idProveedor		int not null,
	email	TEmail,
	CONSTRAINT	PK_eamil_emailProveedores 	primary key (idProveedor,email), 
	CONSTRAINT	FK_idProveedor_emailProveedores 		foreign key (idProveedor)		references Proveedores
	on delete cascade on update cascade
)

--drop table Articulos
create table Articulos
(
	Codigo		int		identity (0,1), 
	Stock		int not null,
	idProveedor	int not null,
	Precio		int not null,
	TipoVenta	TTipoVenta,
	Color		Varchar(30) not null,
	Descripcion	Varchar(200) not null,
	Categoria	Varchar(30) not null,
	CONSTRAINT	PK_Codigo_Articulos			primary key (Codigo),	
	CONSTRAINT	FK_idProveedor_Apartados			foreign key (idProveedor)		references Proveedores,
	CONSTRAINT	CHK_precio_Articulo	CHECK	(precio>0),
	CONSTRAINT	CHK_Stock_Articulo	CHECK	(Stock>=0)
)

--drop table Ventas
create table Ventas
(
	Codigo		int		identity (0,1), 
	cedulaEmpleado		TCedula,
	fecha		date not null,
	monto		int not null,
	CONSTRAINT	PK_Codigo_Ventas			primary key (Codigo),	
	CONSTRAINT	FK_CedulaEmpleado_Ventas			foreign key (cedulaEmpleado)		references Empleados,
	CONSTRAINT	FK_CedulaCliente_Ventas			foreign key (cedulaEmpleado)		references personas
)

--drop table VentasArticulos
create table VentasArticulos
(
	CodigoVenta		int,
	CodigoArticulo int,
	cantidad       int	not null,
	CONSTRAINT	PK_CodigoVenta_CodigoArticulo_VentasArticulos			primary key (CodigoVenta, CodigoArticulo),	
	CONSTRAINT	FK_CodigoVenta_VentasArticulos			foreign key (CodigoVenta)		references Ventas,
	CONSTRAINT	FK_CodigoArticulo_VentasArticulos			foreign key (CodigoArticulo)		references Articulos,
)

--drop table apartados
create table Apartados
(
	Codigo		int		identity (0,1),
	cedulaEmpleado		TCedula,
	cedulaCliente		TCedula,
	fechaCreacion		date not null,
	fechaLimite		date not null,
	monto				int	not null,
	CONSTRAINT	PK_Codigo_Apartados			primary key (Codigo),	
	CONSTRAINT	FK_CedulaEmpleado_Apartados			foreign key (cedulaEmpleado)		references Empleados,
	CONSTRAINT	FK_CedulaCliente_Apartados			foreign key (cedulaCliente)		references personas,
	CONSTRAINT	CHK_fechaCreacion_FechaLimite_Apartados	CHECK	(fechaCreacion<fechaLimite)
)

--drop table ApartadosArticulos
create table ApartadosArticulos
(
	CodigoArticulo		int	not null,
	CodigoApartado		int	not null,
	cantidad			int not null,
	CONSTRAINT	PK_CodigoArticulo_CodigoApartado_ApartadosArticulos			primary key (CodigoArticulo, CodigoApartado),	
	CONSTRAINT	FK_CodigoApartado_ApartadosArticulos			foreign key (CodigoApartado)		references Apartados,
	CONSTRAINT	FK_CodigoArticulo_ApartadosArticulos			foreign key (CodigoArticulo)		references Articulos,
	CONSTRAINT	CHK_cantidad_ApartadosArticulos	CHECK	(cantidad>0),
)

--drop table Abonos
create table Abonos	
(
	Codigo				int		identity (0,1), 
	CodigoApartado		int	not null,
	monto				int	not null,
	fecha				date not null,
	CONSTRAINT	PK_Codigo_Abonos			primary key (Codigo),	
	CONSTRAINT	FK_CodigoApartado_Abonos			foreign key (CodigoApartado)		references Apartados,
	CONSTRAINT	CHK_monto_Abonos	CHECK	(monto>0)
)

--drop table Pedidos
create table Pedidos
(
	Codigo			int		identity (0,1), 
	idProveedor		int	not null,
	nombreSucursal	varchar (50) not null,
	fecha			date not null,
	monto				int	not null,
	CONSTRAINT	PK_Codigo_Pedidos			primary key (Codigo),	
	CONSTRAINT	FK_idProveedor_Pedidos		foreign key (idProveedor)		references Proveedores,
	CONSTRAINT	FK_nombreSucursal_Pedidos	foreign key (nombreSucursal)	references Sucursales
)

--drop table PedidosArticulos
create table PedidosArticulos
(
	CodigoArticulo		int	not null,
	CodigoPedido		int	not null,
	cantidad			int not null,
	CONSTRAINT	PK_CodigoArticulo_CodigoApartado_PedidosArticulos			primary key (CodigoArticulo, CodigoPedido),	
	CONSTRAINT	FK_CodigoPedido_PedidosArticulos		foreign key (CodigoPedido)		references Pedidos,
	CONSTRAINT	FK_CodigoArticulo_PedidosArticulos			foreign key (CodigoArticulo)		references Articulos,
	CONSTRAINT	CHK_cantidad_PedidosArticulos	CHECK	(cantidad>0),
)



/***************************************************************************
					TRANSACCIONES	ANTONY DURAN
****************************************************************************/
/***************************************************************************
							4.DEVOLUCIONES
****************************************************************************/
--Aumentar Stock

create procedure aumentarStock(@codArticulo int)
as
	declare @error tinyint
	set @error = 0

	begin transaction
			update Articulos set Stock = Stock+1	where @codArticulo = Codigo

			if @@ERROR > 0
				set @error =1
			if @error=1
				begin
					rollback tran
					print('No se pudo devolver el artículo apartado')
				end
			else
				commit tran


--Disminui Stock

create procedure disminuirStock(@codArticulo int)
as
	declare @error tinyint, @TotalStock int
	set @TotalStock = 0
	set @error = 0

	begin transaction

		set @TotalStock = (Select Stock from Articulos where Codigo = @codArticulo)			
			if @TotalStock=0
				begin
					rollback tran
					print('No hay ningún artículo en el Sotck!')
				end
			else
			update Articulos set  Stock= Stock-1	where @codArticulo = Codigo
			if @@ERROR > 0
				set @error =1
			if @error=1
				begin
					rollback tran
					print('No se pudo devolver el artículo apartado')
				end
			else
				commit tran


--Devolución de un artículo apartado
create procedure devolverArticuloApartado(@codArticulo int, @codApartado int)
as
	declare @error tinyint

	set @error = 0

	begin transaction 

		delete ApartadosArticulos where @codApartado=CodigoApartado and @codArticulo=@codArticulo
		
		if @@ERROR > 0
			set @error =1

		if @error=1
			begin
				rollback tran
				print('No se pudo devolver el artículo apartado')
			end
		else
			EXEC aumentarStock @codArticulo
			commit tran

--Devolver articulo comprado

create procedure devolverArticuloVendido(@codArticulo int, @codVenta int)
as
	declare @error tinyint

	set @error = 0

	begin transaction 

		delete VentasArticulos where @codVenta= CodigoVenta and @codArticulo=@codArticulo
		
		if @@ERROR > 0
			set @error =1

		if @error=1
			begin
				rollback tran
				print('No se pudo devolver el artículo apartado')
			end
		else
			EXEC aumentarStock @codArticulo
			commit tran


/***************************************************************************
					8.LISTA DE ARTÍCULOS EXCLUIDOS 
****************************************************************************/
create procedure articulosExluidos
as
	declare @error tinyint

	set @error = 0

	begin transaction 
	select a.Codigo, a.Descripcion  from Articulos a where a.Codigo not in (select va.CodigoArticulo from VentasArticulos va)

	if @@ERROR > 0
			set @error =1

		if @error=1
			begin
				rollback tran
				print('Error al buscar artículos excluidos')
			end
		else
			commit tran

/***************************************************************************
					12.REGISTROS DE CUENTAS POR COBRAR
****************************************************************************/
--
create procedure cuentasPorCobrar
as
	declare @error tinyint

	set @error = 0

	begin transaction 
	select (c.nombre +' '+ c.apellido1+' '+ c.apellido2) as 'Nombre completo',
			a.Codigo as 'Código artículo', a.Descripcion, a.Categoria, a.Precio as 'Precio unitario', 
			aa.cantidad
			from apartados ap inner join personas c on ap.cedulaCliente=c.cedula
			inner join ApartadosArticulos aa on ap.Codigo=aa.CodigoApartado
			inner join Articulos a on aa.CodigoArticulo=a.Codigo
			where monto > 0



		if @@ERROR > 0
			set @error =1

		if @error=1
			begin
				rollback tran
				print('Error al mostrar las cuentas por cobrar')
			end
		else
			commit tran
														



/***************************************************************************
									SUCURSALES
****************************************************************************/

create PROCEDURE insSucursal(@Nombre varchar(50),	@idDistrito	smallint, @direccionExacta	varchar (200))
as
	
	declare @error tinyint
	set @error = 0

	begin transaction
			insert into Sucursales values (@Nombre,	@idDistrito, @direccionExacta)
			if @@ERROR > 0
				set @error =1
			if @error=1
				begin
					rollback tran
					print('No se pudo insertar la sucursal')
				end
			else
				commit tran



--drop PROCEDURE borrarSucursal
create PROCEDURE borrarSucursal(@Nombre varchar(50))
as
	
	declare @error tinyint
	set @error = 0

	begin transaction
			delete Sucursales where nombre= @Nombre
			if @@ERROR > 0
				set @error =1
			delete telefonosSucursales where NombreSucursales= @Nombre
			if @@ERROR > 0
				set @error =1
			delete emailSucursales where NombreSucursales= @Nombre
			if @@ERROR > 0
				set @error =1
			if @error=1
				begin
					rollback tran
					print('No se pudo modificar el nombre de la sucursal')
				end
			else
				commit tran

--drop PROCEDURE modNombreSucursal
create PROCEDURE modNombreSucursal(@Nombre varchar(50), @NombreNuevo varchar(50))
as
	
	declare @error tinyint
	set @error = 0

	begin transaction
			update Sucursales set Nombre = @NombreNuevo where nombre= @Nombre
			if @@ERROR > 0
				set @error =1
			if @error=1
				begin
					rollback tran
					print('No se pudo modificar el nombre de la sucursal')
				end
			else
				commit tran


--drop PROCEDURE modDistritoSucursal
create PROCEDURE modDistritoSucursal(@Nombre varchar(50), @idDistritoNuevo	smallint)
as
	
	declare @error tinyint
	set @error = 0

	begin transaction
			update Sucursales set idDistrito = @idDistritoNuevo where Nombre= @Nombre
			if @@ERROR > 0
				set @error =1
			if @error=1
				begin
					rollback tran
					print('No se pudo modificar el distrito de la sucursal')
				end
			else
				commit tran



--drop PROCEDURE modDireccionExactaSucursal
create PROCEDURE modDireccionExactaSucursal(@Nombre varchar(50), @direccionExactaNueva	varchar (200))
as
	
	declare @error tinyint
	set @error = 0

	begin transaction
			update Sucursales set direccionExacta = @direccionExactaNueva where Nombre= @Nombre
			if @@ERROR > 0
				set @error =1
			if @error=1
				begin
					rollback tran
					print('No se pudo modificar la direccion exacta de la sucursal')
				end
			else
				commit tran



--drop PROCEDURE instelefonosSucursales 
create PROCEDURE insTelefonosSucursales(@NombreSucursal varchar(50), @telefono	TTelefono)
as 
	declare @error tinyint
	set @error = 0

	begin transaction
			insert into telefonosSucursales values (@NombreSucursal, @telefono)
			if @@ERROR > 0
				set @error =1
			if @error=1
				begin
					rollback tran
					print('No se pudo agregar el telefono de la sucursal')
				end
			else
				commit tran



--drop PROCEDURE borrarTelefonoSucursal
create PROCEDURE borrarTelefonoSucursal(@Nombre varchar(50), @Telefono TTelefono)
as
	
	declare @error tinyint
	set @error = 0

	begin transaction
			delete telefonosSucursales where NombreSucursales = @Nombre and @Telefono = telefono
			if @@ERROR > 0 
				set @error =1
			if @error=1
				begin
					rollback tran
					print('No se pudo borrar el telefono de la sucursal')
				end
			else
				commit tran



--drop PROCEDURE modTelefonoSucursales 
create PROCEDURE modTelefonoSucursales(@NombreSucursal varchar(50), @telefono	TTelefono, @telefonoNuevo	TTelefono)
as 
	declare @error tinyint
	set @error = 0

	begin transaction
			update telefonosSucursales set telefono = @telefonoNuevo where NombreSucursales = @NombreSucursal and telefono=@telefono
			if @@ERROR > 0
				set @error =1
			if @error=1
				begin
					rollback tran
					print('No se pudo modificar el telefono de la sucursal')
				end
			else
				commit tran




--drop PROCEDURE insEmailSucursales 
create PROCEDURE insEmailSucursales(@NombreSucursal varchar(50), @Email	TEmail)
as 
	declare @error tinyint
	set @error = 0

	begin transaction
			insert into emailSucursales values (@NombreSucursal, @Email)
			if @@ERROR > 0
				set @error =1
			if @error=1
				begin
					rollback tran
					print('No se pudo agregar el email de la sucursal')
				end
			else
				commit tran

--drop PROC borrarEmailSucursal
create PROCEDURE borrarEmailSucursal(@Nombre varchar(50), @Email	TEmail)
as
	
	declare @error tinyint
	set @error = 0

	begin transaction
			delete emailSucursales where NombreSucursales = @Nombre and @Email=email
			if @@ERROR > 0 
				set @error =1
			if @error=1
				begin
					rollback tran
					print('No se pudo borrar el email de la sucursal')
				end
			else
				commit tran


--drop PROCEDURE modEmailSucursales 
create PROCEDURE modEmailSucursales(@NombreSucursal varchar(50), @Email	TEmail, @EmailNuevo	TEmail)
as 
	declare @error tinyint
	set @error = 0

	begin transaction
			update EmailSucursales set Email = @EmailNuevo where NombreSucursales = @NombreSucursal and Email=@Email
			if @@ERROR > 0
				set @error =1
			if @error=1
				begin
					rollback tran
					print('No se pudo modificar el email de la sucursal')
				end
			else
				commit tran


/***************************************************************************
							FUNCIONES
****************************************************************************/
---drop PROCEDURE insFuncion
create PROCEDURE insFuncion(@cedula TCedula, @nombreFuncion varchar(50), @descripcion varchar(200))
as 
	declare @error tinyint
	set @error = 0

	begin transaction
			insert into Funciones values (@cedula,@nombreFuncion, @descripcion)
			if @@ERROR > 0
				set @error =1
			if @error=1
				begin
					rollback tran
					print('No se pudo insertar la función')
				end
			else
				commit tran



create PROCEDURE borrarFuncion(@idFuncion int)
as 
	declare @error tinyint
	set @error = 0

	begin transaction
			delete Funciones where idFuncion = @idFuncion
			if @@ERROR > 0
				set @error =1
			if @error=1
				begin
					rollback tran
					print('No se pudo borrar la función')
				end
			else
				commit tran



--drop PROCEDURE modNombreFuncion
create PROCEDURE modNombreFuncion(@idFuncion int, @nombreFuncion varchar(50))
as 
	declare @error tinyint
	set @error = 0

	begin transaction
			update Funciones set nombre=@nombreFuncion where idFuncion=@idFuncion
			if @@ERROR > 0
				set @error =1
			if @error=1
				begin
					rollback tran
					print('No se pudo modificar el nombre de la función')
				end
			else
				commit tran


--drop PROCEDURE modCedulaFuncion
create PROCEDURE modCedulaFuncion(@idFuncion int, @cedula TCedula)
as 
	declare @error tinyint
	set @error = 0

	begin transaction
			update Funciones set cedulaEmpleado=@cedula where idFuncion=@idFuncion
			if @@ERROR > 0
				set @error =1
			if @error=1
				begin
					rollback tran
					print('No se pudo insertar el roll')
				end
			else
				commit tran




create PROCEDURE modDescripcionFuncion(@idFuncion int, @DescripcionFuncion varchar(50))
as 
	declare @error tinyint
	set @error = 0

	begin transaction
			update Funciones set descripcion=@DescripcionFuncion where idFuncion=@idFuncion
			if @@ERROR > 0
				set @error =1
			if @error=1
				begin
					rollback tran
					print('No se pudo modificar el nombre de la función')
				end
			else
				commit tran


create PROCEDURE insHorario (@cedula TCedula, @dia char(1), @hE Time, @hS Time)
as
	declare @error tinyint
	set @error = 0

	begin transaction
			insert into horarios  values (@cedula, @dia, @hE, @hS)
			if @@ERROR > 0
				set @error =1
			if @error=1
				begin
					rollback tran
				end
			else
				commit tran
	

create PROCEDURE borrarHorario (@cedula TCedula, @dia char(1), @hE Time, @hS Time)
as
	declare @error tinyint
	set @error = 0

	begin transaction
			delete horarios  where @cedula=cedula and @dia=@dia and @hE=HEntrada and  @hS = HSalida
			if @@ERROR > 0
				set @error =1
			if @error=1
				begin
					rollback tran
				end
			else
				commit tran




/***************************************************************************
					TRANSACCIONES	BRANDON CHAVES
****************************************************************************/

create view mostrarApartados
as
	select a.cedulaCliente,a.cedulaEmpleado,a.Codigo,a.fechaCreacion,a.fechaLimite from Apartados a
go


--drop procedure infoEmpleado
create procedure infoEmpleado(@cedula char(11))
as
	if @cedula not in (select cedula from Empleados)
		print 'Cedula inválida'
	select * from Empleados e where e.cedula=@cedula
go



 create procedure infoVentas(@fechaInicial date, @fechaFinal date)
as
	select * from Ventas v where v.fecha>=@fechaInicial and v.fecha<=@fechaFinal

--drop procedure insertarVenta
create procedure insertarVenta(@cedulaEmpleado char(11), @fecha date,@monto int)
as
	begin try
		begin transaction
			insert into Ventas(cedulaEmpleado, fecha, monto)
			values(@cedulaEmpleado, @fecha, @monto) 
		commit
	end try
	begin catch
		if @@trancount>0
			rollback
		print ('Esta venta ya se insertó')
	end catch
go

--drop procedure modificarCedulaVenta
create procedure modificarCedulaVenta(@codigoVenta int, @nCedula char(11))
as
	if @codigoVenta not in (select codigo from Ventas) or @nCedula not in(select cedula from Empleados)
			print 'Codigo de venta o cedula invalida'
	else
		update Ventas set cedulaEmpleado= @nCedula where Ventas.Codigo=@codigoVenta
go


--drop procedure eliminarVenta
create procedure eliminarVenta(@codigo int)
as
	declare @codVenta int, @codArticulo int

	declare curArticulosVenta cursor for
	select va.CodigoArticulo, va.CodigoVenta from VentasArticulos va

	if @codigo not in (select Codigo from Ventas)
		begin
			print 'Codigo de venta inválido'
			return
		end
		else
			begin
				open curArticulosVenta
				fetch next from curArticulosVenta into @codArticulo, @codVenta
				while @@FETCH_STATUS=0
					begin
						if @codVenta=@codigo
							update Articulos set Stock+=(select cantidad from VentasArticulos v where v.CodigoVenta=@codigo and v.CodigoArticulo=@codArticulo) where Articulos.Codigo=@codArticulo
						fetch next from curArticulosVenta into @codVenta,@codArticulo
						end
				close curArticulosVenta	
				Deallocate curArticulosVenta 
				delete from VentasArticulos where CodigoVenta=@codigo
				delete from Ventas where Codigo=@codigo
				print 'Venta cancelada'
			end
go



create procedure insertarVentaArticulo(@codigoVenta int, @codigoArticulo int,@cantidad int)
as		
	declare @error tinyint		
		if @codigoArticulo not in( select Articulos.Codigo from Articulos) or @codigoVenta not in(select Ventas.Codigo from Ventas)
			begin
				print 'Error en los datos de entrada'
				return
			end
			begin transaction
				begin
				insert into VentasArticulos(CodigoVenta,CodigoArticulo,cantidad)
										values(@codigoVenta,@codigoArticulo,@cantidad)
				if @@error>0
					set @error=1
					--update Articulos set Stock-=(select @cantidad from VentasArticulos where CodigoVenta=@codigoVenta)
				if @cantidad<=(select Articulos.Stock from Articulos where Articulos.Codigo=@codigoArticulo)
					begin
						update Articulos set Stock-=@cantidad where Articulos.Codigo=@codigoArticulo
						if @@error>0
							set @error=1
						update  Ventas set monto += ((select Articulos.Precio from Articulos where Articulos.Codigo=@codigoArticulo) * @cantidad) where Ventas.Codigo=@codigoVenta
						if @@error>0
							set @error=1
						if @error=1
							begin
								rollback tran
								print ('Error')
							end
					end	
			commit  
			end
go


--drop procedure eliminarVentasArticulo
create procedure eliminarVentasArticulo(@codigoVenta int,@codigoArticulo int)
as
	declare @monto int
	declare @error tinyint

	set @error=0
	set @monto=0

	begin transaction
		set @monto=(select a.Precio from Articulos a where a.Codigo=@codigoArticulo) * (select v.cantidad from VentasArticulos v where v.CodigoVenta=@codigoVenta and v.CodigoArticulo=@codigoArticulo)
		if @@error>0
			set @error=1
		update Articulos set Stock+=(select cantidad from VentasArticulos where VentasArticulos.CodigoVenta=@codigoVenta and VentasArticulos.CodigoArticulo=@codigoArticulo) where Articulos.Codigo=@codigoArticulo
		if @@error>0
			set @error=1
		update Ventas set monto-=(select Precio from Articulos where Codigo=@codigoArticulo)*(select cantidad from VentasArticulos where CodigoVenta=@codigoVenta and CodigoArticulo=@codigoArticulo) where Ventas.Codigo=@codigoVenta
		if @@error>0
			set @error=1
		delete VentasArticulos where CodigoVenta=@codigoVenta and CodigoArticulo=@codigoArticulo
		if @@error>0
			set @error=1
		if @error=1
			begin
				rollback tran
				print ('Error')
			end
		else 
			commit tran
go



create procedure insertarDistrito(@idCanton tinyint, @nombre varchar(50))
as
	begin try 
		begin transaction
			insert into distritos(idCanton,nombre)values
								 (@idCanton,@nombre)
		commit
	end try
	begin catch
		if @@trancount>0
			rollback
		print 'Error al agregar disrito'		
	end catch
go

create procedure eliminarDistrito(@idDistrito smallint)
as
	if @idDistrito not in (select idDistrito from distritos)
		print 'Distrito inválido'
	delete distritos where distritos.idDistrito=@idDistrito
go



/***************************************************************************
					TRANSACCIONES	JEUDRIN ALI MARCHENA
****************************************************************************/

--DROP PROCEDURE InsertarPersona
CREATE PROCEDURE InsertarPersona(@cedula Tcedula, @nombre VARCHAR(30), @apellido1 VARCHAR(30), @apellido2 VARCHAR(30), @sexo TSexo, 
									@idDistrito SMALLINT, @dirrecionExacta VARCHAR(200), @fechaNacimiento DATE, @telefono TTelefono, @email TEmail)
AS
	DECLARE @error TINYINT
	SET @error = 0
	
	BEGIN TRANSACTION
		INSERT INTO personas(cedula,nombre,apellido1,apellido2,sexo,idDistrito,direccionExacta,fechaNacimiento)
						VALUES(@cedula,@nombre,@apellido1,@apellido2,@sexo,@idDistrito,@dirrecionExacta,@fechaNacimiento)
		IF @@ERROR > 0
		SET @error = 1

		INSERT INTO telefonosPersonas(cedula,telefono) 
								VALUES(@cedula,@telefono)
		IF @@ERROR > 0
			SET @error = 1

		INSERT INTO emailPersona(cedula,email) 
							VALUES(@cedula,@email)
		IF @@ERROR > 0
			SET @error = 1

		IF @error = 1
			BEGIN
				ROLLBACK TRAN
				PRINT ('Esta persona ya se insertó')
			END

		ELSE
			COMMIT TRAN



--MODIFICAR PERSONA

--DROP PROCEDURE ModificarPersona
CREATE PROCEDURE ModificarPersona(@CodigoModificarPersona Tcedula, @nombre VARCHAR(30), @apellido1 VARCHAR(30), @apellido2 VARCHAR(30), @sexo TSexo, 
									@idDistrito SMALLINT, @dirrecionExacta VARCHAR(200), @fechaNacimiento DATE, @telefono TTelefono, @email TEmail)
AS
	BEGIN TRANSACTION
		UPDATE personas SET nombre = @nombre,
							apellido1 = @apellido1,
							apellido2 = @apellido2,
							sexo = @sexo,
							idDistrito = @idDistrito,
							direccionExacta = @dirrecionExacta,
							fechaNacimiento = @fechaNacimiento
						WHERE cedula = @CodigoModificarPersona

		UPDATE telefonosPersonas SET telefono = @telefono
						WHERE cedula = @CodigoModificarPersona

		UPDATE emailPersona SET email = @email
						WHERE cedula = @CodigoModificarPersona
	COMMIT



--ELIMINAR PERSONA

--DROP PROCEDURE EliminarPersona
CREATE PROCEDURE EliminarPersona(@CodigoEliminarPersona Tcedula)
AS
	BEGIN TRANSACTION
		DELETE personas WHERE cedula = @CodigoEliminarPersona
	COMMIT

	
--DROP PROCEDURE InsertarEmpleado
CREATE PROCEDURE InsertarEmpleado(@cedula Tcedula, @fechaIngreso DATE, @Salario INT, @jornada TINYINT, @NombreSucursales VARCHAR(50))
AS
	DECLARE @error TINYINT
	SET @error = 0

	BEGIN TRANSACTION
		INSERT INTO Empleados(cedula,fechaIngreso,Salario,jornada,NombreSucursales)
						VALUES(@cedula,@fechaIngreso,@Salario,@jornada,@NombreSucursales)
		IF @@ERROR > 0
			SET @error = 1

		IF @error = 1
			BEGIN
				ROLLBACK TRAN
				PRINT ('Este empleado ya se insertó')
			END

		ELSE
			COMMIT TRAN


--MODIFICAR EMPLEADO

--DROP PROCEDURE ModificarEmpleado
CREATE PROCEDURE ModificarEmpleado(@CodigoModificarEmpleado Tcedula, @fechaIngreso DATE, @Salario INT, 
									@jornada TINYINT, @NombreSucursales VARCHAR(50))
AS
	BEGIN TRANSACTION
		UPDATE Empleados SET fechaIngreso = @fechaIngreso,
								Salario = @Salario,
								jornada = @jornada,
								NombreSucursales = @NombreSucursales
							WHERE cedula = @CodigoModificarEmpleado
	COMMIT



--ELIMINAR EMPLEADO

--DROP PROCEDURE EliminarEmpleado
CREATE PROCEDURE EliminarEmpleado(@CodigoEliminarEmpleado Tcedula)
AS
	BEGIN TRANSACTION
		DELETE Empleados WHERE cedula = @CodigoEliminarEmpleado
	COMMIT



--INSERTAR ARTICULO

--DROP PROCEDURE InsertarArticulo
CREATE PROCEDURE InsertarArticulo(@Codigo INT, @Stock INT, @idProveedor INT, @Precio INT, @TipoVenta TTipoVenta, 
									@Color VARCHAR(30), @Descripcion VARCHAR(200), @Categoria VARCHAR(30))
AS
	DECLARE @error TINYINT
	SET @error = 0

	BEGIN TRANSACTION
		SET IDENTITY_INSERT dbo.Articulos ON;
		INSERT INTO Articulos(Codigo,Stock,idProveedor,Precio,TipoVenta,Color,Descripcion,Categoria)
						VALUES(@Codigo,@Stock,@idProveedor,@Precio,@TipoVenta,@Color,@Descripcion,@Categoria)
		IF @@ERROR > 0
			SET @error = 1
		SET IDENTITY_INSERT dbo.Articulos OFF;

		IF @@ERROR > 0
			SET @error = 1

		IF @error = 1
			BEGIN
				ROLLBACK TRAN
				PRINT ('Este Artículo ya ha sido insertado')
			END

		ELSE
			COMMIT TRAN



--MODIFICAR ARTICULO

--DROP PROCEDURE ModificarArticulo
CREATE PROCEDURE ModificarArticulo(@CodigoModificarArticulo INT, @Stock INT, @idProveedor INT, @Precio INT, 
									@TipoVenta TTipoVenta, @Color VARCHAR(30), @Descripcion VARCHAR(200), @Categoria VARCHAR(30))
AS
	BEGIN TRANSACTION
		UPDATE Articulos SET Stock = @Stock,
								idProveedor = @idProveedor,
								Precio = @Precio,
								TipoVenta = @TipoVenta,
								Color = @Color,
								Descripcion = @Descripcion,
								Categoria = @Categoria
							WHERE Codigo = @CodigoModificarArticulo
	COMMIT




--ELIMINAR ARTICULO

--DROP PROCEDURE EliminarArticulo
CREATE PROCEDURE EliminarArticulo(@CodigoEliminarArticulo INT)
AS
	BEGIN TRANSACTION
		DELETE Articulos WHERE Codigo = @CodigoEliminarArticulo
	COMMIT



/***************************************************************************
							XAVIER BLANCO
****************************************************************************/


--Inserta un nuevo registro en las tablas apartados
--drop procedure nuevoApartado
create procedure nuevoApartado(@cedEmpleado char(11), @cedCliente char(11), @fechaLimite date)
 as
	declare @error tinyint
	set @error = 0

	begin transaction
		insert into Apartados(cedulaEmpleado,cedulaCliente,fechaCreacion,fechaLimite,monto) values (@cedEmpleado,@cedCliente,GETDATE(),@fechaLimite,0)
		if @@error>0
			set @error = 1
		if @error =1
		begin
			rollback tran
			print ('Error en la insercion del apartado')
		end
		else 
			commit tran

--Modifica la cedula de un empleado en un apartado
--drop procedure modCedEmpleadoApartado
create procedure modCedEmpleadoApartado(@codApartado int, @nuevaCed char(11))
as
	update Apartados set cedulaEmpleado=@nuevaCed where Codigo=@codApartado
go
--Modifica la cedula de un cliente en un apartado
--drop procedure modCedClienteApartado
create procedure modCedClienteApartado(@codApartado int, @nuevaCed char(11))
as
	update Apartados set cedulaCliente=@nuevaCed where Codigo=@codApartado
go
--Modifica la fecha limite en un apartado
--drop procedure modFechaLimitepartado
create procedure modFechaLimiteApartado(@codApartado int, @nuevaFecha date)
as
	update Apartados set fechaLimite=@nuevaFecha where Codigo=@codApartado

--Inserta articulos a un apartado ya creado, los articulos son insertados en la tabla ApartadosArticulos, ademas modifica el de cada respectivo articulo apartado
--drop procedure insertarArticuloApartado
create procedure insertarArticuloApartado(@codApartado int, @codArticulo int, @cantidad int)
as
	declare @error tinyint
	declare @monto int

	set @error=0
	set @monto= (select precio from Articulos a where a.Codigo=@codArticulo) * @cantidad

	begin transaction
		insert into ApartadosArticulos(CodigoArticulo,CodigoApartado,cantidad) values (@codArticulo,@codApartado,@cantidad)
		if @@ERROR>0
			set @error=1

		if @error =1 or (select stock from Articulos a where a.Codigo=@codArticulo) < @cantidad
		begin
			rollback tran
			print ('Error en la insercion')
		end
		else 
			begin
				commit tran
				update Articulos set Stock-=@cantidad where codigo=@codArticulo
				update Apartados set monto += @monto where Codigo=@codApartado
			end


--Elimina articulos de un apartado, actualiza el stock del articulo
--drop procedure eliminarArticuloApartado
create procedure eliminarArticuloApartado(@codApartado int, @codArticulo int)
as
	declare @monto int
	declare @error tinyint

	set @error=0
	set @monto=0

	begin transaction
		set @monto= (select precio from Articulos a where a.Codigo=@codArticulo) * (select cantidad from ApartadosArticulos where CodigoApartado=@codApartado and CodigoArticulo=@codArticulo)
		if @@ERROR>0
			set @error=1

		update Articulos set Stock += (select cantidad from ApartadosArticulos where CodigoApartado=@codApartado and CodigoArticulo=@codArticulo) where Articulos.Codigo=@codArticulo
		if @@ERROR>0
			set @error=1

		update Apartados set monto -= (select precio from Articulos where Codigo=@codArticulo) * (select cantidad from ApartadosArticulos where CodigoApartado=@codApartado and CodigoArticulo=@codArticulo) where Apartados.Codigo=@codApartado
		if @@ERROR>0
			set @error=1

		delete ApartadosArticulos where CodigoApartado=@codApartado and CodigoArticulo=@codArticulo
		if @@ERROR>0
			set @error=1
		if @error =1
			begin
				rollback tran
				print ('Error')
			end
		else 
			commit tran

--Borrar un apartado, elimina el registro en las tablas apartados y apartadosArticulos y actualiza el stock de cada articulo relacionado con el apartado
--drop procedure borrarApartado
create procedure borrarApartado(@codigo int)
	as
		declare @codApartado int
		declare @codArticulo int

		declare curArticulosApartado cursor for
			select CodigoArticulo, CodigoApartado from ApartadosArticulos

		if @codigo not in (select Codigo from Apartados)
		begin
			print 'Codigo de apartado invalido'
		end

		else
			begin
				open curArticulosApartado
				fetch next from curArticulosApartado into @codArticulo, @codApartado
				while @@FETCH_STATUS=0
				begin
					if @codApartado=@codigo
						begin
							update Articulos set Stock+=(select cantidad from ApartadosArticulos a where a.CodigoApartado=@codigo and CodigoArticulo=@codArticulo) where Articulos.Codigo=@codArticulo
						end
					fetch next from CurArticulosApartado into @codArticulo, @codApartado
				end
				close CurArticulosApartado
				Deallocate CurArticulosApartado
				delete from ApartadosArticulos where CodigoApartado=@codigo
				delete from Abonos where CodigoApartado=@codigo
				delete from Apartados where Codigo=@codigo
				print 'Apartado Cancelado'
		end

--Realizar un abono a un apartado
--drop procedure nuevoAbono
create procedure nuevoAbono(@codApartado int,@monto int)
	as
	declare @error tinyint
	set @error = 0

	begin transaction
		insert into Abonos(CodigoApartado,monto,fecha) values (@codApartado,@monto,GETDATE())
		if @@error>0
			set @error = 1

		update Apartados set monto-=@monto where Apartados.Codigo=@codApartado
		if @@error>0
			set @error = 1

		--if (select monto from Apartados where Codigo=@codApartado) = 0
			--procesar como venta

		if @error =1
		begin
			rollback tran
			print ('Error en la insercion del abono')
		end
		else 
			commit tran
		
--Trigger para la insercion de un abono que no permite el abono si el monto es mayor al total restante en el apartado
--drop trigger validAbono		
create trigger validAbono on Abonos
for insert
as
	declare @monto int,
			@codigoApartado int

	select @monto=monto from inserted
	select @codigoApartado=CodigoApartado from inserted

	if (select monto from Apartados where Codigo=@codigoApartado) < @monto
	begin 
		rollback tran
		print 'El monto del abono es mayor al total debido'
	end

--Eliminar un abono
--drop procedure eliminarAbono
create procedure eliminarAbono(@codigo int, @codigoApartado int)
	as
		declare @error int,
				@nMonto int
		set @nMonto=(select monto from Abonos where Abonos.Codigo=@codigo) 
		set @error=0
		begin transaction
			delete Abonos where Codigo=@codigo
			if @@ERROR>0
				set @error=1
			update Apartados set monto += @nMonto where Apartados.Codigo=@codigoApartado
			if @@ERROR>0
				set @error=1
			if @error =1
				begin
					rollback tran
					print ('Error')
				end
				else 
					commit tran

--Insertar un proveedor
--drop procedure insertarProveedor
create procedure insertarProveedor(@nombre varchar(50),@direccion varchar(200))
as
	insert into Proveedores(Nombre,Direccion) values (@nombre,@direccion)

--Modifica el nombre de un proveedor
--drop procedure modNomProveedor
create procedure modNomProveedor(@id int, @nombre varchar(50))
as
	update Proveedores set Nombre=@nombre where idProveedor=@id

--Modifica la direccion de un proveedor
--drop procedure modDirProveedor
create procedure modDirProveedor(@id int, @direccion varchar(200))
as
	update Proveedores set Direccion=@direccion where idProveedor=@id

--Elimina un proveedor
--drop procedure eliminarProveedor
create procedure eliminarProveedor(@id int)
as
	delete Proveedores where idProveedor=@id

--Insertar Telefono de Proveedor
--drop procedure insertarTelProveedor
create procedure insertarTelProveedor(@id int, @telefono char(9))
as
	insert into telefonosProveedores(idProveedor,telefono) values (@id,@telefono)

--Elimina telefono de Proveedor
--drop procedure eliminarTelProveedor
create procedure eliminarTelProveedor(@id int, @telefono char(9))
as
	delete telefonosProveedores where idProveedor=@id and telefono=@telefono

--Insertar Email de Proveedor
--drop procedure insertarEmailProveedor
create procedure insertarEmailProveedor(@id int, @email char(30))
as
	insert into emailProveedores(idProveedor,email) values (@id,@email)

--Elimina email de Proveedor
--drop procedure eliminarEmailProveedor
create procedure eliminarEmailProveedor(@id int, @email char(30))
as
	delete emailProveedores where idProveedor=@id and email=@email

--Inserta un nuevo registro en las tablas pedidos
--drop procedure nuevoPedido
create procedure nuevoPedido(@idProveedor int, @nomSucursal varchar(50))
 as
	declare @error tinyint
	set @error = 0

	begin transaction
		insert into Pedidos(idProveedor,nombreSucursal,fecha,monto) values (@idProveedor,@nomSucursal,GETDATE(),0)
		if @@error>0
			set @error = 1
		if @error =1
		begin
			rollback tran
			print ('Error en la insercion del pedido')
		end
		else 
			commit tran

--Modifica el id del proveedor en un pedido
--drop procedure modIdProveedorPedido
create procedure modIdProveedorPedido(@codPedido int, @nuevoId int)
as
	update Pedidos set idProveedor=@nuevoId where Codigo=@codPedido

--Modifica el nombre de sucursal en un pedido
--drop procedure modNomSucursalPedido
create procedure modNomSucursalPedido(@nombre varchar(50), @codPedido int)
as
	update Pedidos set nombreSucursal=@nombre where Codigo=@codPedido

--Inserta articulos a un pedido ya creado, los articulos son insertados en la tabla PedidosArticulos, ademas modifica el stock de cada respectivo articulo pedido
--drop procedure insertarArticuloPedido
create procedure insertarArticuloPedido(@codPedido int, @codArticulo int, @cantidad int)
as
	declare @error tinyint
	declare @monto int

	set @error=0
	set @monto= (select precio from Articulos a where a.Codigo=@codArticulo) * @cantidad

	begin transaction
		insert into PedidosArticulos(CodigoArticulo,CodigoPedido,cantidad) values (@codArticulo,@codPedido,@cantidad)
		if @@ERROR>0
			set @error=1

		if @error =1 
		begin
			rollback tran
			print ('Error en la insercion')
		end
		else 
			begin
				commit tran
				update Articulos set Stock+=@cantidad where Articulos.Codigo=@codArticulo
				update Pedidos set monto += @monto where Pedidos.Codigo=@codPedido
			end

--Elimina articulos de un pedido, actualiza el stock del articulo
--drop procedure eliminarArticuloPedido
create procedure eliminarArticuloPedido(@codPedido int, @codArticulo int)
as
	declare @monto int
	declare @error tinyint

	set @error=0
	set @monto=0

	begin transaction
		set @monto= (select precio from Articulos a where a.Codigo=@codArticulo) * (select cantidad from PedidosArticulos where CodigoPedido=@codPedido and CodigoArticulo=@codArticulo)
		if @@ERROR>0
			set @error=1

		update Articulos set Stock -= (select cantidad from PedidosArticulos where CodigoPedido=@codPedido and CodigoArticulo=@codArticulo) where Articulos.Codigo=@codArticulo
		if @@ERROR>0
			set @error=1

		update Pedidos set monto -= (select precio from Articulos where Codigo=@codArticulo) * (select cantidad from PedidosArticulos where CodigoPedido=@codPedido and CodigoArticulo=@codArticulo) where Pedidos.Codigo=@codPedido
		if @@ERROR>0
			set @error=1

		delete PedidosArticulos where CodigoPedido=@codPedido and CodigoArticulo=@codArticulo
		if @@ERROR>0
			set @error=1

		if @error =1
			begin
				rollback tran
				print ('Error')
			end
		else 
			commit tran

--Borrar un pedido, elimina el registro en las tablas pedidos y pedidosArticulos y actualiza el stock de cada articulo relacionado con el pedido
--drop procedure borrarPedido
create procedure borrarPedido(@codigo int)
	as
		declare @codPedido int
		declare @codArticulo int

		declare curArticulosPedido cursor for
			select CodigoArticulo, CodigoPedido from PedidosArticulos

		if @codigo not in (select Codigo from Pedidos)
		begin
			print 'Codigo de pedido invalido'
		end

		else
			begin
				open curArticulosPedido
				fetch next from curArticulosPedido into @codArticulo, @codPedido
				while @@FETCH_STATUS=0
				begin
					if @codPedido=@codigo
						begin
							update Articulos set Stock-=(select cantidad from PedidosArticulos pa where pa.CodigoPedido=@codigo and CodigoArticulo=@codArticulo) where Articulos.Codigo=@codArticulo
						end
					fetch next from curArticulosPedido into @codArticulo, @codPedido
				end
				close curArticulosPedido
				Deallocate curArticulosPedido
				delete from PedidosArticulos where CodigoPedido=@codigo
				delete from Pedidos where Codigo=@codigo
				print 'Pedido Cancelado'
		end








/***************************************************************************
						Inserciones de lugares
****************************************************************************/

--Provincias

--delete provincias
Select * from Provincias

Insert into Provincias (idProvincia, nombre) 
	values (1,'San José'),
			(2,'Alajuela'),
			(3,'Cartago'),
			(4,'Heredia'),
			(5,'Guanacaste'),
			(6,'Puntarenas'),
			(7,'Limón')

--Cantones
Insert cantones (idProvincia, nombre) 
values (1,'San José'),
		(1,'Escazú'),
		(1,'Desamparados'),
		(1,'Puriscal'),
		(1,'Tarrazú'),
		(1,'Aserrí'),
		(1,'Mora'),
		(1,'Goicochea'),
		(1,'Santa Ana'),
		(1,'Alajuelita'),
		(1,'Vásquez de Coronado'),
		(1,'Acosta'),
		(1,'Tibás'),
		(1,'Moravia'),
		(1,'Montes de Oca'),
		(1,'Turrubares'),
		(1,'Dota'),
		(1,'Curridabat'),
		(1,'Perez Zeledón'),
		(1,'León Cortés Castro'),
		(2,'Alajuela'),
		(2,'San Ramón'),
		(2,'Grecia'),
		(2,'San Mateo'),
		(2,'Atenas'),
		(2,'Naranjo'),
		(2,'Palmares'),
		(2,'Poás'),
		(2,'Orotina'),
		(2,'San Carlos'),
		(2,'Zarcero'),
		(2,'Valverde Vega'),
		(2,'Upala'),
		(2,'Los Chiles'),
		(2,'Guatuso'),
		(3,'Cartago'),
		(3,'Paraíso'),
		(3,'La Unión'),
		(3,'Jiménez'),
		(3,'Turrialba'),
		(3,'Alvarado'),
		(3,'Oreamuno'),
		(3,'El Guarco'),
		(4,'Heredia'),
		(4,'Barva'),
		(4,'Santo Domingo'),
		(4,'Santa Bárbara'),
		(4,'San Rafael'),
		(4,'San Isidro'),
		(4,'Belen'),
		(4,'Flores'),
		(4,'San Pablo'),
		(4,'Sarapiquí'),		
		(5,'Liberia'),
		(5,'Nicoya'),
		(5,'Sant Cruz'),
		(5,	'Bagaces'),
		(5,'Carrillo'),
		(5,'Cañas'),
		(5,'Abangares'),
		(5,'Tilarán'),
		(5,'Nandayure'),
		(5,'La Cruz'),
		(5,'Hojancha'),
		(6,'Puntarenas'),
		(6,'Esparza'),
		(6,'Buenos Aires'),
		(6,'Montes de Oro'),
		(6,'Osa'),
		(6,'Aguirre'),
		(6,'Golfito'),
		(6,'Coto Brus'),
		(6,'Parrita'),
		(6,'Corredores'),
		(6,'Garabito'),
		(7,'Limón'),
		(7,'Pococí'),
		(7,'Siquirres'),
		(7,'Talamanca'),
		(7,'Matina'),
		(7,'Guácimo')

Insert distritos (idCanton, nombre) 
values (1,'Carmen'),
		(2,'Ciudad Escazú'),
		(21,'Ciudad Alajuela'),
		(22,'Ciudad San Ramón'),
		(36,'Villa Carmen'),
		(4,'Villa Santiago'),
		(44,'Ciudad Heredia'),
		(45,'Ciudad Barva'),
		(53,'Ciudad Puerto Viejo'),		
		(54,'Ciudad Liberia'),
		(55,'Ciudad Nicoya'),
		(65,'Ciudad de Puntarenas'),
		(66,'Ciudad Esparza'),
		(76,'Ciudad de Limón'),
		(77,'Ciudad de Guápiles'),
		(78,'Ciudad de Siquirres')


--Sucursales

Insert into Sucursales(Nombre,idDistrito,direccionExacta) 
values ('Liberia',1,'Guanacaste')


Insert into personas (cedula,nombre,apellido1,apellido2,sexo,idDistrito,direccionExacta,fechaNacimiento) 
values ('0-0000-0001','Leonardo','Víquez','Acuña','M','3','50 Mts Este de Palí','01/01/1980'),
	   ('0-0000-0002','Gaudy','Esquivel','Vega','F','3','Los Cipreces San Juan','02/02/1981'),
	   ('0-0000-0003','Evan Josué','Blanco','Rojas','M','3','100 Este Pulpería los Mangos','03/03/1992'),
	   ('0-0000-0004','Ericka','Ramirez','Chavarria','F','4','200 Sur y 15 Este iglesia San Juan','04/04/1992'),
	   ('0-0000-0005','Yosenth Andrés','Viquez','Benavides','M','3','500 Norte del Hotel la Greta','05/05/1991'),
	   ('0-0000-0006','Ivannia','Abarca','Sánchez','F','3','100 este panadería la Panchita','06/06/1991'),
	   ('0-0000-0007','Juan Carlos','Trejos','Brenes','M','3','Ciudad Quesada','07/07/1987'),
	   ('0-0000-0008','Ronny','Villalobos','Herrera','M','3','Ciudad Quesada','08/08/1985'),
	   ('0-0000-0009','Melissa','Saens','Víquez','F','3','Ciudad Quesada','09/09/1983'),
	   ('0-0000-0010','Ana','Ríos','Ríos','F','3','Ciudad Quesada','10/10/1985'),
	   ('0-0000-0011','Elena','Arrieta','Salazar','F','3','Ciudad Quesada','11/11/1987'),
	   ('0-0000-0012','Ricardo','Brenes','Trejos','M','3','Ciudad Quesada','12/12/1989'),
	   ('5-0400-0598','Jeudrin Alí','Marchena','Sánchez','M','16','Siquirres','20/05/1994'),
	   ('7-0345-0678','Antony','Durán','Hernández','M','16','Siquirres','30/10/1993'),
	   ('5-0456-0789','Brandon','Chavez','Villegas','M','9','Puerto Viejo','23/04/1994'),
	   ('2-0456-0789','Xavier','Arias','Blanco','M','3','San Carlos','15/07/1992')

--Telefonos Personas


Insert into telefonosPersonas 
values  ('0-0000-0001','2401-3109'),
		('0-0000-0001','8895-3002'),
		('0-0000-0002','6432-4456'),
		('0-0000-0003','6534-6788'),
		('0-0000-0004','5668-7989'),
		('0-0000-0005','2344-3545'),
		('5-0400-0598','8564-8885'),
		('7-0345-0678','8580-6489'),
		('5-0456-0789','8691-2046'),
		('2-0456-0789','8755-2695')     	

--Email Persona


Insert into emailPersona
values  ('0-0000-0001','lviquez@itcr.ac.cr'),
		('0-0000-0001','leoviquez@gmail.com'),
		('0-0000-0002','gesquivel@itcr.ac.cr'),
		('0-0000-0003','Evan@gmail.com'),
		('0-0000-0004','Ericka@gmail.com'),
		('0-0000-0005','Yosenth@gmail.com'),
		('0-0000-0006','Ivania@gmail.com'),
		('5-0400-0598','jeudrin@gmail.com'),
		('7-0345-0678','antony@gmail.com'),
		('5-0456-0789','brandon@gmail.com'),
		('2-0456-0789','xavier@gmail.com')

--Empleados

Insert into Empleados(cedula,fechaIngreso,Salario,jornada,NombreSucursales) 
values('2-0456-0789','01/01/2012',1000,4,'Liberia'),
		('5-0456-0789','01/01/2012',1000,4,'Liberia'),
		('7-0345-0678','01/01/2012',1000,4,'Liberia'),
		('5-0400-0598','01/01/2012',1000,4,'Liberia'),
		('0-0000-0006','01/01/2012',1000,4,'Liberia')

--Proveedores

SET IDENTITY_INSERT dbo.Proveedores ON;
GO

Insert into Proveedores (idProveedor,Nombre,Direccion)
values (1,'Picado Internacional','San José'),
	   (2, 'GYG','San José'),
	   (3,'Fashion Yanith','Alajuela'),
	   (4,'Noa Style','Cartago'),
	   (5,'NSK','Alajuela')

SET IDENTITY_INSERT dbo.Proveedores OFF;
GO

--Telefonos Proveedores 
Insert into telefonosProveedores
values  (1,'2401-3109'),
		(2,'2466-2345'),
		(3,'2521-6789'),
		(4,'4989-4768'),
		(5,'2467-3214')

--Email Proveedores 
Insert into emailProveedores
values  (1,'pinter@inter.cr'),
		(3,'fashion@fash.com'),
		(5,'nsk@nsk.cr')

--Articulos 

SET IDENTITY_INSERT dbo.Articulos ON;
GO

Insert into Articulos(Codigo,Stock,idProveedor,Precio,TipoVenta,Color,Descripcion,Categoria)
values (12,23,1,13500,'CONTADO','Rojo','Blusa','Mujer'),
		(3,0,1,8500,'AMBAS','Azul','Falda','Mujer'),
		(6,15,2,3500,'AMBAS','Negro','Pulsera','Mujer'),
		(23,12,3,17300,'AMBAS','Verde','Vestido','Mujer'),
		(5,8,4,11200,'AMBAS','Café','Shorts','Mujer'),
		(8,0,5,6500,'CONTADO','Azul','Carters','Mujer'),
		(10,13,3,9000,'AMBAS','Amarillo','Jeans','Mujer'),
		(2,12,2,2300,'AMBAS','Negro','Arete','Mujer'),
		(7,0,4,4350,'AMBAS','Blanco','Love','Mujer'),
		(26,5,5,6500,'AMBAS','Verde','Conjuntos','Mujer'),
		(9,0,1,14200,'CONTADO','Blanco','Leggings','Mujer'),
		(21,15,3,8350,'CONTADO','Rojo','Mezclilla','Mujer'),
		(19,0,4,6500,'AMBAS','Negro','Cardigans','Mujer'),
		(28,14,5,9300,'AMBAS','Blanco','Pants','Mujer'),
		(13,7,1,7600,'AMBAS','Morado','Jacket','Mujer'),
		(17,0,3,11340,'CONTADO','Rojo','Abrigo','Mujer'),
		(24,2,5,5500,'AMBAS','Anaranjado','Bufanda','Mujer')

SET IDENTITY_INSERT dbo.Articulos OFF;
GO

select * from Horarios


select * from personas
select * from Empleados