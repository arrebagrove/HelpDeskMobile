using System.Collections.Generic;

namespace HelpDeskMobile.Shared.Models
{
    public class ListarTipoEquipoServicioClienteWebServicesResult
    {
        public bool Activo { get; set; }
        public string Descripcion { get; set; }
        public int id_Servicio_General { get; set; }
        public int id_Tipo_Equipo { get; set; }
    }

    public class ListaTipoEquipos
    {
        public List<ListarTipoEquipoServicioClienteWebServicesResult> ListarTipoEquipoServicioClienteWebServicesResult { get; set; }
    }
}