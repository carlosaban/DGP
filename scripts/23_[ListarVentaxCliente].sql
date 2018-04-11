
use [DVGP_CITAVAL]
go

create    PROCEDURE [dbo].[ListarVentaXCliente] (  
@IdCliente INT  
, @IdDocumento int
)  
AS  

BEGIN  
  
  SELECT V.Id_Venta  
  ,V.Id_Cliente  
  ,V.Id_Producto  
  ,[Producto] = P.Nombre  
  ,V.IdTipoDocumentoVenta  
  ,[TipoDocumentoVenta] = ''  
   ,V.NumeroDocumento  
  ,CJ.Fecha as FechaCreacion  
  ,[CantidadJavas] = dbo.DGP_Obtener_CantidadJavas(V.Id_Venta)  
  ,V.Total_Peso_Bruto  
  ,V.Total_Peso_Tara  
   ,V.Total_Peso_Neto  
  ,V.Precio  
  ,V.Monto_SubTotal --// Importe  
  ,V.Monto_Igv  
  ,V.Monto_Total  
  ,V.Total_Devolucion  
  ,V.Total_Amortizacion --// Pago Efectuado  
  ,V.Total_Saldo --// Saldo Anterior  
   ,[Estado] = V.IdEstado  
 FROM dbo.Tb_Venta V
  INNER JOIN dbo.Tb_Producto AS P ON (V.Id_Producto = P.Id_Producto)  
  INNER JOIN dbo.Tb_Caja AS CJ ON (CJ.Id_Caja = V.Id_Caja)  
 WHERE Id_Cliente = @IdCliente  and IdEstado = 'REG'
 and Id_Venta not in (select Id_Venta from [Tb_Amort_Venta] where IdDocumento =  @IdDocumento  and IdEstado <> 'ANL' )
 order by cj.Fecha , v.Total_Saldo
END  
  
  


