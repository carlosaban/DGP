USE [DVGP_CITAVAL]
GO
/****** Object:  StoredProcedure [dbo].[DGP_Listar_VentaCliente]    Script Date: 15/01/2018 04:39:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


alter    PROCEDURE [dbo].[ListarVentaXCliente] (  
@IdCliente INT  
)  
AS  

BEGIN  
  
 SELECT Id_Venta,Monto_Total
 FROM dbo.Tb_Venta  
 WHERE Id_Cliente = @IdCliente  and IdEstado = 'REG'
END  
  
  


