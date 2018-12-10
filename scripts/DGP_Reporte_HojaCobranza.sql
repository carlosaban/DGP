USE [DVGP_CITAVAL]
GO
/****** Object:  StoredProcedure [dbo].[DGP_Reporte_Saldos]    Script Date: 21/09/2018 01:44:16 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

alter procedure [dbo].DGP_Reporte_HojaCobranzaV2 AS 
WITH ClienteInfo ( zona, Id_cliente, Nombres, EsClienteCongelado, ultimaVenta, FechaCobranza , SALDO) AS   
(  
    select iif ( EsClienteCongelado = 1 , 'Congelado', z.descripcion) as zona  ,  cli.Id_cliente , cli.Nombres , cli.EsClienteCongelado 
		 
		, ( select max(cj.Fecha) 
			from  [dbo].[Tb_Venta] v  
			inner join Tb_Caja cj on cj.Id_Caja = v.Id_Caja
			where v.Id_Cliente = cli.Id_Cliente and v.IdEstado<>'ANL' 
		  ) as [ultimaVenta]
		, ( select max(Fecha) -3 
			from  (
					select  cj.fecha as fecha 
					from [dbo].[Tb_Venta] v  
					inner join Tb_Caja cj on cj.Id_Caja = v.Id_Caja
					where v.Id_Cliente = cli.Id_Cliente and v.IdEstado<>'ANL'
					union all
					select fecha 
					from Tb_documento d 
					where d.IdCliente = cli.Id_Cliente and d.IdEstado <> 'ANL'
					) as fechas
			 
		  ) as [FechaCobranza]
		,isnull ( (select sum(v.Monto_Total) 
			   from [dbo].[Tb_Venta] v 
			   inner join Tb_Caja cj on v.Id_Caja = cj.id_caja
		where IdEstado <>'ANL' and v.Id_Cliente = CLI.Id_cliente 
	 ) , 0 ) -
	isnull ( (select sum(d.Monto) from [dbo].[Tb_documento] d 
		where IdEstado <>'ANL' and d.IdCliente = CLI.Id_cliente
		) , 0 )		 AS SALDO
  from   [Tb_Cliente_Proveedor] as cli 
  inner join Tb_Zona z on z.Id_Zona = cli.Id_Zona
   
)  
select 0 as orden
     
	 , zona
     ,  Id_cliente, Nombres, EsClienteCongelado, ultimaVenta, FechaCobranza, 0 as venta , 0 as pagos ,
	 isnull ( (select sum(v.Monto_Total) 
			   from [dbo].[Tb_Venta] v 
			   inner join Tb_Caja cj on v.Id_Caja = cj.id_caja
		where IdEstado <>'ANL' and v.Id_Cliente = ClienteInfo.Id_cliente and cj.Fecha <=  ClienteInfo.FechaCobranza
	 ) , 0 ) -
	isnull ( (select sum(d.Monto) from [dbo].[Tb_documento] d 
		where IdEstado <>'ANL' and d.IdCliente = ClienteInfo.Id_cliente and  d.Fecha <=  ClienteInfo.FechaCobranza
		) , 0 ) as ACUMULADO
	, SALDO	
	,'' as Productos
from ClienteInfo
WHERE SALDO >0
union all
select NUMEROS.VALOR as orden
	 , zona
	 , Id_cliente, Nombres, EsClienteCongelado, ultimaVenta, DATEADD(DD, NUMEROS.VALOR,  ClienteInfo.FechaCobranza) AS FechaCobranza 
	 , isnull ( (select sum(v.Monto_Total) 
			   from [dbo].[Tb_Venta] v 
			   inner join Tb_Caja cj on v.Id_Caja = cj.id_caja
		where IdEstado <>'ANL' and v.Id_Cliente = ClienteInfo.Id_cliente and cj.Fecha = DATEADD(DD, NUMEROS.VALOR,  ClienteInfo.FechaCobranza) 
	 ) , 0 )  AS VENTA
	 

	, isnull ( (select sum(d.Monto) from [dbo].[Tb_documento] d 
		where IdEstado <>'ANL' and d.IdCliente = ClienteInfo.Id_cliente and  d.Fecha =  DATEADD(DD, NUMEROS.VALOR,  ClienteInfo.FechaCobranza)
		) , 0 ) as PAGOS
	, 0 as ACUMULADO
	, SALDO
	,( SELECT SUBSTRING(
						(
							SELECT distinct ',' + p.Nombre+ '(' + CAST(v.Total_Peso_Neto AS VARCHAR) + ')' 
							from [dbo].[Tb_Venta] v
							inner join Tb_Caja cj2 on v.Id_Caja = cj2.Id_Caja
							inner join [dbo].Tb_Producto p on p.Id_Producto = v.Id_Producto
							where v.IdEstado not in ('ANL')
							and cj2.Fecha  = DATEADD(DD, NUMEROS.VALOR,  ClienteInfo.FechaCobranza)
							and v.Id_Cliente = ClienteInfo.Id_cliente
							
							FOR XML PATH('')
						), 2,10000 
					)
	)     as Productos
from ClienteInfo
CROSS JOIN (
			 SELECT 1 AS VALOR
			 UNION ALL
			 SELECT 2
			 UNION ALL 
			 SELECT 3 
) AS NUMEROS
WHERE SALDO > 0 
ORDER BY Nombres , FechaCobranza


/*
select ClienteInfo.*, 
	 isnull ( (select sum(v.Monto_Total) 
			   from [dbo].[Tb_Venta] v 
			   inner join Tb_Caja cj on v.Id_Caja = cj.id_caja
		where IdEstado <>'ANL' and v.Id_Cliente = ClienteInfo.Id_cliente and cj.Fecha <  ClienteInfo.FechaCobranza
	 ) , 0 ) -
	isnull ( (select sum(d.Monto) from [dbo].[Tb_documento] d 
		where IdEstado <>'ANL' and d.IdCliente = ClienteInfo.Id_cliente and  d.Fecha <  ClienteInfo.FechaCobranza
		) , 0 ) as Monto

		, 0 as amortizacion
		

from (

  select cli.Id_cliente , cli.Nombres , cli.EsClienteCongelado
		 
		, ( select max(cj.Fecha) 
			from  [dbo].[Tb_Venta] v  
			inner join Tb_Caja cj on cj.Id_Caja = v.Id_Caja
			where v.Id_Cliente = cli.Id_Cliente and v.IdEstado<>'ANL' 
		  ) as [ultimaVenta]
		, ( select max(cj.Fecha) -3 
			from  [dbo].[Tb_Venta] v  
			inner join Tb_Caja cj on cj.Id_Caja = v.Id_Caja
			where v.Id_Cliente = cli.Id_Cliente and v.IdEstado<>'ANL' 
		  ) as [FechaCobranza]		
  from   [Tb_Cliente_Proveedor] as cli
  ) as ClienteInfo

union all
*/

