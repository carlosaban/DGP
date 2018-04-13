USE [DVGP_CITAVAL]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*LISTAR COMPRA*/
CREATE PROCEDURE [dbo].[DGP_Listar_Compra](
@intIdCompra INT = NULL
) AS
BEGIN
	SELECT	C.IdCompra
		,C.IdTipoDocumentoCompra
		,[TipoDocumentoCompra] = PD.Texto
		,C.NumeroDocumento
		,C.TotalPeso_Bruto
		,C.TotalPeso_Tara
		,C.TotalPeso_Neto
		,C.Precio
		,C.MontoSubTotal
		,C.MontoIgv
		,C.MontoTotal --// Importe
		,C.TotalDevolucion
		,C.TotalAmortizacion --// Pago Efectuado
		,C.TotalSaldo --// Saldo Anterior
		,C.Observacion
		,C.IdEstado
		,C.IdEmpresa
		,[Empresa] = EM.RazonSocial
		,C.IdProducto
		,[Producto] = P.Nombre
		,C.IdCliente
		,[Cliente] = ISNULL(CP.Nombres, 'CLIENTE EVENTUAL')
		,C.IdPersonal
		,C.FechaCreacion
		,C.TotalUnidades
	FROM dbo.TbCompra AS C
		LEFT JOIN dbo.Tb_ParametroDetalle AS PD
			ON (PD.Id_Parametro = 3 AND C.IdTipoDocumentoCompra = PD.Valor)
		INNER JOIN dbo.Tb_Producto AS P
			ON (C.IdProducto = P.Id_Producto)
		LEFT JOIN dbo.Tb_Cliente_Proveedor AS CP 
			ON (C.IdCliente = CP.Id_Cliente)
		INNER JOIN dbo.Tb_Empresa AS EM
			ON (C.IdEmpresa = EM.Id_Empresa)
	WHERE C.IdCompra = ISNULL(@intIdCompra, C.IdCompra)
		AND C.IdEstado != dbo.DGP_COMPRA_ESTADO_ANULADO()
             order by CP.Nombres
END
GO

/*FUNCION COMPRA ESTADO REGISTRADO*/
CREATE FUNCTION [dbo].[DGP_COMPRA_ESTADO_REGISTRADO] () RETURNS CHAR(3)
AS
BEGIN
	RETURN 'REG'
END
go
CREATE FUNCTION [dbo].[DGP_COMPRA_ESTADO_CANCELADO] () RETURNS CHAR(3)
AS
BEGIN
	RETURN 'CAN'
END
go
CREATE FUNCTION [dbo].[DGP_COMPRA_ESTADO_CONGELADO] () RETURNS CHAR(3)
AS
BEGIN
	RETURN 'CON'
END
go
CREATE FUNCTION [dbo].[DGP_COMPRA_ESTADO_ANULADO] () RETURNS CHAR(3)
AS
BEGIN
	RETURN 'ANL'
END
go
/*FUNCION OBTENER CANTIDAD JAVAS*/
CREATE FUNCTION [dbo].[DGP_Obtener_CantidadJavasCompra] (
@intIdCompra	INT
) RETURNS INT
AS
BEGIN
	DECLARE @intCantidadLineasCompra INT;
	DECLARE @intDevolucion		INT;

	/** Cantidades con Devolucion = 'NO' **/
	SELECT @intCantidadLineasCompra = ISNULL(SUM(CantidadJavas), 0)
	FROM dbo.TbLineaCompra
	WHERE IdEstado = dbo.DGP_COMPRA_ESTADO_REGISTRADO()
		AND EsDevolucion = 'N'
		AND IdCompra = @intIdCompra;

	/** Cantidades con Devolucion = 'SI' **/
	SELECT @intDevolucion = ISNULL(SUM(CantidadJavas), 0)
	FROM dbo.TbLineaCompra
	WHERE IdEstado = dbo.DGP_COMPRA_ESTADO_REGISTRADO()
		AND EsDevolucion = 'S'
		AND IdCompra = @intIdCompra;

	RETURN (@intCantidadLineasCompra - @intDevolucion)

