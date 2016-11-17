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
    /// Lógica de interacción para MantenedorCompetencias.xaml
    /// </summary>
    public partial class MantenedorHabilidades : System.Windows.Controls.Page
    {
        ColeccionHabilidad colHab = new ColeccionHabilidad();
        //Collections col = new Collections();
        Habilidad hab = new Habilidad();
        Competencia com = new Competencia();
        int id_com;
        public MantenedorHabilidades()
        {
            InitializeComponent();
            lblUserInfo.Content = Global.NombreUsuario;
        }

        public MantenedorHabilidades(int id)
        {
            InitializeComponent();
            com.Id_competencia = id;
            id_com = id;
            dgHabilidades.ItemsSource = colHab.ObtenerHabPorCom(id);
            //dgHabilidades.ItemsSource = col.ObtenerHabPorCom(id);
        }

        private void dgHabilidades_Loaded(object sender, RoutedEventArgs e)
        {

            dgHabilidades.Columns[0].Visibility = Visibility.Collapsed;
            dgHabilidades.Columns[1].Visibility = Visibility.Collapsed;
            dgHabilidades.Columns[3].Visibility = Visibility.Collapsed;
            dgHabilidades.Columns[5].Visibility = Visibility.Hidden;
            dgHabilidades.Columns[2].Header = "Nombre";
            dgHabilidades.Columns[4].Header = "Alternativa de Pregunta";
            dgHabilidades.Columns[5].Header = "ID Habilidad";
            dgHabilidades.Columns[6].Header = "Competencia";
            dgHabilidades.Columns[7].Header = "Orden Asignado";

        }
        private void dgHabilidades_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            /*Para cambiar nombre de cabecera*/

        }

        private void btnAgregarHabilidad_Click(object sender, RoutedEventArgs e)
        {
            NavigationService navService = NavigationService.GetNavigationService(this);
            InsertarHabilidad nextPage = new InsertarHabilidad(id_com);
            navService.Navigate(nextPage);
        }

        private void btnModificarHabilidad_Click(object sender, RoutedEventArgs e)
        {
            if (dgHabilidades.SelectedItem != null)
            {
                Habilidad hab = (Habilidad)dgHabilidades.SelectedItem;
                int id_habi = Convert.ToInt32(hab.Id_Hab);
                NavigationService navService = NavigationService.GetNavigationService(this);
                ModificarHabilidad nextPage = new ModificarHabilidad(id_habi, id_com);
                navService.Navigate(nextPage);
            }
            else
            {
                MessageBox.Show("Debe seleccionar una Habilidad antes", "Aviso");
            }
        }
        private void btnEliminarHabilidad_Click(object sender, RoutedEventArgs e)
        {
            if (dgHabilidades.SelectedItem != null)
            {
                Habilidad hab = (Habilidad)dgHabilidades.SelectedItem;
                int id_habi = Convert.ToInt32(hab.Id_Hab);
                hab.Id_Habilidad = id_habi;

                string xml = hab.Serializar();
                WFBS.Presentation.ServiceWCF.ServiceWFBSClient servicio = new WFBS.Presentation.ServiceWCF.ServiceWFBSClient();

                if (servicio.EliminarHabilidad(xml))
                {
                    MessageBox.Show("La Habilidad seleccionada ha sido eliminada", "Éxito!");
                    NavigationService navService = NavigationService.GetNavigationService(this);
                    MantenedorHabilidades nextPage = new MantenedorHabilidades(id_com);
                    navService.Navigate(nextPage);
                }
                else
                {
                    MessageBox.Show("No se pudo eliminar la Habilidad", "Aviso");
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar una Habilidad antes", "Aviso");
            }
        }
        
        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            NavigationService navService = NavigationService.GetNavigationService(this);
            MantenedorCompetencias nextPage = new MantenedorCompetencias();
            navService.Navigate(nextPage);
        }
    }
}
