using System;
using System.Collections.Generic;
using System.Text;

namespace DGP.Entities.Seguridad
{
    public class BEPrivilegio
    {
            //public const int Ventas = 1;
            //public const int reportes = 2;
            //public const int Compras = 3;
            //public const int Mantenimiento_de_Compras = 4;
            //public const int Tablero_Electronico = 5;
            //public const int Mantenimiento_de_Venta = 6;
            //public const int Amortizacion = 7;
            //public const int Modificar_Precios_Masivos = 8;
            //public const int Mantenimiento_de_Clientes = 9;
            //public const int Mantenimiento_de_Usuarios = 10;
            //public const int Hoja_de_Cobranza = 11;
            //public const int Tablero_de_ventas = 12;
            //public const int Cuadre_de_Caja = 13;
            //public const int Estado_de_Cuenta_Cliente = 14;
            //public const int Herramientas_de_reportes = 15;
            //public const int Tablero_Registrar_Venta = 16;
            //public const int Tablero_Lista_de_ventas = 17;
            //public const int Tablero_Registrar_Gastos = 18;
            //public const int Modificar_Detalle_de_venta = 19;
            //public const int Cambio_Estado_Venta = 20;
            //public const int Detalle_de_Venta_Lineas_de_Venta = 21;
            //public const int Detalle_de_Venta_Amortizaciones = 22;
            //public const int Detalle_de_Venta_Devoluciones = 23;
            //public const int Amortizacion_Cambio_Cobrador = 24;
            //public const int Amortizacion_Cambio_Fecha_de_Pago = 25;

        public int IdPrivilegio { get; set; }
        public string Descripcion { get; set; }
        public BEPrivilegio Padre { get; set; }
        public BEPrivilegio()
        { 
        
        }
        public BEPrivilegio(int  IdPrivilegio, string descripcion)
        {
            this.IdPrivilegio = IdPrivilegio;
            this.Descripcion = descripcion;
        
        
        }
    }
}
