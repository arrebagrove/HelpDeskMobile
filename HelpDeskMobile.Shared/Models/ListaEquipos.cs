using System;
using System.Collections.Generic;

namespace HelpDeskMobile.Shared.Models
{
    public class ListarEquipoServiciosWebServiceResult
    {
        public bool Activo { get; set; }
        public int Cantidad_Harware_Utilizar { get; set; }
        public int Dia_Solucion { get; set; }
        public object Garantia { get; set; }
        public object Observacion { get; set; }
        public object Part_Number { get; set; }
        public object Referencia_Ubicacion { get; set; }
        public object Usuario_Modifica { get; set; }
        public object Usuario_Registra { get; set; }
        public object descripcion_Equipo { get; set; }
        public DateTime fecha_Inicio_Garantia { get; set; }
        public DateTime fecha_Modifica { get; set; }
        public DateTime fecha_Registro { get; set; }
        public DateTime fecha_fin_Garantia { get; set; }
        public int horas_Solucion { get; set; }
        public int idArea_Cliente { get; set; }
        public int idMarca { get; set; }
        public int idModelo { get; set; }
        public int idTipo_Proteccion_Garantia { get; set; }
        public int id_Equipo { get; set; }
        public int id_Harware_Utilizar { get; set; }
        public int id_Prioridad_Equipo { get; set; }
        public int id_Servicio_General { get; set; }
        public int id_Tipo_Equipo { get; set; }
        public int id_cliente { get; set; }
        public int id_nivel_Equipo { get; set; }
        public int id_proveedor { get; set; }
        public int minutos_Solucion { get; set; }
        public string nombre_Equipo { get; set; }
        public object numero_serie { get; set; }
        public object usuaria_usa { get; set; }
    }

    public class ListaEquipos
    {
        public List<ListarEquipoServiciosWebServiceResult> ListarEquipoServiciosWebServiceResult { get; set; }
    }
}