USE [DVGP_CITAVAL]
GO
/****** Object:  UserDefinedFunction [dbo].[DGP_Obtener_PrecioVentaCliente]    Script Date: 11/07/2019 06:13:44 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER FUNCTION [dbo].[DGP_Obtener_PrecioVentaCliente] (
@intIdCliente	INT
,@intIdProducto	INT
) RETURNS DECIMAL(8,2)
AS
/*
**************************************************
FUNCION		: dbo.DGP_Obtener_PrecioVentaCliente
FECHA CREACION	: 23/04/2009 (dd/MM/yyyy)
AUTOR		: Carlos Abanto
**************************************************
*/

--DECLARE @intIdCliente	INT
--DECLARE @intIdProducto	INT
--SET @intIdCliente = 7
--SET @intIdProducto = 1

BEGIN
	DECLARE @decPrecioVenta	DECIMAL(8,2);
	DECLARE @fechaCompra date;
	set @fechaCompra = getdate(); -- cambiar para q reciba parametro fecha

	IF EXISTS (SELECT PrecioVenta
		FROM dbo.Tb_Prod_x_Cliente
		WHERE Id_Cliente = @intIdCliente
			AND Id_Producto = @intIdProducto
			)
		BEGIN
		  -- PRINT '1'
			SELECT @decPrecioVenta = isnull ( co.precio + pro.Margen , pro.PrecioVenta)
			FROM dbo.Tb_Prod_x_Cliente pro
			left join ( select IdProducto , max(c.[Precio]) as precio  
						  from [dbo].[TbCompra] c 
						  inner join Tb_Cliente_Proveedor cli on c.IdProveedor = cli.Id_Cliente and cli.Estado = 1 and cli.TipoCliente = 'PRO' 
						  where c.IdEstado = 'REG'  
						  and c.Fecha =  @fechaCompra
						  group by IdProducto
						 ) as co on co.IdProducto = pro.Id_Producto
			WHERE Id_Producto = @intIdProducto
				AND Id_Cliente = @intIdCliente;
		END
	ELSE
		BEGIN
		    -- PRINT '2'
			SELECT @decPrecioVenta = isnull ( co.precio + pro.Margen , pro.PrecioVenta)
			FROM dbo.Tb_Producto pro
			left join ( select IdProducto , max(c.[Precio]) as precio  
				from [dbo].[TbCompra] c 
				inner join Tb_Cliente_Proveedor cli on c.IdProveedor = cli.Id_Cliente and cli.Estado = 1 and cli.TipoCliente = 'PRO' 
				where c.IdEstado = 'REG'
				and c.Fecha =  @fechaCompra
				group by IdProducto 
				) as co on co.IdProducto = pro.Id_Producto

			WHERE Id_Producto = @intIdProducto;
		END

	--SELECT @decPrecioVenta;
	RETURN @decPrecioVenta;
END
