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
    /// Lógica de interacción para InsertarPeriodo.xaml
    /// </summary>
    public partial class InsertarPeriodo : System.Windows.Controls.Page
    {
        //Collections col = new Collections();
        ColeccionPeriodoEvaluacion colPe = new ColeccionPeriodoEvaluacion();
        public InsertarPeriodo()
        {
            InitializeComponent();
            lblUserInfo.Content = Global.NombreUsuario;
            DatePeriodo.SelectedDate = DateTime.Now;
        }

        private void cmbPorcentajeE_Loaded(object sender, RoutedEventArgs e)
        {
            cmbPorcentajeE.Items.Add("10%");
            cmbPorcentajeE.Items.Add("20%");
            cmbPorcentajeE.Items.Add("30%");
            cmbPorcentajeE.Items.Add("40%");
            cmbPorcentajeE.Items.Add("50%");
            cmbPorcentajeE.Items.Add("60%");
            cmbPorcentajeE.Items.Add("70%");
            cmbPorcentajeE.Items.Add("80%");
            cmbPorcentajeE.Items.Add("90%");
            cmbPorcentajeE.Items.Add("100%");
            cmbPorcentajeE.SelectedItem = "30%";
        }

        private void cmbPorcentajeAE_Loaded(object sender, RoutedEventArgs e)
        {
            cmbPorcentajeAE.Items.Add("10%");
            cmbPorcentajeAE.Items.Add("20%");
            cmbPorcentajeAE.Items.Add("30%");
            cmbPorcentajeAE.Items.Add("40%");
            cmbPorcentajeAE.Items.Add("50%");
            cmbPorcentajeAE.Items.Add("60%");
            cmbPorcentajeAE.Items.Add("70%");
            cmbPorcentajeAE.Items.Add("80%");
            cmbPorcentajeAE.Items.Add("90%");
            cmbPorcentajeAE.Items.Add("100%");
            cmbPorcentajeAE.SelectedItem = "70%";
        }

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            this.Limpiar();
        }
        private void Limpiar()
        {
            txtVigencia.Text = string.Empty;
            cmbPorcentajeAE.SelectedItem = "70%";
            cmbPorcentajeE.SelectedItem = "30%";
        }

        private void btnIngresar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                PeriodoEvaluacion pe = new PeriodoEvaluacion();
                if (DatePeriodo.SelectedDate.Value != null)
                {
                    if ((txtVigencia.Text.Length > 0 && txtVigencia.Text.Length <= 10) && txtVigencia.Text.Trim() != "")
                    {
                        pe.fechaInicio = DatePeriodo.DisplayDate;
                        pe.vigencia = int.Parse(txtVigencia.Text);

                        #region porcentaje E
                        switch (cmbPorcentajeE.SelectedIndex + 1)
                        {
                            case 1:
                                pe.porcentajeE = 10;
                                break;
                            case 2:
                                pe.porcentajeE = 20;
                                break;
                            case 3:
                                pe.porcentajeE = 30;
                                break;
                            case 4:
                                pe.porcentajeE = 40;
                                break;
                            case 5:
                                pe.porcentajeE = 50;
                                break;
                            case 6:
                                pe.porcentajeE = 60;
                                break;
                            case 7:
                                pe.porcentajeE = 70;
                                break;
                            case 8:
                                pe.porcentajeE = 80;
                                break;
                            case 9:
                                pe.porcentajeE = 90;
                                break;
                            case 10:
                                pe.porcentajeE = 100;
                                break;


                            default:
                                pe.porcentajeE = 10;
                                break;
                        }
                        #endregion
                        #region Porcentaje AE
                        switch (cmbPorcentajeAE.SelectedIndex + 1)
                        {
                            case 1:
                                pe.porcentajeAE = 10;
                                break;
                            case 2:
                                pe.porcentajeAE = 20;
                                break;
                            case 3:
                                pe.porcentajeAE = 30;
                                break;
                            case 4:
                                pe.porcentajeAE = 40;
                                break;
                            case 5:
                                pe.porcentajeAE = 50;
                                break;
                            case 6:
                                pe.porcentajeAE = 60;
                                break;
                            case 7:
                                pe.porcentajeAE = 70;
                                break;
                            case 8:
                                pe.porcentajeAE = 80;
                                break;
                            case 9:
                                pe.porcentajeAE = 90;
                                break;
                            case 10:
                                pe.porcentajeAE = 100;
                                break;


                            default:
                                pe.porcentajeAE = 10;
                                break;
                        }
                        #endregion

                        if (pe.porcentajeAE + pe.porcentajeE != 100)
                        {
                            MessageBox.Show("La suma de los porcentajes debe ser igual a 100", "Aviso");
                        }
                        else
                        {
                            string xml = pe.Serializar();
                            WFBS.Presentation.ServiceWCF.ServiceWFBSClient servicio = new WFBS.Presentation.ServiceWCF.ServiceWFBSClient();

                            if (servicio.CrearPeriodoEvaluacion(xml))
                            {

                                MessageBox.Show("Agregado correctamente", "Éxito!");
                                NavigationService navService = NavigationService.GetNavigationService(this);
                                MantenedorPeriodoEvaluacion nextPage = new MantenedorPeriodoEvaluacion();
                                navService.Navigate(nextPage);
                            }
                            else
                            {
                                MessageBox.Show("No se pudo agregar el Periodo de Evaluación, verifique que los datos sean correctos", "Aviso");

                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("El campo Vigencia es obligatorio y admite solo valores numericos", "Aviso");
                    }
                }
                else
                {
                    MessageBox.Show("El campo Fecha de Inicio es obligatorio. Aviso");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("No se pudo agregar el Periodo de Evaluación!", "Alerta");
            }
        }
        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            NavigationService navService = NavigationService.GetNavigationService(this);
            MantenedorPeriodoEvaluacion nextPage = new MantenedorPeriodoEvaluacion();
            navService.Navigate(nextPage);
        }
    }
}
