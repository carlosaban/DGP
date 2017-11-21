USE [DVGP_CITAVAL]
GO
/****** Object:  StoredProcedure [dbo].[DGP_Aplicar_Vueltos]    Script Date: 15/11/2017 11:14:04 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




ALTER    PROCEDURE [dbo].[DGP_Aplicar_Vueltos] (

  @idVentasVueltos VARCHAR(200)

, @aplicarVuelto BIT

, @idUsuario INT 

, @IdCaja int

, @idVentaSaldos VARCHAR(200)



) AS



BEGIN

declare @IdDocumento int	



DECLARE @TmpVentaSaldos TABLE ( posicion int, IdVenta INT)



INSERT INTO [dbo].[Tb_documento]

           ([IdTipoDocumento]

           ,[Fecha]

           ,[Monto]

           ,[estado]

           ,[EsEliminado]

           ,[FechaModificacion]

           ,[FechaCreacion]

           ,[UsuarioModificacion]

           ,[UsuarioCreacion])

select 

			'VLT'

           ,cast(floor(cast(getdate() as float)) as datetime)

           ,sum([Total_Saldo])

           ,1

           ,0

           ,getdate()

           ,getdate()

           ,@idUsuario

           ,@idUsuario

from  Tb_Venta v 
where v.[Id_Venta] in (SELECT value  FROM Split (@idVentasVueltos))

SELECT @IdDocumento = SCOPE_IDENTITY()





INSERT INTO [dbo].[Tb_Amort_Venta]

           ([Monto]

           ,[NumeroDocumento]

           ,[FechaPago]

           ,[IdTipoAmortizacion]

           ,[Observacion]

           ,[IdEstado]

           ,[Id_Venta]

           ,[Id_Cliente]

           ,[Id_Personal]

           ,[UsuarioCreacion]

           ,[FechaCreacion]

           ,[UsuarioModificacion]

           ,[FechaModificacion]

           ,[id_caja]

           ,[IdDocumento]

		   ,[IdFormaPago]

		   )
			select 

						[Total_Saldo]

						, CAST (@IdDocumento AS VARCHAR(100))

						, cast(floor(cast(getdate() as float)) as datetime)

						,'VLT'

					   , 'VUELTO'

					   , 'REG'

					   ,V.Id_Venta

					   ,V.Id_Cliente

					   ,@idUsuario

					   ,@idUsuario

					   ,getdate()

					   ,@idUsuario

					   ,getdate()

					   ,@IdCaja

					   ,@IdDocumento

					   ,'NEF'

			from  Tb_Venta v 

			where v.[Id_Venta] in (SELECT value  FROM Split (@idVentasVueltos))

-----------------Falta ingresar el vuelto

if  @aplicarVuelto=1
begin
			declare @i int

			declare @MontoAmortizar decimal(8,2)
			declare @tmpAmortizar decimal(8,2)
			declare @MontoTotal decimal(8,2)
			declare @filas int
			set @i = 1
			select @filas = count(*) from  Split (@idVentaSaldos) ---***********************
			select @MontoTotal = abs(Monto) from [Tb_documento] where IdDocumento = @IdDocumento

			Insert into @TmpVentaSaldos  SELECT   ROW_NUMBER() OVER(ORDER BY value)  , value   FROM Split (@idVentaSaldos)



			while @i <= @filas

			begin

				select @MontoAmortizar = [Total_Saldo] FROM [dbo].[Tb_Venta]  WHERE [Id_Venta]  IN ( SELECT IdVenta FROM @TmpVentaSaldos WHERE posicion = @i)
				if @i = @filas 
				begin
					set @tmpAmortizar = @MontoTotal
					
				end
				else
					begin
						set @tmpAmortizar = iif(abs(@MontoTotal) >= abs(@MontoAmortizar) ,@MontoAmortizar , @MontoTotal )	
					end;
				

			   

			   print 'Aplicar vueltos --  @MontoTotal: ' +cast(@MontoTotal  as varchar(100)) + ' @@tmpAmortizar : ' + cast(@tmpAmortizar  as varchar(100))

				

				INSERT INTO [dbo].[Tb_Amort_Venta]

					   ([Monto]

					   ,[NumeroDocumento]

					   ,[FechaPago]

					   ,[IdTipoAmortizacion]

					   ,[Observacion]

					   ,[IdEstado]

					   ,[Id_Venta]

					   ,[Id_Cliente]

					   ,[Id_Personal]

					   ,[UsuarioCreacion]

					   ,[FechaCreacion]

					   ,[UsuarioModificacion]

					   ,[FechaModificacion]

					   ,[id_caja]

					   ,[IdDocumento]

					   ,[IdFormaPago]

					   )

     

						select 

									@tmpAmortizar

									, CAST (@IdDocumento AS VARCHAR(100))

									, cast(floor(cast(getdate() as float)) as datetime)

									,'VLT'

								   , 'VUELTO'

								   , 'REG'

								   ,V.Id_Venta

								   ,V.Id_Cliente

								   ,@idUsuario

								   ,@idUsuario

								   ,getdate()

								   ,@idUsuario

								   ,getdate()

								   ,@IdCaja

								   ,@IdDocumento

								   ,'NEF'

						from  Tb_Venta v 

						where  v.[Id_Venta]  IN ( SELECT IdVenta FROM @TmpVentaSaldos WHERE posicion = @i)



				set  @MontoTotal =@MontoTotal - @tmpAmortizar  



				print 'Aplicar vueltos -- @MontoTotal acarreo : ' + cast(@MontoTotal as varchar(1000))



				set @i = @i+1;


			end;

end -- fin de aplicar vueltos

----------------------------  actualiza ventas ------------------------------///
			DECLARE @TEMPID_VENTA INT

			DELETE FROM @TmpVentaSaldos

			set @i = 1



			select @filas = count(*) from  Split (@idVentasVueltos)



			Insert into @TmpVentaSaldos  SELECT   ROW_NUMBER() OVER(ORDER BY value)  , value   FROM Split (@idVentasVueltos)



			while @i <= @filas

			BEGIN

			    SELECT @TEMPID_VENTA = IdVenta  FROM @TmpVentaSaldos WHERE  POSICION= @i



			    SELECT IdVenta FROM @TmpVentaSaldos WHERE posicion = @i

				EXEC [DGP_Insertar_Venta_Final]  @TEMPID_VENTA  , NULL



				set @i = @i+1;



			END;

			----------------------------------------------------------------------------------------------------------

			DELETE FROM @TmpVentaSaldos

			set @i = 1



			select @filas = count(*) from  Split (@idVentaSaldos)



			Insert into @TmpVentaSaldos  SELECT   ROW_NUMBER() OVER(ORDER BY value)  , value   FROM Split (@idVentaSaldos)



			while @i <= @filas

			BEGIN

			    SELECT @TEMPID_VENTA = IdVenta  FROM @TmpVentaSaldos WHERE  POSICION= @i



			    SELECT IdVenta FROM @TmpVentaSaldos WHERE posicion = @i

				EXEC [DGP_Insertar_Venta_Final]  @TEMPID_VENTA  , NULL



				set @i = @i+1;



			END;



			

END