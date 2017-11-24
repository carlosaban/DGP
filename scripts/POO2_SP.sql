USE [Negocios2017]
GO

/****** Object:  StoredProcedure [dbo].[usp_PedidosAno]    Script Date: 23/11/2017 06:25:55 p.m. ******/
DROP PROCEDURE [dbo].[usp_PedidosAno]
GO

/****** Object:  StoredProcedure [dbo].[usp_PedidosporFechas]    Script Date: 23/11/2017 06:25:55 p.m. ******/
DROP PROCEDURE [dbo].[usp_PedidosporFechas]
GO

/****** Object:  StoredProcedure [dbo].[usp_pedidosProducto]    Script Date: 23/11/2017 06:25:55 p.m. ******/
DROP PROCEDURE [dbo].[usp_pedidosProducto]
GO

/****** Object:  StoredProcedure [dbo].[usp_pedidosProducto]    Script Date: 23/11/2017 06:25:55 p.m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


create  proc [dbo].[usp_pedidosProducto]
@prod int
As
select *
from  tb_pedidoscabe p 
inner join tb_pedidosdeta pd on  p.IdPedido = pd.IdPedido
where pd.IdProducto = @prod

GO

/****** Object:  StoredProcedure [dbo].[usp_PedidosporFechas]    Script Date: 23/11/2017 06:25:55 p.m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

create proc [dbo].[usp_PedidosporFechas]
@f1 date , @f2 date
As
Select * from tb_pedidoscabe 
where FechaPedido between  @f1 and @f2 


GO

/****** Object:  StoredProcedure [dbo].[usp_PedidosAno]    Script Date: 23/11/2017 06:25:55 p.m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

create proc [dbo].[usp_PedidosAno]
@y int
As
Select * from tb_pedidoscabe 
where year(FechaPedido)= @y

GO


