using System;
using System.Collections.Generic;

namespace HelpDeskMobile.Shared.Models
{
    public class ListarServiciosClienteWebServiceResult
    {
        public object Contacto { get; set; }
        public object Contacto_Correo { get; set; }
        public int Dias_Solucion { get; set; }
        public int Hora_Solucion { get; set; }
        public int Hora_recepcion { get; set; }
        public int Minutos_Recepcion { get; set; }
        public int Minutos_Solucion { get; set; }
        public string Tipo_Servicio { get; set; }
        public bool activo { get; set; }
        public object cliente { get; set; }
        public DateTime fecha_Inicio { get; set; }
        public object fecha_Inicio_2 { get; set; }
        public DateTime fecha_Termino { get; set; }
        public object fecha_Termino_2 { get; set; }
        public int id_Servicio_General { get; set; }
        public int id_cliente { get; set; }
        public int id_contacto { get; set; }
        public int id_movimiento { get; set; }
        public int id_tipo_Servicio { get; set; }
        public object observaciones { get; set; }
    }

    public class ListaServicios
    {
        public List<ListarServiciosClienteWebServiceResult> ListarServiciosCliente_Web_ServiceResult { get; set; }
    }
}