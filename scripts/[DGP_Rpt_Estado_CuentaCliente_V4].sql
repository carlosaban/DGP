USE [DVGP_CITAVAL]
GO
/****** Object:  StoredProcedure [dbo].[DGP_Rpt_Estado_CuentaCliente_V4]    Script Date: 17/05/2019 06:52:31 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[DGP_Rpt_Estado_CuentaCliente_V4] (    

	@fechaInicio Datetime     

	,@clientes varchar(1000)  = null
	,@TienePermisoInfoCompra bit = 0
)   
AS  


/* 

*****************************************************    
PROCEDIMIENTO  : dbo.DGP_Rpt_EstadoCuentaCliente    
FECHA   : 01/06/2017 (dd/MM/yyyy)    
AUTOR   : Carlos Abanto G    
***************************************************** 
-- declare @fechaInicio Datetime     
--declare @clientes varchar(1000) -- = null
 --set @fechaInicio = '2017-10-12'
 --set @clientes = '99'      \

*/    

BEGIN  



SELECT * FROM (


  select 
		  1   as orden
		,  null as FECHA  
		, cli.Nombres as CLIENTE
		, 'Saldo Inicial' as PRODUCTO
		, null as IDESTADO
		, null as PRECIO
		, null  as TOTAL_PESO_NETO 
		,IIF( dbo.[DGP_Obtener_Saldo Inicial] (CLI.Id_Cliente , @fechaInicio ) > 0 , dbo.[DGP_Obtener_Saldo Inicial] (CLI.Id_Cliente , @fechaInicio )  , NULL) as MONTO_VENTA
		, IIF( dbo.[DGP_Obtener_Saldo Inicial] (CLI.Id_Cliente , @fechaInicio ) > 0 , NULL , dbo.[DGP_Obtener_Saldo Inicial] (CLI.Id_Cliente , @fechaInicio ) ) as AMORTIZACION
		, null as observacion
		, '' as CompraInfo

  from  dbo.Tb_Cliente_Proveedor cli   
  where  CLI.Id_Cliente in (select * from dbo.Split(@clientes) )  

  union all
  select 3 as ORDEN 
	  , a.Fecha as FECHA		
	  , cli.Nombres as CLIENTE
	  , null as PRODUCTO  
	  , A.IdTipoDocumento as IdEstado
	  , null as PRECIO 
	  , null as TOTAL_PESO_NETO 
	  , NULL AS MONTO_VENTA 
	  , a.Monto*-1 AS AMORTIZACION
	  , substring(per.[Nombre], 1,6)+ '/' + isnull(a.NumeroOperacion,'')+'/' + isnull(a.IdBanco,'')+'/'+ isnull(a.NumeroRecibo , '')  + '/ obs:' + isnull(a.observacion, '')  as   OBSERVACION  
	  , '' as CompraInfo

  from dbo.tb_documento a   
  inner join dbo.Tb_Personal per on per.Id_Personal = a.IdPersonal 
  inner join dbo.Tb_Cliente_Proveedor cli on cli.Id_Cliente = a.IdCliente  
  where a.IdEstado = 'REG'
  and  a.IdTipoDocumento in ( 'AMR', 'NC')
  and  (a.[IdFormaPago] is null or a.[IdFormaPago] not in ('NCC') )
  
  and  a.IdCliente in (select * from dbo.Split(@clientes) )  
  and a.Fecha >= @fechaInicio
  union all 
  select  4 as ORDEN

		 , c.FECHA 
		 , cli.Nombres as CLIENTE
		 , p.Nombre as PRODUCTO  
		 , v.IdEstado 
		 , v.TOTAL_PESO_NETO 
		 , v.PRECIO 
		 , v.Monto_Total as MONTO_venta 
		 , NULL AS AMORTIZACION
		 , V.OBSERVACION AS OBSERVACION
		 ,IIF( @TienePermisoInfoCompra = 0  
				, '' 
				, 
				 ( SELECT SUBSTRING(
								(
									SELECT distinct ',' + Nombres+ '(' + CAST([Precio] AS VARCHAR) + ')' 
									from [dbo].[TbCompra] com
									inner join [dbo].[Tb_Cliente_Proveedor]  prov on prov.Id_Cliente = com.IdProveedor
									where [Fecha] = C.Fecha
									and com.IdEstado not in ('ANL')
									and prov.TipoCliente = 'PRO'
									-- CONVERT (date , CJ.Fecha, 103)
									FOR XML PATH('')
								), 2,10000 
							)
				 )
		 
		 ) as CompraInfo

  from dbo.Tb_Venta v   
  inner join dbo.Tb_Caja c on c.Id_Caja = v.Id_Caja  
  inner join dbo.Tb_Producto p on p.Id_Producto =v.Id_Producto  
  inner join dbo.Tb_Personal per on per.Id_Personal = v.Id_Personal  
  inner join dbo.Tb_Cliente_Proveedor cli on cli.Id_Cliente = v.Id_Cliente  
  where v.IdEstado  not in ('ANL')  
  and p.IdTipoProducto not in ( 9)
  and  v.Id_Cliente in (select * from dbo.Split(@clientes) )  
  and c.Fecha >= @fechaInicio  

  union all 
  select  5 as ORDEN

		 , v.FECHA 
		 , cli.Nombres as CLIENTE
		 , 'Compra ' + p.Nombre as PRODUCTO  
		 , v.IdEstado 
		 , v.TotalPeso_Neto as TOTAL_PESO_NETO 
		 , v.PRECIO 
		 , null as MONTO_venta 
		 , v.MontoTotal*-1 AS AMORTIZACION
		 , NULL OBSERVACION
		 ,IIF( @TienePermisoInfoCompra = 0  
				, '' 
				, 
				 ( SELECT SUBSTRING(
								(
									SELECT distinct ',' + Nombres+ '(' + CAST([Precio] AS VARCHAR) + ')' 
									from [dbo].[TbCompra] com
									inner join [dbo].[Tb_Cliente_Proveedor]  prov on prov.Id_Cliente = com.IdProveedor
									where [Fecha] = v.Fecha
									and prov.TipoCliente = 'PRO'
									and com.IdEstado not in ('ANL')
									-- CONVERT (date , CJ.Fecha, 103)
									FOR XML PATH('')
								), 2,10000 
							)
				 )
		 
		 ) as CompraInfo

  from dbo.TbCompra v   
  inner join dbo.Tb_Producto p on p.Id_Producto =v.IdProducto  
  inner join dbo.Tb_Personal per on per.Id_Personal = v.IdPersonal  
  inner join dbo.Tb_Cliente_Proveedor cli on cli.Id_Cliente = v.IdProveedor  
  where v.IdEstado  not in ('ANL')  
  and  v.IdProveedor in (select * from dbo.Split(@clientes) )  
  and v.Fecha >= @fechaInicio
union all
  select 6 as ORDEN 
	  , a.Fecha as FECHA		
	  , cli.Nombres as CLIENTE
	  , 'Amr. Compra' as PRODUCTO  
	  , A.IdTipoDocumento as IdEstado
	  , null as PRECIO 
	  , null as TOTAL_PESO_NETO 
	  , a.Monto AS MONTO_VENTA 
	  , NULL AS AMORTIZACION
	  , substring(per.[Nombre], 1,6)+ '/' + isnull(a.NumeroOperacion,'')+'/' + isnull(a.IdBanco,'')+'/'+ isnull(a.NumeroRecibo , '') + '/ obs:' + isnull(a.observacion, '')   as   OBSERVACION  
	  , '' as CompraInfo

  from dbo.tb_documentoCompra a   
  inner join dbo.Tb_Personal per on per.Id_Personal = a.IdPersonal 
  inner join dbo.Tb_Cliente_Proveedor cli on cli.Id_Cliente = a.IdCliente  
  where a.IdEstado = 'REG'
  and  a.IdTipoDocumento in ( 'AMR', 'NC')
  and  a.IdCliente in (select * from dbo.Split(@clientes) )  
  and a.Fecha >= @fechaInicio










  ) AS VIEW_ESTADO_CUENTA







  ORDER BY FECHA , orden







  END;