END
go
/*LISTAR AMORTIZACION COMPRA*/
CREATE PROCEDURE [dbo].[DGP_Listar_Amortizacion_Compra] (  

@intIdCompra INT = NULL  
,@intIdCliente INT = NULL  
,@intIdProducto INT = NULL  
, @intIncluCanceldos INT = 0  
) AS  

BEGIN  
 DECLARE @CompraAmortizacion TABLE(  

  intIdAmortizacion       INT  

  ,intIdCompra  INT  

  ,intIdCliente  INT  

  ,intIdProducto  INT  

  ,varTipoDocumento VARCHAR(12)  

  ,varProducto  VARCHAR(100)  

  ,datFecha  DATETIME NULL  

  ,intCantidadJavas INT NULL  

  ,decPesoNeto  DECIMAL(8,2) NULL  

  ,decImporte  DECIMAL(8,2)  

  ,decSaldo  DECIMAL(8,2) NULL  

  ,intIndicador  INT  

  ,intIdPersonal  int  

  ,varPesonal  VARCHAR(50)  

  , varEstado  varchar(3)  

  ,fechaCreacion   datetime null  

 );  
 
 /** Obtener las Compras **/  
 INSERT INTO @CompraAmortizacion

 SELECT   

   [intIdAmortizacion] = 0  

  ,[intIdCompra] = C.IdCompra  

  ,[intIdCliente] = C.IdCliente  

  ,[intIdProducto] = P.Id_Producto  

  ,[varTipoDocumento] = 'Compra'  

  ,[varProducto] = P.Nombre  

  ,[datFecha] = CJ.FECHA --CONVERT(VARCHAR, CJ.FECHA, 103)  

  ,[intCantidadJavas] = dbo.DGP_Obtener_CantidadJavasCompra(C.IdCompra)  

  ,[decPesoNeto] = C.TotalPeso_Neto  

  ,[decImporte] = C.MontoTotal  

  ,[decSaldo] = C.TotalSaldo  

  ,[intIndicador] = '1'  

  ,[intIdPersonal] = cj.Id_Personal  

  ,[varPesonal] = per.Nombre  

  ,[varEstado] = C.idEstado  

  , [fechaCreacion] = C.fechaCreacion  

 FROM dbo.TbCompra AS C  

  INNER JOIN DBO.TB_CAJA AS CJ ON CJ.ID_CAJA = C.IdCaja  

  INNER JOIN dbo.Tb_Producto AS P ON (C.IdProducto = P.Id_Producto)  

  left join dbo.Tb_Personal  as per on per.Id_Personal = cj.Id_Personal  

 WHERE 1=1  

   and C.IdEstado != DBO.DGP_COMPRA_ESTADO_CANCELADO()  

  and C.IdEstado != dbo.DGP_COMPRA_ESTADO_ANULADO()  

  AND C.IdCompra = ISNULL(@intIdCompra, C.IdCompra)  

  AND ISNULL(C.IdCliente, -1) = ISNULL(@intIdCliente, ISNULL(C.IdCliente, -1))  

  AND C.IdProducto = ISNULL(@intIdProducto, C.IdProducto);  

 /** Obtener las Amortizaciones **/  
  INSERT INTO @CompraAmortizacion  

 SELECT   

   [intIdAmortizacion] = AC.IdAmortCompra  

  ,[intIdCompra] = AC.IdCompra  

  ,[intIdCliente] = C.IdCliente  

  ,[intIdProducto] = PR.Id_Producto  

  ,[varTipoDocumento] = 'Amortización'  

  ,[varProducto] = PR.Nombre  

  ,[datFecha] =AC.FechaModificacion -- CONVERT(VARCHAR, AC.fechaPago, 103)  

  ,[intCantidadJavas] = NULL  

  ,[decPesoNeto] = NULL  

  ,[decImporte] = AC.Monto  

  ,[decSaldo] = NULL  

  ,[intIndicador] = '0'  

  ,[intIdPersonal] = AC.IdPersonal  

  ,[varPesonal] = per.Nombre  

  ,[varEstado] = AC.idEstado  

  , [fechaCreacion] = AC.fechaCreacion  

 FROM dbo.Tb_Amort_Compra AS AC  

  INNER JOIN TbCompra AS C ON (AC.IdCompra = C.IdCompra AND C.IdEstado != DBO.DGP_COMPRA_ESTADO_ANULADO() )  

  INNER JOIN DBO.TB_CAJA AS CJ ON CJ.ID_CAJA = C.IdCaja  

  INNER JOIN dbo.Tb_Producto AS PR ON (C.IdProducto = PR.Id_Producto)  

  left join dbo.Tb_Personal  as per on per.Id_Personal = AC.IdPersonal  

 WHERE  1=1  

  and  C.IdEstado != DBO.DGP_COMPRA_ESTADO_CANCELADO()  

  AND AC.IdEstado = dbo.DGP_COMPRA_ESTADO_REGISTRADO()  

  AND AC.IdCompra = ISNULL(@intIdCompra, AC.IdCompra)  

  AND ISNULL(AC.IdCliente, -1) = ISNULL(@intIdCliente, ISNULL(AC.IdCliente, -1))  

  AND PR.Id_Producto = ISNULL(@intIdProducto, PR.Id_Producto);  

 SELECT * FROM @CompraAmortizacion  

 ORDER BY [datFecha] ASC, varTipoDocumento DESC ,fechaCreacion , decSaldo  ;  

