USE [DVGP_CITAVAL]
GO

/****** Object:  StoredProcedure [dbo].[DGP_Rpt_Estado_CuentaCliente_V2]    Script Date: 23/11/2017 10:31:49 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



--ALTER PROCEDURE [dbo].[DGP_Rpt_Estado_CuentaCliente_V2] (    

declare  @fechaInicio Datetime --,    

declare  @clientes varchar(1000) --= null

set @fechaInicio = '2017-11-22'

set @clientes = '136'

--)    

--AS    

/*    

*****************************************************    

PROCEDIMIENTO  : dbo.DGP_Rpt_EstadoCuentaCliente    

FECHA   : 01/06/2017 (dd/MM/yyyy)    

AUTOR   : Carlos Abanto G    

*****************************************************    

*/    

    

BEGIN  



SELECT * FROM (

  select 

		  1   as orden

		,  null as FECHA  

		, cli.Nombres as CLIENTE 

		, 'Saldo inicial' as PRODUCTO

		, null as IDESTADO 

		, null as TOTAL_PESO_NETO 

		, null  as  PRECIO

		

		, IIF( sum (v.Total_Saldo ) > 0,sum (v.Total_Saldo )  , NULL) as MONTO_VENTA

		, IIF( sum (v.Total_Saldo ) > 0 , NULL , sum (v.Total_Saldo )) as AMORTIZACION

		, null as observacion
		, null as fechaVenta
		, null as id_venta
  from dbo.Tb_Venta v   
  inner join dbo.Tb_Caja c on c.Id_Caja = v.Id_Caja  
  inner join dbo.Tb_Producto p on p.Id_Producto =v.Id_Producto  
  inner join dbo.Tb_Personal per on per.Id_Personal = v.Id_Personal  
  inner join dbo.Tb_Cliente_Proveedor cli on cli.Id_Cliente = v.Id_Cliente  
  where v.IdEstado  in ('REG' , 'CON')  
  and  v.Id_Cliente in (select * from dbo.Split(@clientes) )  
  and c.Fecha < @fechaInicio  
  group by   cli.Nombres 
  union all  

  select  2 as ORDEN
		

		 , c.FECHA 

		 , cli.Nombres as CLIENTE

		 , p.Nombre as PRODUCTO  

		 , v.IdEstado 

		 , v.TOTAL_PESO_NETO 

		 , v.PRECIO 

		 , v.Monto_Total as MONTO_venta 

		 , NULL AS AMORTIZACION

		 , NULL OBSERVACION
		 , null as fechaVenta
		 , v.id_venta
  from dbo.Tb_Venta v   

  inner join dbo.Tb_Caja c on c.Id_Caja = v.Id_Caja  

  inner join dbo.Tb_Producto p on p.Id_Producto =v.Id_Producto  

  inner join dbo.Tb_Personal per on per.Id_Personal = v.Id_Personal  

  inner join dbo.Tb_Cliente_Proveedor cli on cli.Id_Cliente = v.Id_Cliente  

  where v.IdEstado  not in ('ANL')  

  and  v.Id_Cliente in (select * from dbo.Split(@clientes) )  

  and c.Fecha >= @fechaInicio  

  union all

  --  Amortizacion

  select 2 as ORDEN 

	  , a.FechaPago as FECHA		

	  , cli.Nombres as CLIENTE

	  , null as PRODUCTO  

	  , A.IdTipoAmortizacion as IdEstado

	  , null as PRECIO 

	  , null as TOTAL_PESO_NETO 

	  , NULL AS MONTO_VENTA 

	  , a.Monto*-1 AS AMORTIZACION

	  , NULL OBSERVACION  
	  , c.Fecha as fechaVenta
	  , v.id_venta
  from dbo.Tb_Amort_Venta a   

  inner join  Tb_Venta v on v.Id_Venta = a.Id_Venta  

  inner join dbo.Tb_Caja c on c.Id_Caja = v.Id_Caja  

  inner join dbo.Tb_Producto p on p.Id_Producto =v.Id_Producto  

  inner join dbo.Tb_Personal per on per.Id_Personal = a.Id_Personal  

  inner join dbo.Tb_Cliente_Proveedor cli on cli.Id_Cliente = v.Id_Cliente  

  where a.IdEstado  not in ('ANL')  

  and v.IdEstado  not in ('ANL')  

  and  a.IdTipoAmortizacion in ( 'AMR')

  and  a.Id_Cliente in (select * from dbo.Split(@clientes) )  
  and c.fecha>= @fechaInicio
  --and (a.FechaPago >= @fechaInicio  or c.fecha>= @fechaInicio)

  --group by A.IdTipoAmortizacion,  a.FechaPago  , cli.Nombres
  union all

   select 2 as ORDEN 

	  , a.FechaPago as FECHA		

	  , cli.Nombres as CLIENTE

	  , null as PRODUCTO  

	  , A.IdTipoAmortizacion as IdEstado

	  , null as PRECIO 

	  , null as TOTAL_PESO_NETO 

	  , NULL AS MONTO_VENTA 

	  , a.Monto AS AMORTIZACION

	  , NULL OBSERVACION  
	  , c.Fecha as fechaVenta
	  , v.id_venta
  from dbo.Tb_Amort_Venta a   

  inner join  Tb_Venta v on v.Id_Venta = a.Id_Venta  

  inner join dbo.Tb_Caja c on c.Id_Caja = v.Id_Caja  

  inner join dbo.Tb_Producto p on p.Id_Producto =v.Id_Producto  

  inner join dbo.Tb_Personal per on per.Id_Personal = a.Id_Personal  

  inner join dbo.Tb_Cliente_Proveedor cli on cli.Id_Cliente = v.Id_Cliente  
  where 1=1 
  and a.IdEstado  not in ('ANL')  
  and a.Monto < 0
  and v.IdEstado  not in ('ANL')  
  and  a.IdTipoAmortizacion in ( 'VLT')
  and  a.Id_Cliente in (select * from dbo.Split(@clientes) )  
  and not exists (select Id_Amort_Venta 
								from Tb_Amort_Venta am
								inner join Tb_Venta ven on ven.Id_Venta = am.Id_Venta
								inner join Tb_Caja caj on caj.Id_Caja = ven.Id_Caja
								where  am.Monto>0 and a.IdDocumento = am.IdDocumento and caj.fecha <@fechaInicio )
  and a.FechaPago >= @fechaInicio  
  and c.Fecha < @fechaInicio

  --group by A.IdTipoAmortizacion,  a.FechaPago  , cli.Nombres

  ) AS VIEW_ESTADO_CUENTA

  ORDER BY FECHA, orden

  END;


