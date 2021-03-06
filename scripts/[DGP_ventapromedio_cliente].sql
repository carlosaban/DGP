USE [DVGP_CITAVAL]
GO
/****** Object:  UserDefinedFunction [dbo].[DGP_Obtener_Saldo Inicial]    Script Date: 08/05/2019 12:01:32 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




alter       FUNCTION [dbo].[DGP_ventapromedio_cliente] (
@IdCliente INT
) RETURNS DECIMAL(18,2)
AS
/*
**************************************************
FUNCION		: dbo.[DGP_Obtener_ventapromedio]
**************************************************
*/
BEGIN
	--declare @IdCliente INT
	--set @IdCliente = 753

	declare @decTotalAmortizacion DECIMAL(18,2)

	select @decTotalAmortizacion =
	isnull ( (
			select avg(venta)
			from (
	
					select cj.fecha,  sum(v.Monto_Total)  AS VENTA
					from [dbo].[Tb_Venta] v 
					inner join Tb_Caja cj on v.Id_Caja = cj.id_caja
					where IdEstado <>'ANL' and v.Id_Cliente = @IdCliente 
					group by cj.fecha 
			 ) AS T
	 
	 ) , 0 ) 
	
	--select  @decTotalAmortizacion;
	RETURN @decTotalAmortizacion;
END