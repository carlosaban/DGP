

ALTER    PROCEDURE [dbo].[DGP_EliminarAmortizacion] (

@intIdAmortizacion	INT  ,

@intIdUsuario	INT 

) AS

/*

**************************************************

PROCEDIMIENTO	: dbo.DGP_Eliminar_LineaVenta

FECHA CREACION	: 11/04/2009 (dd/MM/yyyy

AUTOR		: Alexander Macuri

**************************************************

*/

declare @intIdVenta int;

declare @IdDocumento int;



DECLARE @TmpVentas TABLE ( posicion int identity(1,1), IdVenta INT )



BEGIN





	select @intIdVenta = a.Id_Venta 

			, @IdDocumento = a.IdDocumento

    from dbo.Tb_Amort_Venta a

    inner join dbo.Tb_Venta v on a.id_venta = v.id_venta

	where Id_Amort_Venta =@intIdAmortizacion;

	

	insert  into @TmpVentas

	select a.Id_Venta

	from [Tb_Amort_Venta] a

	WHERE    IdDocumento = @IdDocumento

	and idEstado = dbo.DGP_VENTA_ESTADO_REGISTRADO()



	UPDATE [Tb_Amort_Venta]

	SET 

			 idEstado = dbo.DGP_VENTA_ESTADO_ANULADO() ,

			 UsuarioModificacion = @intIdUsuario ,

			 FechaModificacion = getdate()

	WHERE    IdDocumento = @IdDocumento

	and idEstado = dbo.DGP_VENTA_ESTADO_REGISTRADO()



	update dbo.Tb_documento set

		estado = dbo.DGP_VENTA_ESTADO_ANULADO()

		, UsuarioEliminacion = @intIdUsuario

		, FechaEliminacion = GETDATE()

	where IdDocumento = @IdDocumento



	declare @maxVentas int ;

	declare @i int;

	set @i =1;



	

	select @maxVentas =COUNT(*) from @TmpVentas



	while (@i <=@maxVentas ) 

	begin

	 select @intIdVenta = IdVenta from @TmpVentas where posicion =@i

	 execute dbo.DGP_Insertar_Venta_Final @intIdVenta, null ;

	 set @i = @i + 1 ;

	end;

END;














