use [DVGP_CITAVAL]
go

  update privilegios
  set descripcion ='Mantenimiento de Doc. Pagos Venta' where id=35
  insert into privilegios values (36,'Mantenimiento de Doc. Pagos Compra',1,1)

  insert into perfil_privilegios values(1,36),(2,36),(3,36)


  go