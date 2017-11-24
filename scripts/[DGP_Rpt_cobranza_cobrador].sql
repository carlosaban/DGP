



    ALTER                PROCEDURE [dbo].[DGP_Rpt_cobranza_cobrador] (  

@dtFechaInicial Datetime  ,   

@dtFechaFinal Datetime  ,   

@sPersonal varchar(1000) = null ,  

@intIdCaja  int ,   

@intIdModoReporte int = 0  

)  

AS  

/*  

*****************************************************  

PROCEDIMIENTO  : dbo.DGP_Rpt_EstadoCuentaCliente  

FECHA   : 04/03/2009 (dd/MM/yyyy)  

AUTOR   : Carlos Abanto G  

*****************************************************  

*/  

  

  

Declare @tblListProductos  Table(  

 id_producto int  

   

 )  

Declare @tblListZonas  Table(  

 id_zona int  

   

 )  

  

BEGIN  

  

if @intIdModoReporte = 0   

     begin  

	 print 'q paso'

  SELECT *   

  FROM (  

   SELECT   

    'AMORTIZACIÓN' AS TIPO_MOV ,  

    isnull ( ISNULL (C1.NOMBRES, C2.NOMBRES), v.ClienteEventual  ) AS CONCEPTO ,     

	SUM (a.Monto) AS monto ,  
    NULL AS monto_anterior,
    NULL as FECHA,  
    p.nombre as PERSONAL , 
    null as observacion ,  
    a.fechapago  as fecha_pago ,  
    null as producto  
   FROM  Tb_Amort_Venta A   

   INNER JOIN dbo.Tb_Venta V ON  V.ID_VENTA = A.ID_VENTA  

   inner join dbo.tb_caja cj on cj.id_caja = v.id_caja  

   inner join dbo.Tb_Personal P ON A.id_personal = P.id_personal   

   inner join dbo.Tb_Producto prod on prod.Id_Producto = v.Id_Producto  

   LEFT JOIN dbo.Tb_Cliente_Proveedor C1 ON C1.ID_CLIENTE = V.ID_CLIENTE  

   LEFT JOIN dbo.Tb_Cliente_Proveedor C2 ON C2.ID_CLIENTE = A.ID_CLIENTE   

   WHERE 1=1  

   AND A.IdEstado NOT IN ('ANL')  

   AND v.IdEstado  NOT in ('ANL')  

   and A.FechaPago BETWEEN @dtFechaInicial and @dtFechaFinal  

   GROUP BY isnull ( ISNULL (C1.NOMBRES, C2.NOMBRES), v.ClienteEventual  )  , p.nombre , a.fechapago

   union all  

   SELECT   

    'GASTO' AS TIPO_MOV,  

    [concepto] AS CONCEPTO ,  

    -1*[monto] as MONTO,   

    null as monto_anterior ,  

      

    [Fecha_gasto] as FECHA ,  

    p.nombre as PERSONAL ,  

    g.observacion ,  

    [Fecha_gasto]  as fecha_pago,  

    'pollo' as producto  

   FROM [dbo].[tb_gasto]G  

   INNER JOIN Tb_Personal P ON G.id_personal = P.id_personal  

   WHERE  

    1=1    

    AND g.Fecha_gasto BETWEEN @dtFechaInicial and @dtFechaFinal  

     

   ) tb  

    

  ORDER BY  fecha_pago asc,  PERSONAL, FECHA , CONCEPTO   

  --INNER JOIN   

     end  

else  

     begin  

  

  SELECT TIPO_MOV   

    , null as fecha ,  CONCEPTO   , PERSONAL , dbo.DGP_obs_amortizacion ( id_personal, id_cliente ,fecha_pago ,sum(monto_anterior) ,sum(monto)     ) as observacion , fecha_pago , sum(monto) as monto , sum(monto_anterior) as monto_anterior , producto  

  FROM (  

   SELECT   

    'AMORTIZACIÓN' AS TIPO_MOV ,  

    isnull ( ISNULL (C1.NOMBRES, C2.NOMBRES), v.ClienteEventual  ) AS CONCEPTO ,     

    case a.fechapago  

    when cj.fecha then A.Monto  

    else null  

    end as monto  ,  

    

                            case a.fechapago  

    when cj.fecha then null  

    else  A.Monto  

    end as  monto_anterior ,   

    

    cj.fecha as FECHA,  

    p.nombre as PERSONAL ,   

    a.observacion ,  

    a.fechapago as fecha_pago , 

    p.id_personal ,  

    a.id_cliente ,  

    prod.Nombre as producto  

    

   FROM  Tb_Amort_Venta A   

   INNER JOIN dbo.Tb_Venta V ON  V.ID_VENTA = A.ID_VENTA  

    inner join dbo.tb_caja cj on cj.id_caja = v.id_caja  

   inner join dbo.Tb_Personal P ON A.id_personal = P.id_personal   

   inner join dbo.Tb_Producto prod on prod.Id_Producto = v.Id_Producto  

     

   LEFT JOIN dbo.Tb_Cliente_Proveedor C1 ON C1.ID_CLIENTE = V.ID_CLIENTE  

   LEFT JOIN dbo.Tb_Cliente_Proveedor C2 ON C2.ID_CLIENTE = A.ID_CLIENTE   

   WHERE 1=1  

   AND A.IdEstado NOT IN ('ANL')  

   AND v.IdEstado  NOT in ('ANL')  

   and A.FechaPago BETWEEN @dtFechaInicial and @dtFechaFinal  

   union all  

   SELECT   

    'GASTO' AS TIPO_MOV,  

    [concepto] AS CONCEPTO ,   

    -1*[monto] as MONTO,   

    null as monto_anterior ,  

    [Fecha_gasto] as FECHA ,  

    p.nombre as PERSONAL ,  

    g.observacion ,  

    [Fecha_gasto] as fecha_pago,  

    P.id_personal ,  

    null,  

    'pollo' as producto  

    

   FROM [dbo].[tb_gasto]G  

   INNER JOIN Tb_Personal P ON G.id_personal = P.id_personal  

   WHERE  

    1=1    

    AND g.Fecha_gasto BETWEEN @dtFechaInicial and @dtFechaFinal  

     

   ) tb  

    

  group by TIPO_MOV ,  CONCEPTO  , PERSONAL , observacion , fecha_pago , id_cliente , id_personal , producto  

    

  ORDER BY TIPO_MOV ,  fecha_pago asc,  PERSONAL , CONCEPTO   

  

  

     end  

  

  

END  -- del procedimiento  

  

  

  
