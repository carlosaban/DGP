USE [DVGP_CITAVAL]
GO
/****** Object:  StoredProcedure [dbo].[DGP_Listar_Amortizacion]    Script Date: 18/08/2018 07:10:35 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




         ALTER             PROCEDURE [dbo].[DGP_Listar_Amortizacion] (  

@intIdVenta INT = NULL  

,@intIdCliente INT = NULL  

,@intIdProducto INT = NULL  

, @intIncluCanceldos INT = 0  

) AS  

/*  

**************************************************  

PROCEDIMIENTO : dbo.DGP_Listar_Amortizacion  

FECHA CREACION : 22/03/2009 (dd/MM/yyyy)  

AUTOR  : Alexander Macuri  

**************************************************  

*/  

BEGIN  

 DECLARE @VentaAmortizacion TABLE(  

  intIdAmortizacion       INT  

  , intIdVenta  INT  

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

  , varEstado                     varchar(3)  

  ,fechaCreacion   datetime null  
  , DocumentoPagoInfo varchar(3000)
 );  

  

 /** Obtener las Ventas **/  

 INSERT INTO @VentaAmortizacion  

 SELECT   

   [intIdAmortizacion] = 0  

  ,[intIdVenta] = V.Id_Venta  

  ,[intIdCliente] = V.Id_Cliente  

  ,[intIdProducto] = P.Id_Producto  

  ,[varTipoDocumento] = 'Venta'  

  ,[varProducto] = P.Nombre  

  ,[datFecha] = CJ.FECHA --CONVERT(VARCHAR, CJ.FECHA, 103)  

  ,[intCantidadJavas] = dbo.DGP_Obtener_CantidadJavas(V.Id_Venta)  

  ,[decPesoNeto] = V.Total_Peso_Neto  

  ,[decImporte] = V.Monto_Total  

  ,[decSaldo] = V.Total_Saldo  

  ,[intIndicador] = '1'  

  ,[intIdPersonal] = cj.Id_Personal  

  ,[varPesonal] = per.Nombre  

  ,[varEstado] = v.idEstado  

  , [fechaCreacion] = v.fechaCreacion  
  ,  ( SELECT SUBSTRING(
						(
							SELECT distinct ',' + convert (varchar(10), pago.Fecha , 103) + '(' + CAST(pago.Monto AS VARCHAR) + ')' 
							from Tb_Amort_Venta am
							inner join [dbo].[Tb_documento] pago on pago.IdDocumento = am.IdDocumento 
							
							where 1=1
							and am.Id_Venta = v.Id_Venta
							and pago.IdEstado not in ('ANL')
							-- CONVERT (date , CJ.Fecha, 103)
							FOR XML PATH('')
						), 2,10000 
					)
	) as DocumentoPagoInfo
 FROM dbo.Tb_Venta AS V  

  INNER JOIN DBO.TB_CAJA AS CJ ON CJ.ID_CAJA = V.ID_CAJA  

  INNER JOIN dbo.Tb_Producto AS P ON (V.Id_Producto = P.Id_Producto)  

  left join dbo.Tb_Personal  as per on per.Id_Personal = cj.Id_Personal  

 WHERE 1=1  

   and V.IdEstado != DBO.DGP_VENTA_ESTADO_cancelado()  

  and V.IdEstado != dbo.DGP_VENTA_ESTADO_ANULADO()  

  AND V.Id_Venta = ISNULL(@intIdVenta, V.Id_Venta)  

  AND ISNULL(V.Id_Cliente, -1) = ISNULL(@intIdCliente, ISNULL(V.Id_Cliente, -1))  

  AND V.Id_Producto = ISNULL(@intIdProducto, V.Id_Producto);  

  

 /** Obtener las Amortizaciones **/  

 INSERT INTO @VentaAmortizacion  

 SELECT   

   [intIdAmortizacion] = null--AV.Id_Amort_Venta  

  ,[intIdVenta] = AV.Id_Venta  

  ,[intIdCliente] = VE.Id_Cliente  

  ,[intIdProducto] = PR.Id_Producto  

  ,[varTipoDocumento] = 'Amortización'  

  ,[varProducto] = PR.Nombre  

  ,[datFecha] =CJ.Fecha --AV.fechaPago -- CONVERT(VARCHAR, AV.fechaPago, 103)  

  ,[intCantidadJavas] = NULL  

  ,[decPesoNeto] = NULL  

  ,[decImporte] = sum (AV.Monto  )

  ,[decSaldo] = NULL  

  ,[intIndicador] = '0'  

  ,[intIdPersonal] = null --AV.Id_Personal  

  ,[varPesonal] = null --per.Nombre  

  ,[varEstado] = null --AV.idEstado  

  , [fechaCreacion] = null  
  , DocumentoPagoInfo =''

 FROM dbo.Tb_Amort_Venta AS AV  

  INNER JOIN Tb_Venta AS VE ON (AV.Id_Venta = VE.Id_Venta AND VE.IdEstado != DBO.DGP_VENTA_ESTADO_ANULADO() )  

  INNER JOIN DBO.TB_CAJA AS CJ ON CJ.ID_CAJA = VE.ID_CAJA  

  INNER JOIN dbo.Tb_Producto AS PR ON (VE.Id_Producto = PR.Id_Producto)  

  left join dbo.Tb_Personal  as per on per.Id_Personal = AV.Id_Personal  

 WHERE  1=1  

  and  VE.IdEstado != DBO.DGP_VENTA_ESTADO_cancelado()  

  AND AV.IdEstado = dbo.DGP_VENTA_ESTADO_REGISTRADO()  

  AND AV.Id_Venta = ISNULL(@intIdVenta, AV.Id_Venta)  

  AND ISNULL(AV.Id_Cliente, -1) = ISNULL(@intIdCliente, ISNULL(AV.Id_Cliente, -1))  

  AND PR.Id_Producto = ISNULL(@intIdProducto, PR.Id_Producto)  

  group by AV.Id_Venta ,CJ.Fecha, VE.Id_Cliente, PR.Id_Producto, PR.Nombre  
  ;
  

 SELECT * FROM @VentaAmortizacion  

 ORDER BY [datFecha] ASC   , [intIdVenta] ASC ,   varTipoDocumento DESC ,fechaCreacion , decSaldo  ;  

  

END  
