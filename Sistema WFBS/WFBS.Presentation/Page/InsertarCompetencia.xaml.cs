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
    /// Lógica de interacción para InsertarCompetencia.xaml
    /// </summary>
    public partial class InsertarCompetencia : System.Windows.Controls.Page
    {
        //Collections col = new Collections();
        public InsertarCompetencia()
        {
            InitializeComponent();
            lblUserInfo.Content = Global.NombreUsuario;
            rbNo.IsChecked = true;
        }


        private void cmbNivel_Loaded(object sender, RoutedEventArgs e)
        {
            cmbNivel.Items.Add("0");
            cmbNivel.Items.Add("1");
            cmbNivel.Items.Add("2");
            cmbNivel.Items.Add("3");
            cmbNivel.Items.Add("4");
            cmbNivel.Items.Add("5");
            cmbNivel.SelectedIndex = 0;
        }
        private void RadioButtonChecked(object sender, RoutedEventArgs e)
        {
            if (rbNo.IsChecked == false)
            {
                lbNivel.Visibility = Visibility.Hidden;
                cmbNivel.IsEnabled = false;
                cmbNivel.Visibility = Visibility.Hidden;
            }
            else
            {
                lbNivel.Visibility = Visibility.Visible;
                cmbNivel.IsEnabled = true;
                cmbNivel.Visibility = Visibility.Visible;
            }
        }

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            this.Limpiar();
        }

        private void Limpiar()
        {
            txtNombre.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
            txtSigla.Text = string.Empty;
            cmbNivel.SelectedIndex = 0;
            txtPregunta.Text = string.Empty;
        }

        private void btnIngresar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Competencia com = new Competencia();
                if (txtNombre.Text.Length > 0 && txtNombre.Text.Trim() != "")
                {
                    if (txtDescripcion.Text.Length > 0 && txtDescripcion.Text.Trim() != "")
                    {
                        if ((txtSigla.Text.Length > 0 && txtSigla.Text.Length <= 10) && txtSigla.Text.Trim() != "")
                        {

                            com.Nombre = txtNombre.Text;
                            com.Descripcion = txtDescripcion.Text;
                            com.Sigla = txtSigla.Text;
                            if (rbNo.IsChecked == true)
                                com.Obsoleta = 0;
                            if (rbSi.IsChecked == true)
                                com.Obsoleta = 1;
                            com.Nivel_Optimo = int.Parse(cmbNivel.SelectedItem.ToString());
                            #region Nivel
                            switch (cmbNivel.SelectedIndex)
                            {
                                case 0:
                                    com.Nivel_Optimo = 0;
                                    break;
                                case 1:
                                    com.Nivel_Optimo = 1;
                                    break;
                                case 2:
                                    com.Nivel_Optimo = 2;
                                    break;
                                case 3:
                                    com.Nivel_Optimo = 3;
                                    break;
                                case 4:
                                    com.Nivel_Optimo = 4;
                                    break;
                                case 5:
                                    com.Nivel_Optimo = 5;
                                    break;

                                default:
                                    com.Nivel_Optimo = 0;
                                    break;
                            }
                            #endregion
                            com.Pregunta_Asociada = txtPregunta.Text;

                            string xml = com.Serializar();
                            WFBS.Presentation.ServiceWCF.ServiceWFBSClient servicio = new WFBS.Presentation.ServiceWCF.ServiceWFBSClient();

                            if (servicio.CrearCompetencia(xml))
                            {
                                MessageBox.Show("Agregado correctamente", " Éxito!");
                                this.Limpiar();
                                NavigationService navService = NavigationService.GetNavigationService(this);
                                MantenedorCompetencias nextPage = new MantenedorCompetencias();
                                navService.Navigate(nextPage);
                            }
                            else
                            {
                                MessageBox.Show("No se pudo agregar la Competencia, verifique que los datos sean correctos", "Aviso");
                            }
                        }
                        else
                        {
                            MessageBox.Show("El campo Sigla es obligatorio y admite como máximo 10 caracteres", "Aviso");
                        }

                    }
                    else
                    {
                        MessageBox.Show("El campo Descripción es obligatorio", "Aviso");
                    }

                }
                else
                {
                    MessageBox.Show("El campo Nombre es obligatorio", "Aviso");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("No se pudo agregar la Competencia!", "Alerta");
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
