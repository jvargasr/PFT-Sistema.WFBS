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
    /// Lógica de interacción para InsertarArea.xaml
    /// </summary>
    public partial class InsertarArea : System.Windows.Controls.Page
    {
        public InsertarArea()
        {
            InitializeComponent();
            lblUserInfo.Content = Global.NombreUsuario;
            rbNo.IsChecked = true;
        }

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            this.Limpiar();
        }
        private void Limpiar()
        {
            txtNombre.Text = string.Empty;
            txtAbreviacion.Text = string.Empty;

        }

        private void btnIngresar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Area ar = new Area();
                if (txtNombre.Text.Length > 0 && txtNombre.Text.Trim() != "")
                {
                    if (txtAbreviacion.Text.Length > 0 && txtAbreviacion.Text.Trim() != "")
                    {
                        ar.area = txtNombre.Text;
                        ar.abreviacion = txtAbreviacion.Text;
                        if (rbNo.IsChecked == true)
                            ar.obsoleta = 0;
                        if (rbSi.IsChecked == true)
                            ar.obsoleta = 1;

                        string xml = ar.Serializar();
                        WFBS.Presentation.ServiceWCF.ServiceWFBSClient servicio = new WFBS.Presentation.ServiceWCF.ServiceWFBSClient();

                        if (servicio.CrearArea(xml))
                        {
                            MessageBox.Show("Agregado correctamente", "Éxito!");
                            this.Limpiar();
                            NavigationService navService = NavigationService.GetNavigationService(this);
                            MantenedorArea nextPage = new MantenedorArea();
                            navService.Navigate(nextPage);
                        }
                        else
                        {
                            MessageBox.Show("No se pudo agregar la Área, verifique que los datos sean correctos", "Aviso");

                        }
                    }
                    else
                    {
                        MessageBox.Show("El campo Abreviación es obligatorio", "Aviso");
                    }
                }
                else
                {
                    MessageBox.Show("El campo Nombre es obligatorio", "Aviso");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("No se pudo agregar la Área!", "Alerta");
            }
        }

        private void rbNo_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            NavigationService navService = NavigationService.GetNavigationService(this);
            MantenedorArea nextPage = new MantenedorArea();
            navService.Navigate(nextPage);
        }
    }
}
