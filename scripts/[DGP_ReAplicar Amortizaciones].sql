





alter    PROCEDURE [dbo].[DGP_ReAplicar Amortizaciones] (



  @IdCliente int

, @idUsuario INT 

, @IdCaja int



) AS



BEGIN



declare @IdDocumento int	

declare @bOK bit 

--declare @IdCliente int





set @bOK = 1







DECLARE @TmpVentas TABLE ( posicion int identity(1,1), IdVenta INT , monto decimal (18,2), AmortizacionesAcumulado decimal (18,2) , IdCliente int)

DECLARE @TmpDocumentos TABLE ( posicion int identity(1,1), IdDocumento INT , monto decimal (18,2) , acumulado decimal (18,2) , fecha datetime , IdCliente int , IdPersonal int)



--select @IdCliente = id_cliente from Tb_Venta where Id_Venta = @idVenta 

--

--Actualizar amortizaciones si tenemos documentos



--update v set

--v.IdEstado = dbo.DGP_VENTA_ESTADO_ANULADO()

--, v.UsuarioModificacion = @idUsuario

--,v.FechaModificacion = getdate()

--from Tb_Amort_Venta v where Id_Venta = @idVenta

--exec [dbo].[DGP_Insertar_Venta_Final] @idVenta , null



print 'insertando vebtas' 

insert into @TmpVentas(

IdVenta  , monto , AmortizacionesAcumulado , IdCliente 

)

select  v.Id_Venta , v.Total_Saldo , 0 , v.[Id_Cliente]

from dbo.Tb_Venta v

inner join dbo.Tb_Caja caj on caj.Id_Caja = v.Id_Caja

where v.IdEstado = dbo.DGP_VENTA_ESTADO_REGISTRADO()

and v.Id_Cliente = @IdCliente

order by caj.Fecha , v.Total_Saldo



print 'insertando @TmpDocumentos' 





insert into @TmpDocumentos(

IdDocumento , monto  , acumulado , fecha ,  IdCliente  , IdPersonal

)

select doc.IdDocumento,  

		doc.Monto- (select isnull(sum(a.Monto),0) 

					from Tb_Amort_Venta  a 

					where a.IdDocumento = doc.IdDocumento 

						and  a.IdEstado = dbo.DGP_VENTA_ESTADO_REGISTRADO() 

					)  

		, 0 

		, doc.Fecha 

		, doc.idCliente 

		, doc.IdPersonal

from Tb_documento doc  

where 1=1

AND doc.IdEstado = 'REG' and doc.IdCliente = @IdCliente

order by   doc.Fecha , doc.Monto 



--select doc.IdDocumento,  doc.Monto- sum(a.Monto) , 0 , doc.Fecha , doc.idCliente , doc.IdPersonal

--from Tb_documento doc  

--left join Tb_Amort_Venta a on a.IdDocumento = doc.IdDocumento and a.IdEstado = dbo.DGP_VENTA_ESTADO_REGISTRADO()

--where 1=1

--and a.IdTipoAmortizacion = dbo.DGP_TIPO_AMOR_AMORTIZACION() AND doc.estado = 1 and doc.IdCliente = @IdCliente

--group by   doc.IdDocumento , doc.Fecha , doc.Monto , doc.idCliente, doc.IdPersonal

--having doc.Monto- sum(a.Monto)>0

--order by   doc.Fecha , doc.Monto 



--

--volver a crear amortizaciones en base a los documentos

--

declare @i int = 1

declare @j int =1

declare @maxVenta int = 0

declare @maxDocumentos int =0

select @maxVenta = max(posicion) from @TmpVentas   mentos

select @maxDocumentos = max(posicion) from @TmpDocumentos



print 'insertando documento' 

while (@i <= @maxDocumentos)

begin

		declare @tmpIdDocumento int

		declare @tmpMontoDocumento decimal(18,2)

		declare @tmpAmortizacionesAcumuladoDocumento decimal(18,2)

		declare @tmpFechaDocumento date

		declare @TmpDocumentoIdPersonal int

		

		select @tmpIdDocumento =  IdDocumento  ,@tmpMontoDocumento = monto    , @TmpDocumentoIdPersonal = IdPersonal, @tmpFechaDocumento =fecha    from @TmpDocumentos   WHERE posicion = @i

		set @j = 1

		set @tmpAmortizacionesAcumuladoDocumento = 0

		print 'while (@j <= @maxVenta AND @tmpAmortizacionesAcumuladoDocumento < @tmpMontoDocumento )' 

		while (@j <= @maxVenta AND @tmpAmortizacionesAcumuladoDocumento < @tmpMontoDocumento )

		begin

			

			declare @tmpIdVenta int

			declare @tmpMontoVenta decimal(18,2)

			declare @tmpMontoVentaAmortizar decimal(18,2)



			select @tmpIdVenta =  IdVenta  ,@tmpMontoVenta = monto-AmortizacionesAcumulado   from @TmpVentas   WHERE posicion = @j



			set @tmpMontoVentaAmortizar = @tmpMontoDocumento - @tmpAmortizacionesAcumuladoDocumento



			set @tmpMontoVentaAmortizar = iif(@tmpMontoVentaAmortizar <=@tmpMontoVenta  , @tmpMontoVentaAmortizar , @tmpMontoVenta )

			print '  @tmpMontoVentaAmortizar' 

			if @tmpMontoVentaAmortizar> 0 

			begin 

				INSERT INTO [Tb_Amort_Venta]

									  ([Monto]

									   ,[NumeroDocumento]

									   ,[IdFormaPago]

									   ,[FechaPago]

									   ,[IdTipoAmortizacion]

									   ,[Observacion]

									   ,[IdEstado]

									   ,[Id_Venta]

									   ,[Id_Cliente]

									   ,[Id_Personal]

           

									   ,[UsuarioModificacion]

									   ,[FechaModificacion]

									   ,[id_caja]

									   ,[IdDocumento])

     VALUES

           (@tmpMontoVentaAmortizar 

           ,@tmpIdDocumento

           , 'EFE'

		   ,@tmpFechaDocumento

           ,dbo.DGP_TIPO_AMOR_AMORTIZACION()

		   ,'recalculado'

           ,dbo.DGP_VENTA_ESTADO_REGISTRADO()

           ,@tmpIdVenta

           ,@IdCliente

           ,@TmpDocumentoIdPersonal

           ,@idUsuario

           ,GETDATE()

           ,@IdCaja

           ,@tmpIdDocumento

		   )



				UPDATE @TmpVentas set

				AmortizacionesAcumulado = AmortizacionesAcumulado +  @tmpMontoVentaAmortizar 

				where posicion = @j



			   set @tmpAmortizacionesAcumuladoDocumento = @tmpAmortizacionesAcumuladoDocumento + @tmpMontoVentaAmortizar

		   end

		   set @j=@j+1

		end

		set @i=@i+1

end

-- actualizar las ventas

set @i  = 1



while (@i <= @maxVenta)

begin

select @tmpIdVenta =  IdVenta  ,@tmpMontoVenta = monto-AmortizacionesAcumulado   from @TmpVentas   WHERE posicion = @i



exec [dbo].[DGP_Insertar_Venta_Final] @tmpIdVenta



set @i =@i +1

end;



END