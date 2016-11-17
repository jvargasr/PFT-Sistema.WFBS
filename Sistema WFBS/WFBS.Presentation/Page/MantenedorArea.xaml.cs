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
    /// Lógica de interacción para MantenedorArea.xaml
    /// </summary>
    public partial class MantenedorArea : System.Windows.Controls.Page
    {
        public MantenedorArea()
        {
            InitializeComponent();
            lblUserInfo.Content = Global.NombreUsuario;
        }

        private void btnModificarArea_Click(object sender, RoutedEventArgs e)
        {
            if (dgArea.SelectedItem != null)
            {
                Area ar = (Area)dgArea.SelectedItem;
                int id = Convert.ToInt32(ar.Id_area);
                NavigationService navService = NavigationService.GetNavigationService(this);
                ModificarArea nextPage = new ModificarArea(id);
                navService.Navigate(nextPage);
            }
            else
            {
                MessageBox.Show("Debe seleccionar un Área antes. Aviso:");
            }
        }

        private void btnEliminarArea_Click(object sender, RoutedEventArgs e)
        {
            if (dgArea.SelectedItem != null)
            {
                Area ar = (Area)dgArea.SelectedItem;
                if (ar.obs == "Si")
                {
                    MessageBox.Show("La Área seleccionada se encuentra desactivada", "Aviso");
                }
                else
                {
                    ar.id_area = Convert.ToInt32(ar.Id_area);
                    string xml = ar.Serializar();
                    WFBS.Presentation.ServiceWCF.ServiceWFBSClient servicio = new WFBS.Presentation.ServiceWCF.ServiceWFBSClient();

                    if (servicio.EliminarArea(xml))
                    {
                        MessageBox.Show("la Área seleccionada ha sido desactivado", "Éxito!");
                        NavigationService navService = NavigationService.GetNavigationService(this);
                        MantenedorArea nextPage = new MantenedorArea();
                        navService.Navigate(nextPage);
                    }
                    else
                    {
                        MessageBox.Show("No se pudo desactivar la Área", "Aviso");
                    }
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar un Área antes", "Aviso:");
            }
        }

        private void btnAgregarArea_Click(object sender, RoutedEventArgs e)
        {
            NavigationService navService = NavigationService.GetNavigationService(this);
            InsertarArea nextPage = new InsertarArea();
            navService.Navigate(nextPage);
        }

        private void dgArea_Loaded(object sender, RoutedEventArgs e)
        {
            //Collections col = new Collections();
            ColeccionArea colAreas = new ColeccionArea();
            dgArea.ItemsSource = colAreas.ReadAllAreas();
            dgArea.Columns[3].Visibility = Visibility.Hidden;
            dgArea.Columns[4].Visibility = Visibility.Hidden;
            dgArea.Columns[5].Header = "Obsoleta";
            //dgArea.Columns[6].Visibility = Visibility.Hidden;


            dgArea.Columns[0].Visibility = Visibility.Collapsed;
        }

        private void dgArea_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.Column.Header.ToString() == "area")
            {
                e.Column.Header = "Área";
            }
            if (e.Column.Header.ToString() == "abreviacion")
            {
                e.Column.Header = "Abreviacion";
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
