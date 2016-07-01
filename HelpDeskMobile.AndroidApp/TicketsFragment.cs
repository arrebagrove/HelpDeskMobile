using Android.Content;
using Android.OS;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using HelpDeskMobile.AndroidApp.Helpers;
using HelpDeskMobile.Shared.Models;
using HelpDeskMobile.Shared.Services;
using System;
using System.Linq;

namespace HelpDeskMobile.AndroidApp
{
    public class TicketsFragment : Fragment
    {
        private ListaTickets listaTickets;
        private ListView listView;
        private TicketAdapter adapter;
        private int _position;
        private Context _context;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            listaTickets = new ListaTickets();
        }

        public TicketsFragment(int position, Context context)
        {
            _position = position;
            _context = context;
        }

        public override Android.Views.View OnCreateView(Android.Views.LayoutInflater inflater, Android.Views.ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.TicketsLayout, container, false);

            listView = view.FindViewById<ListView>(Resource.Id.TicketsLayoutListView);
            adapter = new TicketAdapter(this.Context, listaTickets);
            listView.Adapter = adapter;

            UpdateView();

            return view;
        }

        public async override void OnResume()
        {
            base.OnResume();
            var nombreUsuario = Global.GlobalUserSessionInstance().getUserDetails().FirstOrDefault(p => p.Key == UserSessionManager.KEY_USERNAME).Value;
            Android.App.ProgressDialog dialog;
            dialog = Android.App.ProgressDialog.Show(this.Context, "Espere un momento", "Estamos obteniendo los datos");
            try
            {
                listaTickets = await WebServiceConnection.WSBusquedaTickets(nombreUsuario);
                ListaTickets lista = new ListaTickets();
                switch (_position)
                {
                    case 0:
                        lista.ListarTicketClienteWCFResult = listaTickets.ListarTicketClienteWCFResult.Where(p => p.Estado == "PENDIENTE").ToList();
                        break;

                    case 1:
                        lista.ListarTicketClienteWCFResult = listaTickets.ListarTicketClienteWCFResult.Where(p => p.Estado == "EN PROCESO" || p.Estado == "SOLUCIONADO").ToList();
                        break;

                    case 2:
                        lista.ListarTicketClienteWCFResult = listaTickets.ListarTicketClienteWCFResult.Where(p => p.Estado == "VENCIDA").ToList();
                        break;

                    case 3:
                        lista.ListarTicketClienteWCFResult = listaTickets.ListarTicketClienteWCFResult.Where(p => p.Estado == "CANCELADO").ToList();
                        break;

                    default: break;
                }
                adapter.UpdateTickets(lista);
                UpdateView();
            }
            catch (Exception)
            {
                dialog.Dismiss();
                Global.ShowAlert("Al parecer no estás conectado a Internet. Por favor, verifique su conexión y vuelva a iniciar la aplicación.", _context);
            }

            dialog.Dismiss();
        }

        private void UpdateView()
        {
            if (adapter != null)
            {
                adapter.NotifyDataSetChanged();
            }
        }
    }
}