END
GO

/*INSERTAR ADELANTO COMPRA*/
CREATE PROCEDURE [dbo].[DGP_Insertar_AdelantoCompra](
@decMonto		DECIMAL(8,2)
,@varIdFormaPago	VARCHAR(5)
,@varIdTipoAmortizacion	VARCHAR(5)
,@varObservacion	VARCHAR(100)
,@varIdEstado		VARCHAR(5)
,@intIdCliente		INT
,@intIdPersonal		INT
)
AS

BEGIN
	SET NOCOUNT OFF
	INSERT INTO dbo.Tb_Amort_Compra(
		Monto
		,IdFormaPago
		,IdTipoAmortizacion
		,Observacion
		,IdEstado
		,IdCliente
		,IdPersonal
		,UsuarioCreacion
		,FechaCreacion
	) VALUES (
		@decMonto
		,@varIdFormaPago
		,@varIdTipoAmortizacion
		,@varObservacion
		,@varIdEstado
		,@intIdCliente
		,@intIdPersonal
		,@intIdPersonal
		,GETDATE()
	);
END
go
/*INSERTAR AMORTIZACION COMPRA*/
CREATE PROCEDURE [dbo].[DGP_Insertar_AmortizacionCompra](

@decMonto		DECIMAL(8,2)

,@varNumeroDocumento	VARCHAR(20)

,@varIdFormaPago	VARCHAR(5)

,@varIdTipoAmortizacion	VARCHAR(5)

,@varObservacion	VARCHAR(100)

,@varIdEstado		VARCHAR(5)

,@intIdCompra		INT

,@intIdCliente		INT = NULL

,@intIdPersonal		INT

,@intIdUsuarioCreacion	INT

, @intIdCaja INT
,@intIdDocumento INT
)

AS

BEGIN

	SET NOCOUNT OFF

	INSERT INTO dbo.Tb_Amort_Compra(

		Monto

		,NumeroDocumento

		,IdFormaPago

		,IdTipoAmortizacion

		,Observacion

		,IdEstado

		,IdCompra

		,IdCliente

		,IdPersonal

		,UsuarioCreacion

		,FechaCreacion

		,IdCaja

		,IdDocumento

	) VALUES (

		@decMonto

		,@varNumeroDocumento

		,@varIdFormaPago

		,@varIdTipoAmortizacion

		,@varObservacion

		,@varIdEstado

		,@intIdCompra

		,@intIdCliente

		,@intIdPersonal

		,@intIdUsuarioCreacion

		,GETDATE()

		,@intIdCaja

		,@intIdDocumento
	);

END
go
/*Eliminar amortizacion*/
CREATE PROCEDURE [dbo].[DGP_Eliminar_AmortizacionCompra] (

@intIdAmortizacion	INT  ,
@intIdUsuario	INT 

) AS

declare @intIdCompra int;
declare @IdDocumento int;

DECLARE @TmpCompras TABLE ( posicion int identity(1,1), IdCompra INT )

