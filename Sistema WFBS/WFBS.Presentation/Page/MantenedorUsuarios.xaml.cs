using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WFBS.Business.Core;

namespace MasterPages.Page
{
    /// <summary>
    /// Lógica de interacción para MantenedorUsuarios.xaml
    /// </summary>
    public partial class MantenedorUsuarios : System.Windows.Controls.Page
    {

        public MantenedorUsuarios()
        {
            InitializeComponent();
            lblUserInfo.Content = Global.NombreUsuario;
        }

        private void dgUsuarios_Loaded(object sender, RoutedEventArgs e)
        {
            //Collections col = new Collections();
            ColeccionUsuario colUs = new ColeccionUsuario();
            dgUsuarios.ItemsSource = colUs.ReadAllUsuarios();
            dgUsuarios.Columns[3].Visibility = Visibility.Collapsed;
            dgUsuarios.Columns[4].Visibility = Visibility.Collapsed;
            dgUsuarios.Columns[6].Visibility = Visibility.Collapsed;
            dgUsuarios.Columns[7].Visibility = Visibility.Collapsed;

            dgUsuarios.Columns[5].Header = "Jefe a cargo";
            dgUsuarios.Columns[8].Header = "Área";
            dgUsuarios.Columns[10].Header = "Obsoleto";


        }
        private void dgUsuarios_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            /*Para cambiar nombre de cabecera*/

        }

        private void btnAgregarUsuario_Click(object sender, RoutedEventArgs e)
        {
            NavigationService navService = NavigationService.GetNavigationService(this);
            InsertarUsuario nextPage = new InsertarUsuario();
            navService.Navigate(nextPage);
        }

        private void btnModificarUsuario_Click(object sender, RoutedEventArgs e)
        {
            if (dgUsuarios.SelectedItem != null)
            {
                Usuario us = (Usuario)dgUsuarios.SelectedItem;
                //MessageBox.Show(us.Rut, "Éxito!");
                NavigationService navService = NavigationService.GetNavigationService(this);
                ModificarUsuario nextPage = new ModificarUsuario(us.Rut);
                navService.Navigate(nextPage);
            }
            else
            {
                MessageBox.Show("Debe seleccionar un Usuario antes", "Aviso");
            }
        }
        private void btnEliminarUsuario_Click(object sender, RoutedEventArgs e)
        {
            if (dgUsuarios.SelectedItem != null)
            {
                Usuario us = (Usuario)dgUsuarios.SelectedItem;
                if (us.Obs == "Si")
                {
                    MessageBox.Show("El Usuario seleccionado se encuentra desactivado", "Aviso");
                }
                else
                {
                    string xml = us.Serializar();
                    WFBS.Presentation.ServiceWCF.ServiceWFBSClient servicio = new WFBS.Presentation.ServiceWCF.ServiceWFBSClient();

                    if (servicio.EliminarUsuario(xml))
                    {
                        MessageBox.Show("El Usuario seleccionado ha sido desactivado", "Éxito!");
                        NavigationService navService = NavigationService.GetNavigationService(this);
                        MantenedorUsuarios nextPage = new MantenedorUsuarios();
                        navService.Navigate(nextPage);
                    }
                    else
                    {
                        MessageBox.Show("No se pudo desactivar el Usuario", "Aviso");
                    }
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar un Usuario antes", "Aviso");
            }
        }
        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            NavigationService navService = NavigationService.GetNavigationService(this);
            Page2 nextPage = new Page2();
            navService.Navigate(nextPage);
        }
    }
}
