using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using HelpDeskMobile.AndroidApp.Helpers;
using HelpDeskMobile.Shared.Models;
using HelpDeskMobile.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HelpDeskMobile.AndroidApp
{
    public class RegistrarTicketFragment : Android.Support.V4.App.Fragment
    {
        private Dictionary<string, string> usuario;
        private Context context;

        private ProgressDialog busyIndicator;
        private Spinner spinner;
        private Spinner spinner2;
        private Spinner spinner3;
        private Button btnEnviar;
        private EditText descripcionTxt;
        private List<SpinnerItem> itemsServicios;
        private List<SpinnerItem> itemsTipoEquipo;
        private List<SpinnerItem> itemsEquipo;

        private long idServicio;
        private long idTipoEquipo;
        private long idEquipo;

        public RegistrarTicketFragment()
        {
        }

        public RegistrarTicketFragment(Context _context)
        {
            context = _context;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.RegistrarTicketLayout, container, false);
            usuario = Global.GlobalUserSessionInstance().getUserDetails();

            btnEnviar = view.FindViewById<Button>(Resource.Id.RegistrarEnviarBtn);
            descripcionTxt = view.FindViewById<EditText>(Resource.Id.RegistrarDescripcionTxt);

            if (Global.IsNetworkAvailable(context))
            {
                BuildSpinners(view);
                btnEnviar.Enabled = true;
            }
            else
            {
                Global.ShowAlert("Al parecer no estás conectado a Internet. Por favor, verifique su conexión y vuelva a iniciar la aplicación.", context);
                btnEnviar.Enabled = false;
            }

            btnEnviar.Click += BtnEnviar_Click;
            return view;
        }

        private async void BtnEnviar_Click(object sender, EventArgs e)
        {
            if (spinner3.SelectedItemId < itemsEquipo.Count - 1
                && spinner3.SelectedItemId >= 0
                && !String.IsNullOrEmpty(descripcionTxt.Text))
            {
                var button = sender as Button;
                button.Enabled = false;
                button.Clickable = false;

                if (Global.IsNetworkAvailable(context))
                {
                    //  Enviar ticket
                    Ticket ticket = new Ticket();
                    ticket.T = new TicketInfo
                    {
                        Usuario_Cliente = usuario.FirstOrDefault(x => x.Key == UserSessionManager.KEY_USERNAME).Value,
                        id_Cliente = usuario.FirstOrDefault(x => x.Key == UserSessionManager.KEY_ID).Value,
                        idTipoServicio = ((int)this.idServicio).ToString(),
                        idTipoEquipo = ((int)this.idTipoEquipo).ToString(),
                        id_Equipo = ((int)this.idEquipo).ToString(),
                        nombre_Equipo = (string)spinner3.SelectedItem,
                        Fecha_Registro_Ticket = DateTime.Now.ToString("yyyy-MM-dd H:mm:ss"),
                        Observaciones = descripcionTxt.Text
                    };

                    try
                    {
                        busyIndicator = ProgressDialog.Show(this.context, "Espere un momento", "Estamos enviando su incidencia");

                        var numeroTicket = await WebServiceConnection.WSEnviarIncidencia(ticket);
                        //  Enviar email
                        var correo = usuario.FirstOrDefault(p => p.Key == UserSessionManager.KEY_EMAIL).Value;
                        var empresa = usuario.FirstOrDefault(p => p.Key == UserSessionManager.KEY_NOMBRE_EMPRESA).Value;
                        var nombreCliente = usuario.FirstOrDefault(p => p.Key == UserSessionManager.KEY_NOMBRE_CLIENTE).Value;
                        //bool confirmacionCorreo = await WebServiceConnection.WSEnviarCorreo(correo, numeroTicket, empresa, nombreCliente);
                        //if (confirmacionCorreo)
                        //{
                        Toast.MakeText(this.context, $"Incidencia registrada: {numeroTicket}", ToastLength.Long).Show();
                        descripcionTxt.Text = "";
                        //}
                        busyIndicator.Dismiss();
                    }
                    catch (Exception)
                    {
                        Global.ShowAlert("Al parecer no estás conectado a Internet. Por favor, verifique su conexión y vuelva a iniciar la aplicación.", context);
                    }
                }
                else
                {
                    Global.ShowAlert("Al parecer no estás conectado a Internet. Por favor, verifique su conexión y vuelva a iniciar la aplicación.", context);
                }

                button.Enabled = true;
                button.Clickable = true;
            }
            else
            {
                Toast.MakeText(this.context, "Por favor, llene toda la información.", ToastLength.Long).Show();
            }
        }

        private async void BuildSpinners(View view)
        {
            busyIndicator = ProgressDialog.Show(context, "Cargando tus servicios", "Unos segundos...");
            try
            {
                spinner = view.FindViewById<Spinner>(Resource.Id.RegistrarTipoServicioSpinner);

                var lista = await WebServiceConnection.WSBusquedaTipoDeServicios(usuario.FirstOrDefault(x => x.Key == UserSessionManager.KEY_USERNAME).Value);
                var listaString = lista.ListarServiciosCliente_Web_ServiceResult.Select(p => p.Tipo_Servicio).ToList();
                itemsServicios = new List<SpinnerItem>();
                foreach (var item in listaString)
                {
                    itemsServicios.Add(new SpinnerItem(item, true));
                }
                itemsServicios.Add(new SpinnerItem("Seleccione una opción", false));

                MySpinnerAdapter adapter = new MySpinnerAdapter(itemsServicios);
                spinner.Adapter = adapter;
                spinner.SetSelection(itemsServicios.Count - 1);

                spinner = view.FindViewById<Spinner>(Resource.Id.RegistrarTipoServicioSpinner);
                spinner.ItemSelected += Spinner_ItemSelected;

                spinner2 = view.FindViewById<Spinner>(Resource.Id.RegistrarTipoEquipoSpinner);
                spinner2.ItemSelected += Spinner2_ItemSelected;

                spinner3 = view.FindViewById<Spinner>(Resource.Id.RegistrarEquipoSpinner);
                spinner3.ItemSelected += Spinner3_ItemSelected;
            }
            catch (Exception)
            {
                busyIndicator.Dismiss();
                Global.ShowAlert("Al parecer no estás conectado a Internet. Por favor, verifique su conexión y vuelva a iniciar la aplicación.", context);
            }
            finally
            {
                busyIndicator.Dismiss();
            }
        }

        private void Spinner3_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            idEquipo = e.Id + 1;
            spinner3.Enabled = true;
            btnEnviar.Enabled = true;
        }

        private async void Spinner2_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            spinner.Enabled = false;
            spinner2.Enabled = false;
            spinner3.Enabled = false;
            btnEnviar.Enabled = false;

            if (e.Id != itemsTipoEquipo.Count - 1)
            {
                idTipoEquipo = e.Id + 1;
                if (Global.IsNetworkAvailable(context))
                {
                    var lista = await WebServiceConnection.WSBusquedaEquipos(idServicio.ToString(), idTipoEquipo.ToString());
                    var listaString = lista.ListarEquipoServiciosWebServiceResult.Select(p => p.nombre_Equipo).ToList();

                    itemsEquipo = new List<SpinnerItem>();
                    foreach (var item in listaString)
                    {
                        itemsEquipo.Add(new SpinnerItem(item, false));
                    }
                    itemsEquipo.Add(new SpinnerItem("Seleccione una opción", false));

                    MySpinnerAdapter adapter = new MySpinnerAdapter(itemsEquipo);
                    spinner3.Adapter = adapter;
                    spinner3.SetSelection(itemsEquipo.Count - 1);
                }
                else
                {
                    Global.ShowAlert("Al parecer no estás conectado a Internet. Por favor, verifique su conexión y vuelva a iniciar la aplicación.", context);
                }
            }
            else
            {
                spinner3.Adapter = new MySpinnerAdapter(new List<SpinnerItem>());
                spinner3.Enabled = false;
                spinner3.Clickable = false;
            }
            spinner.Enabled = true;
            spinner2.Enabled = true;
        }

        private async void Spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            spinner.Enabled = false;
            spinner2.Enabled = false;
            spinner3.Enabled = false;
            btnEnviar.Enabled = false;

            if (e.Id != itemsServicios.Count - 1)
            {
                idServicio = e.Id + 1;
                if (Global.IsNetworkAvailable(context))
                {
                    var lista = await WebServiceConnection.WSBusquedaTipodeEquipos(idServicio.ToString());
                    var listaString = lista.ListarTipoEquipoServicioClienteWebServicesResult.Select(p => p.Descripcion).ToList();

                    itemsTipoEquipo = new List<SpinnerItem>();
                    foreach (var item in listaString)
                    {
                        itemsTipoEquipo.Add(new SpinnerItem(item, false));
                    }
                    itemsTipoEquipo.Add(new SpinnerItem("Seleccione una opción", false));

                    MySpinnerAdapter adapter = new MySpinnerAdapter(itemsTipoEquipo);
                    spinner2.Adapter = adapter;
                    spinner2.SetSelection(itemsTipoEquipo.Count - 1);
                }
                else
                {
                    Global.ShowAlert("Al parecer no estás conectado a Internet. Por favor, verifique su conexión y vuelva a iniciar la aplicación.", context);
                }
                spinner2.Enabled = true;
                spinner2.Clickable = true;
            }
            else
            {
                spinner2.Enabled = false;
                spinner2.Clickable = false;
                spinner3.Enabled = false;
                spinner3.Clickable = false;
            }

            spinner.Enabled = true;
            spinner.Clickable = true;
        }
    }
}