BEGIN

	select @intIdCompra = A.IdCompra, @IdDocumento = A.IdDocumento
    from dbo.Tb_Amort_Compra A
    inner join dbo.TbCompra C on A.IdCompra = C.IdCompra
	where IdAmortCompra =@intIdAmortizacion;

	insert into @TmpCompras
	select A.IdCompra from [Tb_Amort_Compra] A
	WHERE IdDocumento = @IdDocumento
	and IdEstado = dbo.DGP_COMPRA_ESTADO_REGISTRADO()

	UPDATE [Tb_Amort_Compra]
	SET
		IdEstado = dbo.DGP_COMPRA_ESTADO_ANULADO() ,
		UsuarioModificacion = @intIdUsuario ,
		FechaModificacion = getdate()
	WHERE IdDocumento = @IdDocumento
	and IdEstado = dbo.DGP_COMPRA_ESTADO_REGISTRADO()

	UPDATE dbo.Tb_documentoPagoCompra set
		estado = dbo.DGP_COMPRA_ESTADO_ANULADO()
		, UsuarioModificacion = @intIdUsuario
		, FechaModificacion = GETDATE()

	where IdDocumentoPagoCompra = @IdDocumento

	declare @maxCompras int ;
	declare @i int;

	set @i =1;
	select @maxCompras =COUNT(*) from @TmpCompras

	while (@i <=@maxCompras) 
	begin
	 select @intIdCompra = IdCompra from @TmpCompras where posicion =@i

	 execute dbo.DGP_Insertar_Compra_Final @intIdCompra, null ;

	 set @i = @i + 1 ;

	end;

END
go
/*Anular amortizacion compra*/
CREATE PROCEDURE [dbo].[DGP_AnularAmortizacionesCompra] (
@IdCompra	INT
,@idUsuario	INT
) AS

BEGIN
	SET NOCOUNT OFF
	update  a set
	a.IdEstado = dbo.DGP_COMPRA_ESTADO_ANULADO()
	, a.FechaModificacion = getdate()
    , a.UsuarioModificacion = @idUsuario
	from [Tb_Amort_Compra] a 
	WHERE a.IdCompra = @IdCompra
	and a.IdEstado <>dbo.DGP_COMPRA_ESTADO_ANULADO()
END
GO

/*AMORTIZACIONES SIN APLICAR*/
CREATE PROCEDURE [dbo].[DGP_ObtenerAmortizacionCompraSinAplicar] (
  @IdCliente int
)
AS

BEGIN

select sum(vi.vueltos )
from (
select 
		doc.Monto- (select isnull(sum(a.Monto),0) 
					from Tb_Amort_Compra  a 
					where a.IdDocumento = doc.IdDocumentoPagoCompra 
					and  a.IdEstado = dbo.DGP_COMPRA_ESTADO_REGISTRADO() 
					and a.IdTipoAmortizacion = dbo.DGP_TIPO_AMOR_AMORTIZACION()) as vueltos
		
from Tb_documentoPagoCompra doc  
where 1=1
AND doc.estado = dbo.DGP_COMPRA_ESTADO_REGISTRADO() and doc.IdCliente = @IdCliente
) as vi

END
GO

/*TOTAL DE AMORTIZACIONES COMPRA*/
CREATE FUNCTION [dbo].[DGP_Obtener_TotalAmortizacionesCompra] (
@intIdCompra	INT
) RETURNS DECIMAL(8,2)
AS
BEGIN
	DECLARE @decTotalAmortizacion DECIMAL(8,2);

	IF EXISTS (SELECT IdAmortCompra
		FROM dbo.Tb_Amort_Compra
		WHERE IdCompra = @intIdCompra
			AND IdEstado not in ( dbo.DGP_COMPRA_ESTADO_ANULADO() )
			)
		BEGIN
			SELECT @decTotalAmortizacion = isnull( SUM(Monto) , 0)
			FROM dbo.Tb_Amort_Compra
			WHERE IdCompra = @intIdCompra
				AND IdEstado not in ( dbo.DGP_COMPRA_ESTADO_ANULADO() )
		END
	ELSE
		BEGIN
			SET @decTotalAmortizacion = 0;
		END

	RETURN @decTotalAmortizacion;
END
GO

