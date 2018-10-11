USE [DVGP_CITAVAL]
GO
/****** Object:  UserDefinedFunction [dbo].[DGP_monto_amortizacion]    Script Date: 01/10/2018 10:01:47 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  
  
  
create      FUNCTION [dbo].[DGP_DIFFSALDOS] (  
	@IdCliente INT  
,	@Fecha date
   
) RETURNS decimal(18,2)  
AS  

BEGIN  
DECLARE @saldo decimal(18,2)
DECLARE @saldoAnterior decimal(18,2)

set @saldo = isnull ( (select sum(v.Monto_Total) 
				  from [dbo].[Tb_Venta] v 
				  inner join Tb_Caja cj on cj.Id_Caja = v.Id_Caja
				  where IdEstado <>'ANL' and v.Id_Cliente = @IdCliente  and cj.Fecha <= @Fecha
				) , 0 ) - 
	isnull ( (select sum(d.Monto) 
			  from [dbo].[Tb_documento] d 
			  where IdEstado <>'ANL' and d.IdCliente = @IdCliente and Fecha <= @Fecha
				) , 0 )

		
set @saldoAnterior = isnull ( (select sum(v.Monto_Total) 
				  from [dbo].[Tb_Venta] v 
				  inner join Tb_Caja cj on cj.Id_Caja = v.Id_Caja
				  where IdEstado <>'ANL' and v.Id_Cliente = @IdCliente  and cj.Fecha <= DATEADD(DAY, -1, @Fecha) 
				) , 0 ) - 
	isnull ( (select sum(d.Monto) 
			  from [dbo].[Tb_documento] d 
			  where IdEstado <>'ANL' and d.IdCliente = @IdCliente and Fecha <= DATEADD(DAY, -1, @Fecha) 
				) , 0 )

 RETURN   @saldo - @saldoAnterior
END  
  
  
  
  
  