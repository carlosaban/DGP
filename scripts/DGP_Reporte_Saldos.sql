USE [DVGP_CITAVAL]
GO
/****** Object:  StoredProcedure [dbo].[DGP_Reporte_Saldos]    Script Date: 21/09/2018 01:44:16 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--alter procedure [dbo].DGP_Reporte_Saldos AS 


  select Nombres 
		, VentaDia 
		, SaldoTotal - VentaDia as saldo  
		, SaldoTotal as total 
		,ultimaVenta
		, PeriodoDeuda
		, iif(  DATEDIFF( dd ,   ultimaVenta , getdate()   )  < 15 , 'S' , 'N')  as Rango 
		, dbo.[DGP_DIFFSALDOS] (Id_Cliente , ultimaVenta  -5) as [diffd4]
		, dbo.[DGP_DIFFSALDOS] (Id_Cliente , ultimaVenta  -4) as [diffd3]
		, dbo.[DGP_DIFFSALDOS] (Id_Cliente , ultimaVenta  -3) as [diffd2]
		, dbo.[DGP_DIFFSALDOS] (Id_Cliente ,  ultimaVenta  -2) as [diffd1]
		, dbo.[DGP_DIFFSALDOS] (Id_Cliente , ultimaVenta-1 ) as [diffd0]
		, dbo.[DGP_DIFFSALDOS] (Id_Cliente , ultimaVenta  -5)  +dbo.[DGP_DIFFSALDOS] (Id_Cliente , ultimaVenta  -4)+dbo.[DGP_DIFFSALDOS] (Id_Cliente , ultimaVenta  -3)+dbo.[DGP_DIFFSALDOS] (Id_Cliente , ultimaVenta  -2)+dbo.[DGP_DIFFSALDOS] (Id_Cliente , ultimaVenta -1) as IncrementoSaldo
  
  from (
  
  select cli.Id_cliente , cli.Nombres , cli.EsClienteCongelado,
	 isnull ( (select sum(v.Monto_Total) from [dbo].[Tb_Venta] v 
		where IdEstado <>'ANL' and v.Id_Cliente = cli.Id_cliente 
	 ) , 0 ) - 
	isnull ( (select sum(d.Monto) from [dbo].[Tb_documento] d 
		where IdEstado <>'ANL' and d.IdCliente = cli.Id_cliente
		) , 0 )
		
		 as [SaldoTotal]
		 
		 ,isnull( ( select sum([Monto_Total]) 
			from  [dbo].[Tb_Venta] v  
			inner join Tb_Caja cj on cj.Id_Caja = v.Id_Caja
			where v.Id_Cliente = cli.Id_Cliente and v.IdEstado<>'ANL' 
			and cj.Fecha = CONVERT(date , getdate() , 103) 
			) , 0 ) as [VentaDia]
		, ( select max(cj.Fecha) 
			from  [dbo].[Tb_Venta] v  
			inner join Tb_Caja cj on cj.Id_Caja = v.Id_Caja
			where v.Id_Cliente = cli.Id_Cliente and v.IdEstado<>'ANL' 
		  ) as [ultimaVenta]
		
		 , DATEDIFF( dd ,   ( select max(cj.Fecha) 
								from  [dbo].[Tb_Venta] v  
								inner join Tb_Caja cj on cj.Id_Caja = v.Id_Caja
								where v.Id_Cliente = cli.Id_Cliente and v.IdEstado<>'ANL' 
							  )		 
				   , getdate()   )  as PeriodoDeuda

  from   [Tb_Cliente_Proveedor] as cli
  ) as saldos
  where 1=1
  and [SaldoTotal] > 0 
  and saldos.EsClienteCongelado = 0
  order by iif(  DATEDIFF( dd ,   ultimaVenta , getdate()   )  < 15 , 'S' , 'N') desc, ultimaVenta desc, SaldoTotal desc  , Nombres

  