/*INSERTAR DOCUMENTO PAGO COMPRA*/
CREATE PROCEDURE [dbo].[DGP_Insertar_DocumentoPagoCompra](

@IdTipoDocumento varchar(10)

,@Fecha datetime

,@Monto decimal(18,2)

,@Usuario int

,@IdCaja int

,@IdCliente int

,@IdPersonal int
)

AS

BEGIN
	INSERT INTO [Tb_documentoPagoCompra]

           ([IdTipoDocumento]

           ,[Fecha]

           ,[Monto]

           ,[estado]

           ,[FechaCreacion]

		   ,[FechaModificacion]

		   ,[idCliente]

		   ,[IdPersonal]

		   ,[IdCaja]
)
     VALUES
           (
		    @IdTipoDocumento 

		   ,@Fecha 

		   ,@Monto 

		   ,'REG'

		   ,getdate()

		   ,null

		   ,@idCliente

		   ,@IdPersonal

		   ,@IdCaja
		   )

		   SELECT SCOPE_IDENTITY();

END
GO

/*INSERTAR COMPRA FINAL*/
CREATE PROCEDURE [dbo].[DGP_Insertar_Compra_Final] (
@intIdCompra		INT
,@decPrecioCompra	decimal(8,2) = null
,@IdUsuario int  = 1
)
AS

DECLARE @varEstadoRegistrado	VARCHAR(5);
DECLARE @varEsDevolucion	CHAR(1);
DECLARE @decIGV			DECIMAL(8,2);
DECLARE @decTotalJabas		DECIMAL(8,2);
DECLARE @decPrecio		DECIMAL(8,2);
DECLARE @decTotalPesoBruto	DECIMAL(8,2);
DECLARE @decTotalPesoTara	DECIMAL(8,2);
DECLARE @decTotalPesoNeto	DECIMAL(8,2);
DECLARE @decMontoSubTotal	DECIMAL(8,2);
DECLARE @decMontoIGV		DECIMAL(8,2);
DECLARE @decMontoTotal		DECIMAL(8,2);
DECLARE @decTotalDevolucion	DECIMAL(8,2);
DECLARE @decTotalAmortizacion	DECIMAL(8,2);
DECLARE @intTotalUnidades	INT;
declare @decTotalDevolucionUnidades int;
declare @PrecioCompraBD		decimal(8,2)
BEGIN
	SET NOCOUNT OFF
	/** Establecer valores a los parametrods generales **/
	SET @varEstadoRegistrado =dbo.DGP_COMPRA_ESTADO_REGISTRADO() ; --// Valor de dbo.Tb_ParametroDetalle

	SELECT @decIGV = CONVERT(DECIMAL(8,2), VALOR) --// Valor de dbo.Tb_ParametroDetalle
	FROM dbo.Tb_ParametroDetalle
	WHERE Id_Parametro = 6;

	SELECT @decPrecio = isnull ( @decPrecioCompra , Precio ) , @PrecioCompraBD = Precio  --// Valor de dbo.Tbcompra
	FROM dbo.TbCompra
	WHERE IdCompra = @intIdCompra;

	/** Obtener los Pesos de la Linea de Compras **/
	SET @varEsDevolucion = 'N';
	SELECT 	
		@decTotalPesoBruto = ISNULL(SUM(PesoBruto), 0)
		,@decTotalJabas = ISNULL(SUM(CantidadJavas), 0)
		,@decTotalPesoTara = ISNULL(SUM(PesoTara), 0)
		,@decTotalPesoNeto = ISNULL(SUM(PesoNeto), 0)
		,@intTotalUnidades = isnull(sum(unidades), 0 )
	FROM dbo.TbLineaCompra
	WHERE IdEstado = @varEstadoRegistrado
		AND EsDevolucion = @varEsDevolucion
		AND IdCompra = @intIdCompra;

	/** Validar si tiene Devolucion **/
	DECLARE @chrTieneDevolucion CHAR(1);
	SET @decTotalDevolucion = 0;
	SET @decTotalDevolucionUnidades = 0;
	SET @chrTieneDevolucion = 'N';
	SET @varEsDevolucion = 'S';

	IF EXISTS (SELECT IdLineaCompra
			FROM dbo.TbLineaCompra
			WHERE IdEstado = @varEstadoRegistrado
				AND EsDevolucion = @varEsDevolucion
				AND IdCompra = @intIdCompra
		)
		BEGIN
			SET @chrTieneDevolucion = 'S';
			SELECT @decTotalDevolucion = ISNULL(SUM(PesoNeto), 0)
				 , @decTotalDevolucionUnidades =  ISNULL(SUM(Unidades), 0)
			FROM dbo.TbLineaCompra
			WHERE IdEstado = @varEstadoRegistrado
				AND EsDevolucion = @varEsDevolucion
				AND IdCompra = @intIdCompra;
		END;

	/** Establecer los Montos **/
	SET @decMontoTotal = ((@decTotalPesoNeto - @decTotalDevolucion) * @decPrecio);
	SET @decMontoSubTotal = (@decMontoTotal/(1 + @decIGV));
	SET @decMontoIGV = (@decMontoSubTotal * @decIGV);
	/*si hay cambio de precio anulamos amortizaciones*/
	if @PrecioCompraBD <>@decPrecioCompra
	begin
		exec [dbo].[DGP_AnularAmortizacionesCompra] @intIdCompra,@idUsuario
	end;
	/** Obtener el Total de Amortizaciones **/
	SET @decTotalAmortizacion = 0;
	SET @decTotalAmortizacion = dbo.DGP_Obtener_TotalAmortizacionesCompra(@intIdCompra);
	print cast(@decTotalAmortizacion as varchar(100))
	/** Actualizar la Compra **/
	UPDATE dbo.TbCompra SET
		Precio	= @decPrecio
		,TotalJabas = @decTotalJabas
		,TotalPeso_Bruto = @decTotalPesoBruto
		,TotalPeso_Tara = @decTotalPesoTara
		,TotalPeso_Neto = (@decTotalPesoNeto - @decTotalDevolucion)
		,MontoSubTotal = @decMontoSubTotal --// Importe
		,MontoIgv = @decMontoIGV
		,MontoTotal = @decMontoTotal 
		,TotalDevolucion = @decTotalDevolucion
		,TotalAmortizacion = @decTotalAmortizacion 
		,TotalSaldo = (@decMontoTotal - @decTotalAmortizacion)
		,IdEstado = case (@decMontoTotal - @decTotalAmortizacion) 
			   	 when  0 then dbo.DGP_COMPRA_ESTADO_CANCELADO()
			    	  else dbo.DGP_COMPRA_ESTADO_REGISTRADO()
			    end
		,TotalUnidades = @intTotalUnidades - @decTotalDevolucionUnidades
	WHERE IdCompra = @intIdCompra;

