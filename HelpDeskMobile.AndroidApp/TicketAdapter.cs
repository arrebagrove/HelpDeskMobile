using Android.Content;
using Android.Views;
using Android.Widget;
using HelpDeskMobile.Shared.Models;

namespace HelpDeskMobile.AndroidApp
{
    internal class TicketAdapter : BaseAdapter<ListarTicketClienteWCFResult>
    {
        private ListaTickets _tickets;
        private Context _context;
        private TextView descripcionTicket;
        private TextView idTicket;
        private TextView equipoTicket;
        private TextView tipoServicioTicket;
        private TextView tipoEquipoTicket;

        public TicketAdapter(Context context, ListaTickets tickets)
        {
            _tickets = tickets;
            _context = context;
        }

        public override ListarTicketClienteWCFResult this[int position]
        {
            get
            {
                return (_tickets.ListarTicketClienteWCFResult == null) ? null : _tickets.ListarTicketClienteWCFResult[position];
            }
        }

        public void UpdateTickets(ListaTickets tickets)
        {
            _tickets = tickets;
        }

        public override int Count
        {
            get
            {
                return (_tickets.ListarTicketClienteWCFResult == null) ? 0 : _tickets.ListarTicketClienteWCFResult.Count;
            }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View row = LayoutInflater.From(_context).Inflate(Resource.Layout.TicketAdapterLayout, null, false);

            descripcionTicket = row.FindViewById<TextView>(Resource.Id.TicketAdapterDesc);
            idTicket = row.FindViewById<TextView>(Resource.Id.TicketAdapterIDTicket);
            equipoTicket = row.FindViewById<TextView>(Resource.Id.TickerAdapterEquipo);
            tipoServicioTicket = row.FindViewById<TextView>(Resource.Id.TicketAdapterTipoServicio);
            tipoEquipoTicket = row.FindViewById<TextView>(Resource.Id.TicketAdapterTipoEquipo);

            descripcionTicket.Text = _tickets.ListarTicketClienteWCFResult[position].Observaciones;
            idTicket.Text = _tickets.ListarTicketClienteWCFResult[position].codigo_Ticket;
            equipoTicket.Text = _tickets.ListarTicketClienteWCFResult[position].nombre_Equipo;
            tipoServicioTicket.Text = _tickets.ListarTicketClienteWCFResult[position].TipoServicio;
            tipoEquipoTicket.Text = _tickets.ListarTicketClienteWCFResult[position].TipoEquipo;

            return row;
        }
    }
}