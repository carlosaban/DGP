USE [DVGP_CITAVAL]
GO
/****** Object:  StoredProcedure [dbo].[DGP_Actualizar_Caja]    Script Date: 30/10/2018 11:19:20 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




create  PROCEDURE [dbo].[DGP_Actualizar_Cliente_Congelado] (
@intIdUsuario int

) AS
/*
**************************************************
PROCEDIMIENTO	: dbo.DGP_Actualizar_Caja
FECHA CREACION	: 03/03/2009 (dd/MM/yyyy)
AUTOR		: Alexander Macuri
**************************************************
*/

--declare @intIdUsuario int
--set @intIdUsuario = 1
BEGIN

declare @fechaLimite date
declare @primerdiaMes date 

set @primerdiaMes =  DATEADD(MONTH, -1, DATEADD(DAY, 1, EOMONTH(GETDATE())))
set @fechaLimite = cast( getdate() as date)

--if (@primerdiaMes = @fechaLimite)
	begin 

		UPDATE cliente SET
			EsClienteCongelado = 1
			,UsuarioModificacion = @intIdUsuario
			,FechaModificacion = GETDATE()
		from Tb_Cliente_Proveedor as cliente
		where cliente.estado = 1 
			  and cliente.esClienteCongelado = 0
				and NOT EXISTS (
				select 1 
				from Tb_Venta v 
				inner join Tb_Caja cj on cj.Id_Caja = v.Id_Caja			
				where cj.Fecha > DATEADD (dd,-30, @fechaLimite) and v.Id_Cliente = cliente.Id_Cliente
				and  v.idEstado <>'ANL'
				)
			-- descongelar
		
	UPDATE cliente SET
			EsClienteCongelado = 0
			,UsuarioModificacion = @intIdUsuario
			,FechaModificacion = GETDATE()
		from Tb_Cliente_Proveedor as cliente
		where cliente.estado = 1 
			  and cliente.esClienteCongelado = 1
				and EXISTS (
				select 1 
				from Tb_Venta v 
				inner join Tb_Caja cj on cj.Id_Caja = v.Id_Caja			
				where cj.Fecha > DATEADD (dd,-30, @fechaLimite) and v.Id_Cliente = cliente.Id_Cliente
				and  v.idEstado <>'ANL'
				)

	end

END