END
GO

/*REAPLICAR AMORTIZACIONES*/
CREATE PROCEDURE [dbo].[DGP_ReAplicar_AmortizacionesCompra] (
  @IdCliente int
  , @idUsuario INT 
  , @IdCaja int
)

AS

BEGIN

declare @IdDocumento int	
declare @bOK bit 

set @bOK = 1

DECLARE @TmpCompra TABLE ( posicion int identity(1,1), IdCompra INT , monto decimal (18,2), AmortizacionesAcumulado decimal (18,2) , IdCliente int)

DECLARE @TmpDocumentos TABLE ( posicion int identity(1,1), IdDocumento INT , monto decimal (18,2) , acumulado decimal (18,2) , fecha datetime , IdCliente int , IdPersonal int)

print 'Insertando compras' 

insert into @TmpCompra(IdCompra, monto , AmortizacionesAcumulado , IdCliente)
select  C.IdCompra , C.TotalSaldo , 0 , C.IdCliente
from dbo.TbCompra C
inner join dbo.Tb_Caja caj on caj.Id_Caja = C.IdCaja
where C.IdEstado = dbo.DGP_COMPRA_ESTADO_REGISTRADO()
and C.IdCliente = @IdCliente

order by caj.Fecha , C.TotalSaldo

print 'Insertando @TmpDocumentos' 

