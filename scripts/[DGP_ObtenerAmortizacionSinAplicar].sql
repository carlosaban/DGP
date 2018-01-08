USE [DVGP_CITAVAL]
GO
/****** Object:  StoredProcedure [dbo].[DGP_ObtenerAmortizacionSinAplicar]    Script Date: 07/01/2018 03:33:22 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER    PROCEDURE [dbo].[DGP_ObtenerAmortizacionSinAplicar] (

  @IdCliente int

) AS

BEGIN

select sum(vi.vueltos )
from (

select 
		doc.Monto- (select isnull(sum(a.Monto),0) 
					from Tb_Amort_Venta  a 
					where a.IdDocumento = doc.IdDocumento 
						and  a.IdEstado = dbo.DGP_VENTA_ESTADO_REGISTRADO() 
							and a.IdTipoAmortizacion = dbo.DGP_TIPO_AMOR_AMORTIZACION())  as vueltos

							
	    
		
from Tb_documento doc  
where 1=1
AND doc.IdEstado = dbo.DGP_VENTA_ESTADO_REGISTRADO() and doc.IdCliente = @IdCliente
) as vi

END