insert into @TmpDocumentos(IdDocumento, monto, acumulado, fecha, IdCliente, IdPersonal)
select doc.IdDocumentoPagoCompra,  
		doc.Monto - (select isnull(sum(a.Monto),0) 
			from Tb_Amort_Compra  A 
			where A.IdDocumento = doc.IdDocumentoPagoCompra
			and  A.IdEstado = dbo.DGP_COMPRA_ESTADO_REGISTRADO()
			)  
		, 0
		, doc.Fecha
		, doc.idCliente
		, doc.IdPersonal
from Tb_documentoPagoCompra doc  
where 1=1
AND doc.estado = 'REG' and doc.IdCliente = @IdCliente
order by doc.Fecha, doc.Monto 

--
--volver a crear amortizaciones en base a los documentos
--

declare @i int = 1
declare @j int =1
declare @maxCompra int = 0
declare @maxDocumentos int =0
select @maxCompra = max(posicion) from @TmpCompra

select @maxDocumentos = max(posicion) from @TmpDocumentos

print 'insertando documento' 

while (@i <= @maxDocumentos)
begin
		declare @tmpIdDocumento int
		declare @tmpMontoDocumento decimal(18,2)
		declare @tmpAmortizacionesAcumuladoDocumento decimal(18,2)
		declare @tmpFechaDocumento date
		declare @TmpDocumentoIdPersonal int

		select @tmpIdDocumento = IdDocumento
				,@tmpMontoDocumento = monto
				,@TmpDocumentoIdPersonal = IdPersonal
				,@tmpFechaDocumento = fecha
		from @TmpDocumentos WHERE posicion = @i

		set @j = 1
		set @tmpAmortizacionesAcumuladoDocumento = 0

		print 'while (@j <= @maxCompra AND @tmpAmortizacionesAcumuladoDocumento < @tmpMontoDocumento )' 

		while (@j <= @maxCompra AND @tmpAmortizacionesAcumuladoDocumento < @tmpMontoDocumento )

		begin
			declare @tmpIdCompra int
			declare @tmpMontoCompra decimal(18,2)
			declare @tmpMontoCompraAmortizar decimal(18,2)

			select @tmpIdCompra = IdCompra
					,@tmpMontoCompra = monto-AmortizacionesAcumulado
			from @TmpCompra   WHERE posicion = @j

			set @tmpMontoCompraAmortizar = @tmpMontoDocumento - @tmpAmortizacionesAcumuladoDocumento

			set @tmpMontoCompraAmortizar = iif(@tmpMontoCompraAmortizar <= @tmpMontoCompra,@tmpMontoCompraAmortizar, @tmpMontoCompra)

			print '  @tmpMontoCompraAmortizar' 

			if @tmpMontoCompraAmortizar> 0 

			begin 

				INSERT INTO [Tb_Amort_Compra]
						([Monto]
						,[NumeroDocumento]
						,[IdFormaPago]
						,[IdTipoAmortizacion]
						,[Observacion]
						,[IdEstado]
						,[IdCompra]
						,[IdCliente]
						,[IdPersonal]
						,[UsuarioModificacion]
						,[FechaModificacion]
						,[idCaja]
						,[IdDocumento])
				VALUES
				(@tmpMontoCompraAmortizar
				,@tmpIdDocumento
				,'EFE'
				,dbo.DGP_TIPO_AMOR_AMORTIZACION()
				,'recalculado'
				,dbo.DGP_COMPRA_ESTADO_REGISTRADO()
				,@tmpIdCompra
				,@IdCliente
				,@TmpDocumentoIdPersonal
				,@idUsuario
				,GETDATE()
				,@IdCaja
				,@tmpIdDocumento)

				UPDATE @TmpCompra set
				AmortizacionesAcumulado = AmortizacionesAcumulado +  @tmpMontoCompraAmortizar
				where posicion = @j

			   set @tmpAmortizacionesAcumuladoDocumento = @tmpAmortizacionesAcumuladoDocumento + @tmpMontoCompraAmortizar

		   end

		   set @j=@j+1

		end

		set @i=@i+1

end

-- actualizar las compras

set @i  = 1

while (@i <= @maxCompra)

begin

select @tmpIdCompra = IdCompra  ,@tmpMontoCompra = monto-AmortizacionesAcumulado
from @TmpCompra WHERE posicion = @i

exec [dbo].[DGP_Insertar_Compra_Final] @tmpIdCompra

set @i =@i +1

end;
END

